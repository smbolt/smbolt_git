using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace Org.EDI.OopFactory
{
  public class ClaimXmlTransformationService
  {
    public ClaimXmlTransformationService()
    {
    }

    public string TransformClaimXmlToEdiXml(string claimXml, FileStream transformStream)
    {
      XslCompiledTransform transform = null;
      MemoryStream outputStream = null;

      try
      {
        transform = new XslCompiledTransform();
        if (transformStream != null)
          transform.Load(XmlReader.Create(transformStream));

        outputStream = new MemoryStream();

        transform.Transform(XmlReader.Create(new StringReader(claimXml)), new XsltArgumentList(), outputStream);
        outputStream.Position = 0;

        string ediXml = new StreamReader(outputStream).ReadToEnd();

        return ediXml;
      }
      catch (Exception ex)
      {

        throw new Exception("An exception occurred during the processing of an XSLT transformation.", ex);
      }
      finally
      {
        if (transformStream != null)
        {
          transformStream.Close();
          transformStream.Dispose();
        }

        if (outputStream != null)
        {
          outputStream.Close();
          outputStream.Dispose();
        }
      }
    }
  }
}
