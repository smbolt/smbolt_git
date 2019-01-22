using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  public class PdfUtility
  {
    public static DxWorkbook GetWorkbook(PdfExtractRequest request)
    {
      return GetWorkbook(request.FullPath, request.FileNamePrefix, String.Empty, request.MapName);
    }

    public static string WriteWorkbookToFile(PdfExtractRequest request)
    {
      try
      {
        string fullMapPath = request.FullMapPath;
        string mapFileName = Path.GetFileName(fullMapPath);
        string mapPath = Path.GetDirectoryName(fullMapPath);
        var dxWb = GetWorkbook(request.FullMapPath, request.FileNamePrefix, mapPath, mapFileName);

        dxWb.EnsureIntegrity();

        XElement wbXml = null;

        using (var f = new ObjectFactory2())
        {
          wbXml = f.Serialize(dxWb);
        }

        string processedFolder = Path.GetDirectoryName(request.FullPath);
        string regressionFolder = Path.GetFullPath(Path.Combine(processedFolder, @"..\MappedRegression\"));
        string fileName = Path.GetFileNameWithoutExtension(request.FullPath);
        DirectoryInfo di = Directory.CreateDirectory(regressionFolder);

        string wbXmlString = wbXml.ToString();
        string regressionFilePath = regressionFolder + request.FileNamePrefix + fileName + ".xml";

        File.WriteAllText(regressionFilePath, wbXmlString);

        return regressionFilePath;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a Compare MappedFile from the PDF file '" +
                            request.FullPath + "'.", ex);
      }
    }

    public static DxWorkbook GetWorkbook(string fullPath, string fileNamePrefix, string mapPath, string mapFileName, int exceptionLimit = 0)
    {
      try
      {
        if(!File.Exists(fullPath))
          throw new Exception("The pdf file does not exist at the specified path '" + fullPath + "'.");

        if (mapPath.IsBlank())
          mapPath = g.CI("MapPath");

        var extractSpecSet = GetExtractSpecSet(mapPath, mapFileName);
        var extractSpec = extractSpecSet.Values.First();

        Text text = null;

        using (var textExtractor = new TextExtractor())
        {
          switch (extractSpec.FileType)
          {
            case FileType.XML:
              text = textExtractor.ExtractTextFromXml(false, fullPath);
              break;

            case FileType.PDF:
              text = textExtractor.ExtractTextFromPdf(false, fullPath, extractSpec.ExtractionMap);
              break;
          }

          var wb = text.GetWorkbook(extractSpec, exceptionLimit);

          if (text.CxExceptionList.Count > exceptionLimit)
          {
            var sb = new StringBuilder();
            foreach (CxException cx in text.CxExceptionList)
            {
              sb.Append(cx.ToReport() + g.crlf2);
            }

            string report = sb.ToString();
            throw new Exception("One or more exceptions occurred during text extract processing." + g.crlf2 + report);
          }

          wb.MapPath = mapPath + @"\" + mapFileName;
          wb.IsMapped = true;

          return wb;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract statement data from the pdf file.", ex);

      }
    }

    private static ExtractSpecSet GetExtractSpecSet(string mapPath, string mapName)
    {
      try
      {
        var ext = Path.GetExtension(mapName).ToLower();

        if (ext != ".map")
          mapName += ".ext";

        string fullMapPath = mapPath + @"\" + mapName;

        return GetExtractSpecSet(fullMapPath);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the ColumnIndexMap from map path '" + mapPath + "' " +
                            "and map name '" + mapName + "'.", ex);
      }
    }

    private static ExtractSpecSet GetExtractSpecSet(string fullMapPath)
    {
      try
      {
        if (!File.Exists(fullMapPath))
          throw new Exception("The map file does not exist at '" + fullMapPath + "'.");

        string mapXmlString = File.ReadAllText(fullMapPath);

        if (!mapXmlString.IsValidXml())
          throw new Exception("The map file at '" + fullMapPath + "' does not contain valid XML.");

        var mapXml = XElement.Parse(mapXmlString);

        ExtractSpecSet extractSpecSet = null;

        using (var f = new ObjectFactory2())
        {
          extractSpecSet = f.Deserialize(mapXml) as ExtractSpecSet;

          string mapPath = Path.GetDirectoryName(fullMapPath);
          if (extractSpecSet.ColumnIndexMapName.IsBlank())
            extractSpecSet.ColumnIndexMapName = "ColumnIndexMap";

          string columnIndexMapFullPath = mapPath + @"\" + extractSpecSet.ColumnIndexMapName + ".xml";

          if (!File.Exists(columnIndexMapFullPath))
            throw new Exception("The ColumnIndexMap file does not exist at '" + columnIndexMapFullPath + "'.");

          string columnIndexMapString = File.ReadAllText(columnIndexMapFullPath);

          if (!columnIndexMapString.IsValidXml())
            throw new Exception("The column index map file at '" + fullMapPath + "' does not contain valid XML.");

          var columnIndexMapXml = XElement.Parse(columnIndexMapString);
          Tsd.ColumnIndexMap = f.Deserialize(columnIndexMapXml) as ColumnIndexMap;
        }

        if (extractSpecSet.Values.Count != 1)
          throw new Exception("There must be 1 ExtractSpecs in the ExtractSpecSet - found " + extractSpecSet.Values.Count.ToString() + ".");

        extractSpecSet.PopulateReferences();

        ExtractionMap extractionMap = null;
        var extractSpec = extractSpecSet.Values.First();
        if (extractSpec.ExtractionMap != null)
          extractionMap = extractSpec.ExtractionMap;

        return extractSpecSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the ExtractSpecSet (PDF map file) for map name '" + fullMapPath + "'.", ex);
      }
    }
  }
}
