using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace Org.WSO
{
  public class SimpleServiceAdapter
  {
    public string SendMessage(string message, string endpoint, int sendTimeoutSeconds = 0)
    {
      BasicHttpBinding httpBinding = new BasicHttpBinding();

      httpBinding.MaxReceivedMessageSize = 16777216;
      httpBinding.ReaderQuotas.MaxStringContentLength = 16777216;
      httpBinding.ReaderQuotas.MaxDepth = 64;
      httpBinding.ReaderQuotas.MaxBytesPerRead = 16348;
      httpBinding.ReaderQuotas.MaxArrayLength = 65536;

      if (sendTimeoutSeconds > 0)
      {
        int hours = 0;
        int minutes = sendTimeoutSeconds / 60;
        int seconds = sendTimeoutSeconds % 60;
        httpBinding.SendTimeout = new System.TimeSpan(hours, minutes, seconds); 
      }

      EndpointAddress endpointAddress = new EndpointAddress(endpoint);

      ISimpleService service = new ChannelFactory<ISimpleService>(httpBinding, endpointAddress).CreateChannel();
      string response = service.SendMessage(message);
      ((IChannel)service).Close();
      return response;
    }

    public void SendMessageOneWay(string message, string endpoint)
    {            
      BasicHttpBinding httpBinding = new BasicHttpBinding();

      httpBinding.MaxReceivedMessageSize = 16777216;
      httpBinding.ReaderQuotas.MaxStringContentLength = 16777216;
      httpBinding.ReaderQuotas.MaxDepth = 64;
      httpBinding.ReaderQuotas.MaxBytesPerRead = 16348;
      httpBinding.ReaderQuotas.MaxArrayLength = 65536;

      EndpointAddress endpointAddress = new EndpointAddress(endpoint);           

      ISimpleServiceOneWay logging = new ChannelFactory<ISimpleServiceOneWay>(httpBinding, endpointAddress).CreateChannel();
      logging.SendMessageOneWay(message);
      ((IChannel)logging).Close();
    }

    public string SendMessage_TCP(string message, string endpoint)
    {
      //Note:  You may have to modify these on both the sender and receiver
      XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
      quotas.MaxStringContentLength = 2147483647;
      quotas.MaxArrayLength = 2147483647;
      quotas.MaxBytesPerRead = 16348;
      quotas.MaxDepth = 64;

      NetTcpBinding tcpBinding = new NetTcpBinding();
      tcpBinding.ReaderQuotas = quotas;
      tcpBinding.MaxReceivedMessageSize = 2147483647;
      tcpBinding.MaxBufferSize = 2147483647;         

      EndpointAddress endpointAddress = new EndpointAddress(endpoint);            
            
      ISimpleService logging = new ChannelFactory<ISimpleService>(tcpBinding, endpointAddress).CreateChannel();
      string response = logging.SendMessage(message);
      ((IChannel)logging).Close();
      return response;
    }
  }
}
