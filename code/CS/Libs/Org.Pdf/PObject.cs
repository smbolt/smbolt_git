using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using Org.GS;

namespace Org.Pdf
{
  public class PObject
  {
    public string Name {
      get;
      protected set;
    }
    public int PObjectNumber {
      get;
      private set;
    }
    private bool _isPage;
    public string Path {
      get {
        return Get_Path();
      }
    }
    public int Level {
      get {
        return Get_Level();
      }
    }
    public bool IsPage {
      get {
        return _isPage;
      }
    }
    private Page _page;
    public Page Page {
      get {
        return Get_Page();
      }
    }
    public Document Document {
      get;
      private set;
    }
    public PdfObject iPdfObject {
      get;
      set;
    }
    public bool HasIndirectReference {
      get {
        return this.IndirectReference != null;
      }
    }
    public string IndirectReferenceDisplay {
      get {
        return this.IndirectReference != null ? "{" + this.IndirectReference.ToString() + "}" : String.Empty;
      }
    }
    public PdfIndirectReference IndirectReference {
      get;
      private set;
    }
    public PdfObject iPdfIndirectObject {
      get;
      private set;
    }
    public PObjectType PObjectType {
      get {
        return this.Get_PObjectType();
      }
    }
    public bool IsImage {
      get {
        return Get_IsImage();
      }
    }
    public bool ContainsAnImage {
      get {
        return Get_ContainsAnImage(this);
      }
    }
    public bool IsDecendentOfImage {
      get {
        return Get_IsDescendentOfImage(this);
      }
    }
    public double? NumberValue {
      get {
        return Get_NumberValue();
      }
    }
    public string StringValue {
      get {
        return Get_StringValue();
      }
    }
    public string NameValue {
      get {
        return Get_NameValue();
      }
    }
    public bool? BooleanValue {
      get {
        return Get_BooleanValue();
      }
    }
    public PObjectSet ChildObjects {
      get;
      private set;
    }
    public PObjectSet DictionaryValue {
      get {
        return Get_DictionaryValue();
      }
    }
    public PObjectSet ArrayValue {
      get {
        return Get_ArrayValue();
      }
    }
    public int Count {
      get {
        return Get_Count();
      }
    }
    public PObject Parent {
      get;
      private set;
    }
    public string DisplayText {
      get {
        return Get_DisplayText();
      }
    }
    public byte[] Bytes {
      get;
      protected set;
    }
    private PdfImage _pdfImage;
    public PdfImage PdfImage {
      get {
        return Get_PdfImage();
      }
    }

    public PObject(Document document, string name, PdfObject pdfObject, PObject parent = null, bool isPage = false)
    {
      try
      {
        this.Document = document;
        this.PObjectNumber = this.Document.NextObjectNumber;

        if (this.PObjectNumber == this.Document.BreakOnObject)
          System.Diagnostics.Debugger.Break();

        _isPage = isPage;
        this.Name = name;
        this.iPdfObject = pdfObject;
        this.IndirectReference = this.iPdfObject.GetIndirectReference();
        if (this.IndirectReference != null)
          this.iPdfIndirectObject = this.IndirectReference.GetRefersTo();
        this.Parent = parent;
        this.ChildObjects = new PObjectSet(this);

        if (!_isPage)
          Populate();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of PObject.", ex);
      }
    }

    private void Populate()
    {
      try
      {
        if (this.iPdfObject == null)
          return;

        switch (this.PObjectType)
        {
          case PObjectType.PdfArray:
            this.Document.WriteLog("ARRAY" + g.crlf);
            for (int i = 0; i < this.Count; i++)
            {
              var arrayEntryObject = ((PdfArray)this.iPdfObject).Get(i);
              string arrayEntryObjectName = this.Name + "[" + i.ToString() + "]";
              PObject arrayEntryPObject = new PObject(this.Document, arrayEntryObjectName, arrayEntryObject, this);

              if (this.ChildObjects.ContainsKey(arrayEntryObjectName))
                throw new Exception("The object named '" + this.Name + "' (PdfArray) already contains a child object named '" + arrayEntryObjectName +
                                    "'. Object path is " + this.Path + ".");

              this.ChildObjects.Add(arrayEntryObjectName, arrayEntryPObject);
            }
            break;

          case PObjectType.PdfDictionary:
          case PObjectType.PdfStream:
            this.Document.WriteLog("DICTIONARY" + g.crlf);
            var keySetSet = ((PdfDictionary)this.iPdfObject).KeySet();
            foreach (var key in keySetSet)
            {
              string keyName = key.ToString();
              if (keyName == "/Parent")
                continue;

              PdfObject pdfObject = ((PdfDictionary)this.iPdfObject).Get(key);

              if (keyName == "/P" && pdfObject.GetPObjectType() == PObjectType.PdfDictionary)
              {
                continue;
              }

              PObject dictEntryObject = new PObject(this.Document, keyName, pdfObject, this);

              if (this.ChildObjects.ContainsKey(keyName))
                throw new Exception("The object named '" + this.Name + "' (PdfDictionary) already contains a child object named '" + keyName +
                                    "'. Object path is " + this.Path + ".");

              this.ChildObjects.Add(keyName, dictEntryObject);
            }

            if (this.PObjectType == PObjectType.PdfStream)
            {
              this.Page.AddToPdfStreamList(this);
              if (this.IsImage)
              {
                this.CreatePdfImage();
                //PdfStream stream = (PdfStream)this.iPdfObject;
                //int length = stream.GetLength();
                //using (var ms = this.Document.PdfReader.ReadStream(stream, true))
                //{
                //  this.Bytes = new byte[length];
                //  ms.Read(this.Bytes, 0, length);
                //  ms.Close();
                //}
              }
            }

            break;

          case PObjectType.PdfBoolean:
          case PObjectType.PdfNumber:
          case PObjectType.PdfString:
          case PObjectType.PdfName:
          case PObjectType.PdfNull:
          case PObjectType.PdfLiteral:
          case PObjectType.Null:
          case PObjectType.Undefined:
          case PObjectType.NotSet:
            this.Page.IncrementObjectCount();
            break;


          default:
            break;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the Populate method of PObject.", ex);
      }
    }

    private void CreatePdfImage()
    {
      try
      {
        //var parent = this.Parent;
        //var image = new iText.Layout.Element.Image(

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a PdfImage object.", ex);
      }
    }

    public override string ToString()
    {
      return this.Name ?? "NULL-NAME" + " (" + this.PObjectType.ToString() + ")";
    }

    private PObjectType Get_PObjectType()
    {
      if (this.iPdfObject == null)
        return PObjectType.Null;
      return this.iPdfObject.GetPObjectType();
    }

    private double? Get_NumberValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsNumber())
        return null;

      return ((PdfNumber)this.iPdfObject).GetValue();
    }

    private string Get_StringValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsString())
        return null;

      return ((PdfString)this.iPdfObject).GetValue();
    }

    private string Get_NameValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsName())
        return null;

      return ((PdfName)this.iPdfObject).GetValue();
    }

    private bool? Get_BooleanValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsBoolean())
        return null;

      return ((PdfBoolean)this.iPdfObject).GetValue();
    }

    private PObjectSet Get_DictionaryValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsDictionary())
        return null;

      PObjectSet pObjectSet = new PObjectSet(this);
      pObjectSet.pObjectSetType = PObjectSetType.Dictionary;


      return pObjectSet;
    }

    private PObjectSet Get_ArrayValue()
    {
      if (this.iPdfObject == null)
        return null;

      if (!this.iPdfObject.IsArray())
        return null;

      PObjectSet pObjectSet = new PObjectSet(this);
      pObjectSet.pObjectSetType = PObjectSetType.Array;


      return pObjectSet;
    }

    private int Get_Count()
    {
      if (this.iPdfObject == null)
        return 0;

      if (this.iPdfObject.IsArray())
        return ((PdfArray)this.iPdfObject).Size();

      if (this.iPdfObject.IsDictionary())
        return ((PdfDictionary)this.iPdfObject).Size();

      if (this.iPdfObject.IsStream())
        return ((PdfStream)this.iPdfObject).Size();

      return 0;
    }

    private string Get_Path()
    {
      string path = this.Name;

      PObject parent = this.Parent;
      while (parent != null)
      {
        path = parent.Name + @"\" + path;
        parent = parent.Parent;
      }

      path = @"doc-root\" + path;

      return path;
    }

    private int Get_Level()
    {
      int level = 0;

      PObject parent = this.Parent;
      while (parent != null)
      {
        level++;
        parent = parent.Parent;
      }

      return level;
    }

    private Page Get_Page()
    {
      if (_page != null)
        return _page;

      if (this.IsPage)
      {
        _page = (Page)this;
        return _page;
      }

      if (this.Parent == null)
        throw new Exception("An object which is not a page does not have a parent object. All objects which are not 'Pages' must have a parent object.");

      PObject parent = this.Parent;
      if (parent.IsPage)
      {
        _page = (Page)parent;
        return _page;
      }

      while (!parent.IsPage)
      {
        parent = parent.Parent;

        if (parent == null)
          throw new Exception("Cannot locate Page for object '" + this.Name + "'. Object type is '" + this.PObjectType.ToString() + "'.");

        if (parent.IsPage)
        {
          _page = (Page)parent;
          return _page;
        }
      }

      throw new Exception("Cannot locate Page for object '" + this.Name + "'. Object type is '" + this.PObjectType.ToString() + "'.");

    }

    private bool Get_IsImage()
    {
      if (this.PObjectType != PObjectType.PdfStream)
        return false;

      if (this.ChildObjects.Count == 0)
        return false;

      foreach (var childObject in this.ChildObjects.Values)
      {
        if (childObject.Name == "/Subtype" && childObject.NameValue == "Image")
          return true;
      }

      return false;
    }

    private bool Get_ContainsAnImage(PObject o)
    {
      if (o.IsImage)
        return true;

      foreach (var childObject in o.ChildObjects.Values)
      {
        bool containsImage = Get_ContainsAnImage(childObject);
        if (containsImage)
          return true;
      }

      return false;
    }

    private bool Get_IsDescendentOfImage(PObject o)
    {
      if (o.IsImage)
        return true;

      if (o.Parent == null)
        return false;

      PObject parent = o.Parent;
      while (parent != null)
      {
        if (parent.IsImage)
          return true;
        parent = parent.Parent;
      }

      return false;
    }

    public void GetReport(StringBuilder sb)
    {
      sb.Append(g.BlankString(2 + this.Level * 2) + this.Name + " (" + this.PObjectType.ToString() + ")" + g.crlf);
      foreach (var childObject in this.ChildObjects.Values)
      {
        childObject.GetReport(sb);
      }
    }

    private string Get_DisplayText()
    {
      StringBuilder sb = new StringBuilder();


      if (this.IsPage)
      {
        Page page = (Page) this;
        string pageText = page.PageRawText;
        if (pageText.IsBlank())
          pageText = "[No text on page]";

        sb.Append("Page Name   : " + page.Name + g.crlf +
                  "Page Number : " + page.PageNumber.ToString() + g.crlf2 +
                  "Page Text   : " + g.crlf + pageText);

        return sb.ToString();
      }

      switch (this.PObjectType)
      {
        case PObjectType.PdfBoolean:
          PdfBoolean pdfBoolean = (PdfBoolean)this.iPdfObject;
          sb.Append("PdfBoolean" + g.crlf2 + this.BooleanValue.Value.ToString() + g.crlf);
          break;

        case PObjectType.PdfNumber:
          PdfNumber pdfNumber = (PdfNumber)this.iPdfObject;
          sb.Append("PdfNumber" + g.crlf2 + this.NumberValue.Value.ToString() + g.crlf);
          break;

        case PObjectType.PdfString:
          PdfString pdfString = (PdfString)this.iPdfObject;
          sb.Append("PdfString" + g.crlf2 + this.StringValue + g.crlf);
          break;

        case PObjectType.PdfName:
          PdfName pdfName = (PdfName)this.iPdfObject;
          string nameValue = pdfName.GetValue();
          sb.Append("PdfName" + g.crlf2 + nameValue);
          break;

        case PObjectType.PdfArray:
          PdfArray pdfArray = (PdfArray)this.iPdfObject;
          sb.Append("PdfArray         Name : " + this.Name + g.crlf2);
          sb.Append("Entries          Count: " + this.Count.ToString() + g.crlf2);

          for (int i = 0; i < this.Count; i++)
          {
            var arrayEntryObject = ((PdfArray)this.iPdfObject).Get(i);
            string arrayEntryObjectName = this.Name + "[" + i.ToString() + "]";
            string arrayEntryObjectDisplay = arrayEntryObject.GetPObjectType().ToString();
            sb.Append(arrayEntryObjectName.PadToLength(30) + "    " + arrayEntryObjectDisplay + g.crlf);
          }
          break;

        case PObjectType.PdfDictionary:
          PdfDictionary pdfDictionary = (PdfDictionary)this.iPdfObject;
          sb.Append("PdfDictionary    Name : " + this.Name + g.crlf2);
          sb.Append("Entries          Count: " + this.Count.ToString() + g.crlf2);

          var keySetSet = pdfDictionary.KeySet();
          foreach (var key in keySetSet)
          {
            string keyName = key.ToString();
            if (keyName == "/Parent")
              continue;

            PdfObject pdfObject = ((PdfDictionary)this.iPdfObject).Get(key);

            if (keyName == "/P" && pdfObject.GetPObjectType() == PObjectType.PdfDictionary)
              continue;

            string dictEntryObjectDisplay = pdfObject.GetPObjectType().ToString();
            sb.Append(keyName.PadToLength(30) + "    " + dictEntryObjectDisplay + g.crlf);
          }
          break;

        case PObjectType.PdfStream:
          if (this.IsImage)
          {
            if (this.Bytes != null && this.Bytes.Length > 0)
              sb.Append(this.Bytes.ToHexDump() + g.crlf);
            else
              sb.Append((this.Bytes == null ? "Byte array is null" : "Byte array has length of zero"));
          }
          else
          {
            sb.Append("Stream is not an image");
          }
          break;

        case PObjectType.PdfNull:
          sb.Append("The object is of type PdfNull.");
          break;

        case PObjectType.PdfLiteral:
          var pdfLiteral = (PdfLiteral)this.iPdfObject;
          break;

        case PObjectType.Null:
          sb.Append("The object is null (type PObjectType.Null");
          break;

        case PObjectType.Undefined:
          sb.Append("The object is undefined (type PObjectType.Undefined");
          break;

        case PObjectType.NotSet:
          sb.Append("The object is of PObjectType.NotSet");
          break;
      }

      string displayText = sb.ToString();

      return displayText;
    }

    private PdfImage Get_PdfImage()
    {
      try
      {
        if (_pdfImage != null)
          return _pdfImage;

        //string imageName = this.ChildObjects?.Where(o => o.Key == "/Name").FirstOrDefault().Value?.NameValue;
        //int height = Convert.ToInt32(this.ChildObjects?.Where(o => o.Key == "/Height").FirstOrDefault().Value?.NumberValue);
        //int width = Convert.ToInt32(this.ChildObjects?.Where(o => o.Key == "/Width").FirstOrDefault().Value?.NumberValue);
        //int length = Convert.ToInt32(this.ChildObjects?.Where(o => o.Key == "/Length").FirstOrDefault().Value?.NumberValue);
        //int bitsPerComponent = Convert.ToInt32(this.ChildObjects?.Where(o => o.Key == "/BitsPerComponent").FirstOrDefault().Value?.NumberValue);
        //var pdfColorSpace = g.ToEnum<PdfColorSpace>(this.ChildObjects?.Where(o => o.Key == "/ColorSpace").FirstOrDefault().Value?.NameValue, PdfColorSpace.NotSet);
        //var pdfImageFilter = g.ToEnum<PdfImageFilter>(this.ChildObjects?.Where(o => o.Key == "/Filter").FirstOrDefault().Value?.NameValue, PdfImageFilter.NotSet);

        //PObject decodeParms = this.ChildObjects?.Where(o => o.Key == "/DecodeParms").FirstOrDefault().Value as PObject;
        //int decodeParmsColumns = Convert.ToInt32(decodeParms?.ChildObjects?.Where(o => o.Key == "/Columns").FirstOrDefault().Value?.NumberValue);
        //int decodeParmsRows = Convert.ToInt32(decodeParms?.ChildObjects?.Where(o => o.Key == "/Rows").FirstOrDefault().Value?.NumberValue);
        //int decodeParmsK = Convert.ToInt32(decodeParms?.ChildObjects?.Where(o => o.Key == "/K").FirstOrDefault().Value?.NumberValue);
        //var pdfDecodeParms = new PdfImageDecodeParms(decodeParmsColumns, decodeParmsRows, decodeParmsK);

        //_pdfImage = new PdfImage(this.Bytes, imageName, height, width, length, bitsPerComponent, pdfColorSpace, pdfImageFilter, pdfDecodeParms);




        return _pdfImage;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a PdfImage object.", ex);
      }
    }
  }
}
