using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Ifx.OdbcData;
using Org.GS.Logging;
using Org.GS;



namespace Org.IfxExtract
{
	public partial class frmMain : Form
	{
		public a a;
		private string _mainFormTitle = "Informix Data Extract";
		private string _connectionString;
		private StoresRepository _repo;

		public frmMain()
		{
			InitializeComponent();
			InitializeForm();
		}


		private void Action(object sender, EventArgs e)
		{
			string action = g.GetActionFromEvent(sender);

			switch (action)
			{
				case "WriteLog":
					WriteLog();
					break;

				case "AddModule":
					AddModule();
					break;

				case "GetIfxData":
					this.GetIfxData();
					break;

				case "Exit":
					this.Close();
					break;
			}
		}

		private void GetIfxData()
		{
			txtOut.Text = String.Empty;
			Application.DoEvents();
			System.Threading.Thread.Sleep(50); 

			try
			{
				if (_repo == null)
					_repo = new StoresRepository(_connectionString);

				txtOut.Text = _repo.GetIfxData();

			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred attempting to retrieve data from the Informix database." + g.crlf2 + ex.ToReport(),
												_mainFormTitle + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void AddModule()
		{
			txtOut.Text = String.Empty;
			Application.DoEvents();
			System.Threading.Thread.Sleep(50); 

			try
			{
				if (_repo == null)
					_repo = new StoresRepository(_connectionString);

				bool success = _repo.AddModule("Test Module"); 

			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred attempting to retrieve data from the Informix database." + g.crlf2 + ex.ToReport(),
												_mainFormTitle + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void WriteLog()
		{
			Application.DoEvents();
			System.Threading.Thread.Sleep(50); 

			try
			{
				if (_repo == null)
					_repo = new StoresRepository(_connectionString);

				var logRecord = new LogRecord();
				logRecord.LogDateTime = DateTime.Now;
				logRecord.SeverityCode = LogSeverity.INFO;
				logRecord.Message = "This is a test log record written at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
				logRecord.ModuleId = 1;
				logRecord.EventCode = 2;
				logRecord.EntityId = 3;
				logRecord.UserName = g.SystemInfo.DomainAndUser;
				logRecord.ClientHost = null;
				logRecord.ClientIp = null;
				logRecord.ClientUser = null;
				logRecord.ClientApplication = null;
				logRecord.ClientApplicationVersion = null;
				logRecord.TransactionName = null;
				logRecord.NotificationSent = false;

				bool logSuccess = _repo.WriteLog(logRecord);

			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred attempting to retrieve data from the Informix database." + g.crlf2 + ex.ToReport(),
												_mainFormTitle + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InitializeForm()
		{

			try
			{
				a = new a();

				_connectionString = g.CI("ConnectionString"); 

			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred attempting to initialize the application object (a)." + g.crlf2 + ex.ToReport(),
												_mainFormTitle + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{

			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
												_mainFormTitle + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
