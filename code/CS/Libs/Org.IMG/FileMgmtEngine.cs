using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.IMG
{
  public class FileMgmtEngine : IDisposable
  {
		private FileSystemUtility _fsu;
		private string _imgFmt;
		private string _imgExt;

    public FileMgmtEngine()
    {
			_fsu = new FileSystemUtility();
    }

    public void AssertDirectoryStructure(string rootFolder)
    {
      try
      {
        if (rootFolder.IsBlank())
          throw new Exception("The rootFolder parameter is null or blank."); 

        rootFolder = rootFolder.RemoveTrailingSlash();

        if (!Directory.Exists(rootFolder))
          Directory.CreateDirectory(rootFolder);

        string workRoot = rootFolder + @"\Images_Work";
        if (!Directory.Exists(workRoot))
          Directory.CreateDirectory(workRoot);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to assert the directory structure.", ex); 
      }
    }

		public void CleanUp(string rootFolder, string imgFmt)
		{
			_imgFmt = imgFmt;
			_imgExt = "." + _imgFmt;

			VerifyWorkRoot(rootFolder);
			string workRoot = rootFolder + @"\Images_Work";

			string currentFolder = GetCurrentFolder(rootFolder);

			if (currentFolder.IsBlank())
				return;

			if (!Directory.Exists(currentFolder + @"\" + _imgFmt.ToUpper()))
			{
				_imgFmt = _imgFmt == "tif" ? "bmp" : "tif";
				_imgExt = "." + _imgFmt;
			}

			_fsu.MoveFiles(currentFolder + @"\" + _imgFmt.ToUpper(), rootFolder);
			Directory.Delete(currentFolder + @"\" + _imgFmt.ToUpper());
			File.Delete(currentFolder + @"\status.txt");
			_fsu.MoveFiles(currentFolder, rootFolder);
			_fsu.DeleteDirectoryRecursive(currentFolder);

			List<string> clipFiles = Directory.GetFiles(rootFolder, "*_clip.*").ToList();
			foreach (string clipFile in clipFiles)
				File.Delete(clipFile); 
		}

    public string MigrateImages(string rootFolder, string imgFmt)
    {
			_imgFmt = imgFmt;
			_imgExt = "." + _imgFmt;

			try
			{
				List<string> imageFiles = Directory.GetFiles(rootFolder).ToList();

				if (imageFiles.Count == 0)
					return String.Empty;

				this.CreateCurrentFolder(rootFolder);
				string currentFolder = GetCurrentFolder(rootFolder);

				_fsu.MoveFiles(rootFolder, currentFolder);
				_fsu.MoveFiles(currentFolder, currentFolder + @"\" + _imgFmt.ToUpper(), "f*" + _imgExt, false); 
				File.WriteAllText(currentFolder + @"\status.txt", "IMAGES_MIGRATED" + g.crlf + DateTime.Now.ToString("yyyyMMddHHmmss") + g.crlf);
				return currentFolder; 
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to migrate images.", ex); 
			}
    }

		public void CreateCurrentFolder(string rootFolder)
		{
			try
			{
				VerifyWorkRoot(rootFolder);
				string workRoot = rootFolder + @"\Images_Work";

				string currentFolderName = DateTime.Now.ToString("yyyyMMddHHmmss");
				string currentFolderPath = workRoot + @"\" + currentFolderName;
				while (Directory.Exists(currentFolderPath))
				{
					System.Threading.Thread.Sleep(1000);
					currentFolderName = DateTime.Now.ToString("yyyyMMddHHmmss");
					currentFolderPath = workRoot + @"\" + currentFolderName;
				}

				Directory.CreateDirectory(currentFolderPath);
				File.WriteAllText(currentFolderPath + @"\status.txt", "NEW" + g.crlf + DateTime.Now.ToString("yyyyMMddHHmmss") + g.crlf);
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to create the 'current folder'.", ex); 
			}
		}

    public string GetCurrentFolder(string rootFolder)
    {
      VerifyWorkRoot(rootFolder);
      string workRoot = rootFolder + @"\Images_Work";
      List<string> datedFoldersPaths = Directory.GetDirectories(workRoot).ToList();

			if (datedFoldersPaths.Count == 0)
				return String.Empty;

			List<string> datedFolderNames = new List<string>();
			foreach(var datedFoldersPath in datedFoldersPaths)
				datedFolderNames.Add(Path.GetFileName(datedFoldersPath)); 

			datedFolderNames.Sort();
			string currentFolderPath = workRoot + @"\" + datedFolderNames.Last();
			return currentFolderPath;
    }

    private void VerifyWorkRoot(string rootFolder)
    {      
      if (rootFolder.IsBlank())
        throw new Exception("The rootFolder parameter is null or blank."); 

      rootFolder = rootFolder.RemoveTrailingSlash();

      if (!Directory.Exists(rootFolder))
        throw new Exception("The rootFolder '" + rootFolder + "' does not exist."); 

      string workRoot = rootFolder + @"\Images_Work";

      if (!Directory.Exists(workRoot))
        throw new Exception("The workRoot folder '" + workRoot + "' does not exist."); 
    }

    public void Dispose()
    {

    }
  }
}
