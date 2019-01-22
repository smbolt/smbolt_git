using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Org.GS.Licensing
{
  public class ComputerSignature
  {
    public string Version { get; set; }
    public DateTime SignatureDateTime { get; set; }
    public string ComputerName { get; set; }
    public string Baseboard_SerialNumber { get; set; }
    public string Cpu_ProcessorID { get; set; }
    public string DiskDrive_SerialNumber { get; set; }
    public string SystemEnclosure_SerialNumber { get; set; }
    public string System_MakeModelType { get; set; }

    public ComputerSignature()
    {
      this.Version = "1.0";
      this.ComputerName = String.Empty;
      this.Baseboard_SerialNumber = String.Empty;
      this.Cpu_ProcessorID = String.Empty;
      this.DiskDrive_SerialNumber = String.Empty;
      this.SystemEnclosure_SerialNumber = String.Empty;
      this.System_MakeModelType = String.Empty;
    }

    public void LoadFromXml(XElement xml)
    {
      this.Version = xml.Element("Version").Value.Trim();
      this.SignatureDateTime = DateTime.Parse(xml.Element("SignatureDateTime").Value.Trim());
      this.ComputerName = xml.Element("ComputerName").Value.Trim();
      this.Baseboard_SerialNumber = xml.Element("Baseboard_SerialNumber").Value.Trim();
      this.Cpu_ProcessorID = xml.Element("Cpu_ProcessorID").Value.Trim();
      this.DiskDrive_SerialNumber = xml.Element("DiskDrive_SerialNumber").Value.Trim();
      this.SystemEnclosure_SerialNumber = xml.Element("SystemEnclosure_SerialNumber").Value.Trim();
      this.System_MakeModelType = xml.Element("System_MakeModelType").Value.Trim();
    }

    public XElement GetXml()
    {
      XElement cs = new XElement("ComputerSignature");

      cs.Add(new XElement("Version", this.Version.Trim()));
      cs.Add(new XElement("SignatureDateTime", this.SignatureDateTime.ToString("MM/dd/yyyy HH:mm:ss")));
      cs.Add(new XElement("ComputerName", this.ComputerName.Trim()));
      cs.Add(new XElement("Baseboard_SerialNumber", this.Baseboard_SerialNumber.Trim()));
      cs.Add(new XElement("Cpu_ProcessorID", this.Cpu_ProcessorID.Trim()));
      cs.Add(new XElement("DiskDrive_SerialNumber", this.DiskDrive_SerialNumber.Trim()));
      cs.Add(new XElement("SystemEnclosure_SerialNumber", this.SystemEnclosure_SerialNumber.Trim()));
      cs.Add(new XElement("System_MakeModelType", this.System_MakeModelType.Trim()));

      return cs;
    }
  }
}
