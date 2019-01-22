using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using OopFactory.X12.Parsing;
using OopFactory.X12.Hipaa.Claims.Services;
using Org.EDI.OopFactory;

namespace Org.EDI
{
    public class EdiHelper
    {
        public static string TransformEdiToFormattedEdi(string filename)
        {
            StringBuilder sb = new StringBuilder();
            var parser = new X12Parser(true);

            byte[] header = new byte[6];
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                // peak at first 6 characters to determine if this is a unicode file
                fs.Read(header, 0, 6);
                fs.Close();
            }
            Encoding encoding = (header[1] == 0 && header[3] == 0 && header[5] == 0) ? Encoding.Unicode : Encoding.UTF8;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    var interchanges = parser.ParseMultiple(fs, encoding);

                    foreach (var interchange in interchanges)
                    {
                        sb.Append(interchange.SerializeToX12(true)); 
                    }

                }
                catch (Exception ex)
                {
                    return "An exception occurred in method 'TransformEdiToFormattedEdi' - exception message is: " +
                        ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace; 
                }
            }

            string oopEdiString = sb.ToString();
            return oopEdiString;
        }

        public static string ParseRawEdiToEdiXml(string filename)
        {
            StringBuilder sb = new StringBuilder();
            var parser = new X12Parser(true);

            byte[] header = new byte[6];
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                // peak at first 6 characters to determine if this is a unicode file
                fs.Read(header, 0, 6);
                fs.Close();
            }
            Encoding encoding = (header[1] == 0 && header[3] == 0 && header[5] == 0) ? Encoding.Unicode : Encoding.UTF8;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    var interchanges = parser.ParseMultiple(fs, encoding);

                    foreach (var interchange in interchanges)
                    {
                        sb.Append(interchange.Serialize());
                    }

                }
                catch (Exception ex)
                {
                    return "An exception occurred in method 'TransformEdiXml' - exception message is: " +
                        ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace;
                }
            }

            string oopEdiString = sb.ToString();
            return oopEdiString;
        }

        public static string TransformEdiToOopBus(string filename)
        {
            StringBuilder sb = new StringBuilder();

            FileStream inputFilestream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            InstitutionalClaimToUB04ClaimFormTransformation institutionalClaimToUB04ClaimFormTransformation = new InstitutionalClaimToUB04ClaimFormTransformation("UB04_Red.gif");

            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"),
                institutionalClaimToUB04ClaimFormTransformation,
                new DentalClaimToJ400FormTransformation("ADAJ400_Red.gif"),
                new X12Parser(true));
            
            Dictionary<string, string> revenueDictionary = new Dictionary<string, string>();
            revenueDictionary["0572"] = "Test Code";
            service.FillRevenueCodeDescriptionMapping(revenueDictionary);
            var claimDoc = service.Transform837ToClaimDocument(inputFilestream);
            sb.Append(claimDoc.Serialize());

            string oopEdiString = sb.ToString();
            return oopEdiString;
        }

        public static string TransformBusXmlToEdiXml(string claimXmlPath, string toEdiXsltPath)
        {
            string claimXml = File.ReadAllText(claimXmlPath); 
            FileStream transformStream = new FileStream(toEdiXsltPath, FileMode.Open);

            var service = new ClaimXmlTransformationService();
            string ediXml = service.TransformClaimXmlToEdiXml(claimXml, transformStream);

            transformStream.Close();
            transformStream.Dispose();

            return ediXml;
        }

        public static string TransformEdiToOopEdi(string filename)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Not yet implemented.");

            string oopEdiString = sb.ToString();
            return oopEdiString;
        }

    }
}
