using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Vault
  {
    public SortedList<string, string> VaultItems;

    public Vault()
    {
      this.VaultItems = new SortedList<string, string>();
    }

    public void LoadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
        throw new Exception("Vault file does not exist at '" + fileName + "'.");

      string vaultFile = File.ReadAllText(fileName);
      this.LoadFromString(vaultFile);
    }

    public void LoadFromString(string vaultFile)
    {
      int vaultLength = vaultFile.Length;
      int blockLength = 20000;
      int vaultBodyLength = vaultLength - blockLength;

      string vaultBody = vaultFile.Substring(0, vaultBodyLength);
      string block = vaultFile.Substring(vaultBodyLength, 20000);

      string d1 = block.Substring(19974, 1);
      string d2 = block.Substring(19979, 1);
      string d3 = block.Substring(19984, 1);
      string d4 = block.Substring(19989, 1);
      string d5 = block.Substring(19994, 1);
      string d6 = block.Substring(19999, 1);

      int lengthTokenLength = Int32.Parse(d1 + d2 + d3 + d4 + d5 + d6);
      string lengthToken = block.Substring(0, lengthTokenLength);
      string lengthString = TokenMaker.DecodeToken(lengthToken);

      int[] lengths = new int[lengthString.Length / 4];

      for (int i = 0; i < lengths.Length; i++)
      {
        int beg = i * 4;
        lengths[i] = Int32.Parse(lengthString.Substring(beg, 4));
      }

      int pos = 0;
      for (int i = 0; i < lengths.Length; i+=2)
      {
        int length = lengths[i];
        string keyToken = vaultBody.Substring(pos, length);
        pos += length;

        length = lengths[i + 1];
        string valueToken = vaultBody.Substring(pos, length);
        pos += length;

        string key = TokenMaker.DecodeToken(keyToken);
        string value = TokenMaker.DecodeToken(valueToken);

        if (this.VaultItems.ContainsKey(key))
          throw new Exception("Duplicate vault item named '" + key + "' was encountered when attempting to load vault from file.");

        this.VaultItems.Add(key, value);
      }
    }

    public void Clear()
    {
      this.VaultItems = new SortedList<string, string>();
    }

    public void AddToVault(string name, string value)
    {
      if (this.VaultItems.ContainsKey(name))
        throw new Exception("Duplicate vault item named '" + name + "' was encountered.");

      this.VaultItems.Add(name, value);
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach(KeyValuePair<string, string> kvp in this.VaultItems)
      {
        sb.Append("Name  : " + kvp.Key + g.nl);
        sb.Append("Value : " + kvp.Value + g.nl2);
      }

      return sb.ToString();
    }

    public string SaveVault()
    {
      StringBuilder sbBody = new StringBuilder();
      StringBuilder sbLengths = new StringBuilder();

      foreach (KeyValuePair<string, string> kvp in this.VaultItems)
      {
        string keyToken = TokenMaker.GenerateToken(kvp.Key);
        string valueToken = TokenMaker.GenerateToken(kvp.Value);

        sbBody.Append(keyToken);
        sbBody.Append(valueToken);
        sbLengths.Append(keyToken.Length.ToString("0000") + valueToken.Length.ToString("0000"));
      }

      string vaultBody = sbBody.ToString();

      int bodyLength = vaultBody.Length;

      string lengthString = sbLengths.ToString();
      string lengthToken = TokenMaker.GenerateToken(lengthString);
      int lengthTokenLength = lengthToken.Length;
      string lengthTokenLengthString = lengthTokenLength.ToString("000000");

      StringBuilder sbBlock = new StringBuilder();

      while (sbBlock.Length < 20000)
      {
        string dummyKey = TokenMaker.GenerateRandomKey(100);
        string dummyKeyToken = TokenMaker.GenerateToken(dummyKey);
        sbBlock.Append(dummyKeyToken);
      }

      string block = sbBlock.ToString().Substring(0, 20000);
      block = block.ReplaceAtPosition(lengthToken, 0);
      block = block.ReplaceAtPosition(lengthTokenLengthString[0].ToString(), 19974);
      block = block.ReplaceAtPosition(lengthTokenLengthString[1].ToString(), 19979);
      block = block.ReplaceAtPosition(lengthTokenLengthString[2].ToString(), 19984);
      block = block.ReplaceAtPosition(lengthTokenLengthString[3].ToString(), 19989);
      block = block.ReplaceAtPosition(lengthTokenLengthString[4].ToString(), 19994);
      block = block.ReplaceAtPosition(lengthTokenLengthString[5].ToString(), 19999);

      string fullString = vaultBody + block;

      string vaultPath = g.GetAppPath() + @"\VaultFiles\vault.bin";
      File.WriteAllText(vaultPath, fullString);

      fullString = File.ReadAllText(vaultPath);

      return fullString;
    }
  }
}
