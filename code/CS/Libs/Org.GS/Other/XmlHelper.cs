using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

namespace Org.GS
{
  public static class XmlHelper
  {
    public static decimal? GetAttrValueAsDecimal(XElement xElem, string name)
    {
      decimal? returnVal = null;

      decimal val;
      var attr = xElem.Attribute(name);
      if (attr != null)
      {
        if (decimal.TryParse(attr.Value, out val))
          returnVal = val;
      }
      return returnVal;
    }

    public static int? GetAttrValueAsInt(XElement xElem, string name)
    {
      int? returnVal = null;

      int val;
      XAttribute attr = xElem.Attribute(name);
      if (attr != null)
      {
        if (int.TryParse(attr.Value, out val))
          returnVal = val;
      }
      return returnVal;
    }

    public static int? GetElemValueAsInt(XElement xElem)
    {
      int? retVal = null;

      int val;
      if (int.TryParse(xElem.Value, out val))
        retVal = val;

      return retVal;
    }

    public static string GetAttrValueAsString(XElement xElem, string name)
    {
      string retVal = string.Empty;
      XAttribute attr = xElem.Attribute(name);

      if (attr != null)
        retVal = attr.Value;

      return retVal;
    }

    public static string GetAttributeValueAsString(this XElement xElem, string name)
    {
      string retVal = string.Empty;
      XAttribute attr = xElem.Attribute(name);

      if (attr != null)
        retVal = attr.Value;

      return retVal;
    }

    public static XElement GetMetaDataElement(XElement metaData, string elementName)
    {
      IEnumerable<XElement> metaDataElements = from e in metaData.Elements("DataItem") where e.Attribute("Name").Value == elementName select e;

      if (metaDataElements.Count() == 1)
        return metaDataElements.First();

      return null;
    }

    public static XElement GetChildDataItemElement(XElement parentElement, string elementName)
    {
      IEnumerable<XElement> childElements = from e in parentElement.Elements("DataItem") where e.Attribute("Name").Value == elementName select e;

      if (childElements.Count() == 1)
        return childElements.First();

      return null;
    }

    public static XElement GetElementByAttributeValue(XElement parent, string elementName, string attributeName, string attributeValue)
    {
      IEnumerable<XElement> elements = from e in parent.Elements(elementName) where e.Attribute(attributeName).Value == attributeValue select e;

      if (elements.Count() == 1)
        return elements.First();

      return null;
    }

    public static void AssertAttributes(XElement parent, string attributeNameList)
    {
      List<string> attributeNames = attributeNameList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

      foreach (string attributeName in attributeNames)
      {
        if (parent.Attribute(attributeName) == null)
          throw new Exception("Required attribute '" + attributeName + "' does not exist in element '" + parent.Name.LocalName + ".");
      }
    }
  }
}
