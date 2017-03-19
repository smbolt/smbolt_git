using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace Org.GS
{
  public class FileSystemUtility : IDisposable
  {
    private bool _isDryRun;
    private Encryptor encryptor = new Encryptor();

    public FileSystemUtility(bool isDryRun = false)
    {
      _isDryRun = isDryRun;
    }

    public void InitializeEncryption()
    {
      this.encryptor = new Encryptor();
    }

    public void InitializeEncryption(string token, bool isKey)
    {
      this.encryptor = new Encryptor();
    }

    public bool CanAccessFileSystem(string fullPath, FileSystemAccessType fileSystemAccessType)
    {
      try
      {
        switch (fileSystemAccessType)
        {
          case FileSystemAccessType.ListDirectory:
            if (!Directory.Exists(fullPath))
              return false;
            string[] files = Directory.GetFiles(fullPath);
            return true;

          case FileSystemAccessType.WriteFile:
            string testContent = "TEST FILE CONTENT";
            string testFileName = fullPath.EnsureSingleTrailingSlash() + @"WriteFileTest";
            int seq = 0;
            string fullTestFileName = testFileName + seq.ToString() + ".txt";
            while (File.Exists(fullTestFileName))
            {
              seq++;
              fullTestFileName = testFileName + seq.ToString() + ".txt";
            }
            File.WriteAllText(fullTestFileName, testContent);
            File.Delete(fullTestFileName);
            return true;

          default:
            throw new Exception("An exception occurred in the FileSystemUtility.CanAccessFileSystem method - FileSystemAccessType '" +
                                fileSystemAccessType.ToString() + "' is not yet implemented.");
        }
      }
      catch
      {
        return false;
      }
    }

    public bool AssertAccess(string fileSystemPath, FileSystemAccess fileSystemAccess)
    {
      if (IsDirectory(fileSystemPath))
        return AssertDirectoryAccess(fileSystemPath, fileSystemAccess);
      else
        return AssertFileAccess(fileSystemPath, fileSystemAccess);
    }

    public bool AssertDirectoryAccess(string fileSystemPath, FileSystemAccess fileSystemAccess)
    {
      try
      {
        switch (fileSystemAccess)
        {
          case FileSystemAccess.ReadOnly:
            if (!CanAccessFileSystem(fileSystemPath, FileSystemAccessType.ListDirectory))
              return false;
            break;

          case FileSystemAccess.ReadWrite:
            if (!CanAccessFileSystem(fileSystemPath, FileSystemAccessType.ListDirectory))
              return false;
            if (!CanAccessFileSystem(fileSystemPath, FileSystemAccessType.WriteFile))
              return false;
            break;
        }

        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool AssertFileAccess(string fileSystemPath, FileSystemAccess fileSystemAccess)
    {
      try
      {

        return true;
      }
      catch
      {
        return false;
      }
    }

    public string RemoveTrailingSlash(string path)
    {
      path = path.Trim();

      if (path.Length == 0)
        return String.Empty;

      int lastCharPos = path.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = path[lastCharPos].ToString();

      if (lastChar == @"\" || lastChar == @"/")
        return path.Substring(0, path.Length - 1);

      return path;
    }

    public bool IsDirectory(string fileSystemPath)
    {
      FileAttributes fa;

      if (fileSystemPath.IsBlank())
        return false;

      try
      {
        fa = File.GetAttributes(fileSystemPath);

        if ((fa & FileAttributes.Directory) == FileAttributes.Directory)
          return true;

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to determine whether the path '" +
            fileSystemPath + "' is a file or a folder.", ex);
      }
    }

    public string DownloadFile(string target, int segmentLength)
    {
      DateTime dtBegin = DateTime.Now;
      SegmentizeFile(target, segmentLength);
            
      // web service (etc.) transfer of file

      DesegmentizeFile(target);
      DateTime dtEnd = DateTime.Now;

      TimeSpan ts = dtEnd - dtBegin;

      return ts.ToString();
    }

    public void SegmentizeFile(string target, int segmentLength)
    {
      if (this.encryptor == null)
        throw new Exception("Encryption is not initialized.");

      try
      {
        target = target.Trim();
        ASCIIEncoding enc = new ASCIIEncoding();
        DateTime operationBeginDT = DateTime.Now;

        string originalFileName = Path.GetFileNameWithoutExtension(target);
        string originalPath = Path.GetDirectoryName(target);
        string extension = Path.GetExtension(target);
        ClearDirectoryOfFiles(originalPath, "*.seg");

        byte[] wholeFileBytes = File.ReadAllBytes(target);                                      // read entire file into a byte array
        string encryptedWholeFile = encryptor.EncryptByteArray(wholeFileBytes);                 // encrypt the byte array into an encrypted string
        byte[] bytes = enc.GetBytes(encryptedWholeFile + "[END_OF_FILE]");                      // get the bytes, after appending an EOF marker

        MemoryStream ms = new MemoryStream(bytes);                                              // load the bytes into a stream
        int segmentCount = ((int)ms.Length / segmentLength) + 1;                                       // figure out how many 100K segments to use
        int segmentNumber = 0;

        while (ms.Position < ms.Length - 1)                                                     // loop through the steam processing a segment at a time
        {
          long bytesToRead;

          // are we at the last segment?
          if (ms.Position + segmentLength < ms.Length - 1)
            bytesToRead = segmentLength;
          else
            bytesToRead = ms.Length - ms.Position;

          byte[] buffer = new byte[bytesToRead];                                              // create a buffer
          ms.Read(buffer, 0, (int)bytesToRead);                                               // fill the buffer with bytes from the stream

          segmentNumber++;
          FileSegmentHeader header = new FileSegmentHeader();                                 // build the segment header object
          header.OperationBeginDT = operationBeginDT;
          if (segmentCount == segmentNumber)
            header.OperationEndDT = DateTime.Now;
          header.SegmentCreateDT = DateTime.Now;
          header.SourceFileFullPath = target;
          header.OriginalFullFileLength = wholeFileBytes.Length;
          header.FullBase64Length = bytes.Length - 13;
          header.NumberOfSegments = segmentCount;
          header.SegmentNumber = segmentNumber;
          header.SegmentLength = (int) bytesToRead;
          string headerText = header.GetHeaderText();                                         // get the header in XML form

          string segment = headerText + enc.GetString(buffer) + "[END_OF_SEGMENT]";           // create the full segment (header / segment / end-of-segment marker)

          // determine the file name and write the segment out
          string segmentText = "(seg-" + segmentNumber.ToString("000") + "-of-" + segmentCount.ToString("000") + ")";
          string segmentFileName = originalPath + @"\" + originalFileName + segmentText + "(ext-" + extension.Replace(".", String.Empty) + ").seg";
          File.WriteAllText(segmentFileName, segment);

        }
                
        // close and dispose the stream
        ms.Close();
        ms.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void DesegmentizeFile(string target)
    {
      if (this.encryptor == null)
        throw new Exception("Encryption is not initialized.");

      target = target.Trim();
      DateTime operationBeginDT = DateTime.Now;
      StringBuilder sbBase64 = new StringBuilder();
      ASCIIEncoding enc = new ASCIIEncoding();

      MemoryStream msAll = null;                                                  // create stream to contain the whole file
      int fileSize = 0;

      string originalFileName = Path.GetFileNameWithoutExtension(target);
      string originalPath = Path.GetDirectoryName(target);
      string extension = Path.GetExtension(target);

      // get an array of all segment files
      string[] segmentFiles = Directory.GetFiles(originalPath, originalFileName + "*(seg-*.seg");

      // loop through the segment files
      foreach (string segmentFile in segmentFiles)
      {
        string segText = File.ReadAllText(segmentFile);                         // read the whole file
        string headerLengthString = segText.Substring(41, 6);                   // get the length of the header
        int headerLength = Int32.Parse(headerLengthString);
        byte[] bytes = enc.GetBytes(segText);                                   // convert the full file to bytes
        MemoryStream ms = new MemoryStream(bytes);                              // load the bytes to a MemoryStream

        byte[] headerBytes = new byte[headerLength];                            // create a byte array for the header
        ms.Read(headerBytes, 0, headerLength);                                  // read the header (positions the stream just after the header)
        string headerText = enc.GetString(headerBytes);                         // conver the header to a string
        FileSegmentHeader header = new FileSegmentHeader(headerText);           // create the FileSegmentHeader object from the header (XML)

        if (header.SegmentNumber == 1)                                          // if this is the first segment, create the stream for the complete file from
        {                                                                       // the record of how long the original file was
          msAll = new MemoryStream(header.FullBase64Length);
          fileSize = header.OriginalFullFileLength;
        }

        int netSegmentLength = bytes.Length - headerLength - 16;                // compute the net segment length (total lenght, less the header, less 16 for "[END_OF_SEGMENT]"
        if (header.SegmentNumber == header.NumberOfSegments)                    // the last segment contains an "[END_OF_FILE]" marker that is 13 bytes long
          netSegmentLength -= 13;                                             // the actual data does not include the 13 byte end of file marker

        byte[] segmentDataBase64 = new byte[netSegmentLength];                  // create byte array for the segment
        ms.Read(segmentDataBase64, 0, netSegmentLength);
        msAll.Write(segmentDataBase64, 0, segmentDataBase64.Length);

        if (header.SegmentNumber == header.NumberOfSegments)                    // if this is the last segment, we need to read forward 13 bytes 
        {
          byte[] endOfFileBytes = new byte[13];
          ms.Read(endOfFileBytes, 0, 13);
          string endOfFileString = enc.GetString(endOfFileBytes);             // convert the end of file marker to a string for human-readability/confirmation
        }

        byte[] endOfSegmentBytes = new byte[16];                                // create an array for the "end of segment indicator ([END_OF_SEGMENT])
        ms.Read(endOfSegmentBytes, 0, 16);                                      // read the end of segment into the array (positioning the MemoryStream at its end)
        string endOfSegment = enc.GetString(endOfSegmentBytes);                 // convert the end of segment to a string for human-readability/confirmation
      }

      byte[] fileBytes = msAll.GetBuffer();                                       // convert the encrypted string to a normal byte array which will contain the whole file
      string fileString = enc.GetString(fileBytes);                               // convert the byte array to a string so it can be decrytped
      Decryptor decryptor = new Decryptor();
      byte[] decryptedBytes = decryptor.DecryptByteArray(fileString);             // decrypt the string into a byte array
      byte[] trimmedBytes = new byte[fileSize];                                   // trim off the excess binary zeros (padding from encryption/decryption)
      Buffer.BlockCopy(decryptedBytes, 0, trimmedBytes, 0, fileSize);             // copy the right sized chunk to the final byte array

      // determine the file name and write the file
      string fileName = originalPath + @"\" + originalFileName + "(DOWNLOADED)" + extension;
      File.WriteAllBytes(fileName, trimmedBytes);
      ClearDirectoryOfFiles(originalPath, "*.seg");
    }

    public void ClearDirectoryOfFiles(string directory, string pattern)
    {
      string[] files;

      if (pattern.Trim().Length > 0)
        files = Directory.GetFiles(directory, pattern.Trim());
      else
        files = Directory.GetFiles(directory);

      foreach (string file in files)
        File.Delete(file);
    }

    public void DeleteDirectoryRecursive(string path)
    {
      List<string> files = Directory.GetFiles(path).ToList();

      foreach (string file in files)
      {
        FileAttributes fa = File.GetAttributes(file);
        File.SetAttributes(file, FileAttributes.Normal);
        File.Delete(file);
      }

      List<string> folders = Directory.GetDirectories(path).ToList();
      foreach (string folder in folders)
      {
        DeleteDirectoryRecursive(folder);
      }

      Directory.Delete(path);
    }

    public void DeleteDirectoryContentsRecursive(string path)
    {
      List<string> files = Directory.GetFiles(path).ToList();

      foreach (string file in files)
      {
        FileAttributes fa = File.GetAttributes(file);
        File.SetAttributes(file, FileAttributes.Normal);
        File.Delete(file);
      }

      List<string> folders = Directory.GetDirectories(path).ToList();
      foreach (string folder in folders)
      {
        DeleteDirectoryRecursive(folder);
      }

      Directory.Delete(path);
    }

    public List<OSFile> DeleteFiles(string path, FileMatchCriteria fmc)
    {
      List<string> targetFilesFullPaths = Directory.GetFiles(path).ToList();
      List<OSFile> deletedFiles = new List<OSFile>();

      foreach (string targetFileFullPath in targetFilesFullPaths)
      {
        if (fmc.IncludeThisFile(targetFileFullPath))
        {
          FileAttributes fa = File.GetAttributes(targetFileFullPath);
          File.SetAttributes(targetFileFullPath, FileAttributes.Normal);
          if (!_isDryRun)
            File.Delete(targetFileFullPath);
          var deletedFile = new OSFile(new OSFolder(path), targetFileFullPath);
          deletedFile.IsFileIncluded = true;
          deletedFiles.Add(deletedFile);
        }
      }
      return deletedFiles;
    }

    public void CopyFoldersAndFiles(string sourcePath, string destPath)
    {
      List<string> files = Directory.GetFiles(sourcePath).ToList();

      if (!Directory.Exists(destPath))
      {
        Directory.CreateDirectory(destPath); 
      }

      foreach (string file in files)
      {
        File.Copy(file, destPath + @"\" + Path.GetFileName(file)); 
      }

      List<string> directories = Directory.GetDirectories(sourcePath).ToList();
      foreach (string directory in directories)
      {
        string newDirectoryName = Path.GetFileName(directory);
        if (!Directory.Exists(destPath + @"\" + newDirectoryName))
        {
          Directory.CreateDirectory(destPath + @"\" + newDirectoryName);
        }
        CopyFoldersAndFiles(directory, destPath + @"\" + newDirectoryName); 
      }
    }

    public List<OSFile> CopyFiles(string sourcePath, string destPath)
		{
			return CopyFiles(sourcePath, destPath, "*.*", true); 
		}

    public List<OSFile> CopyFiles(string sourcePath, string destPath, string pattern)
		{
			return CopyFiles(sourcePath, destPath, pattern, true); 
		}

    public List<OSFile> CopyFiles(string sourcePath, string destPath, bool overwrite)
		{
			return CopyFiles(sourcePath, destPath, "*.*", overwrite); 
		}

    public List<OSFile> CopyFiles(string sourcePath, string destPath, string pattern, bool overwrite)
    {
      var fileMatchCriteria = new FileMatchCriteria(pattern);
      return CopyFiles(sourcePath, destPath, fileMatchCriteria, overwrite);
    }

    public List<OSFile> CopyFiles(string sourcePath, string destPath, FileMatchCriteria fmc, bool overwrite)
    {
      List<string> sourceFileFullPaths = Directory.GetFiles(sourcePath).ToList();
      List<OSFile> copiedFiles = new List<OSFile>();

      if (!Directory.Exists(destPath))
      {
        if (!_isDryRun)
          Directory.CreateDirectory(destPath); 
      }

      foreach (string sourceFileFullPath in sourceFileFullPaths)
      {
        if (fmc.IncludeThisFile(sourceFileFullPath))
        {
          string fileName = Path.GetFileName(sourceFileFullPath);
          if (!_isDryRun)
            File.Copy(sourceFileFullPath, destPath + @"\" + fileName, overwrite);
          var copiedFile = new OSFile(new OSFolder(sourcePath), sourceFileFullPath);
          copiedFile.IsFileIncluded = true;
          copiedFiles.Add(copiedFile);
        }
      }

      return copiedFiles;
    }

		public List<OSFile>  MoveFiles(string sourcePath, string destPath)
		{
			return MoveFiles(sourcePath, destPath, "*.*", true);
		}

		public List<OSFile>  MoveFiles(string sourcePath, string destPath, string pattern)
		{
			return MoveFiles(sourcePath, destPath, pattern, true);
		}

		public List<OSFile>  MoveFiles(string sourcePath, string destPath, bool overwrite)
		{
			return MoveFiles(sourcePath, destPath, "*.*", overwrite);
		}

		public List<OSFile>  MoveFiles(string sourcePath, string destPath, string pattern, bool overwrite)
		{
      var fileMatchCriteria = new FileMatchCriteria(pattern);
			return MoveFiles(sourcePath, destPath, fileMatchCriteria, overwrite);
		}

    public List<OSFile>  MoveFiles(string sourcePath, string destPath, FileMatchCriteria fmc, bool overwrite)
    {
      List<string> sourceFileFullPaths = Directory.GetFiles(sourcePath).ToList();
      List<OSFile> movedFiles = new List<OSFile>();

      if (!Directory.Exists(destPath))
      {
        if (!_isDryRun)
          Directory.CreateDirectory(destPath); 
      }

      foreach (string sourceFileFullPath in sourceFileFullPaths)
      {
        if (fmc.IncludeThisFile(sourceFileFullPath))
        {
          string fileName = Path.GetFileName(sourceFileFullPath);
				  string destFullFileName = destPath + @"\" + fileName;

          if (!_isDryRun && File.Exists(destFullFileName) && !overwrite)
            throw new Exception("File moved operation failed because a file named '" + fileName + "' already exists in the folder '" + destPath + "'.");

          if (!_isDryRun)
            File.Move(sourceFileFullPath, destFullFileName);
          var movedFile = new OSFile(new OSFolder(destPath), destFullFileName);
          movedFile.IsFileIncluded = true;
          movedFiles.Add(movedFile);
        }
      }

      return movedFiles;
    }

    public ModuleOnDiskSetType GetModuleSetType(string path)
    {
      if (!this.IsDirectory(path))
        return ModuleOnDiskSetType.NotModuleSet;

      var level1Folders = this.GetDirectoryNames(path); 

      if (this.AllAreVersionFolderNames(level1Folders))
        return ModuleOnDiskSetType.OneLevel;

      foreach (var level1Folder in level1Folders)
      {
        string level1Path = path + @"\" + level1Folder;
        var level2Folders = this.GetDirectoryNames(level1Path);
        if (!this.AllAreVersionFolderNames(level2Folders))
          return ModuleOnDiskSetType.NotModuleSet;
      }

      return ModuleOnDiskSetType.TwoLevel;
    }

    public List<string> GetDirectoryNames(string path)
    {
      var directoryNames = new List<string>();

      if (!this.IsDirectory(path))
        return directoryNames;

      var fullDirectoryNames = Directory.GetDirectories(path);
      foreach (var fullDirectoryName in fullDirectoryNames)
        directoryNames.Add(Path.GetFileName(fullDirectoryName)); 

      return directoryNames;
    }

    public bool AllAreVersionFolderNames(List<string> folderNames)
    {
      foreach (var folderName in folderNames)
      {
        if (folderName.CharCount('.') != 3)
          return false;

        string[] tokens = folderName.Split(Constants.PeriodDelimiter);
        if (tokens.Length != 4)
          return false;

        foreach (var token in tokens)
          if (token.IsNotNumeric())
            return false;
      }

      return true;
    }

    public void Dispose()
    {
    }
  }
}
