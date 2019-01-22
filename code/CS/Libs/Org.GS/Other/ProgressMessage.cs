using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [Serializable]
  public class ProgressMessage
  {
    public string ActivityName { get; set; }
    public int CompletedItems { get; set; }
    public int TotalItems { get; set; }
    public string MessageText { get; set; }
    public string ActvityProgress { get { return Get_ActivityProgress(); } }

    public ProgressMessage()
    {
      this.ActivityName = String.Empty;
      this.CompletedItems = 0;
      this.TotalItems = 0;
      this.MessageText = String.Empty;
    }

    public ProgressMessage(string activityName, int completedItems, int totalItems, string messageText)
    {
      this.ActivityName = activityName;
      this.CompletedItems = completedItems;
      this.TotalItems = totalItems;
      this.MessageText = messageText;
    }

    public ProgressMessage(int completedItems, int totalItems)
    {
      this.ActivityName = String.Empty;
      this.CompletedItems = completedItems;
      this.TotalItems = totalItems;
      this.MessageText = String.Empty;
    }

    private string Get_ActivityProgress()
    {
      if (this.ActivityName.IsBlank())
      {
        return this.CompletedItems.ToString("###,###,##0") + " of " +
               this.TotalItems.ToString("###,###,##0") + " items" +
               GetPercentageComplete() + ".";
      }
      else
      {
        return this.ActivityName + " - " + this.CompletedItems.ToString("###,###,##0") + " of " +
               this.TotalItems.ToString("###,###,##0") + " items" +
               GetPercentageComplete() + ".";
      }
    }

    private string GetPercentageComplete()
    {
      if (this.TotalItems == 0)
        return String.Empty;

      float pct = this.CompletedItems / this.TotalItems * 100;
      return " (" + pct.ToString("##0.00") + "%)"; 
    }
  }
}