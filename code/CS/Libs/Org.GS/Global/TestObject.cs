using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TestObject
  {
    public int ObjectId { get; set; }
    public int RunDuration { get; set; }
    public int Decrement { get; set; }
    public int SecondsRemaining { get; set; }
    public int ThreadId { get; set; }

    public TestObject(int objectId, int runDuration, int decrement)
    {
      this.ObjectId = objectId;
      this.RunDuration = runDuration;
      this.Decrement = decrement;
      this.SecondsRemaining = runDuration;
      this.ThreadId = 0; 
    }
  }
}
