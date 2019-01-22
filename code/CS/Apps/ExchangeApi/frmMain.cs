using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;


namespace ExchangeApi
{
  public partial class frmMain : Form
  {
    private string crlf = Environment.NewLine;
    private string crlf2 = Environment.NewLine + Environment.NewLine;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void btnGetEmail_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {

        var service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
        service.Credentials = new WebCredentials("sbolt@adsdi.onmicrosoft.com", "gen126Office365");
        service.UseDefaultCredentials = true;
        service.AutodiscoverUrl("sbolt@adsdi.onmicrosoft.com", RedirectionCallback);

        EmailMessage emailMessage = new EmailMessage(service);
        emailMessage.ToRecipients.Add("sbolt@gulfportenergy.com");
        emailMessage.Subject = "Test from Office365 Exchange";
        emailMessage.Body = "Test Body";
        emailMessage.Send();

      }
      catch (Exception ex)
      {
        txtOut.Text = "Exception occurred" + crlf2 +
                      "Message:" + crlf + ex.Message + crlf2 +
                      "Stack Trace: " + crlf + ex.StackTrace;

        this.Cursor = Cursors.Default;
        return;
      }


      this.Cursor = Cursors.Default;
    }

    static bool RedirectionCallback(string url)
    {
      // Return true if the URL is an HTTPS URL.
      return url.ToLower().StartsWith("https://");
    }

    private static bool RedirectionUrlValidationCallback(string redirectionUrl)
    {
      // The default for the validation callback is to reject the URL.
      bool result = false;

      Uri redirectionUri = new Uri(redirectionUrl);

      // Validate the contents of the redirection URL. In this simple validation
      // callback, the redirection URL is considered valid if it is using HTTPS
      // to encrypt the authentication credentials. 
      if (redirectionUri.Scheme == "https")
      {
        result = true;
      }
      return result;
    }



    private void InitializeForm()
    {

    }
  }
}
