using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK
{
  public class TaskExecution
  {
    public DateTime TimePlacedInQueue {
      get;
      set;
    }
    public DateTime TimeDispatched {
      get;
      set;
    }
    public DateTime TimeCompleted {
      get;
      set;
    }
    public DateTime PreviousTimePlacedInQueue {
      get;
      set;
    }
    public DateTime NextTimerTarget {
      get;
      set;
    }
    public int AlignmentAdjustment {
      get;
      set;
    }
    public string Key {
      get {
        return Get_Key();
      }
    }

    public TaskExecution(DateTime timePlacedInQueue)
    {
      this.TimePlacedInQueue = timePlacedInQueue;
      this.TimeDispatched = DateTime.MinValue;
      this.TimeCompleted = DateTime.MinValue;
      this.PreviousTimePlacedInQueue = DateTime.MinValue;
      this.NextTimerTarget = DateTime.MinValue;
      this.AlignmentAdjustment = 0;
    }

    public string Get_Key()
    {
      Int64 timeFired = Int64.Parse(this.TimePlacedInQueue.ToString("yyyyMMddHHmmssfff"));
      Int64 intKey = 99999999999999999 - timeFired;
      string key = intKey.ToString();
      return key;
    }
  }
}
