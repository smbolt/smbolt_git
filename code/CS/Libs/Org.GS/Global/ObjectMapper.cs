using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS
{
  public class ObjectMapper
  {
    public void MapObject(object source, object destination)
    {
      string sourceType = source.GetType().Name;
      string destType = destination.GetType().Name;

      PropertyInfo[] sourceProps = source.GetType().GetProperties();
      PropertyInfo[] destProps = destination.GetType().GetProperties();

      foreach (PropertyInfo dpi in destProps)
      {
        foreach (PropertyInfo spi in sourceProps)
        {
          string sourcePropType = spi.PropertyType.Name;
          if (sourcePropType.Contains("Nullable"))
            sourcePropType = Nullable.GetUnderlyingType(spi.PropertyType).Name;

          string destPropType = dpi.PropertyType.Name;
          if (destPropType.Contains("Nullable"))
            destPropType = Nullable.GetUnderlyingType(dpi.PropertyType).Name;

          if (dpi.Name == spi.Name && sourcePropType == destPropType)
          {
            dpi.SetValue(destination, spi.GetValue(source, null), null);
            break;
          }
        }
      }
    }
  }
}
