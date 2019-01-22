using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class TransactionBase
  {
    [XMap]
    public string Name { get; set; }

    [XMap]
    public string Version { get; set; }

    [XMap(DefaultValue = "NotSet", Name = "TransactionStatus")]
    public TransactionStatus TransactionStatus { get; set; }

    [XMap(DefaultValue = "")]
    public string Message { get; set; }

    [XMap(DefaultValue = "")]
    public string Code { get; set; }

    public TransactionBase()
    {
      this.Name = String.Empty;
      this.Version = String.Empty;
      this.TransactionStatus = TransactionStatus.NotSet;
      this.Message = String.Empty;
      this.Code = String.Empty;
    }

    public void AutoInit()
    {
      this.Name = this.GetType().Name; 
    }
  }

  public static class TransactionBase_ExtensionMethods
  {
    public static SpParmSet GetSpParms(this TransactionBase trans, List<string> parms)
    {
      if (parms == null)
        throw new Exception("No list of stored procedure parameters was provided to the GetSpParms extension method."); 

      var spParms = new SpParmSet();

      Type transType = trans.GetType();           

      for (int i = 0; i < parms.Count; i++)
      {
        string propertyName = parms.ElementAt(i); 
        string parmPropertyName = propertyName;
        string objectPropertyName = propertyName;

        if (propertyName.Contains(":"))
        {
          string[] tokens = propertyName.ToTokenArray(Constants.ColonDelimiter); 
          if (tokens.Length != 2)
            throw new Exception("Invalid property name format set to GetSpParms extension method '" + propertyName + "'."); 
          parmPropertyName = tokens[0];
          objectPropertyName = tokens[1];
        }

        var pi = transType.GetProperty(objectPropertyName); 
        if (pi == null)
          throw new Exception("No property named '" + objectPropertyName + "' exists in type '" + transType.FullName + "', cannot create " + 
                              "stored procedure parameters from transaction object.");

        var propertyValue = pi.GetValue(trans); 
        var spParm = new SpParm(parmPropertyName, propertyValue); 
        spParms.Add(spParm); 
      }

      return spParms;
    }

  }
}
