using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.MX.Model
{
  public enum AppType
  {
    Default,
    Type1
  }

  public enum ServerType
  {
    Default,
    Type1
  }

  public enum MeasuredValueTypeEnum
  {
    Discrete,
    Average,
    Count
  }

  public enum MetricStateEnum
  {
    Actual,
    Forecast
  }

  public enum MetricTypeEnum
  {
    System,
    Business
  }

  public enum MetricValueTypeEnum
  {
    Default
  }

}
