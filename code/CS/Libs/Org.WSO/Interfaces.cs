using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public interface IRequestProcessorFactory : IDisposable
  {
    string Name { get; }
    IRequestProcessor CreateRequestProcessor(string nameAndVersion);
  }

  public interface IRequestProcessor : IDisposable
  {
    int EntityId { get; }
    XElement ProcessRequest();
    void SetBaseAndEngine(ServiceBase serviceBase, TransactionEngine transactionEngine);
  }

  public interface IMessageFactory : IDisposable
  {
    WsMessage CreateRequestMessage(WsParms wsParms);
  }
}
