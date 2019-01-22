using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class SendFilesProcessor : RequestProcessorBase, IRequestProcessor
  {
    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());

      ObjectFactory2 f = new ObjectFactory2();
      SendFilesRequest sendFilesRequest = f.Deserialize(base.TransactionEngine.TransactionBody) as SendFilesRequest;

      int filesStored = 0;
      string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

      string fileStore = g.AppDataPath + @"\Imports";
      foreach (FileObject fo in sendFilesRequest.FileObjects)
      {
        string fileName = Path.GetFileName(fo.FileName);
        string fileData = Encoding.UTF8.GetString(Convert.FromBase64String(fo.Data));
        //File.WriteAllText(fileStore + @"\" + timestamp + "-" + fileName, fileData);
        File.WriteAllText(fileStore + @"\" + fileName, fileData);
        filesStored++;
      }

      SendFilesResponse sendFilesResponse = new SendFilesResponse();
      sendFilesResponse.TransactionStatus = TransactionStatus.Success;
      sendFilesResponse.Message = "Total files stored = " + filesStored.ToString() + ".";
      //base.WriteSuccessLog("0000", "000");
      XElement response = f.Serialize(sendFilesResponse);
      return response;
    }
  }
}
