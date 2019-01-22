using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.WebServices.Data;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Ops.Business
{
  public class ExchangeServiceManager
  {
    private ExchangeService _exchangeService {
      get;
      set;
    }

    public ExchangeServiceManager(ExchangeService exchangeService)
    {
      _exchangeService = exchangeService;
    }

    public ExchangeServiceManager(ConfigSmtpSpec exchangeSmtpSpec)
    {
      _exchangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
      if (exchangeSmtpSpec.EmailFromAddress.IsBlank())
        throw new Exception("The EmailFromAddress in the Exchange SMTP Spec was blank.");
      if (exchangeSmtpSpec.SmtpPassword.IsBlank())
        throw new Exception("The SmtpPassword in the Exchange SMTP Spec was blank.");
      _exchangeService.Credentials = new WebCredentials(exchangeSmtpSpec.EmailFromAddress, exchangeSmtpSpec.SmtpPassword);
      _exchangeService.AutodiscoverUrl(exchangeSmtpSpec.EmailFromAddress);
    }

    public List<Item> GetItemsByDate(string folderName, DateTime beginDateTime, DateTime endDateTime)
    {
      Folder folder = GetFolder(folderName);

      SearchFilter greaterThanFilter = new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.DateTimeReceived, beginDateTime);
      SearchFilter lessThanFilter = new SearchFilter.IsLessThan(ItemSchema.DateTimeReceived, endDateTime);
      SearchFilter filter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, greaterThanFilter, lessThanFilter);
      FindItemsResults<Item> itemsInRange = folder.FindItems(filter, new ItemView(100));

      return BindItems(itemsInRange);
    }

    public List<Item> GetItemsByDate(WellKnownFolderName wellKnownFolderName, DateTime beginDateTime, DateTime endDateTime)
    {
      SearchFilter greaterThanFilter = new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.DateTimeReceived, beginDateTime);
      SearchFilter lessThanFilter = new SearchFilter.IsLessThan(ItemSchema.DateTimeReceived, endDateTime);
      SearchFilter filter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, greaterThanFilter, lessThanFilter);
      FindItemsResults<Item> itemsInRange = _exchangeService.FindItems(wellKnownFolderName, filter, new ItemView(100));

      return BindItems(itemsInRange);
    }

    public List<Item> GetItems(string folderName)
    {
      Folder folder = GetFolder(folderName);
      FindItemsResults<Item> items = folder.FindItems(new ItemView(100));

      return BindItems(items);
    }

    public List<Item> GetItems(WellKnownFolderName wellKnownFolderName)
    {
      FindItemsResults<Item> items = _exchangeService.FindItems(wellKnownFolderName, new ItemView(100));

      return BindItems(items);
    }

    public List<Item> GetItems(string folderName, SearchFilter searchFilter)
    {
      Folder folder = GetFolder(folderName);
      FindItemsResults<Item> items = folder.FindItems(searchFilter, new ItemView(100));

      return BindItems(items);
    }

    public List<Item> GetItems(WellKnownFolderName wellKnownFolderName, SearchFilter searchFilter)
    {
      FindItemsResults<Item> items = _exchangeService.FindItems(wellKnownFolderName, searchFilter, new ItemView(100));

      return BindItems(items);
    }

    private Folder GetFolder(string folderName)
    {
      var folderView = new FolderView(2);
      folderView.Traversal = FolderTraversal.Deep;
      SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName);
      var folderResults = _exchangeService.FindFolders(WellKnownFolderName.Root, folderFilter, folderView);

      if (folderResults.TotalCount < 1)
        throw new Exception("Cannot find folder '" + folderName + "'.");
      if (folderResults.TotalCount > 1)
        throw new Exception("Multiple folders named '" + folderName + "'.");

      Folder folder = folderResults.Folders.First();

      return folder;
    }

    private List<Item> BindItems(FindItemsResults<Item> items)
    {
      List<Item> boundItems = new List<Item>();

      PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties);
      propertySet.RequestedBodyType = BodyType.Text;

      List<ItemId> itemIds = new List<ItemId>();
      foreach (var item in items)
        itemIds.Add(item.Id);

      if (itemIds.Count == 0)
        return boundItems;

      var results = _exchangeService.BindToItems(itemIds, propertySet).ToList();

      foreach (var result in results)
        boundItems.Add(result.Item);

      return boundItems;
    }

    public void MoveItem(Item item, string folderName)
    {
      var folder = GetFolder(folderName);
      item.Move(folder.Id);
    }

    public void MoveItems(List<Item> items, string folderName)
    {
      if (items.Count == 0)
        return;

      var folder = GetFolder(folderName);

      List<ItemId> itemIds = new List<ItemId>();
      foreach (var item in items)
        itemIds.Add(item.Id);

      _exchangeService.MoveItems(itemIds, folder.Id);
    }

    public void SendEmail(ExchangeEmail exchangeEmail)
    {

    }

    public void SendEmails(List<ExchangeEmail> exchangeEmails)
    {

    }

    //private static bool RedirectionCallback(string url)
    //{
    //  // Return true if the URL is an HTTPS URL.
    //  return !url.ToLower().StartsWith("https://");
    //}
  }
}