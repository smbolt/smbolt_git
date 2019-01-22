using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Org.GS
{
  public class Encryptor
  {
    public string Key {
      get;
      set;
    }
    public string KeyId {
      get;
      set;
    }
    private byte[] KeyIdBytes;

    private bool _isInitialized = false;
    public bool IsInitialized
    {
      get {
        return _isInitialized;
      }
      set {
        _isInitialized = value;
      }
    }

    public byte[] InitializationVector {
      get;
      set;
    }
    public byte[] SaltValue {
      get;
      set;
    }
    public byte[] PassPhrase {
      get;
      set;
    }
    public string HashAlgorithm {
      get;
      set;
    }
    public int PasswordIterations {
      get;
      set;
    }
    public int KeySize {
      get;
      set;
    }

    public Encryptor()
    {
      string key = this.GetKey();
      this.Key = key;
      string rawKey = String.Empty;
      this.KeyId = TokenMaker.RemoveIdFromKey(key, out rawKey);
      this.KeyIdBytes = this.GetKeyIdBytes();
      this.Initialize_EncryptionKeys(rawKey);
    }

    public Encryptor(string keyId)
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

    private object EncryptString_LockObject = new object();
    public string EncryptString(string plainText)
    {
      lock (EncryptString_LockObject)
      {
        try
        {
          byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
          PasswordDeriveBytes password = new PasswordDeriveBytes(PassPhrase, SaltValue, HashAlgorithm, PasswordIterations);
          byte[] keyBytes = password.GetBytes(KeySize / 8);
          RijndaelManaged symmetricKey = new RijndaelManaged();
          symmetricKey.Mode = CipherMode.CBC;
          ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, InitializationVector);
          MemoryStream memoryStream = new MemoryStream();
          CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
          cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
          cryptoStream.FlushFinalBlock();
          byte[] cipherTextBytes = memoryStream.ToArray();
          memoryStream.Close();
          cryptoStream.Close();
          byte[] cipherTextWithKey = new byte[cipherTextBytes.Length + this.KeyIdBytes.Length];
          System.Buffer.BlockCopy(cipherTextBytes, 0, cipherTextWithKey, 0, cipherTextBytes.Length);
          System.Buffer.BlockCopy(this.KeyIdBytes, 0, cipherTextWithKey, cipherTextBytes.Length, this.KeyIdBytes.Length);
          string cipherText = Convert.ToBase64String(cipherTextWithKey);
          return cipherText;
        }
        catch (Exception ex)
        {
          throw new EncryptionException("An error occurred in Encryption.EncryptString: " + ex.Message);
        }
      }
    }

    private object EncryptByteArray_LockObject = new object();
    public string EncryptByteArray(byte[] plainTextBytes)
    {
      lock (EncryptByteArray_LockObject)
      {
        try
        {
          PasswordDeriveBytes password = new PasswordDeriveBytes(PassPhrase, SaltValue, HashAlgorithm, PasswordIterations);
          byte[] keyBytes = password.GetBytes(KeySize / 8);
          RijndaelManaged symmetricKey = new RijndaelManaged();
          symmetricKey.Mode = CipherMode.CBC;
          ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, InitializationVector);
          MemoryStream memoryStream = new MemoryStream();
          CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
          cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
          cryptoStream.FlushFinalBlock();
          byte[] cipherTextBytes = memoryStream.ToArray();
          memoryStream.Close();
          cryptoStream.Close();
          byte[] cipherTextWithKey = new byte[cipherTextBytes.Length + this.KeyIdBytes.Length];
          System.Buffer.BlockCopy(cipherTextBytes, 0, cipherTextWithKey, 0, cipherTextBytes.Length);
          System.Buffer.BlockCopy(this.KeyIdBytes, 0, cipherTextWithKey, cipherTextBytes.Length, this.KeyIdBytes.Length);
          string cipherText = Convert.ToBase64String(cipherTextWithKey);
          return cipherText;
        }
        catch (Exception ex)
        {
          throw new EncryptionException("An error occurred in Encryptor.EncryptByteArray: " + ex.Message);
        }
      }
    }

    private object DecryptString_LockObject = new object();
    public string DecryptString(string cipherText)
    {
      lock (DecryptString_LockObject)
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
          throw new EncryptionException("An error occurred in Encryption.DecryptString: " + ex.Message);
        }
      }
    }

    private object DecryptByteArray_LockObject = new object();
    public byte[] DecryptByteArray(string cipherText)
    {
      lock (DecryptByteArray_LockObject)
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
          throw new EncryptionException("An error occurred in Encryption.DecryptByteArray: " + ex.Message);
        }
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
        throw new EncryptionException("An error occurred in Encryptor.Initialize_EncryptionKeys: " + ex.Message);
      }
    }

    public void Initialize_EncryptionKeys(byte[] DEK)
    {
      try
      {
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
        throw new EncryptionException("An error occurred in Encryptor.Initialize_EncryptionKeys: " + ex.Message);
      }
    }

    public string GetKey()
    {
      if (g.Vault == null)
        g.Vault = new Vault();

      return g.Vault.VaultItems.Values[0];
    }

    public string GetKey(string token)
    {
      return TokenMaker.DecodeToken(token);
    }

    public string GetNewEncryptedToken(string id)
    {
      return this.EncryptString(TokenMaker.GenerateToken(TokenMaker.GenerateEncryptionKey(id)));
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
