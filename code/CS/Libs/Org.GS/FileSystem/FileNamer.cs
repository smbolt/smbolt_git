using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Org.GS
{
	public class FileNamer
	{
		public string FolderPath { get; set; }
		public string FolderName { get; set; }
		public string FileName { get; set; }
		public string FileNameNoExt { get; set; }
		public string Extension { get; set; }

		public FileNamer(string fullFilePath)
		{
			try
			{				
				this.FolderPath = Path.GetDirectoryName(fullFilePath);
				this.FolderName = Path.GetFileName(this.FolderPath);
				this.FileName = Path.GetFileName(fullFilePath);
				this.FileNameNoExt = Path.GetFileNameWithoutExtension(this.FileName);
				this.Extension = Path.GetExtension(this.FileName);
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred attempting to create a new FileNameParts object from the path '" + fullFilePath + "'.", ex); 
			}
		}

		public string AppendBeforeExtension(string textToAppend)
		{
			return this.FolderPath + @"\" + this.FileNameNoExt + textToAppend + this.Extension; 
		}
	}
}
