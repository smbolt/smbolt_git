using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Org.GS
{
    public enum FolderPermissionsResult
    {
        NotSet,
        ValidationSuccess,
        PathIsNotSpecified,
        PathIsNotValidDirectory,
        PathDoesNotExist,
        CannotCreateTestFolder,
        CannotCreateTestFile,
        CannotDeleteTestFile,
        CannotDeleteTestFolder,
        ExceptionOccurred
    }

    public class FileSystemChecks
    {
        public static Exception Exception { get; set; }

        public static FolderPermissionsResult CheckFolderPermissions(string path)
        {
            try
            {
                Exception = null;
                path = g.RemoveTrailingSlash(path.Trim());

                // check whether a path was passed in
                if (path.Length == 0)
                    return FolderPermissionsResult.PathIsNotSpecified;


                // try to trigger an exception due to an invalid path
                try
                {
                    Path.GetFullPath(path);
                }
                catch
                {
                    return FolderPermissionsResult.PathIsNotValidDirectory;
                }


                // does a folder exist at the specified path
                if (!Directory.Exists(path))
                    return FolderPermissionsResult.PathDoesNotExist;


                // attempt to create a test folder under the ufs path
                string tempName = "Temp" + DateTime.Now.ToString("yyyyMMddhhmmss");

                try
                {
                    Directory.CreateDirectory(path + @"\" + tempName);
                }
                catch
                {
                    return FolderPermissionsResult.CannotCreateTestFolder;
                }


                // attempt to create a test file under the ufs path
                try
                {
                    File.WriteAllText(path + @"\" + tempName + @"\" + tempName + ".txt", "Test");
                }
                catch
                {
                    return FolderPermissionsResult.CannotCreateTestFile;
                }


                // attempt to delete the test file under the ufs path
                try
                {
                    File.Delete(path + @"\" + tempName + @"\" + tempName + ".txt");
                }
                catch
                {
                    return FolderPermissionsResult.CannotDeleteTestFile;
                }


                // attempt to delete the test folder under the ufs path
                try
                {
                    Directory.Delete(path + @"\" + tempName);
                }
                catch
                {
                    return FolderPermissionsResult.CannotDeleteTestFolder;
                }


                // passed all the validation tests successfully
                return FolderPermissionsResult.ValidationSuccess;

            }
            catch (Exception ex)
            {
                Exception = ex;
                return FolderPermissionsResult.ExceptionOccurred;
            }
        }
    }
}
