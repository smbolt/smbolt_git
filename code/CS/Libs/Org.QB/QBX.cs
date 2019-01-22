using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Interop.QBXMLRP2;
using Org.GS.Logging;
using Org.GS;

namespace Org.QB
{
  public class QBX : IDisposable
  {
    private RequestProcessor2 _qbrp;
    private string _sessionId;
    private Logger _logger;

    public QBX(Logger logger)
    {
      _logger = logger;
    }

    public void OpenConnection()
    {
      try
      {
        _qbrp = new RequestProcessor2();
        _qbrp.OpenConnection2("ContursiIntegration", "Quickbooks Workbench", QBXMLRPConnectionType.localQBD);
      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting to open a connection to Quickbooks.";
        WriteLog(errorMessage, ex);
        throw new Exception(errorMessage, ex);
      }
    }

    public void BeginSession()
    {
      try
      {
        if (_qbrp == null)
          throw new Exception("The connection to Quickbooks is null.");

        _sessionId = _qbrp.BeginSession("", QBFileMode.qbFileOpenDoNotCare);
      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting to begin a session with Quickbooks.";
        WriteLog(errorMessage, ex);
        throw new Exception(errorMessage, ex);
      }
    }

    public void SendRequest()
    {
      try
      {

        XmlDocument inputXMLDoc = new XmlDocument();
        inputXMLDoc.AppendChild(inputXMLDoc.CreateXmlDeclaration("1.0", null, null));
        inputXMLDoc.AppendChild(inputXMLDoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));
        XmlElement qbXML = inputXMLDoc.CreateElement("QBXML");
        inputXMLDoc.AppendChild(qbXML);
        XmlElement qbXMLMsgsRq = inputXMLDoc.CreateElement("QBXMLMsgsRq");
        qbXML.AppendChild(qbXMLMsgsRq);
        qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
        XmlElement custQueryRq = inputXMLDoc.CreateElement("CustomerQueryRq");
        qbXMLMsgsRq.AppendChild(custQueryRq);
        custQueryRq.SetAttribute("requestID", "1");
        XmlElement nameFilter = inputXMLDoc.CreateElement("NameFilter");
        custQueryRq.AppendChild(nameFilter);
        XmlElement matchCriterion = inputXMLDoc.CreateElement("MatchCriterion");
        nameFilter.AppendChild(matchCriterion);
        matchCriterion.InnerText = "Contains";
        XmlElement nameValue = inputXMLDoc.CreateElement("Name");
        nameFilter.AppendChild(nameValue);
        nameValue.InnerText = "Test";

        string input = inputXMLDoc.OuterXml;

        string response = _qbrp.ProcessRequest(_sessionId, input);

        var responseDoc = new XmlDocument();
        responseDoc.LoadXml(response);

        string responseXml = responseDoc.OuterXml;




      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting send a request Quickbooks.";
        WriteLog(errorMessage, ex);
        throw new Exception(errorMessage, ex);
      }
    }

    public void EndSession()
    {
      try
      {
        if (_qbrp == null)
          throw new Exception("The connection to Quickbooks is null.");

        _qbrp.EndSession(_sessionId);
      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting to close the Quickbooks session.";
        WriteLog(errorMessage, ex);
        throw new Exception(errorMessage, ex);
      }
    }

    public void CloseConnection()
    {
      try
      {
        _qbrp.CloseConnection();
        _qbrp = null;
      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting to close the Quickbooks connection.";
        WriteLog(errorMessage, ex);
        throw new Exception(errorMessage, ex);
      }
    }

    public void Dispose()
    {
      try
      {
        if (_qbrp != null)
        {


        }
      }
      catch (Exception ex)
      {

      }
    }

    private void WriteLog(string logMessage, Exception ex = null)
    {
      if (_logger == null)
        return;

      _logger.Log(logMessage);
    }
  }
}
