using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using System.Xml.Linq;

namespace Org.GS
{
  public enum XType
  {
    NotSet,
    Attribute,
    Element,
    All
  }

  public enum XParmSource
  {
    NotSet,
    Parent,
    EntryAssemblyName,
    ConstuctorParm,
    Attribute
  }

  public static class XmlMapper
  {
    public static SortedList<string, Type> Types {
      get;
      set;
    }
    private static List<Assembly> _assemblies = new List<Assembly>();
    private static List<Assembly> _loadedAssemblies = new List<Assembly>();

    public static void Load()
    {
      LoadTypes();
    }

    public static bool AddAssembly(Assembly assembly)
    {
      if (!_assemblies.Contains(assembly))
      {
        _assemblies.Add(assembly);
        AugmentTypes(assembly);
        return true;
      }
      return false;
    }

    private static void LoadTypes()
    {
      if (Types == null)
        Types = new SortedList<string,Type>();

      Assembly thisAssembly = Assembly.GetExecutingAssembly();
      if (!_assemblies.Contains(thisAssembly))
        _assemblies.Add(thisAssembly);

      foreach(var assembly in _assemblies)
      {
        if (!_loadedAssemblies.Contains(assembly))
        {
          AugmentTypes(assembly);
        }
      }
    }

    private static void AugmentTypes(Assembly assembly)
    {
      if (!_loadedAssemblies.Contains(assembly))
      {
        List<Type> types = assembly.GetTypes().ToList();

        foreach (Type type in types)
        {
          XMap xMap = (XMap) type.GetCustomAttributes(typeof(XMap), true).FirstOrDefault();
          if (xMap != null)
          {
            string name = type.Name;
            if (xMap.Name.IsNotBlank())
              name = xMap.Name;
            if (xMap.ClassName.IsNotBlank())
              name = xMap.ClassName;
            if (name.IsNotBlank() && !Types.ContainsKey(name))
              Types.Add(name, type);
          }
        }

        _loadedAssemblies.Add(assembly);
      }
    }

  }
}
