using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Org.GS.Security;
using Org.GS;

namespace Org.AuthTokenViewer
{
  public partial class MainWindow : Window
  {
    private a a;
    private bool _suppressTextChanagedEvent;

    public MainWindow()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

        _suppressTextChanagedEvent = true;

        txtAuthToken.Text = String.Empty;
        ClearResults();

        _suppressTextChanagedEvent = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "Authorization Token Viewer - Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
    }

    private void Action(object sender, RoutedEventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "GenerateToken":
          GenerateToken();
          break;

        case "Close":
          this.Close();
          break;
      }
    }

    private void GenerateToken()
    {
      var securityToken = new SecurityToken();
      securityToken.AccountId = 12345;
      securityToken.AuthenticationDateTime = DateTime.Now;
      securityToken.TokenExpirationDateTime = DateTime.Now.AddMinutes(30);
      string securityTokenString = securityToken.SerializeToken();
      txtAuthToken.Text = securityTokenString;
    }

    private void txtAuthToken_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (_suppressTextChanagedEvent)
        return;

      string token = txtAuthToken.Text;
      if (token.Length > 2)
      {
        if (token.StartsWith("\"") && token.EndsWith("\""))
          token = token.Substring(1, token.Length - 2);
      }

      ProcessAuthToken(token);
    }

    private void ProcessAuthToken(string authTokenString)
    {
      try
      {
        SecurityToken token = new SecurityToken();
        token.DeserializeToken(authTokenString);

        if (token.IsValid)
        {
          lblAccountIdValue.Content = token.AccountId.ToString();
          lblAuthDateTimeValue.Content = token.AuthenticationDateTime.ToString("MM-dd-yyyy HH:mm:ss.fff");
          lblExpireDateTimeValue.Content = token.TokenExpirationDateTime.ToString("MM-dd-yyyy HH:mm:ss.fff");
          lblSessionIdValue.Content = token.SessionId;
          lblIsExpiredValue.Content = token.IsExpired.ToString();
        }
        else
        {
          ClearResults();
          lblAccountIdValue.Content = "Invalid";
        }
      }
      catch (Exception ex)
      {
        ClearResults();
        lblAccountIdValue.Content = "Deserialization Error";
      }
    }

    private void ClearResults()
    {
      lblAccountIdValue.Content = String.Empty;
      lblAuthDateTimeValue.Content = String.Empty;
      lblExpireDateTimeValue.Content = String.Empty;
      lblSessionIdValue.Content = String.Empty;
      lblIsExpiredValue.Content = String.Empty;
    }
  }
}
