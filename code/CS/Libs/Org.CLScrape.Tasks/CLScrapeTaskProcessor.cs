using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using HtmlAgilityPack;
using Org.TP.Concrete;
using Org.GS;

namespace Org.CLScrape.Tasks
{
  public class CLScrapeTaskProcessor : TaskProcessorBase
  {
    public override TaskResult ProcessTask()
    {
      TaskResult taskResult = base.InitializeTaskResult();

      var sb = new StringBuilder();
      
      try
      {
        base.Notify(base.ProcessorName + " starting on thread " + Thread.CurrentThread.ManagedThreadId.ToString() + ".");
        //base.ProgressNotify(base.ProcessorName, base.ProcessorName + " is progressing.", 0, 3);

        string stateListUrl = @"https://www.craigslist.org/about/sites#US";

        //var htmlWeb = new HtmlWeb();
        //var stateListDoc = htmlWeb.Load(stateListUrl);

        //var articleNode = stateListDoc.DocumentNode.SelectSingleNode("//body/article");
        //if (articleNode == null)
        //  throw new Exception("Main article node not found in state list page."); 

        //var bodyNode = articleNode.SelectSingleNode("./section[@class='body']");
        //if (bodyNode == null)
        //  throw new Exception("Body node not found in state list page."); 

        //HtmlNode usCountryNode = null;
        //var countryNodes = bodyNode.SelectNodes("./h1");
        //foreach (var countryNode in countryNodes)
        //{
        //  var countryAnchor = countryNode.SelectSingleNode("./a");
        //  if (countryAnchor != null)
        //  {
        //    var countryNameAttr = countryAnchor.Attributes["name"];
        //    if (countryNameAttr != null)
        //    {
        //      if (countryNameAttr.Value.Trim().ToUpper() == "US")
        //      {
        //        usCountryNode = countryNode;
        //        break;
        //      }
        //    }
        //  }
        //}

        //if (usCountryNode == null)
        //  throw new Exception("US country node not found in state list page.");

        //HtmlNode countryColMaskDiv = null;
        //HtmlNode currentNode = usCountryNode;
        //while (currentNode.NextSibling != null)
        //{
        //  currentNode = currentNode.NextSibling;
        //  if (currentNode.NodeType == HtmlNodeType.Element && currentNode.Name == "div")
        //  {
        //    countryColMaskDiv = currentNode;
        //    break;
        //  }
        //}

        //if (countryColMaskDiv == null)
        //  throw new Exception("US country 'colmask' div not found on state list page.");

        //var usElements = countryColMaskDiv.SelectNodes(".//*");

        //var cityUrls = new Dictionary<string, string>();

        //StringBuilder sb = new StringBuilder();
        //bool stateStarted = false;
        //string stateName = String.Empty;

        //foreach (var usElement in usElements)
        //{
        //  if (!stateStarted)
        //  {
        //    if (usElement.Name == "h4")
        //    {
        //      stateStarted = true;
        //      stateName = usElement.InnerText.Trim();
        //      sb.Append(stateName + g.crlf); 
        //    }
        //    else
        //    {
        //      continue;
        //    }
        //  }

        //  if (usElement.Name == "h4")
        //  {
        //    sb.Append(g.crlf); 
        //    stateName = usElement.InnerText.Trim();
        //    if (stateName.ToLower() != "territories")
        //      sb.Append(stateName + g.crlf); 
        //  }

        //  // list of cities for the state
        //  if (usElement.Name == "ul" && stateName.ToLower() != "territories")
        //  {
        //    var liList = usElement.SelectNodes("./li");
        //    foreach (var liItem in liList)
        //    {
        //      var cityAnchor = liItem.SelectSingleNode("./a");
        //      if (cityAnchor != null)
        //      {
        //        var hrefAttr = cityAnchor.Attributes["href"];
        //        string cityUrl = hrefAttr.Value;
        //        string cityName = cityAnchor.InnerText.Trim();
        //        sb.Append("    " + cityName + " [" + cityUrl + "]" + g.crlf);

        //        string cityNameUpper = cityName.ToUpper();
        //        if (!cityUrls.ContainsKey(cityNameUpper))
        //          cityUrls.Add(cityNameUpper, cityUrl); 
        //      }
        //    }
        //  }
        //}

        //foreach (var cityUrl in cityUrls)
        //{
        //  string url = cityUrl.Value + @"d/computer-gigs/search/cpg";
        //  var cityGigs = new HtmlWeb();
        //  var cityGigsDoc2 = cityGigs.Load(url);

        //  var listings = cityGigsDoc2.DocumentNode.SelectNodes("//li[@class='result-row']");
        //  foreach (var listing in listings)
        //  {
        //    DateTime postTime = new DateTime(2000,1, 1); 
        //    string postUrl = String.Empty;
        //    long postId = 0;

        //    var timeElement = listing.SelectSingleNode(".//time");
        //    var dtAttr = timeElement.Attributes["datetime"];
        //    if (dtAttr != null)
        //    {
        //      string dateTime = dtAttr.Value;
        //      DateTime.TryParse(dateTime, out postTime); 
        //    }

        //    var pElement = listing.SelectSingleNode("./p");
        //    if (pElement != null)
        //    {
        //      var aElement = pElement.SelectSingleNode("./a");
        //      if (aElement != null)
        //      {
        //        var hRefAttr = aElement.Attributes["href"];
        //        if (hRefAttr != null)
        //          postUrl = hRefAttr.Value;

        //        var pidAttr = aElement.Attributes["data-id"];
        //        if (pidAttr != null)
        //          postId = Int64.Parse(pidAttr.Value);

        //        string title = HtmlEntity.DeEntitize(aElement.InnerText.Trim());
        //      }
        //    }
        //  }
        //}


        base.Notify(base.ProcessorName + " processing is finished.");
        return taskResult.Success(base.ProcessorName + " completed successfully.");
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }
  }
}
