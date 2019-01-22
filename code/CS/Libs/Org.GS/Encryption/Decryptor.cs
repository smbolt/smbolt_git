using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Runtime.Serialization;

namespace Org.GS
{
  public class Decryptor
  {
    public string Key { get; set; }
    public string KeyId { get; set; }
    private byte[] KeyIdBytes; 

    private bool _isInitialized = false;
    public bool IsInitialized
    {
      get { return _isInitialized; }
      set { _isInitialized = value; }
    }

    public byte[] InitializationVector { get; set; }
    public byte[] SaltValue { get; set; }
    public byte[] PassPhrase { get; set; }
    public string HashAlgorithm { get; set; }
    public int PasswordIterations { get; set; }
    public int KeySize { get; set; }

    public Decryptor()
    {
      string key = this.GetKey();
      this.Key = key;
      string rawKey = String.Empty;
      this.KeyId = TokenMaker.RemoveIdFromKey(key, out rawKey);
      this.KeyIdBytes = this.GetKeyIdBytes();
      this.Initialize_EncryptionKeys(rawKey);            
    }

    public Decryptor(string keyId)
    {
      this.KeyId = keyId;
      this.KeyIdBytes = this.GetKeyIdBytes();

      if (!g.Vault.VaultItems.ContainsKey(this.KeyId))
        throw new Exception("Vault does not contain key '" + this.KeyId + "'.");

      string key = g.Vault.VaultItems[this.KeyId];
      this.Key = key;
      string rawKey = String.Empty;
      this.KeyId = TokenMaker.RemoveIdFromKey(key, out rawKey);
      this.KeyIdBytes = this.GetKeyIdBytes();
      this.Initialize_EncryptionKeys(rawKey);  
    }

    public string DecryptString(string cipherText)
    {
      try
      {
        byte[] cipherTextWithKey = Convert.FromBase64String(cipherText);
        byte[] cipherTextBytes = new byte[cipherTextWithKey.Length - 4];
        System.Buffer.BlockCopy(cipherTextWithKey, 0, cipherTextBytes, 0, cipherTextWithKey.Length - 4);
        byte[] keyIdBytes = new byte[4];
        System.Buffer.BlockCopy(cipherTextWithKey, cipherTextBytes.Length, keyIdBytes, 0, 4);
        PasswordDeriveBytes password = new PasswordDeriveBytes(PassPhrase, SaltValue, HashAlgorithm, PasswordIterations);
        byte[] keyBytes = password.GetBytes(KeySize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitializationVector);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        return plainText;
      }
      catch (Exception ex)
      {
        throw new EncryptionException("An error occurred in Decryptor.DecryptString: " + ex.Message);
      }
    }

    public byte[] DecryptByteArray(string cipherText)
    {
      try
      {
        byte[] cipherTextWithKey = Convert.FromBase64String(cipherText);
        byte[] cipherTextBytes = new byte[cipherTextWithKey.Length - 4];
        System.Buffer.BlockCopy(cipherTextWithKey, 0, cipherTextBytes, 0, cipherTextWithKey.Length - 4);
        byte[] keyIdBytes = new byte[4];
        System.Buffer.BlockCopy(cipherTextWithKey, cipherTextBytes.Length, keyIdBytes, 0, 4);
        PasswordDeriveBytes password = new PasswordDeriveBytes(PassPhrase, SaltValue, HashAlgorithm, PasswordIterations);
        byte[] keyBytes = password.GetBytes(KeySize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitializationVector);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return plainTextBytes;
      }
      catch (Exception ex)
      {
        throw new EncryptionException("An error occurred in Decryptor.DecryptByteArray: " + ex.Message);
      }
    }

    public void Initialize_EncryptionKeys(string key)
    {
      try
      {
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] DEK = encoding.GetBytes(key);

        byte[] initVector = new byte[16];
        byte[] passPhrase = new byte[16];
        byte[] saltValue = new byte[16];

        Buffer.BlockCopy(DEK, 0, initVector, 0, 16);
        Buffer.BlockCopy(DEK, 16, passPhrase, 0, 16);
        Buffer.BlockCopy(DEK, 32, saltValue, 0, 16);

        InitializationVector = initVector;
        PassPhrase = passPhrase;
        SaltValue = saltValue;
        HashAlgorithm = "SHA1";
        PasswordIterations = 2;
        KeySize = 256;

        IsInitialized = true;
      }
      catch (Exception ex)
      {
        throw new EncryptionException("An error occurred in Decryptor.Initialize_EncryptionKeys: " + ex.Message);
      }
    }

    public string GetKey()
    {
      return g.Vault.VaultItems.Values[0];
    }

    public string GetKey(string token)
    {
      return TokenMaker.DecodeToken(token);
    }

    private byte[] GetKeyIdBytes()
    {
      byte[] keyIdBytes = new byte[4];

      keyIdBytes[0] = (byte)this.KeyId[0];
      keyIdBytes[1] = (byte)this.KeyId[1];
      keyIdBytes[2] = (byte)this.KeyId[2];
      keyIdBytes[3] = (byte)this.KeyId[3];

      return keyIdBytes;
    }
  }
}
