using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Linq;
using Org.GS.Dynamic;

namespace Org.GS
{

  public class ObjectFactory2 : IDisposable
  {
    public bool LogToMemory { get; set; }
    private int level = 0; 

    private string _xmlIn = String.Empty;
    private string _xmlVerify = String.Empty;
    private string _serializeDebugBreak = String.Empty;
    private string _serializeDebugBreakObjectName = String.Empty;
    private string _deserializeDebugBreak = String.Empty;
    private string _deserializeDebugBreakObjectName = String.Empty;
    private static List<string> _failedGetTypeAttempts = new List<string>();

    private string _typeBeingSerialized;
    private string _propertyBeingSerialized;
    private int _serializationDepth;

    public string SerializeDebugBreak
    {
      set 
      { 
        _serializeDebugBreak = value;
        if (_serializeDebugBreak.Contains("."))
        {
          string[] tokens = _serializeDebugBreak.ToTokenArray(Constants.DotDelimiter);
          if (tokens.Length == 2)
          {
            _serializeDebugBreak = tokens[0];
            _serializeDebugBreakObjectName = tokens[1];
          }
        }  
      }
    }

    public string DeserializeDebugBreak
    {
      set 
      {
        _deserializeDebugBreak = value;
        if (_deserializeDebugBreak.Contains("."))
        {
          string[] tokens = _deserializeDebugBreak.ToTokenArray(Constants.DotDelimiter);
          if (tokens.Length == 2)
          {
            _deserializeDebugBreak = tokens[0];
            _deserializeDebugBreakObjectName = tokens[1];
          }
        } 
      }
    }

    public bool ValidateXml { get; set; }
    public bool StopAtMemoryLogCount { get; set; }
    public int MemoryLogCount { get; set; }

    private bool StopAtMemoryLog { get { return this.StopAtMemoryLogCount && this.MemoryLogCount > 0; } }

    public bool InDiagnosticsMode { get; set; }


    public ObjectFactory2()
    {
      Initialize();
    }

    public ObjectFactory2(bool inDiagnosticsMode)
    {
      Initialize();
      this.InDiagnosticsMode = inDiagnosticsMode;

      if (this.InDiagnosticsMode)
      {
        this.LogToMemory = true;
        SetUpDiagnosticsBreakpoints();
      }
    }

    private void Initialize()
    {
      this.StopAtMemoryLogCount = false;
      this.MemoryLogCount = 0; 
      this.InDiagnosticsMode = false;

      this.ValidateXml = false;
      g.InitializeXmlMapper();
    }

    private void SetUpDiagnosticsBreakpoints()
    {
      _deserializeDebugBreak = g.GetCI("Deserialize_DebugBreak");
      _serializeDebugBreak = g.GetCI("Serialize_DebugBreak"); 

      if (_serializeDebugBreak.Contains("."))
      {
        string[] tokens = _serializeDebugBreak.ToTokenArray(Constants.DotDelimiter);
        if (tokens.Length == 2)
        {
          _serializeDebugBreak = tokens[0];
          _serializeDebugBreakObjectName = tokens[1]; 
        }
      }

      if (_deserializeDebugBreak.Contains("."))
      {
        string[] tokens = _deserializeDebugBreak.ToTokenArray(Constants.DotDelimiter);
        if (tokens.Length == 2)
        {
          _deserializeDebugBreak = tokens[0];
          _deserializeDebugBreakObjectName = tokens[1]; 
        }
      }
    }

    public void SetDebugBreak(string debugBreak, string breakProcess)
    {
      string elementBreak = debugBreak;
      string attributeBreak = String.Empty;

      if (debugBreak.Contains("."))
      {
        string[] tokens = debugBreak.ToTokenArray(Constants.DotDelimiter);
        if (tokens.Length == 2)
        {
          elementBreak = tokens[0];
          attributeBreak = tokens[1]; 
        }
      }

      if (breakProcess.In("Both,Serialization"))
      {
        _serializeDebugBreak = elementBreak;
        if (attributeBreak.IsNotBlank())
          _serializeDebugBreakObjectName = attributeBreak;
      }

      if (breakProcess.In("Both,Deserialization"))
      {
        _deserializeDebugBreak = elementBreak;
        if (attributeBreak.IsNotBlank())
          _deserializeDebugBreakObjectName = attributeBreak;
      }
    }

    public object Deserialize(XElement xml, bool supressEquivalenceCheck = true)
    {
      try
      {
        bool equivalent = false;

        level = 0;
        WriteMemoryLog(level.ToString("00") + " - Deserializing " + xml.Name + "  Xml=" + xml.ToString().PadToLength(35).Trim());

        _xmlIn = xml.ToString();

        object o = Deserialize(null, xml, null, null);

        XElement verify = this.Serialize(o);

        if (!supressEquivalenceCheck)
          equivalent = verify.IsEquivalent(xml);

        if (!equivalent && !supressEquivalenceCheck)
          throw new Exception("The Deserialized objects xml was not logically equivalent to the serialized xml from that object.");

        o.InvokeAutoInit();

        return o;
      }
      catch (Exception ex)
      {
        string report = ex.ToReport();
        string log = "OBJECT FACTORY LOG" + g.crlf2 + g.MemoryLog;
        throw new Exception("An exception occurred during the Deserialization process." + g.crlf + report + g.crlf2 + log);
      }
    }

    // This method returns a business object for the provided xml based on the XMap custom attributes of the object
    public object Deserialize(object parent, XElement xml, ConstructorParms constructorParms, string propertyClassName)
    {
      try
      {
        if (xml == null)
          return null; 

        level++;
      
        WriteMemoryLog(level.ToString("00") + " - Deserializing " + xml.Name + "  Xml=" + xml.ToString().PadToLength(35).Trim());  

        string className = xml.Name.LocalName;
        string xmlLocalName = xml.Name.LocalName;

        if (propertyClassName.IsNotBlank())
          className = propertyClassName;

        if (xml.GetAttributeValue("ClassName").IsNotBlank())
          className = xml.GetAttributeValue("ClassName"); 

        string nameAttribute = xml.GetAttributeValue("Name"); 

        if (this.InDiagnosticsMode)
        {
          if (_deserializeDebugBreakObjectName.IsNotBlank())
          {
            if (xmlLocalName == _deserializeDebugBreak && nameAttribute == _deserializeDebugBreakObjectName)
              Debugger.Break();
          }
          else
          {
            if (xmlLocalName == _deserializeDebugBreak)
              Debugger.Break();
          }
        }

        if (!XmlMapper.Types.ContainsKey(className))
          AttemptToLocateAndMapAssembly(className); 

        if (!XmlMapper.Types.ContainsKey(className))
          return DeserializeNativeTypes(parent, xml, className);

        Type type = XmlMapper.Types[className];
        XMap xMap = type.GetXMap();

        object o = ConstructObject(parent, type, constructorParms, xml);

        if (type.IsDerivedFromGenericCollection()) 
        {
          if (xMap.CollectionElements.IsBlank())
            throw new Exception("The required CollectionElements property of the XMap custom attribute on Class '" + type.FullName + "'.");

          IEnumerable<XElement> collectionElements = GetCollectionElements(xml, xMap); 

          if (collectionElements != null)
            PopulateCollectionObject(o, collectionElements, xMap);
        }

        var piList = type.GetXMapProperties();
            
        foreach (var pi in piList)
        {
          string piName = pi.Name;
          string elementName = pi.Name;
          XMap propXMap = pi.GetXMap();
          bool myParent = propXMap.MyParent;
        
          bool isRequired = propXMap.IsRequired;
          if (propXMap.Name.IsNotBlank())
            elementName = propXMap.Name;

          // set the parent property if applicable
          if (myParent)
          {
            string piType = pi.PropertyType.Name;
            string parentType = parent.GetType().Name;
            if (piType == parentType)
              pi.SetValue(o, parent, null);
            else
              pi.SetValue(o, null, null); 
            continue;
          }

          // set properties derived from generic collections
          if (pi.IsDerivedFromGenericCollection())
          {
            XElement collectionElement = xml.Element(elementName);
            if (collectionElement != null)
            {
              object coll = Deserialize(o, collectionElement, null, propXMap.ClassName);
              pi.SetValue(o, coll, null);
            }
            continue;
          }

          // set other types of properties
          XObject propertyValue = xml.GetPropertyValue(pi, type); 

          if (isRequired && propertyValue == null)
            throw new Exception("No XML " + propXMap.XType.ToString() + " found for property '" + pi.GetPropertyName() +
                "' when populating object of type '" + type.Name + "'.");
        
          SetPropertyValue(o, pi, propertyValue, propXMap);
        
          continue;
        }

        o.InvokeAutoInit();
      
        level--; 
        return o;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to deserialize object from xml '" + xml.ToString().PadTo(35).Trim() + "'.", ex);
      }
    }

    private object DeserializeNativeTypes(object parent, XElement xml, string className)
    {
      WriteMemoryLog(level.ToString("00") + " - DeserializeNativeTypes " + xml.Name + " ClassName:" + className + "  Xml=" + xml.ToString().PadToLength(35).Trim());  

      // determine if the object to create is a native type which is part of a collection i.e. Dictionary<string,string> or List<string>
      if (parent != null)
      {
        if (parent.IsGenericCollection())
        {
          Type[] genArgTypes = parent.GetGenericArguments();

          bool typesAllNative = true;
          foreach (Type genericArgType in genArgTypes)
          {
            if (!genericArgType.FullName.Contains("System."))
            {
              typesAllNative = false;
              break;
            }
          }

          if (typesAllNative)
          {
            // currently only working for List<string> or Dictionary<string,string>
            string key = xml.GetAttributeValue("K");
            string value = xml.GetAttributeValue("V");

            switch (genArgTypes.Count())
            {
              case 1:
                if (value != null)
                  return value;
                break;

              case 2:
                if (key != null && value != null)
                {
                  var kvpType = typeof(KeyValuePair<,>);
                  var kvpConcreteType = kvpType.MakeGenericType(genArgTypes);
                  var kvp = Activator.CreateInstance(kvpConcreteType, new string[] { key, value });
                  return kvp;
                }
                break;
            }

          }
        }
        else
        {
          if (parent.IsDerivedFromGenericCollection())
          {
            string parentClassName = parent.GetType().Name;
            if (XmlMapper.Types.ContainsKey(parentClassName))
            {
              XMap parentXMap = parent.GetType().GetCustomAttribute<XMap>();

              if (parentXMap.CollectionElements == className && parentXMap.UseKeyValue && parent.IsDerivedFromGenericCollection())
              {
                Type[] argTypes = GetGenericTypeArgs(parent.GetType());
                if (argTypes.Length > 0)
                {
                  string key = xml.GetAttributeValue("K");
                  string value = xml.GetAttributeValue("V");
                  switch (argTypes.Length)
                  {
                    case 1:
                      if (value != null)
                        return value;
                      break;

                    case 2:
                      if (key != null && value != null)
                      {
                        var kvpType = typeof(KeyValuePair<,>);
                        var kvpConcreteType = kvpType.MakeGenericType(argTypes);
                        var kvp = Activator.CreateInstance(kvpConcreteType, new string[] { key, value });
                        return kvp;
                      }
                      break;
                  }
                }
              }
            }
          }
          else
          {
            var pi = parent.GetType().GetProperty(className);
            if (pi != null)
            {
              var propXMap = (XMap)pi.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
              if (propXMap != null)
              {
                var xmlValue = xml;

                switch (propXMap.XType)
                {
                  case XType.Element:
                    if (propXMap.WrapperElement.IsNotBlank())
                    {
                      if (xml.Name.LocalName != propXMap.WrapperElement && xml.Element(propXMap.WrapperElement) != null)
                        xmlValue = xml.Element(propXMap.WrapperElement);

                      if (xmlValue.Name.LocalName == propXMap.WrapperElement)
                      {
                        if (propXMap.CollectionElements.IsNotBlank())
                        {
                          var collectionElements = xml.Elements(propXMap.CollectionElements);
                          Type[] argTypes = GetGenericTypeArgs(pi.PropertyType);
                          
                          if (argTypes.Length > 0)
                          {
                            switch (argTypes.Length)
                            {
                              case 1:
                                if (pi.PropertyType.Name == "List`1")
                                {
                                  var listType = typeof(List<>);
                                  var listConcreteType = listType.MakeGenericType(argTypes);
                                  var list = Activator.CreateInstance(listConcreteType);
                                  var addMethod = list.GetType().GetMethod("Add"); 
                                  foreach (var collectionElement in collectionElements)
                                  {
                                    var elementValue = collectionElement.Value;
                                    if (elementValue != null)
                                    {
                                      switch (argTypes[0].Name)
                                      {
                                        case "String":
                                          addMethod.Invoke(list, new object[] { elementValue.ToString() }); 
                                          break;
                                      }
                                    }
                                  }

                                  return list;
                                }
                                break;

                              case 2:
                                Type twoArgType = null;

                                switch (pi.PropertyType.Name)
                                {
                                  case "Dictionary`2":
                                    twoArgType = typeof(System.Collections.Generic.Dictionary<,>);
                                    break;

                                  case "SortedList`2":
                                    twoArgType = typeof(System.Collections.Generic.SortedList<,>);
                                    break;

                                  case "SortedDictionary`2":
                                    twoArgType = typeof(System.Collections.Generic.SortedDictionary<,>);
                                    break;

                                  default:
                                    throw new Exception("The type '" + pi.PropertyType.Name + "' is not supported in the method 'DeserializeNativeTypes'.");
                                }

                                var twoArgConcreteType = twoArgType.MakeGenericType(argTypes);
                                var twoArgObject = Activator.CreateInstance(twoArgConcreteType);
                                var twoArgAddMethod = twoArgObject.GetType().GetMethod("Add");

                                foreach (var collectionElement in collectionElements)
                                {
                                  if (!collectionElement.AttributeExists("K"))
                                    throw new Exception("The 'K' (key) attribute does not exist in the collection element '" + collectionElement.ToString() + "'.");
                                  if (!collectionElement.AttributeExists("V"))
                                    throw new Exception("The 'V' (value) attribute does not exist in the collection element '" + collectionElement.ToString() + "'.");
                                  var key = collectionElement.Attribute("K").Value;
                                  var value = collectionElement.Attribute("V").Value;
                                  twoArgAddMethod.Invoke(twoArgObject, new object[] { key, value });
                                }

                                return twoArgObject;
                            }
                          }
                        }
                      }
                    }
                    break;

                  case XType.Attribute:

                    break;
                }
              }
            }
          }
        }
      }

      throw new Exception("Missing XMap attribute on type. XmlMapper.Types collection does not contain an entry for type name '" + className + "'.");
    }

    private Type[] GetGenericTypeArgs(Type type)
    {
      Type[] args = new Type[0]; 

      Type theType = type;
      while(args.Length == 0 && type != null)
      {
        args = type.GetGenericArguments();
        if (args.Length > 0)
          return args;
        type = type.BaseType;
      }

      return args; 
    }

    private List<PropertyInfo> GetXMapProperties(Type type)
    {
      List<PropertyInfo> xmapPiList = new List<PropertyInfo>();
      List<PropertyInfo> piList = type.GetProperties().ToList();

      foreach (PropertyInfo pi in piList)
      {
        XMap propXMap = (XMap)pi.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
        if (propXMap != null)
            xmapPiList.Add(pi); 
      }
      return xmapPiList;
    }
    
    private string GetCollectionElementName(XMap xMap, PropertyInfo pi)
    {
      string collectionElementName = String.Empty;

      if (pi.IsGenericCollection())
      {
        if (xMap.CollectionElements.IsBlank())
          throw new Exception("The required CollectionElements property of the XMap custom attribute on property '" + pi.Name + 
              "' cannot be blank for a property that is a generic collection.");

        return xMap.CollectionElements;
      }

      XMap classXMap = (XMap)pi.PropertyType.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
      if (classXMap.CollectionElements.IsBlank())
        throw new Exception("The required CollectionElements property of the XMap custom attribute on property '" + pi.Name + "' is blank '.");

      return classXMap.CollectionElements;
    }

    private IEnumerable<XElement> GetCollectionElements(XElement xml, XMap xMap)
    {
      IEnumerable<XElement> collectionElements = null;

      if (xml == null)
        return null;

      if (xMap.UseKeyValue)
      {
        collectionElements = xml.Elements(xMap.CollectionElements); 
      }
      else
      {
        if (xMap.WrapperElement.IsBlank())
          collectionElements = xml.Elements(xMap.CollectionElements);
        else
        {
          string elementName = xml.Name.LocalName;
          if (elementName == xMap.WrapperElement)
          {
            if (xml.Element(xMap.WrapperElement) == null)
            {
              collectionElements = xml.Elements(xMap.CollectionElements);
            }
            else
            {
              if (xml.Element(xMap.WrapperElement) != null)
                collectionElements = xml.Element(xMap.WrapperElement).Elements(xMap.CollectionElements);
            }
          }
          else
          {
            if (xml.Element(xMap.WrapperElement) != null)
              collectionElements = xml.Element(xMap.WrapperElement).Elements(xMap.CollectionElements);
          }

        }
      }

      return collectionElements;
    }

    private void SetPropertyValue(object o, PropertyInfo pi, XObject xml, XMap propXMap)
    {
      string xmlString = xml != null ? xml.ToString().PadToLength(40) : "Null"; 
      WriteMemoryLog(level.ToString("00") + " - Entering SetPropertyValue for Property '" + pi.Name + "', xml value is '" + xmlString + "'."); 

      string defaultValue = propXMap.DefaultValue;
      string format = propXMap.Format;
      string propertyName = pi.Name;

      if (propXMap.Name.IsNotBlank())
      {
        propertyName = propXMap.Name;
      }
      else
      {
        XMap propTypeXMap = pi.PropertyType.GetXMap();

        if (propTypeXMap != null)
          propertyName = GetPropertyName(pi.Name, propTypeXMap.Name);
      }

      // if the property is for a type (complex object, which is an XElement)
      if (propXMap.IsObject)
      {
        if (xml == null)
        {
          // don't set property - leave null
          return;
        }
        else
        {
          if (xml.NodeType == System.Xml.XmlNodeType.Element)
          {
            XElement xElement = xml as XElement;
            propertyName = xElement.Name.LocalName;
          }
          else
          {
            throw new Exception("A property that is marked as XMap.IsObject = true must be an element, not an attribute.  Found a non-element property for " +
                                "property '" + pi.PropertyType.Name + "' on object type '" + o.GetType().Name + "'."); 
          }
        }
      }

      string className = propertyName;
      if (propXMap.ClassName.IsNotBlank())
        className = propXMap.ClassName;

      if (propXMap.XType == XType.Element)
      {
        if (!XmlMapper.Types.ContainsKey(className))
          AttemptToLocateAndMapAssembly(className);
      }

      if (XmlMapper.Types.ContainsKey(className))
      {
        if (xml == null)
          return;

        object objectProperty = Deserialize(o, (XElement)xml, null, propXMap.ClassName);
        pi.SetValue(o, objectProperty, null);
        return;
      }

      // For enumeration types
      if (pi.PropertyType.IsEnum)
      {
        if (xml == null)
        {
          if (defaultValue.IsBlank())
            throw new Exception("Enum type '" + pi.PropertyType.Name + "' in object of type '" + o.GetType().Name + "' cannot be set to null and no default value is provided.");
        }

        string enumString = (string) GetSimpleXObjectValue(xml, defaultValue);

        if (enumString == null)
          throw new Exception("Value fo populating enum type '" + pi.PropertyType.Name + "' in object of type '" + o.GetType().Name + "' " +
            "was not located, " + xml.NodeType.ToString() + " not found.");

        if (!Enum.IsDefined(pi.PropertyType, enumString))
          throw new Exception("Enum type '" + pi.PropertyType.Name + "' cannot be set to the value '" + enumString + "'.");

        object enumValue = Enum.ToObject(pi.PropertyType, Enum.Parse(pi.PropertyType, enumString));
        pi.SetValue(o, enumValue, null);
        return;
      }

      bool isNullable = false;
      string typeName = pi.PropertyType.Name;
      if (typeName == "Nullable`1")
      {
        Type[] genericTypes = GetGenericTypeArgs(pi.PropertyType);
        if (genericTypes.Length == 1)
        {
          typeName = genericTypes[0].Name;
          isNullable = true; 
        }
      }

      // For other types
      switch (typeName)
      {
        case "XElement":
          pi.SetValue(o, xml, null); 
          break;

        case "String":
          pi.SetValue(o, GetSimpleXObjectValue(xml, defaultValue), null);
          break;

        case "Boolean":
          if (isNullable)
            pi.SetValue(o, g.GetNullableBooleanValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetBooleanValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Int32":
          if (isNullable)
            pi.SetValue(o, g.GetNullableInt32Value(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetInt32Value(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Int64":
          if (isNullable)
            pi.SetValue(o, g.GetNullableInt64Value(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetInt64Value(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Float":
          if (isNullable)
            pi.SetValue(o, g.GetNullableFloatValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetFloatValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Decimal":
          if (isNullable)
            pi.SetValue(o, g.GetNullableDecimalValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetDecimalValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "DateTime":
          if (isNullable)
            pi.SetValue(o, g.GetNullableDateTimeValue(GetSimpleXObjectValue(xml, defaultValue), format), null);
          else
            pi.SetValue(o, g.GetDateTimeValue(GetSimpleXObjectValue(xml, defaultValue), format), null);
          break;

        case "TimeSpan":
          if (isNullable)
            pi.SetValue(o, g.GetNullableTimeSpanValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          else
            pi.SetValue(o, g.GetTimeSpanValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Single":
          if (isNullable)
            pi.SetValue(o, g.GetNullableSingleValue(GetSimpleXObjectValue(xml, defaultValue)), null); 
          else
            pi.SetValue(o, g.GetSingleValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        case "Point":
          pi.SetValue(o, g.GetPointValue(GetSimpleXObjectValue(xml, defaultValue)), null); 
          break;

        case "PointF":
          pi.SetValue(o, g.GetPointFValue(GetSimpleXObjectValue(xml, defaultValue)), null); 
          break;

        case "Size":
          pi.SetValue(o, g.GetSizeValue(GetSimpleXObjectValue(xml, defaultValue)), null); 
          break;

        case "SizeF":
          pi.SetValue(o, g.GetSizeFValue(GetSimpleXObjectValue(xml, defaultValue)), null); 
          break;

        case "Type":
          pi.SetValue(o, g.GetTypeValue(GetSimpleXObjectValue(xml, defaultValue)), null);
          break;

        default:
          WriteMemoryLog(level.ToString("00") + " - Throwing exception:  Type '" + pi.PropertyType.Name + "' not yet supported in 'ObjectFactory.SetPropertyValue()' method."); 
          throw new Exception("Type '" + typeName + "' (Nullable`1 = " + isNullable.ToString() + ") not yet supported in 'ObjectFactory.SetPropertyValue()' method.");
      }
    }

    // Used to set the value of a collection property and populate the elements of the collection.
    public void SetCollectionProperty(object parent, object o, PropertyInfo pi, XElement collectionElement)
    {
      if (collectionElement == null)
        return; 

      XMap propXMap = (XMap) pi.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
      string collectionElementName = propXMap.CollectionElements;

      IEnumerable<XElement> collectionElements = collectionElement.Elements(collectionElementName);
      if (collectionElements == null)
        return;

      // is the property a native .Net generic collection?
      if (pi.IsGenericCollection())
      {
        var genericArgs = pi.PropertyType.GetGenericArguments();
        switch (genericArgs.Count())
        {
          case 1:
            var listType = typeof(List<>);
            var listConcreteType = listType.MakeGenericType(genericArgs);
            var newList = Activator.CreateInstance(listConcreteType);
            PopulateCollectionObject(newList, collectionElements, propXMap);
            pi.SetValue(o, newList, null);
            break;

          default:
            var dictType = typeof(Dictionary<,>);
            var dictConcreteType = dictType.MakeGenericType(genericArgs);
            var newDict = Activator.CreateInstance(dictConcreteType);
            PopulateCollectionObject(newDict, collectionElements, propXMap);
            pi.SetValue(o, newDict, null);
            break;
        }
      }
      else // it must be a custom (Org) collection property with XMap custom attribute
      {
        XMap propTypeXMap = (XMap) pi.PropertyType.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

        if (propTypeXMap == null)
          throw new Exception("XMap custom attribute not found on property '" + pi.Name + "' of type '" + o.GetType().FullName + "'.");

        // build the object, populate it and set the value of the property to the newly created object
        object collectionObject = ConstructObject(o, pi.PropertyType, null, null);
        PopulateCollectionObject(collectionObject, collectionElements, propTypeXMap);
        pi.SetValue(o, collectionObject, null);
      }     
    }

    // Used to populate an object, which is a collection, with the elements of the collection.
    public void PopulateCollectionObject(object o, IEnumerable<XElement> collectionElements, XMap objectXMap)
    {
      if (this.LogToMemory)
      {
        string startOfCollection = String.Empty;
        if (collectionElements != null)
        {
          if (collectionElements.Count() > 0)
            startOfCollection = collectionElements.First().ToString().PadToLength(35).Trim(); 
        }

        WriteMemoryLog(level.ToString("00") + " - PopulateCollectionObject XMap name=" + objectXMap.Name + "  className=" + objectXMap.ClassName + 
                      "  First XElement=" + startOfCollection);
      }

      foreach (XElement collectionElement in collectionElements)
      {
        // build the object
        var item = Deserialize(o, collectionElement, null, objectXMap.ClassName);

        // add the object to the collection
        MethodInfo mi = o.GetType().GetMethod("Add");
        switch (mi.GetParameters().Count())
        {
          // Lists
          case 1:
            object[] listParms = new object[] { item };
            mi.Invoke(o, listParms);
            break;

          // Dictionaries
          case 2:
            object key = GetObjectKey(item);
            bool sequenceDuplicates = objectXMap.SequenceDuplicates;

            object value = item;
            if (item.GetType().Name.Contains("KeyValuePair"))
            {
              PropertyInfo pi = item.GetType().GetProperty("Value");
              value = pi.GetValue(item, null);
            }

            if (!sequenceDuplicates)
            {
              XMap propTypeXMap = (XMap) o.GetType().GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
              if (propTypeXMap != null)
                sequenceDuplicates = propTypeXMap.SequenceDuplicates;
            }                                                

            MethodInfo containsKeyMi = o.GetType().GetMethod("ContainsKey");
            if (containsKeyMi == null)
              throw new Exception("Method 'ContainsKey' could not be found in collection object '" + o.GetType().FullName + "'.");

            if (Convert.ToBoolean(containsKeyMi.Invoke(o, new object[] { key })))
            {
              int seq = 0;
              if (sequenceDuplicates)
              {
                string keySeq = key + "-" + seq.ToString("000");
                while (Convert.ToBoolean(containsKeyMi.Invoke(o, new object[] { keySeq })))
                {
                  seq++;
                  if (seq == 1000)
                    throw new Exception("Maximum sequence number reached for '" + keySeq + "' attempting to add item to collection '" + o.GetType().FullName + "'.");
                  keySeq = key + "-" + seq.ToString("000");
                }
                key = keySeq;
              }
              else
              {
                throw new Exception("Key with value '" + key + "' already exists in object '" + o.GetType().FullName + "'.");
              }
            }

            object[] dictParms = new object[] { key, value };
            mi.Invoke(o, dictParms);
            break;
        }
      }            
    }

    private object GetObjectKey(object o)
    {            
      string keyName = "";
      XMap objXMap = (XMap) o.GetType().GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

      if (objXMap == null)
      {
        if (o.IsDerivedFromGenericCollection())
        {
          if (o.GetType().GetProperty("Key") != null)
          {
            PropertyInfo pi = o.GetType().GetProperty("Key");
            return pi.GetValue(o, null); 
          }
        }
      }
      else
      {
        keyName = objXMap.KeyName;
        if (objXMap.CompositeKey.IsNotBlank())
        {
          string compositeKey = String.Empty;
          List<string> keyPartNames = objXMap.CompositeKey.Split(new string[] {"|||"}, StringSplitOptions.RemoveEmptyEntries).ToList();
          foreach (string keyPartName in keyPartNames)
          {
            PropertyInfo pi = o.GetType().GetProperty(keyPartName);
            if (pi == null)
              throw new Exception("Composite key part '" + keyPartName + "' was not located as a property in object '" + o.GetType().Name + "'.");

            object propValue = pi.GetValue(o, null); 
            if (propValue == null)
              throw new Exception("Composite key part '" + keyPartName + "' is null in object '" + o.GetType().Name + "'.");

            string keyPart = propValue.ToString().Trim();

            if (compositeKey.IsNotBlank())
              compositeKey = compositeKey + "|||";

            compositeKey = compositeKey + keyPart; 
          }

          return compositeKey; 
        }
      }          

      List<PropertyInfo> piList = o.GetType().GetProperties().ToList();

      foreach (PropertyInfo pi in piList)
      {
        // for keys specified at the object level (KeyName=)
        string propertyName = pi.Name;
        if (propertyName == keyName)
        {
          return pi.GetValue(o, null);
        }

        // for keys specified at the property level (IsKey = true)
        XMap propXMap = pi.GetXMap();
        if (propXMap != null)
        {
          if (propXMap.IsKey)
          {
            return pi.GetValue(o, null);
          }
        }
        else // for native collections
        {
          if (pi.Name.ToLower() == "key")
          {
            return pi.GetValue(o, null);
          }
        }
      }

      throw new Exception("No property of the object '" + o.GetType().FullName + "' is designated as a key by use of XMap property 'IsKey'.");
    }

    // Used to get simple (non-complex) values from the xml
    // Used for XAttributes and XElements with no children, just string values
    private object GetSimpleXObjectValue(XObject xml, object defaultValue)
    {
      if (xml == null)
        return defaultValue;

      string xType = xml.GetType().Name;

      if (xType == "XAttribute")
        return ((XAttribute)xml).Value;

      return ((XElement)xml).Value;
    }

    // Locate the constructor and call it to create the object
    private object ConstructObject(object parent, Type type, ConstructorParms constructorParms, XElement xml)
    {
      WriteMemoryLog(level.ToString("00") + " - Entering ConstructObject ElementName=" + xml.Name + " Xml=" + xml.ToString().PadToLength(35).Trim());

      ConstructorInfo constructorInfo = null;
      List<ConstructorInfo> ciList = type.GetConstructors().ToList();
      List<XParm> xParmList = new List<XParm>();
      List<ParameterInfo> parmInfoList = new List<ParameterInfo>();

      foreach (ConstructorInfo ci in ciList)
      {
        xParmList = ci.GetCustomAttributes(typeof(XParm), true).OfType<XParm>().ToList();

        // get a list of the constructors parameters
        parmInfoList = ci.GetParameters().ToList();

        if (parmInfoList.Count == 0 && xParmList.Count == 0)
        {
          constructorInfo = ci;
          break;
        }

        if (parmInfoList.Count == xParmList.Count)
        {
          bool parmsMatch = true;

          for (int i = 0; i < parmInfoList.Count; i++)
          {
            bool parmFound = false;
            for (int j = 0; j < xParmList.Count; j++)
            {
              if (parmInfoList[i].Name == xParmList[j].Name)
              {
                parmFound = true;
                break;
              }
            }

            if (!parmFound)
            {
              parmsMatch = false;
              break;
            }
          }

          if (parmsMatch)
          {
            constructorInfo = ci;
            break;
          }
        }
      }

      if (constructorInfo == null)
          throw new Exception("ObjectFactory could not locate a parameterless constructor for type '" + type.FullName + "'.");

      object[] parms = new object[xParmList.Count];

      int index = 0;
      foreach (ParameterInfo pi in parmInfoList)
      {
        XParm xParm = null;
        foreach (XParm p in xParmList)
        {
          if (pi.Name == p.Name)
          {
            xParm = p;
            break;
          }
        }

        switch (xParm.ParmSource)
        {
          case XParmSource.Parent:
            var parameter = parmInfoList.ElementAt(index);
            string parameterTypeName = parameter.ParameterType.Name;
            string parentTypeName = parent.GetType().Name;
            if (parameterTypeName == parentTypeName)
            {
              parms[index++] = parent;
            }
            else
            {
              if (xParm.Required)
              {
                throw new Exception("For object type '" + type.Name + "', the constructor parameter (XParmSource=Parent) is expecting object of type '" + 
                                    parameterTypeName + "' but found " + "supplied parameter of type '" + parentTypeName + "'.");
              }
              else
              {
                parms[index++] = null;
              }
            }
            break;

          case XParmSource.EntryAssemblyName:
            string entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;
            parms[index++] = entryAssemblyName;
            break;

          case XParmSource.ConstuctorParm:
            if (constructorParms == null)
              throw new Exception("ObjectFactory could not locate a constructor parm '" + xParm.Name + 
                  "' because ConstructorParm collection is null when calling constructor for type '" + type.FullName + "'.");
            if (!constructorParms.ContainsKey(xParm.Name))
              throw new Exception("ObjectFactory could not locate a constructor parm '" + xParm.Name + 
                  "' in ConstructorParm collection for calling constructor for type '" + type.FullName + "'.");
            parms[index++] = constructorParms[xParm.Name];
            break;

          case XParmSource.Attribute:
            if (xml.Attribute(xParm.AttrName) == null)
              throw new Exception("ObjectFactory could not locate a constructor parm '" + xParm.Name + "' specified attribute '" + xParm.AttrName + 
                  "' is null when calling constructor for type '" + type.FullName + "'.");
            parms[index++] = xml.Attribute(xParm.AttrName).Value.Trim();
            break;

          default:
            throw new Exception("Population of constructor parameters for ParmSource='" + xParm.ParmSource.ToString() + "' is not yet implemented.");
        }
      }

      object o = constructorInfo.Invoke(parms);

      WriteMemoryLog(level.ToString("00") + " - Exiting ConstructObject ElementName=" + xml.Name + " Xml=" + xml.ToString().PadToLength(35).Trim());

      return o;
    }
        
    public XElement Serialize(object o)
    {
      return Serialize(o, 0);
    }

    // This method returns xml from the provided object base on the XMap custom attributes of the object
    public XElement Serialize(object o, int serializationDepth)
    {
      try
      {
        if (o == null)
          return null;

        int depth = serializationDepth + 1;
        _serializationDepth = depth;
        _typeBeingSerialized = "Unknown";
        _propertyBeingSerialized = String.Empty;

        Type type = o.GetType();
        _typeBeingSerialized = type.Name;

        WriteMemoryLog(level.ToString("00") + " - Serialize object type " + type.FullName);

        XMap xMap = type.XMap();

        string elementName = type.TypeXMapName();

        if (CheckSerializeDebug(o, elementName))
          Debugger.Break();

        XElement xml = new XElement(elementName);

        if (xMap.ClassName.IsNotBlank())
          xml.Add(new XAttribute("ClassName", xMap.ClassName));

        if (type.IsDerivedFromGenericCollection()) 
        {
          XElement xElement = ApplyWrapperIfUsed(xml, xMap.WrapperElement);
          LoadCollectionElements(o, xElement, xMap, depth);
        }

        List<PropertyInfo> piList = type.GetXMapProperties();

        foreach (PropertyInfo pi in piList)
        {
          _propertyBeingSerialized = pi.Name;
          XMap propXMap = pi.XMap();

          if (pi.XMap().MyParent)
            continue;

          object value = pi.GetValue(o, null);

          if (value == null)
            continue;
          
          switch (pi.GetXObjectType(value))
          {
            case XObjectType.XElement:
              xml.Add(value);
              break;

            case XObjectType.GenericCollectionBased:
              XElement temp = new XElement("Temp");
              LoadCollectionElements(value, temp, propXMap, depth);

              // check to see if collection may be non-native and may contain properties
              List<PropertyInfo> collectionProperties = pi.PropertyType.GetProperties().Where(x => x.GetCustomAttribute<XMap>() != null).ToList();
              foreach (PropertyInfo collectionProperty in collectionProperties)
              {
                XMap collPropXMap = collectionProperty.GetCustomAttribute<XMap>();

                // Don't map parent referrences to Xml
                if (collPropXMap.MyParent)
                  continue;

                string collPropertyName = GetPropertyName(collectionProperty.Name, collPropXMap.Name);
                if (collPropXMap.XType == XType.Attribute)
                {
                  object collPropertyValue = collectionProperty.GetValue(pi.GetValue(o, null), null);
                  string collPropertyValueString = String.Empty;
                  if (collPropertyValue != null)
                    collPropertyValueString = collPropertyValue.ToString();
                  string name = collPropertyName;
                  if (collPropXMap.Name.IsNotBlank())
                    name = collPropXMap.Name;

                  bool collIsExplicit = collPropXMap.IsExplicit;
                  bool collIsRequired = collPropXMap.IsRequired;
                  bool collIsDefaultValue = IsDefaultValue(collectionProperty.PropertyType, collPropXMap.DefaultValue, collPropertyValueString, collPropXMap.Format);

                  if (collIsExplicit || collIsRequired || !collIsDefaultValue)
                  {
                    temp.Add(new XAttribute(name, collPropertyValue.ToString()));
                  }
                }
                else
                {
                  if (collPropXMap.XType == XType.Element)
                  {
                    object collPropertyValue = collectionProperty.GetValue(pi.GetValue(o, null), null);
                    string name = collPropertyName;
                    if (collPropXMap.Name.IsNotBlank())
                      name = collPropXMap.Name;
                    temp.Add(Serialize(collPropertyValue, depth));
                  }
                }
              }

              if (!temp.HasElements && !temp.HasAttributes)
                break;

              XElement xElement = xml;

              string wrapperElement = pi.PropTypeXMap()?.WrapperElement ?? String.Empty;
              if (wrapperElement.IsBlank() && propXMap.WrapperElement.IsNotBlank())
                wrapperElement = propXMap.WrapperElement;

              if (wrapperElement.IsNotBlank() && temp.Element(wrapperElement) == null)
                xElement = ApplyWrapperIfUsed(xml, wrapperElement);


              foreach (XAttribute attr in temp.Attributes())
              {
                var existingAttr = xElement.Attribute(attr.Name); 
                if (existingAttr != null) 
                  throw new Exception("Duplicate attribute '" + attr.Name + "' cannot be added to Temp element '" + temp.ToString() + "'.");
                xElement.Add(attr);
              }

              foreach (XElement element in temp.Elements())
                xElement.Add(element);
              break;

            case XObjectType.Complex:
              XElement complexElement = Serialize(value, depth);
              if (complexElement != null)
                if (complexElement.HasElements || complexElement.HasAttributes)
                  xml.Add(complexElement);
              break;

            default:  // XObjectType = Simple
              string valueString = String.Empty;
              if (value != null)
                valueString = value.ToString();

              if (propXMap.Format.IsNotBlank())
                valueString = ApplyFormat(pi.PropertyType, valueString, propXMap.Format);

              if (pi.PropertyType.Name == "Point")
              {
                valueString = valueString.Replace("{X=", String.Empty).Replace("Y=", String.Empty)
                                          .Replace("}", String.Empty).Replace(" ", String.Empty);
              }

              if (pi.PropertyType.Name == "SizeF")
              {
                valueString = valueString.Replace("{Width=", String.Empty).Replace("Height=", String.Empty)
                                          .Replace("}", String.Empty).Replace(" ", String.Empty);
              }

              if (pi.PropertyType.Name == "PointF" || pi.PropertyType.Name == "Size")
                throw new Exception("Serialization of type 'PointF and 'Size' are not yet implemented.  Need to code proper string representation of value.");

              bool isExplicit = propXMap.IsExplicit; // ,Explicit means it must be represented in the XML
              bool isRequired = propXMap.IsRequired;
              bool isDefaultValue = IsDefaultValue(pi.PropertyType, propXMap.DefaultValue, valueString, propXMap.Format);

              if (isExplicit || isRequired || !isDefaultValue)
              {
                if (propXMap.XType == XType.Element)
                  xml.Add(new XElement(pi.PiXMapName(), valueString));
                else
                  xml.Add(new XAttribute(pi.PiXMapName(), valueString));
              }
              break;
          }
          
        }

        return xml;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during object serialization.  Object type is '" + _typeBeingSerialized + "' property being serialized = '" + 
                            _propertyBeingSerialized + "' depth = " + _serializationDepth.ToString() + g.crlf2 +
                            "Memory Log: " + g.MemoryLog + ".", ex); 
      }
    }

    private bool CheckSerializeDebug(object o, string elementName)
    {
      string nameProperty = String.Empty;
      var namePi = o.GetType().GetProperty("Name");
      if (namePi != null)
        nameProperty = namePi.GetValue(o) != null ? namePi.GetValue(o).ToString() : String.Empty;

      if (_serializeDebugBreakObjectName.IsNotBlank())
      {
        if (elementName == _serializeDebugBreak && nameProperty == _serializeDebugBreakObjectName)
          return true;
      }
      else
      {
        if (elementName == _serializeDebugBreak)
          return true;
      }

      return false;
    }

    private bool IsDefaultValue(Type type, string defaultValue, string objectValue, string format)
    {
      if (type.IsEnum)
        return defaultValue == objectValue;

      string typeName = type.Name;
      if (typeName == "Nullable`1")
      {
        Type[] genericTypes = GetGenericTypeArgs(type);
        if (genericTypes.Length == 1)
        {
          typeName = genericTypes[0].Name;
        }
      }

      switch (typeName)
      {
        case "String":
        case "Boolean":
        case "Int32":
        case "Int64":
        case "DateTime":
        case "TimeSpan":
        case "Single":
        case "Decimal":
        case "Float":
        case "Type":
          return defaultValue == objectValue;

        case "Point":
        case "PointF":
        case "Size":
        case "SizeF":
        case "Object":
          return false;

        default:
          throw new Exception("Type '" + typeName + "' not yet supported in 'ObjectFactory.IsDefaultValue()' method.");
      }
    }

    private string ApplyFormat(Type type, string value, string format)
    {
      if (type.IsEnum)
        return value;

      string typeName = type.Name;
      if (typeName == "Nullable`1")
      {
        Type[] genericTypes = GetGenericTypeArgs(type);
        if (genericTypes.Length == 1)
        {
          typeName = genericTypes[0].Name;
        }
      }
      
      switch (typeName)
      {
        case "String":
        case "Boolean":
        case "Int32":
          return value;

        case "DateTime":
          DateTime dt = DateTime.Parse(value);
          return dt.ToString(format); 

        case "TimeSpan":
          TimeSpan ts = TimeSpan.Parse(value);
          return ts.ToString(format); 

        default:
          throw new Exception("Type '" + typeName + "' not yet supported in 'ObjectFactory.ApplyFormat()' method.");
      }
    }

    // Used to load the elements of a collection.
    private void LoadCollectionElements(object o, XElement xml, XMap objectXmap, int serializationDepth)
    {
      if (o == null)
          return;

      WriteMemoryLog(level.ToString("00") + " - LoadCollectionElements object type " + o.GetType().FullName + " ElementName=" + xml.Name + " Xml=" + xml.ToString().PadToLength(35).Trim()); 

      Type type = o.GetType();
      string typeName = type.Name;
      XMap xMap = (XMap) type.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

      Type argType0 = null;
      Type argType1 = null;

      // if collection is Org XClass type
      if (xMap != null)
      {
        while (type != null)
        {
          if (type.IsGenericCollection())
            break;
          type = type.BaseType;
        }

        if (type == null)
            throw new Exception("Unable to locate generic collection type in class ancestry of type '" + o.GetType().FullName + "'.");

        int genericArgCount = type.GetGenericArguments().Count();

        switch (genericArgCount)
        {
          case 1:
            argType0 = type.GetGenericArguments().First();
            IEnumerable<object> list = (IEnumerable<object>)o;
            XMap listItemXMap = (XMap) argType0.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

            // Custom Org XMap List
            if (listItemXMap != null)
            {
              foreach (object item in list)
              {
                xml.Add(Serialize(item, serializationDepth));
              }
            }
            else // a native List
            {
              if (xMap.UseKeyValue && xMap.CollectionElements.IsNotBlank())
              {
                string elementName = xMap.CollectionElements;
                string wrapperElementName = xMap.WrapperElement;
                if (list.Count() > 0)
                {
                  if (wrapperElementName.IsNotBlank())
                  {
                    XElement wrapper = new XElement(wrapperElementName);
                    xml.Add(wrapper); 
                    foreach (object item in list)
                    {
                      wrapper.Add(new XElement(elementName, new XAttribute("V", item.ToString())));
                    }
                  }
                  else
                  {
                    foreach (object item in list)
                    {
                      xml.Add(new XElement(elementName, new XAttribute("V", item.ToString())));
                    }
                  }
                }
              }
            }
            break;

          case 2:
            argType0 = type.GetGenericArguments().First();
            argType1 = type.GetGenericArguments().Last();
            XMap dictEntryXMap = (XMap) argType1.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

            // Custom Org XMap Typed Dictionary
            if (dictEntryXMap != null)
            {
              foreach (System.Collections.DictionaryEntry de in (System.Collections.IDictionary) o)
              {
                object key = de.Key;
                object value = de.Value;
                xml.Add(Serialize(value, serializationDepth));
              }
            }
            else // A native Dictionary or SortedList
            {
              if (xMap.UseKeyValue && xMap.CollectionElements.IsNotBlank())
              {
                System.Collections.IDictionary dict = (System.Collections.IDictionary)o;

                string elementName = xMap.CollectionElements;
                string wrapperElementName = xMap.WrapperElement;
                if (dict.Count > 0)
                {
                  if (wrapperElementName.IsNotBlank())
                  {
                    XElement wrapper = new XElement(wrapperElementName);
                    xml.Add(wrapper);
                    foreach (System.Collections.DictionaryEntry de in dict)
                    {
                      object key = de.Key;
                      object value = de.Value;
                      wrapper.Add(new XElement(elementName, new XAttribute("K", key.ToString()), new XAttribute("V", value.ToString())));
                    }
                  }
                  else
                  {
                    foreach (System.Collections.DictionaryEntry de in dict)
                    {
                      object key = de.Key;
                      object value = de.Value;
                      xml.Add(new XElement(elementName, new XAttribute("K", key.ToString()), new XAttribute("V", value.ToString())));
                    }
                  }
                }
              }
            }
            break;
        }

      }
      else
      {
        switch (type.GetGenericArguments().Count())
        {
          case 1:
            argType0 = type.GetGenericArguments().First();
            IEnumerable<object> list = (IEnumerable<object>)o;
            foreach (object item in list)
            {
              Type listItemType = item.GetType();
              XObjectType xObjectType = listItemType.GetXObjectType();
              XMap listItemXMap = listItemType.GetXMap();

              if (listItemXMap != null)
              {
                xml.Add(Serialize(item, serializationDepth));
              }
              else
              {
                if (xObjectType == XObjectType.GenericCollectionBased)
                {
                  LoadCollectionElements(item, xml, listItemXMap, serializationDepth + 1);
                }
                else
                {
                  var targetElement = xml;

                  string wrapperElement = String.Empty;
                  if (objectXmap.WrapperElement.IsNotBlank())
                    wrapperElement = objectXmap.WrapperElement;

                  string collectionElements = String.Empty;
                  if (objectXmap.CollectionElements.IsNotBlank())
                    collectionElements = objectXmap.CollectionElements;

                  if (collectionElements.IsBlank())
                    throw new Exception("Serialization of a native List requires the specification of the element name in the 'CollectionElements' property of the XMap attribute." +
                                        "  The type being serialized is '" + type.FullName + "'."); 

                  if (wrapperElement.IsNotBlank())
                  {
                    if (xml.Element(wrapperElement) == null)
                      xml.Add(new XElement(wrapperElement));
                      
                    targetElement = xml.Element(wrapperElement);
                  }                  

                  switch (listItemType.Name)
                  {
                    case "String":
                      targetElement.Add(new XElement(collectionElements, item.ToString())); 
                      break;

                    default:
                      throw new Exception("Currently serialization of native List objects of type '" + listItemType.Name + "' are not supported." +
                                          "  The type being serialized is '" + type.FullName + "'."); 
                  }
                }
              }
            }
            break;

          case 2:                        
              argType0 = type.GetGenericArguments().First();
              argType1 = type.GetGenericArguments().Last();
              XMap dictEntryXMap = (XMap)argType1.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();

              // Custom Org XMap Typed Dictionary
              if (dictEntryXMap != null)
              {
                foreach (System.Collections.DictionaryEntry de in (System.Collections.IDictionary)o)
                {
                  object key = de.Key;
                  object value = de.Value;
                  xml.Add(Serialize(value, serializationDepth));
                }
              }
              else // A native Dictionary
              {
                if (objectXmap == null)
                  throw new Exception("XMap attribute required on native dictionaries in order to specify xml element name and other properties. " +
                                      "This error occurred when processing collection object of type '" + o.GetType().Name + "'.");

                string elementName = objectXmap.CollectionElements;
                if (elementName.IsBlank())
                  throw new Exception("XMap 'CollectionElements' attribute is required on native dictionaries in order to specify the xml element name for collection entry. " +
                                      "This error occurred when processing collection object of type '" + o.GetType().Name + "'.");
                            
                foreach (System.Collections.DictionaryEntry de in (System.Collections.IDictionary)o)
                {
                  object key = de.Key;
                  object value = de.Value;
                  XElement entry = new XElement(elementName);
                  entry.Add(new XAttribute("K", key));
                  entry.Add(new XAttribute("V", value));
                  xml.Add(entry);
                }
              }
              break;
        }
      }
    }

    private XElement ApplyWrapperIfUsed(XElement xml, string wrapperElement)
    {
      if (wrapperElement.IsBlank())
        return xml;

      xml.Add(new XElement(wrapperElement));

      return xml.Element(wrapperElement);
    }

    private string GetPropertyName(string piName, string overrideName)
    {
      if (overrideName.IsNotBlank())
        return overrideName;

      return piName;
    }

    private void AttemptToLocateAndMapAssembly(string className)
    {
      if (!_failedGetTypeAttempts.Contains(className))
      {
        StartupLogging.WriteStartupLog("Attempting to locate and map assembly for type '" + className + "'."); 
        Type locatedType = AttemptToLocateType(className);
        if (locatedType == null)
        {
          _failedGetTypeAttempts.Add(className);
        }
        else
        {
          var typeAssembly = Assembly.GetAssembly(locatedType);
          if (typeAssembly == null)
          {
            _failedGetTypeAttempts.Add(className);
          }
          else
          {
            StartupLogging.WriteStartupLog("Adding assembly to XmlMapper: " + typeAssembly.FullName + "."); 
            XmlMapper.AddAssembly(typeAssembly);
          }
        }
      }
    }

    private Type AttemptToLocateType(string className)
    {
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      
      foreach (var assembly in assemblies)
      {
        var orgAssemblyTag = assembly.CustomAttributes.Where(a => a.AttributeType.Name == "OrgAssemblyTag").FirstOrDefault();
        if (orgAssemblyTag != null)
        {
          var types = assembly.GetTypes();
          foreach (var type in types)
          {
            if (type.IsClass && type.Name == className)
              return type;
          }
        }
      }

      return null;
    }

    private void WriteMemoryLog(string log)
    {
      if (!this.InDiagnosticsMode)
        return;

      if (!this.LogToMemory)
        return;

      g.LogToMemory(log);
      
      if (this.StopAtMemoryLog && g.MemoryLogCount == this.MemoryLogCount)
      {
        Debugger.Break();
      }
    }
   
    public void Dispose() { }

  }
}
