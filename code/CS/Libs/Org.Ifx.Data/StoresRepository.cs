using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.Ifx.OdbcData
{
  public class StoresRepository : IDisposable
	{
		private OdbcConnection _conn;
		private string _connectionString;
		private ConfigDbSpec _configDbSpec;

		public DateTime _importDate;

		public StoresRepository(ConfigDbSpec configDbSpec)
		{
			_importDate = DateTime.Now;

			_configDbSpec = configDbSpec;
			if (!_configDbSpec.IsReadyToConnect())
				throw new Exception(configDbSpec + "' is not ready to connect.");
			_connectionString = _configDbSpec.ConnectionString;
		}

		public StoresRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public string GetIfxData()
		{
			StringBuilder sb = new StringBuilder();

			try
			{
        EnsureConnection();

				using (OdbcDataAdapter adapter = new OdbcDataAdapter("select * from customer", _conn))
				{
					DataTable dataTable = new DataTable();
					int num = adapter.Fill(dataTable);

					foreach (DataRow row in dataTable.Rows)
					{
						string customerNbr = row["customer_num"].DbToString();
						string fName = row["fname"].DbToString();
						string lName = row["lname"].DbToString();
						string company = row["company"].DbToString();

						sb.Append(customerNbr.PadTo(6) +
											fName.PadTo(20) +
											lName.PadTo(20) +
											company + g.crlf); 
					}

					string data = sb.ToString();
					return data;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to get data from the Informix database.", ex);
			}
		}

		public bool WriteLog(LogRecord logRecord)
		{
			try
			{
				EnsureConnection();

				string sql = "INSERT INTO informix.AppLog " + g.crlf +
										 "( " + g.crlf +
										 "  logdatetime, " + g.crlf +
										 "  severitycode, " + g.crlf +
										 "  message, " + g.crlf +
										 "  moduleid, " + g.crlf +
										 "  eventcode, " + g.crlf +
										 "  entityid, " + g.crlf +
										 "  username, " + g.crlf +
										 "  clienthost, " + g.crlf +
										 "  clientip, " + g.crlf +
										 "  clientuser, " + g.crlf +
										 "  clientapplication, " + g.crlf +
										 "  clientapplicationversion, " + g.crlf +
										 "  transactionname, " + g.crlf +
										 "  notificationsent " + g.crlf +
										 ") " + g.crlf +
										 "VALUES ( " +
										 "'" + logRecord.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss.fffff") + "', " + g.crlf + 
										 "'" + logRecord.SeverityCode.ToString() + "', " + g.crlf + 
										 "'" + logRecord.Message + "', " + g.crlf + 
										 logRecord.ModuleId.ToString() + ", " + g.crlf + 
										 logRecord.EventCode.ToString() + ", " + g.crlf + 
										 logRecord.EntityId.ToString() + ", " + g.crlf + 
										 "'" + logRecord.UserName + "', " + g.crlf + 
										 (logRecord.ClientHost != null ? "'" + logRecord.ClientHost + "'" : "NULL") + ", " + g.crlf + 
										 (logRecord.ClientIp != null ? "'" + logRecord.ClientIp + "'" : "NULL") + ", " + g.crlf +
										 (logRecord.ClientUser != null ? "'" + logRecord.ClientUser + "'" : "NULL") + ", " + g.crlf +
										 (logRecord.ClientApplication != null ? "'" + logRecord.ClientApplication + "'" : "NULL") + ", " + g.crlf + 
										 (logRecord.ClientApplicationVersion != null ? "'" + logRecord.ClientApplicationVersion + "'" : "NULL") + ", " + g.crlf +
										 (logRecord.TransactionName != null ? "'" + logRecord.TransactionName + "'" : "NULL") + ", " + g.crlf + 
										 (logRecord.NotificationSent ? true : false) + g.crlf + 
										 ") "; 


				var cmd = new OdbcCommand(sql, _conn);
				var result = cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to write a log record to the AppLog table of the OpsMgmt database.", ex); 
			}
		}


		public bool AddModule(string moduleName)
		{
			try
			{
				EnsureConnection();

				string sql = "INSERT INTO informix.Modules " + g.crlf +
										 "( " + g.crlf +
										 "  moduleName " + g.crlf +
										 ") " + g.crlf +
										 "VALUES ( " +
										 "'" + moduleName + "' " + g.crlf +
										 ") ";


				var cmd = new OdbcCommand(sql, _conn);
				var result = cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to write a log record to the AppLog table of the OpsMgmt database.", ex);
			}
		}

		private void EnsureConnection()
		{
			try
			{
				if (_conn == null)
				{
					_conn = new OdbcConnection();
					_conn.ConnectionString = _connectionString;
				}

				if (_conn.State != ConnectionState.Open)
					_conn.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to ensure (or create) the database connection.", ex);
			}
		}

		public void Dispose()
		{
			if (_conn == null)
				return;

			if (_conn.State == ConnectionState.Open)
				_conn.Close();
			_conn.Dispose();
			_conn = null;
		}
	}
}
