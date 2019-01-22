using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.GS.Configuration;
using Org.GS;

namespace Org.Cfg
{
  public static class ExtensionMethods
  {

		public static COSet JsonToCOSet(this string json)
		{
			try
			{
				if (json == null)
					return null;

				JArray array = (JArray)JsonConvert.DeserializeObject(json);

				var o = array.ToObject<object>();

				if (o == null)
					return null;

				var coSet = new COSet();

				dynamic dynSet = (dynamic)o;

				if (dynSet.Count == null)
					throw new Exception("All configuration objects must be contained within Json Arrays.");

				foreach (var dynItem in dynSet)
				{
          string ciType = null;
          string ciName = null; 

          if (dynItem.CIType == null && dynItem.CIName == null)
          {
            if (dynItem.K != null && dynItem.V != null)
            {
              ciType = "CI";
              ciName = "CI";
            }
          }
          else
          {
					  ciType = dynItem.CIType;
					  ciName = dynItem.CIName;
          }

					if (ciType.IsBlank() || ciName.IsBlank())
						throw new Exception("All configuration objects must have values supplied in both CIType and CIName properties. " +
																"An object was encountered with CIType=" + (ciType == null ? "null" : ciType) + " and " +
																"CIName=" + (ciName == null ? "null" : ciName) + ".");

					string compoundName = ciType + "." + ciName;
					if (ciType != "CI" && coSet.ContainsKey(compoundName))
						throw new Exception("A duplicate compound name (CIType.CIName) has been encountered when loading the json-based configurations. " +
																"The duplicate compound name is '" + compoundName + "'.");

					switch (ciType)
					{
            case "CI":
              coSet.Add(coSet.Count.ToString("0000"), new CI(dynItem.K.ToString(), dynItem.V.ToString()));
              break;

						case "ConfigDbSpec":
							var configDbSpec = ((object)dynItem).ToConfigDbSpec();
							if (configDbSpec != null)
								coSet.Add(compoundName, configDbSpec);
							break;

						case "ConfigSmtpSpec":
							var configSmtpSpec = ((object)dynItem).ToConfigSmtpSpec();
							if (configSmtpSpec != null)
								coSet.Add(compoundName, configSmtpSpec);
              break;

            case "ConfigFtpSpec":
              var configFtpSpec = ((object)dynItem).ToConfigFtpSpec();
              if (configFtpSpec != null)
                coSet.Add(compoundName, configFtpSpec);
              break;


					}
				}

				return coSet;
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to create a COSet object from a json string. " +
														"The json string is '" + g.crlf + json.PadTo(200).Trim() + "'.", ex); 
			}
		}


    public static object JsonDeserialize(this string json)
    {
      try
      {
        if (json == null)
          return null;

        JObject o = (JObject)JsonConvert.DeserializeObject(json);


        return o;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a COSet object from a json string. " +
                            "The json string is '" + g.crlf + json.PadTo(200).Trim() + "'.", ex);
      }

    }

    public static object JsonDeserialize<T>(this string json)
    {
      try
      {
        if (json == null)
          return null;

        object o = JsonConvert.DeserializeObject<T>(json);


        return o;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a COSet object from a json string. " +
                            "The json string is '" + g.crlf + json.PadTo(200).Trim() + "'.", ex);
      }

    }

		public static ConfigDbSpec ToConfigDbSpec(this object o)
		{
			if (o == null)
				return null;

			dynamic d = (dynamic)o;

			var spec = new ConfigDbSpec();
			spec.CIName = d.CIName == null ? String.Empty : d.CIName;
			spec.DbServer = d.DbServer == null ? String.Empty : d.DbServer;
			spec.DbDsn = d.DbDsn == null ? String.Empty : d.DbDsn;
			spec.DbName = d.DbName == null ? String.Empty : d.DbName;
			spec.DbUserId = d.DbUserId == null ? String.Empty : d.DbUserId;
			spec.DbPassword = d.DbPassword == null ? String.Empty : d.DbPassword;
			spec.DbPasswordEncoded = d.DbPasswordEncoded == null ? false : d.DbPasswordEncoded;
			spec.DbType = g.ToEnum<DatabaseType>(d.DbType, DatabaseType.NotSet);
			spec.DbUseWindowsAuth = d.DbUseWindowsAuth == null ? false : d.DbUseWindowsAuth;
			spec.DbEfProvider = d.DbEfProvider == null ? String.Empty : d.DbEfProvider;
			spec.DbEfMetadata = d.DbEfMetadata == null ? String.Empty : d.DbEfMetadata;
			return spec;
		}

		public static ConfigSmtpSpec ToConfigSmtpSpec(this object o)
		{
			if (o == null)
				return null;

			dynamic d = (dynamic)o;

			var spec = new ConfigSmtpSpec();
			spec.CIName = d.CIName == null ? String.Empty : d.CIName;
			spec.SmtpServer = d.SmtpServer == null ? String.Empty : d.SmtpServer;
			spec.SmtpPort = d.SmtpPort == null ? String.Empty : d.SmtpPort;
			spec.SmtpUserId = d.SmtpUserId == null ? String.Empty : d.SmtpUserId;
			spec.SmtpPassword = d.SmtpPassword == null ? String.Empty : d.SmtpPassword;
			spec.EnableSSL = d.EnableSSL == null ? false : d.EnableSSL;
			spec.PickUpFromIIS = d.PickUpFromIIS == null ? false : d.PickUpFromIIS;
			spec.AllowAnonymous = d.AllowAnonymous == null ? false : d.AllowAnonymous;
			spec.EmailFromAddress = d.EmailFromAddress == null ? String.Empty : d.EmailFromAddress;
			return spec;
		}

    public static ConfigFtpSpec ToConfigFtpSpec(this object o)
    {
      if (o == null)
        return null;

      dynamic d = (dynamic)o;

      var spec = new ConfigFtpSpec();
      spec.CIName = d.CIName == null ? String.Empty : d.CIName;
      spec.FtpServer = d.FtpServer == null ? String.Empty : d.FtpServer;
      spec.FtpUserId = d.FtpUserId == null ? String.Empty : d.UserId;
      spec.FtpPassword = d.FtpPassword == null ? String.Empty : d.FtpPassword;
      spec.FtpKeepAlive = d.FtpKeepAlive == null ? false : d.FtpKeepAlive;
      spec.FtpUsePassive = d.FtpUsePassive == null ? false : d.FtpUsePassive;
      spec.FtpUseBinary = d.FtpUseBinary == null ? false : d.FtpUseBinary;
      spec.FtpBufferSize = d.FtpBufferSize == null ? 8192 : d.FtpBufferSize;
      return spec;
    }

		public static string ToCISetFormat(this string json)
		{
			string ciSetFormat = json.Replace("{", g.crlf + "  { ")
															 .Replace("]", g.crlf + "]")
															 .Replace(":\"", ": \"")
															 .Replace(",\"", ", \"")
															 .Replace("\"}", "\" }");

			return ciSetFormat;															 
		}

  }
}
