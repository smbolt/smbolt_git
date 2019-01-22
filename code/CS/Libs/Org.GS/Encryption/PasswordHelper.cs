using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

namespace Org.GS.Encryption
{
  public static class PasswordHelper
  {
    private static object HashPassword_LockObject = new object();
    public static string HashPassword(string password)
    {
      string hashedPassword = String.Empty;

      if (Monitor.TryEnter(HashPassword_LockObject, 1000))
      {
        try
        {
          var data = System.Text.Encoding.ASCII.GetBytes(password);
          var sha1 = new SHA1CryptoServiceProvider();
          var sha1data = sha1.ComputeHash(data);
          hashedPassword = Convert.ToBase64String(sha1data);
        }
        catch(Exception ex)
        {
          throw new Exception("An exception occurred attempting to write a log record to the database.", ex);
        }
        finally
        {
          Monitor.Exit(HashPassword_LockObject);
        }
      }
            
      return hashedPassword;
    }
  }
}
