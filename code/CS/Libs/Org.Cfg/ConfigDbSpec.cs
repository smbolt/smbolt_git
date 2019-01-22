using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.GS;

namespace Org.Cfg
{
	public class ConfigDbSpec : CO
	{
		public override string CIType { get { return this.GetType().Name; } }

		[DefaultValue("")]
		public string DbServer { get; set; }

		[DefaultValue("")]
		public string DbDsn { get; set; }

		[DefaultValue("")]
		public string DbName { get; set; }

		[DefaultValue("")]
		public string DbUserId { get; set; }

		[DefaultValue("")]
		public string DbPassword { get; set; }

		[DefaultValue(false)]
		public bool DbPasswordEncoded { get; set; }

		[DefaultValue(0)]
		public DatabaseType DbType { get; set; }

		[DefaultValue(false)]
		public bool DbUseWindowsAuth { get; set; }

		[DefaultValue("")]
		public string DbEfProvider { get; set; }

		[DefaultValue("")]
		public string DbEfMetadata { get; set; }

		[JsonIgnore]
		public string ConnectionString { get { return Get_ConnectionString(); } }


		private string Get_ConnectionString()
		{
			switch (this.DbType)
			{
				case DatabaseType.SqlServer:
					if (this.DbUseWindowsAuth)
						return "Server=" + this.DbServer + "; Database=" + this.DbName + "; Trusted_Connection=True;";
					else
						return "Data Source=" + this.DbServer + "; Initial Catalog=" + this.DbName + "; User Id=" + this.DbUserId + ";" + " Password=" + this.DbPassword + ";";

				case DatabaseType.OracleDsn:
				case DatabaseType.SqlServerDsn:
					return "DSN=" + this.DbDsn + ";" + "Provider=" + String.Empty + ";" + "UID=" + this.DbUserId + ";" + "PWD=" + this.DbPassword + ";";

				case DatabaseType.MySql:
					return "server=" + this.DbServer + "; uid=" + this.DbUserId + "; pwd=" + this.DbPassword + "; database=" + this.DbName + ";";

				case DatabaseType.SqlServerEF:
					return "";
			}

			return String.Empty;
		}

		public bool IsReadyToConnect()
		{
			switch (this.DbType)
			{
				case DatabaseType.OracleDsn:
				case DatabaseType.SqlServerDsn:
					return (this.DbDsn.IsNotBlank() && this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());

				case DatabaseType.SqlServerEF:
					if (this.DbUseWindowsAuth)
						return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbEfProvider.IsNotBlank() && this.DbEfMetadata.IsNotBlank());
					else
						return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbEfProvider.IsNotBlank() && this.DbEfMetadata.IsNotBlank() &&
										this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());

				default:
					if (this.DbUseWindowsAuth)
						return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank());
					else
						return (this.DbServer.IsNotBlank() && this.DbName.IsNotBlank() && this.DbUserId.IsNotBlank() && this.DbPassword.IsNotBlank());
			}
		}
	}
}
