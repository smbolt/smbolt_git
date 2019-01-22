using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [XMap(XType = XType.Element)]
  public class Task
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "Task", WrapperElement = "TaskSet")]
    public TaskSet TaskSet { get; set; }

    public Task()
    {
      this.Name = String.Empty;
      this.TaskSet = new TaskSet();
    }
  }
}

