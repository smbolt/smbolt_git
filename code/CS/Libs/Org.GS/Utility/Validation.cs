using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.OrgUtility
{
  public enum ValidationResultCode
  {
    OK,
    Warning,
    Error
  }

  public class ValidationResult
  {
    public ValidationResultCode ValidationResultCode { get; set; }
    public string ValidationMessage { get; set; }
    public ValidationResult()
    {
      ValidationResultCode = ValidationResultCode.OK;
      ValidationMessage = String.Empty;
    }
  }

  public class Validation
  {
    public static ValidationResult ValidateEmailAddress(string emailAddress)
    {
      ValidationResult result = new ValidationResult();

      // email address cannot be blank
      if (emailAddress.Length < 1)
      {
        result.ValidationMessage = "The email address must be entered - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // email address cannot contain embedded spaces
      if (emailAddress.IndexOf(' ') > -1)
      {
        result.ValidationMessage = "The email address cannot contain embedded spaces - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }
            
      // email address cannot include illegal characters
      string legalChars = "1234567890abcdefghijklmnopqrstuvwxyz_-@.";
      string emailAddressLC = emailAddress.ToLower();
      for (int i = 0; i < emailAddressLC.Length; i++)
      if (legalChars.IndexOf(emailAddressLC[i]) == -1)
      {
        result.ValidationMessage = "The email address contains the illegal character \"" + emailAddressLC[i] + "\"" +
            " in position " + (i + 1).ToString() + " - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // email address must include an "@"
      int atPos = emailAddress.IndexOf('@');
      if (atPos == -1)
      {
        result.ValidationMessage = "The email address must contain the \"at sign\" character (@) - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      int atPosLast = emailAddress.LastIndexOf('@');
                
      // there cannot be more than one "@"  
      if (atPosLast != atPos)
      {
        result.ValidationMessage = "There cannot be more than one \"at sign\" character (@) in the email address - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // email address must include a period (.)
      int perPos = emailAddress.IndexOf('.');
      if (perPos == -1)
      {
        result.ValidationMessage =  "The email address must contain a period (.) - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // email address must have a period afte the "@"
      int perPosLast = emailAddress.LastIndexOf('.');
      if (perPosLast < atPos)
      {
        result.ValidationMessage = "The email address must contain a period (.) after the \"at sign\" (@) - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // there must be at least one character between the "@" and the last period
      if (perPosLast < atPos + 2)
      {
        result.ValidationMessage = "The email address must contain at least one character after the \"at sign\" (@) and before the period (.) - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // there must be at least 2 characters following the period
      int lth = emailAddress.Length;
      if (perPosLast > lth - 3)
      {
        result.ValidationMessage = "The email address must contain at least two characters after the last period.  For example \"myaddress@abc.us\" or " +
                "\"myaddress@abc.com\" .";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      return result;
    }

    public static ValidationResult ValidatePassword(string password)
    {
      ValidationResult result = new ValidationResult();

      // email address cannot be blank
      if (password.Length < 1)
      {
        result.ValidationMessage = "The password cannot be blank - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // password cannot contain embedded spaces
      if (password.IndexOf(' ') > -1)
      {
        result.ValidationMessage = "The password cannot contain embedded spaces - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      // password cannot include illegal characters
      string legalChars = "1234567890abcdefghijklmnopqrstuvwxyz_-@.";
      string passwordLC = password.ToLower();
      for (int i = 0; i < passwordLC.Length; i++)
      if (legalChars.IndexOf(passwordLC[i]) == -1)
      {
        result.ValidationMessage = "The password contains the illegal character \"" + passwordLC[i] + "\"" +
            " in position " + (i + 1).ToString() + " - please correct and try again.";
        result.ValidationResultCode = ValidationResultCode.Error;
        return result;
      }

      return result;
    }
  }
}
