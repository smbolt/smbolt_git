using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ObjectMap : Dictionary<string, ObjectMapContext>
  {
    public AliasSet AliasSet { get; set; }

    public ObjectMap()
    {
      this.AliasSet = new AliasSet();
    }

    public ObjectMapContext GetContext(string contextName)
    {
      if (this.ContainsKey(contextName))
        return this[contextName];

      return null;
    }

    public string GetAliasedTypeName(object o)
    {
      string fullTypeName = o.GetType().FullName;
      ObjectMapPrefix sourcePrefix = (ObjectMapPrefix) o.GetType().GetCustomAttributes(typeof(ObjectMapPrefix), false).FirstOrDefault();
      if (sourcePrefix != null)
      {
        string prefix = "@" + sourcePrefix.MapPrefix;
        if (this.AliasSet.ContainsKey(prefix))
        {
          string replaceValue = this.AliasSet[prefix].Value;
          string aliasedTypeName = fullTypeName.Replace(replaceValue, prefix);
          return aliasedTypeName;
        }
      }

      return fullTypeName;
    }

    public void LoadFromXml(XElement xml)
    {
      XElement aliases = xml.Element("Aliases");
      if (aliases != null)
      {
        IEnumerable<XElement> aliasElements = aliases.Elements("Alias");
        foreach (XElement aliasElement in aliasElements)
        {
          Alias alias = new Alias(aliasElement);
          if (!this.AliasSet.ContainsKey(alias.Spec))
            this.AliasSet.Add(alias.Spec, alias);
        }
      }

      IEnumerable<XElement> contextElements = xml.Elements("Context");
      foreach (XElement contextElement in contextElements)
      {
        ObjectMapContext context = new ObjectMapContext(contextElement);
        if (!this.ContainsKey(context.Name))
          this.Add(context.Name, context);
      }
    }
  }
}
