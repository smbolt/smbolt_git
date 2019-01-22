using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Org.ApiHost;
using Org.GS;

namespace ApiHost
{
    public partial class frmMain : Form
    {
        private WebHost _webHost;
        private string _url;

        public frmMain()
        {
            InitializeComponent();
            InitializeForm();
        }


        private void Action(object sender, EventArgs e)
        {
            switch (sender.ActionTag())
            {
                case "StartApiHost":
                    StartApiHost();
                    break;

                case "StopApiHost":
                    StopApiHost();
                    break;

                case "ClearDisplay":
                    ClearDisplay();
                    break;

                case "Go":
                    Go();
                    break;

                case "ClearBrowser":
                    ClearBrowser();
                    break;

                case "Exit":
                    this.Close();
                    break;
            }
        }

        private void StartApiHost()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                _webHost = new WebHost(_url);
                _webHost.Start();

                WriteToDisplay("API Host listening at " + _url, true);

                btnStartApiHost.Enabled = false;
                btnStopApiHost.Enabled = true;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                txtDisplay.Text = "An exception occurred while attempting start the API Host." + g.crlf + ex.ToReport();
                return;
            }
        }

        private void StopApiHost()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                _webHost.Stop();

                WriteToDisplay("API Host is stopped.", true);

                btnStartApiHost.Enabled = true;
                btnStopApiHost.Enabled = false;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                txtDisplay.Text = "An exception occurred while attempting stop the API Host." + g.crlf + ex.ToReport();
                return;
            }
        }

        private void Go()
        {
            tabMain.SelectedTab = tabPageBrowser;

            RunApiRequest();

            //browser.Navigate("http://localhost:5002/api/account");
        }


        private async Task<string> RunApiRequest()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string apiRequest = cboApiRequest.Text;

                HttpResponseMessage responseMessage;
                using (HttpClient httpClient = new HttpClient())
                {
                    //if (_tokenResponse != null)
                    //    httpClient.SetBearerToken(_tokenResponse.AccessToken);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiRequest);
                    responseMessage = await httpClient.SendAsync(request);
                }

                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        browser.DocumentText = await responseMessage.Content.ReadAsStringAsync();
                        break;

                    case System.Net.HttpStatusCode.NotFound:
                        browser.DocumentText = "NotFound";
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        browser.DocumentText = responseMessage.ReasonPhrase;
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



        private void ClearBrowser()
        {
            browser.DocumentText = String.Empty;
        }


        private void InitializeForm()
        {
            try
            {
                new a();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred while attempting to initialize the application object 'a'." + g.crlf + ex.ToReport(),
                                "API Host - Application Object (a) Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.SetInitialSizeAndLocation();

                tabMain.SelectedTab = tabPageDisplay;

                _url = g.CI("WebHostURL");

                cboApiRequest.LoadItems(g.GetList("ApiRequests"));

                btnStopApiHost.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred application initialization'." + g.crlf + ex.ToReport(),
                                "API Host - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteToDisplay(string message, bool append = false)
        {
            if (append)
                txtDisplay.Text += message + g.crlf;
            else
                txtDisplay.Text = message;

            txtDisplay.SelectionStart = 0;
            txtDisplay.SelectionLength = 0;
        }

        private void ClearDisplay()
        {
            txtDisplay.Text = String.Empty;
        }
    }
}
