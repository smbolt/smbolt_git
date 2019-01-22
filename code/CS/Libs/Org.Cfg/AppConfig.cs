using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Cfg
{
	public class AppConfig
	{
    private EncryptedFilePair _jsonFilePairs;

		private string _appConfigPath;
		private COSet _coSet;

		private CI[] _ciSet;
		public CI[] CISet { get { return Get_CISet(); } }

		private Encryptor _encryptor;

		public AppConfig()
		{
			_coSet = new COSet();
			_appConfigPath = g.AppConfigPath;
			_encryptor = new Encryptor();
      _ciSet = new CI[16];
      _jsonFilePairs = new EncryptedFilePair();
		}

		public void Load()
		{
			try
			{
				List<EncryptedFilePair> jsonFilePairs = GetJsonFiles();

				foreach (var jsonFilePair in jsonFilePairs)
				{
					if (jsonFilePair.UnencryptedFilePath.IsNotBlank())
					{
						string unencryptedData = File.ReadAllText(jsonFilePair.UnencryptedFilePath);
						string encryptedData = _encryptor.EncryptString(unencryptedData);
						jsonFilePair.EncryptedFilePath = jsonFilePair.UnencryptedFilePath.Replace(".json", ".jsonx");
						File.WriteAllText(jsonFilePair.EncryptedFilePath, encryptedData); 
					}

					string json = _encryptor.DecryptString(File.ReadAllText(jsonFilePair.EncryptedFilePath));

					try
					{
						var coSet = json.JsonToCOSet();
						foreach (var co in coSet)
						{
              if (co.Value.GetType().Name == "CI")
              {
                this.AddCI((CI)co.Value);
                continue;
              }

							co.Value.SourceFileName = jsonFilePair.EncryptedFilePath;
							if (_coSet.ContainsKey(co.Key))
								throw new Exception("A duplicate CO key was encountered while attempting to load the AppConfig object from json. " +
									                  "The duplicate key is '" + co.Key + "' found in file '" + jsonFilePair.EncryptedFilePath + "'.");

							_coSet.Add(co.Key, co.Value); 
						}
					}
					catch (Exception ex)
					{
            throw new Exception("An exception occurred while attempting to load the Org.Cfg.AppConfig object. The file being " +
                                "processed is '" + jsonFilePair.EncryptedFilePath + "'.", ex); 
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to load the AppConfig object.", ex); 
			}
		}

		public void ClearCISet()
		{
      _ciSet = new CI[16];
		}

		public void AddCI(CI ci)
		{
			if (_ciSet == null)
				_ciSet = new CI[16];

			while (true)
			{
				for (int i = _ciSet.Length - 16; i < _ciSet.Length; i++)
				{
					if (_ciSet[i] == null)
					{
						_ciSet[i] = ci;
						return;
					}
				}

				var ciSet = new CI[_ciSet.Length + 16];
				for (int i = 0; i < _ciSet.Length; i++)
				{
					if (_ciSet[i] != null)
						ciSet[i] = _ciSet[i];
				}

				_ciSet = ciSet;
			}
		}

		public CI[] Get_CISet()
		{
      if (_ciSet == null)
        _ciSet = new CI[16];

			int ciCount = 0;
			for (int i = 0; i < _ciSet.Length; i++)
			{
				if (_ciSet[i] != null)
					ciCount++;
			}

			if (ciCount == 0)
				return _ciSet;

			var ciSet = new CI[ciCount];
			for (int i = 0; i < ciCount; i++)
				ciSet[i] = _ciSet[i];

			return ciSet;
		}

		private List<EncryptedFilePair> GetJsonFiles()
		{
			try
			{
				var filePairs = new List<EncryptedFilePair>();

				var searchParms = new SearchParms();
				searchParms.RootPath = _appConfigPath;
				var folder = new OSFolder(searchParms);
				folder.BuildFolderAndFileList();

				var rawFileList = folder.FileList;
				var lcFileList = new List<string>();
				foreach (var rawFile in rawFileList.Values)
				{
					string fullPath = rawFile.FullPath.ToLower();
					string ext = Path.GetExtension(fullPath);
					if (ext.In(".json,.jsonx"))
						lcFileList.Add(rawFile.FullPath.ToLower());
				}

				foreach (var lcFile in lcFileList)
				{
					string ext = Path.GetExtension(lcFile);
					EncryptedFilePair pair = null;

          string pairedFileName = ext == ".json" ?
            lcFile.Replace(".json", ".jsonx") : lcFile.Replace(".jsonx", ".json"); 

          // find paired object for this file if it exists
					if (ext == ".json")
            pair = filePairs.Where(f => f.EncryptedFilePath == pairedFileName).FirstOrDefault();
					else
            pair = filePairs.Where(f => f.UnencryptedFilePath == pairedFileName).FirstOrDefault();

					if (pair == null)
					{
						pair = new EncryptedFilePair();
						filePairs.Add(pair); 
					}

					if (ext == ".json")
						pair.UnencryptedFilePath = lcFile;
					else
						pair.EncryptedFilePath = lcFile;
				}

				return filePairs;
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to retrive the list of json for the AppConfig object from " +
														"directory '" + _appConfigPath + "'.", ex); 
			}
		}

		public bool CIExists(string key)
		{
			if (_ciSet == null)
				return false;

			return _ciSet.Where(c => c.K == key).Count() > 0;
		}

		public string CI(string key)
		{
			if (_ciSet == null)
				return String.Empty;

			var ci = _ciSet.Where(c => c.K == key).FirstOrDefault();

			if (ci == null || ci.V == null)
				return String.Empty;

			return ci.V.Trim();
		}

		public string CI(string key, string defaultValue)
		{
			if (_ciSet == null)
				return defaultValue;

			var ci = _ciSet.Where(c => c.K == key).FirstOrDefault();

			if (ci == null || ci.V == null)
				return defaultValue;

			return ci.V.Trim();
		}

		public ConfigDbSpec GetConfigDbSpec(string name)
		{
			string key = "ConfigDbSpec." + name; 

			if (_coSet == null || !_coSet.ContainsKey(key))
				return null;

			var co = _coSet[key];

			if (co.GetType().Name != "ConfigDbSpec")
				return null;

			return (ConfigDbSpec)co;
		}

		public ConfigSmtpSpec GetConfigSmtpSpec(string name)
		{
			string key = "ConfigSmtpSpec." + name; 

			if (_coSet == null || !_coSet.ContainsKey(key))
				return null;

			var co = _coSet[key];

			if (co.GetType().Name != "ConfigSmtpSpec")
				return null;

			return (ConfigSmtpSpec)co;
		}

    public ConfigFtpSpec GetConfigFtpSpec(string name)
    {
      string key = "ConfigFtpSpec." + name;

      if (_coSet == null || !_coSet.ContainsKey(key))
        return null;

      var co = _coSet[key];

      if (co.GetType().Name != "ConfigFtpSpec")
        return null;

      return (ConfigFtpSpec)co;
    }

	}
}
