using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.WebServices.Data;
using Org.GS.Configuration;

namespace Org.Ops.Business
{
  public class ExchangeServiceManager
  {
    private ExchangeService _exchangeService { get; set; }

    public ExchangeServiceManager(ExchangeService exchangeService)
    {
      _exchangeService = exchangeService;
    }

    public ExchangeServiceManager(string emailAddress, string emailPassword)
    {
      _exchangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
      _exchangeService.Credentials = new WebCredentials(emailAddress, emailPassword);
      _exchangeService.UseDefaultCredentials = true;
      _exchangeService.AutodiscoverUrl(emailAddress, RedirectionCallback);
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

    public void SendEmail(string subject, string body, string recipientEmail)
    {
      EmailMessage emailMessage = new EmailMessage(_exchangeService);
      emailMessage.ToRecipients.Add(recipientEmail);
      emailMessage.Subject = subject;
      emailMessage.Body = body;
      emailMessage.Send();
    }

    public void SendEmail(string subject, string body, List<string> toRecipients)
    {
      EmailMessage emailMessage = new EmailMessage(_exchangeService);
      emailMessage.ToRecipients.AddRange(toRecipients);
      emailMessage.Subject = subject;
      emailMessage.Body = body;
      emailMessage.Send();
    }

    public void SendEmail(string subject, string body, List<EmailAddress> toRecipients)
    {
      EmailMessage emailMessage = new EmailMessage(_exchangeService);
      emailMessage.ToRecipients.AddRange(toRecipients);
      emailMessage.Subject = subject;
      emailMessage.Body = body;
      emailMessage.Send();
    }

    public void SendVotingEmail(string subject, string body, string recipientEmail)
    {
      List<EmailAddress> emailAddressSet = new List<EmailAddress>();
      emailAddressSet.Add(recipientEmail);
      SendVotingEmail(subject, body, emailAddressSet);
    }

    public void SendVotingEmail(string subject, string body, List<string> toRecipients)
    {
      List<EmailAddress> emailAddressSet = new List<EmailAddress>();
      foreach (var recipient in toRecipients)
        emailAddressSet.Add(recipient);
      SendVotingEmail(subject, body, emailAddressSet);
    }

    public void SendVotingEmail(string subject, string body, List<EmailAddress> toRecipients)
    {
      //hex string used to send with email with approve/reject voting
      string approvalEmailHexString = "02010600000000000000055265706C790849504D2E4E6F7465074D6573736167650252450500000000000000000100000000000000020000006600000002000000010000000C5265706C7920746F20416C6C0849504D2E4E6F7465074D65737361676502524505000000000000000001000000000000000200000067000000030000000200000007466F72776172640849504D2E4E6F7465074D6573736167650246570500000000000000000100000000000000020000006800000004000000030000000F5265706C7920746F20466F6C6465720849504D2E506F737404506F7374000500000000000000000100000000000000020000006C000000080000000400000007417070726F76650849504D2E4E6F74650007417070726F766500000000000000000001000000020000000200000001000000FFFFFFFF040000000652656A6563740849504D2E4E6F7465000652656A65637400000000000000000001000000020000000200000002000000FFFFFFFF0401055200650070006C00790002520045000C5200650070006C007900200074006F00200041006C006C0002520045000746006F007200770061007200640002460057000F5200650070006C007900200074006F00200046006F006C00640065007200000741007000700072006F00760065000741007000700072006F007600650006520065006A0065006300740006520065006A00650063007400";

      //extended property definition for approve/reject voting
      var epd = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 0x00008520, MapiPropertyType.Binary);

      //convert hex string to array of bytes
      int NumberChars = approvalEmailHexString.Length;
      byte[] bytes = new byte[NumberChars / 2];
      for (int i = 0; i < NumberChars; i += 2)
        bytes[i / 2] = Convert.ToByte(approvalEmailHexString.Substring(i, 2), 16);

      EmailMessage emailMessage = new EmailMessage(_exchangeService);
      emailMessage.ToRecipients.AddRange(toRecipients);
      emailMessage.Subject = subject;
      emailMessage.Body = body;
      emailMessage.SetExtendedProperty(epd, bytes);
      emailMessage.Send();
    }

    public void MoveItem(Item item, string folderName)
    {
      var folder = GetFolder(folderName);
      item.Move(folder.Id);
    }

    public void MoveItem(Item item, WellKnownFolderName wellKnownFolderName)
    {
      item.Move(wellKnownFolderName);
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

    public void MoveItems(List<Item> items, WellKnownFolderName wellKnownFolderName)
    {
      if (items.Count == 0)
        return;

      List<ItemId> itemIds = new List<ItemId>();
      foreach (var item in items)
        itemIds.Add(item.Id);

      _exchangeService.MoveItems(itemIds, wellKnownFolderName);
    }

    private static bool RedirectionCallback(string url)
    {
      // Return true if the URL is an HTTPS URL.
      return url.ToLower().StartsWith("https://");
    }
  }

  public static class StringMethodExtensions
  {
    private static string _paraBreak = "\r\n\r\n";
    private static string _link = "<a href=\"{0}\">{1}</a>";
    private static string _linkNoFollow = "<a href=\"{0}\" rel=\"nofollow\">{1}</a>";

    /// <summary>
    /// Returns a copy of this string converted to HTML markup.
    /// </summary>
    public static string ToHtml(this string s)
    {
      return ToHtml(s, false);
    }

    /// <summary>
    /// Returns a copy of this string converted to HTML markup.
    /// </summary>
    /// <param name="nofollow">If true, links are given "nofollow"
    /// attribute</param>
    public static string ToHtml(this string s, bool nofollow)
    {
      StringBuilder sb = new StringBuilder();

      int pos = 0;
      while (pos < s.Length)
      {
        // Extract next paragraph
        int start = pos;
        pos = s.IndexOf(_paraBreak, start);
        if (pos < 0)
          pos = s.Length;
        string para = s.Substring(start, pos - start).Trim();

        // Encode non-empty paragraph
        if (para.Length > 0)
          EncodeParagraph(para, sb, nofollow);

        // Skip over paragraph break
        pos += _paraBreak.Length;
      }
      // Return result
      return sb.ToString();
    }

    /// <summary>
    /// Encodes a single paragraph to HTML.
    /// </summary>
    /// <param name="s">Text to encode</param>
    /// <param name="sb">StringBuilder to write results</param>
    /// <param name="nofollow">If true, links are given "nofollow"
    /// attribute</param>
    private static void EncodeParagraph(string s, StringBuilder sb, bool nofollow)
    {
      // Start new paragraph
      sb.AppendLine("<p>");

      // HTML encode text
      s = HttpUtility.HtmlEncode(s);

      // Convert single newlines to <br>
      s = s.Replace(Environment.NewLine, "<br />\r\n");

      // Encode any hyperlinks
      EncodeLinks(s, sb, nofollow);

      // Close paragraph
      sb.AppendLine("\r\n</p>");
    }

    /// <summary>
    /// Encodes [[URL]] and [[Text][URL]] links to HTML.
    /// </summary>
    /// <param name="text">Text to encode</param>
    /// <param name="sb">StringBuilder to write results</param>
    /// <param name="nofollow">If true, links are given "nofollow"
    /// attribute</param>
    private static void EncodeLinks(string s, StringBuilder sb, bool nofollow)
    {
      // Parse and encode any hyperlinks
      int pos = 0;
      while (pos < s.Length)
      {
        // Look for next link
        int start = pos;
        pos = s.IndexOf("[[", pos);
        if (pos < 0)
          pos = s.Length;
        // Copy text before link
        sb.Append(s.Substring(start, pos - start));
        if (pos < s.Length)
        {
          string label, link;

          start = pos + 2;
          pos = s.IndexOf("]]", start);
          if (pos < 0)
            pos = s.Length;
          label = s.Substring(start, pos - start);
          int i = label.IndexOf("][");
          if (i >= 0)
          {
            link = label.Substring(i + 2);
            label = label.Substring(0, i);
          }
          else
          {
            link = label;
          }
          // Append link
          sb.Append(String.Format(nofollow ? _linkNoFollow : _link, link, label));

          // Skip over closing "]]"
          pos += 2;
        }
      }
    }
  }
}