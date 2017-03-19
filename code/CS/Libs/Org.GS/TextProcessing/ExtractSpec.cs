using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements="Tsd")] 
  public class ExtractSpec : Dictionary<string, Tsd>
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public string RecogSpecName { get; set; }

    [XMap]
    public string Desc { get; set; }

    [XMap]
    public string Entity { get; set; }

    [XMap(DefaultValue = "True")]
    public bool RunExtract { get; set; }

    [XMap(IsRequired=true, DefaultValue = "DefaultRootName")]
    public string ExtractRoot { get; set; }

    public XElement RootElement { get { return Get_RootElement(); } }

    public ExtractSpecSet ExtractSpecSet { get; set; }
    public string FullFilePath { get; set; }

    public ExtractSpec()
    {
      this.Name = String.Empty;
      this.RecogSpecName = String.Empty;
      this.Desc = String.Empty;
      this.Entity = String.Empty;
      this.ExtractRoot = String.Empty;
      this.RunExtract = true;
      this.FullFilePath = String.Empty;
    }

    private XElement Get_RootElement()
    {
      XElement root = null;

      string extractRootSpec = this.ExtractRoot;
      string rootName = String.Empty;

      int openPar = extractRootSpec.IndexOf('(');
      if (openPar > -1)
      {
        int closeParen = extractRootSpec.IndexOf(')', openPar);
        if (closeParen == -1)
          throw new CxException(95, new object[] { this });
        rootName = extractRootSpec.GetTextBefore(Constants.OpenParen).Trim();
        if (rootName.IsBlank())
          throw new CxException(96, new object[] { this });
        if (rootName.Contains(' '))
          throw new CxException(97, new object[] { this });
        root = new XElement(rootName);

        string attrString = extractRootSpec.GetTextBetween(Constants.OpenParen, Constants.CloseParen);
        if (attrString.IsBlank())
          throw new CxException(98, new object[] { this });
        string[] attrs = attrString.Split(Constants.CommaDelimiter);
        foreach (string attr in attrs)
        {
          string attrName = String.Empty;
          string attrValue = String.Empty;

          if (attr.Contains("="))
          {
            string[] tokens = attr.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 2)
              throw new CxException(99, new object[] { this });
            if (tokens[0].Contains(' '))
              throw new CxException(100, new object[] { this });
            attrName = tokens[0];
            if (root.Attribute(attrName) != null)
              throw new CxException(101, new object[] { this });
            attrValue = tokens[1];
            root.Add(new XAttribute(attrName, attrValue));
          }
          else
          {
            attrName = attr;
            if (root.Attribute(attrName) != null)
              throw new CxException(102, new object[] { this });
            attrValue = String.Empty;
            root.Add(new XAttribute(attrName, attrValue));
          }
        }
      }
      else
      {
        rootName = extractRootSpec.Trim();
        if (rootName.IsBlank())
          throw new CxException(96, new object[] { this });
        if (rootName.Contains(' '))
          throw new CxException(97, new object[] { this });
        root = new XElement(rootName);
      }

      return root;
    }

    public void Reset()
    {
      foreach (var tsd in this.Values)
        ResetTsd(tsd);
    }

    private void ResetTsd(Tsd tsd)
    {
      foreach (var cmd in tsd)
      {
        cmd.ActiveToRun = true;
      }

      foreach (var childTsd in tsd.TsdSet.Values)
        ResetTsd(childTsd); 
    }
  }
}
