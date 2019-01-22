using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DB
{
  public class ModelFactory
  {
    public ModelBase CreateModel(object entity, EntityModelMap entityModelMap)
    {
      object model = Activator.CreateInstance(entityModelMap.ModelType);

      foreach (var piPair in entityModelMap.PropertyInfoPairSet.Values)
      {
        object value = piPair.EntityPropertyInfo.GetValue(entity);

        switch (piPair.MappingRule)
        {
          case MappingRule.None:
            piPair.ModelPropertyInfo.SetValue(model, value);
            break;

          case MappingRule.DoubleToDecimal:
            piPair.ModelPropertyInfo.SetValue(model, value.ToDecimal());
            break;

          case MappingRule.Boolean1:
            value = piPair.EntityPropertyInfo.GetValue(entity);
            if (value != null && value.ToString().ToUpper().In("Y,1"))
              piPair.ModelPropertyInfo.SetValue(model, true);
            else
              piPair.ModelPropertyInfo.SetValue(model, false);
            break;
        }

      }

      return (ModelBase) model;
    }
  }
}
