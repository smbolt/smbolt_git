using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS
{
  public class ObjectMapContext : SortedList<string, ObjectMapEntry>
  {
    public string Name { get; set; }

    public ObjectMapContext()
    {
    }

    public ObjectMapContext(XElement xml)
    {
      this.Name = XmlHelper.GetAttributeValueAsString(xml, "Name");

      IEnumerable<XElement> entryElements = xml.Elements("Entry");
      foreach (XElement entryElement in entryElements)
      {
        ObjectMapEntry entry = new ObjectMapEntry(entryElement);
        if (!this.ContainsKey(entry.Spec))
          this.Add(entry.Spec, entry);
      }            
    }

    public ObjectMapContext GetEntriesFor(string key)
    {
      ObjectMapContext entries = new ObjectMapContext();

      foreach (ObjectMapEntry e in this.Values)
      {
        if (e.Spec.StartsWith(key))
          entries.Add(e.Spec, e);
      }

      return entries;
    }

    public string GetSourceProperty(string sourceType)
    {
      ObjectMapContext entries = new ObjectMapContext();

      foreach (ObjectMapEntry e in this.Values)
      {
        if (e.Spec.Contains(":" + sourceType))
          entries.Add(e.Spec, e);
      }

      if (entries.Count > 1)
        throw new Exception("More than one ObjectMapEntry exists in ObjectMapContext named '" + this.Name + "' for the aliased source type '" + sourceType + "'.");

      if (entries.Count == 0)
        return String.Empty;

      ObjectMapEntry entry = entries.Values.First();
      string entrySpec = entry.Spec;
      string[] tokens = entrySpec.Split(':');

      if (tokens.Length != 2)
        throw new Exception("Illegally formatted ObjectMapEntry '" + entrySpec + "' exists in ObjectMapContext named '" + this.Name + "' for the aliased source type '" + sourceType + "'.");

      if (!tokens[1].Contains(sourceType + "."))
        throw new Exception("Illegally formatted ObjectMapEntry '" + entrySpec + "' exists in ObjectMapContext named '" + this.Name + "' for the aliased source type '" + sourceType + "'.");

      string propertyName = tokens[1].Replace(sourceType + ".", String.Empty).Trim();

      return propertyName;
    }
  }
}
