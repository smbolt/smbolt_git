using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace Org.GS
{
  public class FileSegmentHeader
  {
    public DateTime OperationBeginDT {
      get;
      set;
    }
    public DateTime OperationEndDT {
      get;
      set;
    }
    public DateTime SegmentCreateDT {
      get;
      set;
    }
    public string SourceFileFullPath {
      get;
      set;
    }
    public int OriginalFullFileLength {
      get;
      set;
    }
    public int FullBase64Length {
      get;
      set;
    }
    public int NumberOfSegments {
      get;
      set;
    }
    public int SegmentNumber {
      get;
      set;
    }
    public int SegmentLength {
      get;
      set;
    }

    public FileSegmentHeader()
    {
      Initialize();
    }

    public FileSegmentHeader(string headerText)
    {
      Initialize();

      string headerLeader = headerText.Substring(0, 48);
      string headerXmlLengthString = headerLeader.Substring(41, 6);
      int headerXmlLength = Int32.Parse(headerXmlLengthString);
      int xmlLength = headerText.Length - 68;
      string headerXml = headerText.Substring(48, xmlLength);

      XElement header = XElement.Parse(headerXml);
      this.OperationBeginDT = DateUtility.GetDateFromLongNumeric(header.Element("OperationBeginDT").Value.Trim());
      this.OperationEndDT = DateUtility.GetDateFromLongNumeric(header.Element("OperationEndDT").Value.Trim());
      this.SegmentCreateDT = DateUtility.GetDateFromLongNumeric(header.Element("SegmentCreateDT").Value.Trim());
      this.SourceFileFullPath = header.Element("SourceFileFullPath").Value.Trim();
      this.OriginalFullFileLength = Int32.Parse(header.Element("OriginalFullFileLength").Value.Trim());
      this.FullBase64Length = Int32.Parse(header.Element("FullBase64Length").Value.Trim());
      this.SegmentNumber = Int32.Parse(header.Element("SegmentNumber").Value.Trim());
      this.NumberOfSegments = Int32.Parse(header.Element("NumberOfSegments").Value.Trim());
      this.SegmentLength = Int32.Parse(header.Element("SegmentLength").Value.Trim());
    }

    private void Initialize()
    {
      OperationBeginDT = DateTime.MinValue;
      OperationEndDT = DateTime.MinValue;
      SegmentCreateDT = DateTime.MinValue;
      SourceFileFullPath = String.Empty;
      OriginalFullFileLength = 0;
      FullBase64Length = 0;
      NumberOfSegments = 0;
      SegmentNumber = 0;
      SegmentLength = 0;
    }

    public string GetHeaderText()
    {
      XElement header = new XElement("SegmentHeader");
      header.Add(new XElement("OperationBeginDT", this.OperationBeginDT.ToString("yyyyMMddHHmmssfff")));
      header.Add(new XElement("OperationEndDT", this.OperationEndDT.ToString("yyyyMMddHHmmssfff")));
      header.Add(new XElement("SegmentCreateDT", this.SegmentCreateDT.ToString("yyyyMMddHHmmssfff")));
      header.Add(new XElement("SourceFileFullPath", this.SourceFileFullPath.Trim()));
      header.Add(new XElement("OriginalFullFileLength", this.OriginalFullFileLength.ToString("00000000")));
      header.Add(new XElement("FullBase64Length", this.FullBase64Length.ToString("00000000")));
      header.Add(new XElement("SegmentNumber", this.SegmentNumber.ToString("0000")));
      header.Add(new XElement("NumberOfSegments", this.NumberOfSegments.ToString("0000")));
      header.Add(new XElement("SegmentLength", this.SegmentLength.ToString("00000000")));

      string headerXml = header.ToString(SaveOptions.DisableFormatting);
      int headerXmlLength = headerXml.Length;
      int totalHeaderLength = headerXmlLength + 68;
      string headerLeader = "[SEGMENT_HEADER_BEGIN: SEG=" + this.SegmentNumber.ToString("0000") + "-" + this.NumberOfSegments.ToString("0000") + " LTH=" + totalHeaderLength.ToString("000000") + "]";
      string headerText = headerLeader + headerXml + "[SEGMENT_HEADER_END]";

      return headerText;
    }
  }
}
