using System;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using sms2.cdyne.com;

namespace Org.GS
{
  public class CdyneSmsAdapter
  {
    public void SendSms(string phoneNumber, string message, string licenseKey, string endpoint)
    {
      try
      {
        BasicHttpBinding httpBinding = new BasicHttpBinding();

        httpBinding.ReaderQuotas.MaxStringContentLength = 1048576;
        httpBinding.ReaderQuotas.MaxDepth = 64;
        httpBinding.ReaderQuotas.MaxBytesPerRead = 16348;
        httpBinding.ReaderQuotas.MaxArrayLength = 65536;

        //httpBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
        //httpBinding.SendTimeout = new TimeSpan(1, 0, 0);

        EndpointAddress endpointAddress = new EndpointAddress(endpoint);

        Isms smsClient = new ChannelFactory<Isms>(httpBinding, endpointAddress).CreateChannel();
        SMSResponse response = smsClient.SimpleSMSsend(phoneNumber, message, new Guid(licenseKey));
        ((IChannel)smsClient).Close();
      }
      catch (Exception ex)
      {
        string exMessage = ex.Message;
      }
    }
  }
}
