using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Security
{
  public class SecurityToken
  {
    public int AccountId {
      get;
      set;
    }
    public DateTime AuthenticationDateTime {
      get;
      set;
    }
    public DateTime TokenExpirationDateTime {
      get;
      set;
    }
    public string SessionId {
      get;
      set;
    }
    public bool IsExpired {
      get {
        return DateTime.Now > TokenExpirationDateTime;
      }
    }
    public bool IsValid {
      get;
      set;
    }
    public string DebugString {
      get {
        return Get_DebugString();
      }
    }
    public string Message {
      get;
      set;
    }

    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

    public SecurityToken()
    {
      this.AccountId = 0;
      this.AuthenticationDateTime = DateTime.MinValue;
      this.TokenExpirationDateTime = DateTime.MinValue;
      this.SessionId = CreateSessionId();
      this.IsValid = true;
      this.Message = String.Empty;
    }

    public void InitializeInvalid()
    {
      this.SessionId = String.Empty;
      this.IsValid = false;
      this.Message = "Initialized as invalid";
    }

    public string SerializeToken()
    {
      XElement tokenRoot = new XElement("T");
      tokenRoot.Add(new XAttribute("A", this.AccountId.ToString()));
      tokenRoot.Add(new XAttribute("ADT", this.AuthenticationDateTime.ToCCYYMMDDHHMMSS()));
      tokenRoot.Add(new XAttribute("XDT", this.TokenExpirationDateTime.ToCCYYMMDDHHMMSS()));
      tokenRoot.Add(new XAttribute("SID", this.SessionId.Trim()));

      string xml = tokenRoot.ToString();
      Encryptor encryptor = new Encryptor();
      string tokenized = encryptor.EncryptString(xml);

      return tokenized;
    }

    public string SerializeToXml()
    {
      XElement tokenRoot = new XElement("T");
      tokenRoot.Add(new XAttribute("A", this.AccountId.ToString()));
      tokenRoot.Add(new XAttribute("ADT", this.AuthenticationDateTime.ToCCYYMMDDHHMMSS()));
      tokenRoot.Add(new XAttribute("XDT", this.TokenExpirationDateTime.ToCCYYMMDDHHMMSS()));
      tokenRoot.Add(new XAttribute("SID", this.SessionId.Trim()));

      string xml = tokenRoot.ToString();

      return xml;
    }

    public string DeserializeToken(string encryptedToken)
    {
      Decryptor decryptor = new Decryptor();
      string decryptedToken = decryptor.DecryptString(encryptedToken);
      XElement tokenRoot = XElement.Parse(decryptedToken);
      this.AccountId = Int32.Parse(tokenRoot.Attribute("A").Value);
      this.AuthenticationDateTime = tokenRoot.Attribute("ADT").Value.CCYYMMDDHHMMSSToDateTime();
      this.TokenExpirationDateTime = tokenRoot.Attribute("XDT").Value.CCYYMMDDHHMMSSToDateTime();
      this.SessionId = tokenRoot.Attribute("SID").Value.Trim();
      this.IsValid = true;

      string xml = this.SerializeToXml();

      return xml;
    }

    private string CreateSessionId()
    {
      int length = 8;
      Random rand = new Random();

      string chars = String.Empty;
      for (int i = 0; i < length; i++)
      {
        chars += _chars[rand.Next(0, 62)];
      }

      return chars;
    }

    private string Get_DebugString()
    {
      return this.AccountId.ToString() + "|" +
             this.AuthenticationDateTime.ToString("yyyyMMddHHmmss") + "|" +
             this.TokenExpirationDateTime.ToString("yyyyMMddHHmmss") + "|" +
             this.SessionId + "|" +
             this.IsExpired.ToString().Substring(0, 1);
    }
  }
}
