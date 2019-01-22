using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdentityModel.Client;
using ApiClient.Properties;
using Org.ApiClient;
using Org.GS;

namespace ApiClient
{
    public partial class frmMain : Form
    {
        private ApiClientState _apiClientState;

        private Dictionary<string, string> _grantTypes;
        private Dictionary<string, string> _clientSecrets;
        private Dictionary<string, string> _resourceOwnerPasswordUserPasswords;

        private string _selectedGrantType;

        private DiscoveryResponse _discoveryResponse;
        private TokenResponse _tokenResponse;

        private bool _initializationComplete;

        public frmMain()
        {
            InitializeComponent();
            InitializeForm();
        }


        private void Action(object sender, EventArgs e)
        {
            switch (sender.ActionTag())
            {
                case "Discover":
                    Discover();
                    break;

                case "GetToken":
                    GetToken(_discoveryResponse);
                    break;

                case "Login":
                    Login(@"http://localhost:59418/api/account/login");
                    break;

                case "RunApiRequest":
                    txtOut.Text = String.Empty;
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                    RunApiRequest();
                    break;

                case "Exit":
                    this.Close();
                    break;
            }
        }

        #region IdentityServer Interactions

        private async Task<DiscoveryResponse> Discover()
        {
            var url = GetOAuthServerURL();

            Application.DoEvents();

            using (var identityClient = new ApiIdentityClient())
            {
                _discoveryResponse = await identityClient.DiscoverEndpoints(url);
                DisplayDiscovery();
                return _discoveryResponse;
            }
        }

        private void DisplayDiscovery()
        {
            try
            {
                if (_discoveryResponse == null)
                {
                    txtOut.Text = "The DiscoveryResponse object is null.";
                    return;
                }

                if (_discoveryResponse.IsError)
                {
                    txtOut.Text = "The DiscoveryResponse object contains an error." + g.crlf2 +
                                  "Error Type : " + _discoveryResponse.ErrorType.ToString() + g.crlf +
                                  "Error      : " + _discoveryResponse.Error + g.crlf +
                                  "Exception  : " + _discoveryResponse.Exception?.ToReport();
                    return;
                }

                txtOut.Text = _discoveryResponse.Json.ToString();
            }
            catch (Exception ex)
            {
                txtOut.Text = "An exception occurred while attempting to report on the DiscoveryResponse results." + g.crlf2 + ex.ToReport();
            }
        }

        private async Task<TokenResponse> GetToken(DiscoveryResponse discoveryResponse)
        {
            string client = cboClient.Text;
            string secret = cboClientSecret.Text;

            string user = cboUser.Text;
            string password = cboPassword.Text;

            string apiResource = cboApiResource.Text;

            if (apiResource.IsBlank())
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select or enter an API resource.",
                                "API Client - API Resource is Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboApiResource.Select();
                return null;
            }

            if (client.IsBlank())
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select or enter a client.",
                                "API Client - Client is Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboClient.Select();
                return null;
            }

            if (_selectedGrantType == "ResourceOwnerPassword")
            {
                if (user.IsBlank())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select or enter a user.",
                                    "API Client - User is Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboUser.Select();
                    return null;
                }

                if (password.IsBlank())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select or enter a password.",
                                    "API Client - Password is Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboPassword.Select();
                    return null;
                }

            }


            txtOut.Text = "Requesting a token...";
            Application.DoEvents();

            using (var tokenClient = new ApiTokenClient(discoveryResponse))
            {
                _tokenResponse = await tokenClient.RequestToken(_selectedGrantType, discoveryResponse.TokenEndpoint, apiResource, client, secret, user, password);
                DisplayTokenResponse();
                return _tokenResponse;
            }
        }

        private void DisplayTokenResponse()
        {
            try
            {

                if (_tokenResponse == null)
                {
                    txtOut.Text = "The TokenResponse object is null.";
                    return;
                }


                if (_tokenResponse.IsError)
                {
                    txtOut.Text = "The TokenResponse object contains an error." + g.crlf2 +
                                  "Error Type : " + _tokenResponse.ErrorType.ToString() + g.crlf +
                                  "Error      : " + _tokenResponse.Error + g.crlf +
                                  "Exception  : " + _tokenResponse.Exception?.ToReport();
                    return;
                }

                txtOut.Text = _tokenResponse.Json.ToString();
            }
            catch (Exception ex)
            {
                txtOut.Text = "An exception occurred while attempting to report on the TokenResponse results." + g.crlf2 + ex.ToReport();
            }
        }

        private async Task<string> Login(string apiRequest)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiRequest);
                responseMessage = await httpClient.SendAsync(request);
            }

            string response = await responseMessage.Content.ReadAsStringAsync();

            txtOut.Text = response;

            return response;
        }

        private string GetOAuthServerURL()
        {
            if (cboApiResource.Text == "HTTPS")
                return g.CI("OAuthServerURLHttps");
            else
                return g.CI("OAuthServerURLHttp");
        }

        #endregion

        #region API Interactions
        private async Task<string> RunApiRequest(string apiRequest = null)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                apiRequest = cboApiRequest.Text;

                HttpResponseMessage responseMessage;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (_tokenResponse != null)
                        httpClient.SetBearerToken(_tokenResponse.AccessToken);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiRequest);
                    responseMessage = await httpClient.SendAsync(request);
                }

                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        txtOut.Text = await responseMessage.Content.ReadAsStringAsync();
                        break;

                    case System.Net.HttpStatusCode.NotFound:
                        txtOut.Text = "NotFound";
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        txtOut.Text = responseMessage.ReasonPhrase;
                        break;
                }


                this.Cursor = Cursors.Default;

                return String.Empty;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An exception occurred while attempting to hit the API." + g.crlf + ex.ToReport(),
                                "API Client - API Hit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return String.Empty;
            }
        }

        #endregion

        #region Application Initialization

        private void InitializeForm()
        {
            try
            {
                new a();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred while attempting to initialize the application object 'a'." + g.crlf + ex.ToReport(), 
                                "API Client - Application Object (a) Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _initializationComplete = false;

                _apiClientState = new ApiClientState();

                this.SetInitialSizeAndLocation();

                //splitMain.Panel1Collapsed = true;
                btnRunApiRequest.Enabled = false;

                _grantTypes = g.GetDictionary("GrantTypes");
                cboGrantType.LoadItems(_grantTypes.Keys.ToList());
                if (cboGrantType.Items.Count == 1)
                    cboGrantType.SelectedIndex = 0;

                cboApiResource.LoadItems(g.GetList("ApiResources"));
                cboApiResource.SelectedIndex = 0;

                cboUser.Text = String.Empty;
                cboUser.Enabled = false;
                cboPassword.Text = String.Empty;
                cboPassword.Enabled = false;

                cboApiRequest.LoadItems(g.GetList("ApiRequests"));

                _resourceOwnerPasswordUserPasswords = g.GetDictionary("ResourceOwnerPasswordUserPasswords");

                _initializationComplete = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred application initialization'." + g.crlf + ex.ToReport(),
                                "API Client - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeImages()
        {
            tvImageList.Images.Add("configure", Resources.configure);
            tvImageList.Images.Add("discover", Resources.discover);
            tvImageList.Images.Add("auth_token", Resources.auth_token);
            tvImageList.Images.Add("id_token", Resources.id_token);
            tvImageList.Images.Add("interaction", Resources.interaction);
            tvImageList.Images.Add("success", Resources.success);
            tvImageList.Images.Add("doc", Resources.doc);
            tvImageList.Images.Add("failed", Resources.failed);
            tvImageList.Images.Add("not_secure", Resources.not_secure);
            tvImageList.Images.Add("error_doc", Resources.error_doc);
            tvImageList.Images.Add("open", Resources.open);
            tvImageList.Images.Add("login", Resources.login);
        }


        #endregion

        #region Form Events

        private void cboApiRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRunApiRequest.Enabled = cboApiRequest.Text.IsNotBlank();
        }

        private void cboGrantType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initializationComplete)
                return;

            _selectedGrantType = cboGrantType.Text;

            string clientList = _selectedGrantType + "Clients";
            cboClient.LoadItems(g.GetList(clientList));
            cboClient.SelectedIndex = 0;

            if (_selectedGrantType == "ResourceOwnerPassword")
            {
                cboUser.Text = String.Empty;
                cboUser.Enabled = true;
                cboPassword.Text = String.Empty;
                cboPassword.Enabled = true;

                cboUser.LoadItems(_resourceOwnerPasswordUserPasswords.Keys.ToList());
                cboUser.SelectedIndex = 0;
            }
            else
            {
                cboUser.Text = String.Empty;
                cboUser.Enabled = false;
                cboPassword.Text = String.Empty;
                cboPassword.Enabled = false;
            }
        }


        private void cboClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            string client = cboClient.Text;

            if (client.IsBlank())
            {
                cboClientSecret.Text = String.Empty;
                return;
            }

            _clientSecrets = g.GetDictionary(_selectedGrantType + "ClientSecrets");

            cboClientSecret.LoadItems(_clientSecrets.Values.ToList());

            string selectedSecret = String.Empty;
            if (_clientSecrets.ContainsKey(client))
                selectedSecret = _clientSecrets[client];

            if (selectedSecret.IsNotBlank())
                cboClientSecret.SelectItem(selectedSecret);
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboUser.Enabled)
                return;

            string selectedUser = cboUser.Text;

            if (selectedUser.IsBlank())
            {
                cboPassword.Text = String.Empty;
            }
            else
            {
                cboPassword.LoadItems(_resourceOwnerPasswordUserPasswords.Values.ToList());

                if (_resourceOwnerPasswordUserPasswords.ContainsKey(selectedUser))
                {
                    cboPassword.SelectItem(_resourceOwnerPasswordUserPasswords[selectedUser]);
                }
            }
        }

        #endregion
    }



}
