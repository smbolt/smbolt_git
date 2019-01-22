using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Org.WSO
{
  [ServiceContract]
  public interface ISimpleService
  {
    [OperationContract]
    string SendMessage(string value);
  }

  [ServiceContract]
  public interface ISimpleServiceOneWay
  {
    [OperationContract(IsOneWay = true)]
    void SendMessageOneWay(string value);
  }
}
