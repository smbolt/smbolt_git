using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Org.GS;

namespace Org.GS
{
  public class TokenMaker
  {
    private static string sorter = InitializeSorter();
    private static int fff = 0;
    private static int holdfff = 0;
    private static string randomChars = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()_+=[]{};:<>?ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=[]{};:<>?";
    private static string randomCharsKeyGen = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
    private static Random rand;
    private static string holdKey = String.Empty;
    private static string savedToken = String.Empty;
    private static object lockObject1 = new object();
    private static object lockObject2 = new object();
    private static object lockObject3 = new object();

    public static string ExplainResults { get; set; }

    public static string GenerateEncryptionKey(string id)
    {
      lock (lockObject1)
      {
        while (fff == holdfff)
        {
          System.Threading.Thread.Sleep(12);
          fff = Int32.Parse(DateTime.Now.ToString("fff"));
        }

        holdfff = fff;
        rand = new Random(fff);
        string rawKey = GenerateRandomKey(48);
        string key = EmbedIdInKey(rawKey, id);
        string rawKey2 = String.Empty;
        //string id2 = RemoveIdFromKey(key, out rawKey2);
        return key;
      }
    }

    public static string EmbedIdInKey(string key, string id)
    {
      if (key.Length != 48)
        throw new Exception("Raw key length must be 48 characters - found " + key.Length.ToString() + " characters.");
      if (id.Length != 4)
        throw new Exception("Length of ID to embed in key must be 4 characters - found " + id.Length.ToString() + " characters.");
      if (id.Substring(1, 3).IsNotNumeric())
        throw new Exception("Last three characters of ID to embed in key must be numeric - found " + id.Substring(1, 3) + " characters.");

      string[] keyBytes = new string[4];
      for (int i = 0; i < 4; i++)
      {
        string keyByte = key[i].ToString();
        int keyPos = randomCharsKeyGen.IndexOf(keyByte);
        int offset = -1;

        for (int j = 0; true; j++)
        {
          int checkPos = keyPos + j;
          if (checkPos > randomCharsKeyGen.Length - 1)
            checkPos -= randomCharsKeyGen.Length;

          if (randomCharsKeyGen[checkPos].ToString() == id[i].ToString())
          {
            offset = j;
            break;
          }
        }

        keyBytes[i] = randomCharsKeyGen[offset].ToString();
      }

      int insertPos = 5;
      string updatedKey = key;
      for (int i = 0; i < keyBytes.Length; i++)
      {
        updatedKey = updatedKey.Insert(insertPos, keyBytes[i].ToString());
        insertPos += 7;
      }

      return updatedKey;
    }

    public static string RemoveIdFromKey(string keyIn, out string keyOut)
    {
      if (keyIn.Length != 52)
        throw new Exception("Key length must be 52 characters - found " + keyIn.Length.ToString() + " characters.");

      string workKey = keyIn;

      string rawId = keyIn.Substring(5, 1) + keyIn.Substring(12, 1) + keyIn.Substring(19, 1) + keyIn.Substring(26, 1);
      keyOut = keyIn.Substring(0, 5) + keyIn.Substring(6, 6) + keyIn.Substring(13, 6) + keyIn.Substring(20, 6) + keyIn.Substring(27, 25);

      string[] rawBytes = new string[4];
      for (int i = 0; i < rawBytes.Length; i++)
        rawBytes[i] = rawId[i].ToString();

      string[] idBytes = new string[4];

      for (int i = 0; i < 4; i++)
      {
        int offset = randomCharsKeyGen.IndexOf(rawBytes[i]);
        string keyByte = keyIn[i].ToString();
        int keyPos = randomCharsKeyGen.IndexOf(keyByte);
        int offsetOfTranslated = keyPos + offset;
        if (offsetOfTranslated > randomCharsKeyGen.Length - 1)
          offsetOfTranslated -= randomCharsKeyGen.Length;
        idBytes[i] = randomCharsKeyGen[offsetOfTranslated].ToString();
      }

      string id = idBytes[0].ToString() + idBytes[1].ToString() + idBytes[2].ToString() + idBytes[3].ToString();
      return id;
    }

    public static string GenerateEncryptionKey(string password, string id)
    {
      lock (lockObject2)
      {
        rand = new Random(Int32.Parse(DateTime.Now.ToString("fff")));

        string iv = GenerateRandomKey(16);
        string passPhrase = GenerateHash(password);
        string salt = GenerateHash("salt");

        return iv + " " + passPhrase + " " + salt;
      }
    }

    public static string GenerateHash(string s)
    {
      lock (lockObject3)
      {
        byte[] array = new ASCIIEncoding().GetBytes(s);
        SHA384Cng sha384 = new SHA384Cng();
        byte[] hash = sha384.ComputeHash(array);
        string base64 = Convert.ToBase64String(hash);

        if (base64.Contains(@"+") || base64.Contains(@"/"))
          base64 = base64.Replace(@"+", String.Empty).Replace(@"/", String.Empty);

        if (base64.Length == 0)
          throw new Exception("Unable to create hash because base 64 representation of hashed value '" + s + "' is an empty string.");

        while (base64.Length < 16)
          base64 += base64;

        return base64.Substring(0, 16);
      }
    }

    public static string GenerateToken(string keyValue)
    {
      if (rand ==  null)
        rand = new Random(Int32.Parse(DateTime.Now.ToString("fff")));

      holdKey = keyValue;

      //string sorter = "!#$%&()*+,-.:;<=>?@CFJMPSVY[]^_bdfhjlnprsuvxz{|}";
      int[] codes = new int[keyValue.Length];
      int[] adjCodes = new int[keyValue.Length];
      StringBuilder sb = new StringBuilder();

      sb.Append("The key value for which we will generate a token is: " + keyValue + g.crlf + g.crlf);
      sb.Append("First get the ASCII codes for each character in the key." + g.crlf + g.crlf);
      sb.Append("Index    Char     ASCII" + g.crlf);

      // store the integer values of each character in the codes array
      for (int i = 0; i < keyValue.Length; i++)
      {
        char c = keyValue[i];
        int n = Convert.ToInt32(c);
        codes[i] = n;
        sb.Append("  " + i.ToString("00") + "       " + c + "        " + n.ToString("000") + g.crlf);
      }

      sb.Append(g.crlf + "Next, subtract all the codes from 288." + g.crlf);

      // load the adjusted codes array by subtracting the entries in the code array from 288
      for (int i = 0; i < keyValue.Length; i++)
      {
        char c = keyValue[i];
        adjCodes[i] = 288 - codes[i];
        sb.Append("  " + i.ToString("00") + "       " + c + "        " + adjCodes[i].ToString("000") + g.crlf);

      }

      sb.Append(g.crlf + "Next, encode the numeric value (ASCII), add the sorter character, it's ASCII code and random characters." + g.crlf2);
      SortedList<string, String> randomlySortedEntries = new SortedList<string, string>();

      for (int i = 0; i < keyValue.Length; i++)
      {
        int sorterAscii1 = sorter[i * 2];
        int sorterAscii2 = sorter[i * 2 + 1];

        //int sorterAscii = Convert.ToInt32(sorter[i]);
        string sorterAsciiString = sorterAscii1.ToString("000") + sorterAscii2.ToString("000");
        int digitsValue = 0;

        string sorterAscii2String = sorterAscii2.ToString("000");
        foreach (char c in sorterAscii2String)
        {
          int n = Int32.Parse(c.ToString());
          digitsValue += n;
        }

        bool isAdded = false;
        string key = String.Empty;
        string entry = String.Empty;

        while (!isAdded)
        {
          string randomChars = GetRandomChars(digitsValue);
          entry = ((char)adjCodes[i]).ToString() + sorter[i * 2 + 1] + randomChars + sorter[i * 2];
          key = entry.Substring(2, digitsValue);
          if (!randomlySortedEntries.ContainsKey(key))
          {
            randomlySortedEntries.Add(key, entry);
            isAdded = true;
          }
        }

        sb.Append("  " + i.ToString("00") + "  key = " + key + "  entry = " + entry + g.crlf);
      }

      sb.Append(g.crlf2 + "Token Value Is:" + g.crlf2);
      StringBuilder sbToken = new StringBuilder();

      foreach (KeyValuePair<string, String> kvp in randomlySortedEntries)
          sbToken.Append(kvp.Value);

      string token = sbToken.ToString();

      savedToken = token;

      sb.Append(token);

      // Now, decode the token

      // BEGINNING OF THE DECODING CODE
      // DECODES THE STRING VALUE "token" AND PLACES THE DECODED VALUE IN "decodedKey"
      sb.Append(g.crlf2 + "Now to decode it." + g.crlf2);

      SortedList<string, string> sortedEntries = new SortedList<string, string>();

      for (int i = 0; i < token.Length; i++)
      {
        i++;
        int sorterCode2 = Convert.ToInt32(token[i]);
        string sorterCode2String = sorterCode2.ToString("000");
        int offset = 0;
        foreach (char ch in sorterCode2String)
          offset += Int32.Parse(ch.ToString());

        offset++;
        int sorterCode1 = Convert.ToInt32(token[i + offset]);
        string sorterCode1String = sorterCode1.ToString("000");
        string sorterValueString = sorterCode1String + sorterCode2String;

        string entry = token.Substring((i - 1), 2) + token.Substring(i + 1, offset);

        sortedEntries.Add(sorterValueString, entry);
        i += offset;
      }   

      StringBuilder sbKey = new StringBuilder();

      sbToken = new StringBuilder();
      foreach (KeyValuePair<string, string> kvp in sortedEntries)
        sbToken.Append(kvp.Value);

      string sortedToken = sbToken.ToString();

      for (int i = 0; i < sortedToken.Length; i++)
      {
        int asciiCode = Convert.ToInt32(sortedToken[i]);
        int code = 288 - asciiCode;
        char c = Convert.ToChar(code);
        sbKey.Append(c);
        i++;
        int sorterCode = Convert.ToInt32(sortedToken[i]);
        string sorterValueString = sorterCode.ToString();

        int offset = 0;
        foreach (char ch in sorterValueString)
          offset += Int32.Parse(ch.ToString());
        offset++;

        i += offset;
      }

      string decodedKey = sbKey.ToString();
      //return decodedKey;

      // END OF DECODING CODE


      sb.Append(g.crlf + "Key is: " + g.crlf2 + "  " + decodedKey.ToString());
      if (holdKey == decodedKey)
        sb.Append("  MATCHED");
      else
        sb.Append("  DOES NOT MATCH THE ORIGINAL KEY: " + holdKey);

      sb.Append(g.crlf2);

      ExplainResults = sb.ToString();

      return token;
    }


    public static string GenerateToken2(string keyValue)
    {
      if (keyValue.IsBlank())
        return String.Empty;

      holdKey = keyValue;

      int[] codes = new int[keyValue.Length];
      int[] adjCodes = new int[keyValue.Length];
      StringBuilder sb = new StringBuilder();

      sb.Append("The key value for which we will generate a token is: " + keyValue + g.crlf + g.crlf);
      sb.Append("First get the ASCII codes for each character in the key." + g.crlf + g.crlf);
      sb.Append("Index    Char     ASCII" + g.crlf);

      // store the integer values of each character in the codes array
      for (int i = 0; i < keyValue.Length; i++)
      {
        char c = keyValue[i];
        int n = Convert.ToInt32(c);
        codes[i] = n;
        sb.Append("  " + i.ToString("00") + "       " + c + "        " + n.ToString("000") + g.crlf);
      }

      sb.Append(g.crlf + "Next, subtract all the codes from 288." + g.crlf);

      // load the adjusted codes array by subtracting the entries in the code array from 288
      for (int i = 0; i < keyValue.Length; i++)
      {
        char c = keyValue[i];
        adjCodes[i] = 288 - codes[i];
        sb.Append("  " + i.ToString("00") + "       " + c + "        " + adjCodes[i].ToString("000") + g.crlf);
      }

      StringBuilder sb2 = new StringBuilder();
      for (int i = 0; i < adjCodes.Length; i++)
        sb2.Append((char)adjCodes[i]);
      string codeString = sb2.ToString();

      byte[] codeBytes = Encoding.UTF8.GetBytes(codeString);
      var encodedString = Convert.ToBase64String(codeBytes);

      // Now, decode the token

      var decodedBytes = Convert.FromBase64String(encodedString);
      string decodedString = Encoding.UTF8.GetString(decodedBytes);

      int[] decodedCodes = new int[decodedString.Length];
      for (int i = 0; i < decodedString.Length; i++)
        decodedCodes[i] = decodedString[i];

      int[] unadjCodes = new int[decodedCodes.Length];
      for (int i = 0; i < decodedCodes.Length; i++)
        unadjCodes[i] = 288 - decodedCodes[i];

      StringBuilder sb3 = new StringBuilder();
      foreach (int unadjCode in unadjCodes)
        sb3.Append((char)unadjCode);

      string checkKey = sb3.ToString();
      if (keyValue != checkKey)
        throw new Exception("The encoded value could not be decoded back to the original value."); 

      return encodedString;
    }

    public static string DecodeToken2(string encodedString)
    {
      if (encodedString.IsBlank())
        return String.Empty;

      var decodedBytes = Convert.FromBase64String(encodedString);
      string decodedString = Encoding.UTF8.GetString(decodedBytes);

      int[] decodedCodes = new int[decodedString.Length];
      for (int i = 0; i < decodedString.Length; i++)
        decodedCodes[i] = decodedString[i];

      int[] unadjCodes = new int[decodedCodes.Length];
      for (int i = 0; i < decodedCodes.Length; i++)
        unadjCodes[i] = 288 - decodedCodes[i];

      StringBuilder sb3 = new StringBuilder();
      foreach (int unadjCode in unadjCodes)
        sb3.Append((char)unadjCode);

      string plainText = sb3.ToString();

      return plainText;
    }

    public static string GenerateRandomKey(int keyLength)
    {
      StringBuilder sbRandomKey = new StringBuilder();

      string doNotDuplicate = String.Empty;
      string tempKeyChar = String.Empty;

      for (int i = 0; i < keyLength; i++)
      {
        tempKeyChar = GetRandomCharsGenKey(1);
        while (tempKeyChar == doNotDuplicate)
          tempKeyChar = GetRandomCharsGenKey(1);

        doNotDuplicate = tempKeyChar;                
        sbRandomKey.Append(tempKeyChar);
      }

      return sbRandomKey.ToString();
    }

    private static string GetRandomChars(int length)
    {
      string chars = String.Empty;

      for (int i = 0; i < length; i++)
      {
        chars += randomChars[rand.Next(0, 106)];
      }

      return chars;
    }

    private static string GetRandomCharsGenKey(int length)
    {
      if (rand == null)
        rand = new Random();

      string chars = String.Empty;
      for (int i = 0; i < length; i++)
      {
        chars += randomCharsKeyGen[rand.Next(0, 62)];
      }

      return chars;
    }
        
    public static string DecodeToken(string token)
    {
      SortedList<string, string> sortedEntries = new SortedList<string, string>();
      StringBuilder sbToken = new StringBuilder();

      for (int i = 0; i < token.Length; i++)
      {
        i++;
        int sorterCode2 = Convert.ToInt32(token[i]);
        string sorterCode2String = sorterCode2.ToString("000");
        int offset = 0;
        foreach (char ch in sorterCode2String)
          offset += Int32.Parse(ch.ToString());

        offset++;
        int sorterCode1 = Convert.ToInt32(token[i + offset]);
        string sorterCode1String = sorterCode1.ToString("000");
        string sorterValueString = sorterCode1String + sorterCode2String;

        string entry = token.Substring((i - 1), 2) + token.Substring(i + 1, offset);

        sortedEntries.Add(sorterValueString, entry);
        i += offset;
      }   

      StringBuilder sbKey = new StringBuilder();

      foreach (KeyValuePair<string, string> kvp in sortedEntries)
        sbToken.Append(kvp.Value);

      string sortedToken = sbToken.ToString();

      for (int i = 0; i < sortedToken.Length; i++)
      {
        int asciiCode = Convert.ToInt32(sortedToken[i]);
        int code = 288 - asciiCode;
        char c = Convert.ToChar(code);
        sbKey.Append(c);
        i++;
        int sorterCode = Convert.ToInt32(sortedToken[i]);
        string sorterValueString = sorterCode.ToString();
        int offset = 0;
        foreach (char ch in sorterValueString)
          offset += Int32.Parse(ch.ToString());
        offset++;

        i += offset;
      }

      string decodedKey = sbKey.ToString();
      return decodedKey;
    }

    private static string InitializeSorter()
    {
      string sorter1 = "!#$%&()*+,-.0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~";
      string sorter2 = sorter1;

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < sorter1.Length; i++)
      {
        for (int j = 0; j < sorter2.Length; j++)
        {
          char[] sorterEntry = new char[] { sorter1[i], sorter2[j] };
          sb.Append(sorterEntry);
        }
      }
            
      return sb.ToString();
    }
  }
}
