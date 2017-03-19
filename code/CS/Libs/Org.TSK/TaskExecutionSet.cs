using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK
{
  public class TaskExecutionSet : SortedList<string, TaskExecution>
  {
    public object AddItem_LockObject = new object();
    public void AddItem(string key, TaskExecution te)
    {
      lock (AddItem_LockObject)
      {
        while (this.Count > 9)
        {
          string lastKey = this.Last().Key;
          this.Remove(lastKey);
        }

        if (this.Count > 0)
          te.PreviousTimePlacedInQueue = this.First().Value.TimePlacedInQueue;

        while (this.ContainsKey(key))
        {
          long tempKey = long.Parse(key);
          tempKey--;
          key = tempKey.ToString();
        }

        this.Add(key, te);
      }
    }
  }
}
