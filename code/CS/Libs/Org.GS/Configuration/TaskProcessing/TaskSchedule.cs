using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [XMap(XType = XType.Element, CollectionElements = "TaskScheduleElement")]
  public class TaskSchedule : List<TaskScheduleElement>
  {
  }
}
