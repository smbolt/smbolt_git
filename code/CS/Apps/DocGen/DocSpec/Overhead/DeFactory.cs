using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public static class DeFactory
  {
    private static SortedList<string, Type> TypeSet = null;

    public static DocumentElement CreateDeObject(string parentSet, string tagName, XElement xml, Doc doc, DocumentElement parent)
    {
      if (TypeSet == null)
        Initialize();

      Type t = null;
      if (TypeSet.ContainsKey(tagName))
        t = TypeSet[tagName];
      else if (parentSet.IsNotBlank())
        if (TypeSet.ContainsKey(parentSet + "." + tagName))
          t = TypeSet[parentSet + "." + tagName];

      if (t == null)
        throw new Exception("DeFactory cannot locate a type for element '" + tagName + "' + ParentSet='" + parentSet + "'.");

      DocumentElement de = (DocumentElement) Activator.CreateInstance(t, new object[] { xml, doc, parent });

      return de;
    }

    private static void Initialize()
    {
      TypeSet = new SortedList<string, Type>();

      Assembly a = Assembly.GetExecutingAssembly();
      List<Type> types = a.GetTypes().ToList();
      foreach (Type type in types)
      {
        Meta metaAttribute = (Meta)Attribute.GetCustomAttribute(type, typeof(Meta));
        if (metaAttribute != null)
        {
          string oxName = String.Empty;
          if (metaAttribute.OxName != null)
            oxName = metaAttribute.OxName;

          string childOfSet = String.Empty;
          if (metaAttribute.ChildOfSet != null)
            childOfSet = metaAttribute.ChildOfSet;

          string key = String.Empty;

          if (oxName.IsNotBlank())
          {
            string prefix = childOfSet.Trim();
            if (prefix.IsNotBlank())
              key = prefix + ".";

            key += oxName;

            if (!TypeSet.ContainsKey(key))
              TypeSet.Add(key, type);
          }
        }
      }
    }
  }
}
