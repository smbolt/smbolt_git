using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;
using Org.GS;


namespace Org.DocGen.DocSpec
{
    public class DocPackage
    {
        public bool IsAdsdi { get; set; }
        public Doc DocIn { get; set; }
        public Doc DocOut { get; set; }
        public ContentItemSet ContentItemSet { get; set; }
        public DocControl DocControl { get; set; }
        public Dictionary<string, DocPart> DocPartsIn { get; set; }
        public Dictionary<string, DocPart> DocPartsOut { get; set; }

        public string DocxFileName
        {
            get { return Path.GetFileName(this.DocControl.DocInfo.FullDocxPath); }
        }

        public string DocxFullFilePath
        {
            get { return this.DocControl.DocInfo.FullDocxPath; }
        }

        public DocPackage()
        {
            this.IsAdsdi = true;
            this.DocControl = new DocControl();
            this.DocPartsIn = new Dictionary<string, DocPart>();
            this.DocPartsOut = new Dictionary<string, DocPart>();
            this.ContentItemSet = new ContentItemSet();
        }

        public void Load()
        {
            string packagesPath = this.DocControl.DocInfo.PackagePath;
            string packageName = this.DocControl.DocInfo.PackageName;

            if (!Directory.Exists(packagesPath))
                throw new Exception("The packages path supplied '" + packagesPath + "' does not exist.");

            string packageFolder = packagesPath + @"\" + packageName;

            if (!Directory.Exists(packageFolder))
                throw new Exception("The package folder '" + packageFolder + "' does not exist.");

            string relsFolder = packageFolder + @"\_rels";

            if (!Directory.Exists(packageFolder))
                throw new Exception("The '_rels' folder does not exist in package folder '" + packageFolder + "'.");

            string relsPath = relsFolder + @"\.rels";

            if (!File.Exists(relsPath))
                throw new Exception(@"The '.rels' file does not exist in package\_rels folder '" + relsPath + "'.");

            XElement relsElement = XElement.Parse(File.ReadAllText(relsPath));
            XNamespace relsNs = relsElement.Name.Namespace;

            IEnumerable<XElement> relElements = relsElement.Elements(relsNs + "Relationship");

            foreach (XElement relElement in relElements)
            {
                string type = relElement.GetAttributeValue("Type");
                string target = relElement.GetAttributeValue("Target");
                string id = relElement.GetAttributeValue("Id");

                int pos = type.LastIndexOf(@"/");
                if (pos != -1)
                {
                    string docPartType = type.Substring(pos + 1);
                    DocPart docPart = new DocPart(docPartType, id, target);
                    string partXmlFile = packageFolder + docPart.Target;

                    if (!File.Exists(partXmlFile))
                        throw new Exception(@"The document part '" + docPartType + "' file '" + partXmlFile  + "' cannot be found.");
                    docPart.DocPartXml = XElement.Parse(File.ReadAllText(partXmlFile));
                    this.DocPartsIn.Add(docPart.DocPartType, docPart);
                }
            }

            if (!this.DocPartsIn.ContainsKey("officeDocument"))
                throw new Exception(@"The document part 'officeDocument' cannot be found.");

            if (!this.DocPartsIn.ContainsKey("map"))
                throw new Exception(@"The document part 'map' cannot be found.");

            DocPart officeDocumentPart = this.DocPartsIn["officeDocument"];
            string documentPath = officeDocumentPart.DocPartPath;
            string documentFileName = officeDocumentPart.DocPartFileName;

            List<string> fullFilePaths = Directory.GetFiles(packageFolder + documentPath).ToList();
            foreach (string fullFilePath in fullFilePaths)
            {
                string fileName = Path.GetFileName(fullFilePath);
                if (fileName != documentFileName)
                {
                    string docPartType = Path.GetFileNameWithoutExtension(fileName);
                    string id = docPartType;
                    string target = documentPath + @"/" + fileName;
                    DocPart docPart = new DocPart(docPartType, id, target);
                    docPart.DocPartXml = XElement.Parse(File.ReadAllText(fullFilePath));
                    this.DocPartsIn.Add(docPart.DocPartType, docPart);
                }
            }
        }

        public void AddGenerated(XElement xml)
        {
            string docPartType = xml.Name.LocalName;
            string id = docPartType; 
            DocPart docPart = new DocPart(docPartType, id, String.Empty);

            docPart.DocPartXml = xml;
            this.DocPartsOut.Add(docPart.DocPartType, docPart);
        }

        public void RefreshContent()
        {
            foreach (ContentItem ci in this.ContentItemSet.Values)
            {
                if (this.DocOut.DeSet.ContainsKey(ci.DeName))
                {
                    this.DocOut.DeSet[ci.DeName].ContentQuery = ci.ContentQuery;
                    this.DocOut.DeSet[ci.DeName].ContentValue = ci.ContentValue;
                }
            }
        }
    }
}
