using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class Cmd
  {
    [XMap]
    public string Code { get; set; }
    [XMap(DefaultValue = "False")]
    public bool Break { get; set; }
    public string Verb { get { return Get_Verb(); } }
		public string[] _parms;
    public string[] Parms { get { return Get_Parms(); } }
		public string ParmsString { get { return Get_ParmsString(); } }
    public bool IsRequired { get { return Get_IsRequired(); } }
    public string TokenType { get { return Get_TokenType(); } }
    public string DataName { get { return Get_DataName(); } }
    public string DataFormat { get { return Get_DataFormat(); } }
    public bool PositionAtEnd { get { return Get_PositionAtEnd(); } }
    public bool Advance { get { return Get_Advance(); } }
		public bool RemoveStoredToken { get { return Get_RemoveStoredToken(); } }
		public int StoredTokenIndex { get { return Get_StoredTokenIndex(); } }
		public bool UsePriorEnd { get { return Get_UsePriorEnd(); } }
		public bool HasNoParameters { get { return this.Parms == null || this.Parms.Length == 0; } }
		public string TextToFind { get { return this.Get_TextToFind(); } }
		public bool ExcludedLastToken { get { return this.Get_ExcludeLastToken(); } }
    public bool ActiveToRun { get; set; }

		public Cmd()
		{
      this.Code = String.Empty;
      this.Break = false;
      this.ActiveToRun = true;
		}

    private string Get_Verb()
    {
      if (this.Code.IsBlank())
        return String.Empty;

      return this.Code.GetTextBefore(Constants.OpenParen); 
    }

    private bool Get_IsRequired()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
        if (parm.ToString().ToLower() == "required")
          return true;
      }

      return false;
    }

    private string Get_DataName()
    {
			string parm = String.Empty;

			try
			{
				if (this.Parms == null || this.Parms.Length == 0)
					return String.Empty;

				parm = this.Parms[0].ToString();

				int openPos = parm.IndexOf("[");
				int closePos = parm.IndexOf("]", openPos > -1 ? openPos : 0);

				if (openPos > -1 && closePos == -1 || openPos == -1 && closePos > -1)
					throw new Exception("Unmatched brackets were found in the command parameter '" + parm + "'.");

				if (closePos > -1)
					parm = parm.Substring(closePos + 1);

				int periodPos = parm.IndexOf(".");

				if (periodPos > -1)
					parm = parm.Substring(0, periodPos);

				string dataName = parm.Trim();

				if (dataName.Contains(' '))
					throw new Exception("The data name '" + dataName + "' cannot included embedded blanks.");

				return dataName;
			}
			catch (Exception ex)
			{
				throw new Exception("An exception occurred while attempting to extract the data name from the command parameter '" +
														parm + "'.", ex);
			}
    }

    private string Get_TokenType()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return String.Empty;

      string parm = this.Parms[0].ToString();

			int openPos = parm.IndexOf("[");
      int closePos = parm.IndexOf("]", openPos > -1 ? openPos : 0);

			if (openPos > -1 && closePos == -1 || openPos == -1 && closePos > -1)
				throw new Exception("Unmatched brackets were found in the command parameter '" + parm + "'."); 

			if (openPos == -1)
				return String.Empty;

			string tokenType = parm.Substring(openPos + 1, closePos - (openPos + 1)).ToLower().Trim();

			switch (tokenType)
			{
				case "dec":
				case "date":
				case "time":
				case "mm/yyyy":
				case "bool":
					return tokenType;
			}

			throw new Exception("Invalid token type specified '[" + parm + "]' in command parameter '" + parm + "'."); 
    }

    private string Get_DataFormat()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return String.Empty;

      string parm = this.Parms[0].ToString();

      int periodPos = parm.IndexOf(".");

      if (periodPos == -1)
        return String.Empty;

			string dataFormat = parm.Substring(periodPos + 1).ToLower().Trim();

			switch (dataFormat)
			{
				case "h24:mm:ss":
				case "h12:mm:ss":
				case "ccyymmdd":
				case "mm/dd/yyyy":
					return dataFormat;
			}

			throw new Exception("Invalid data format specified '" + dataFormat + "' in the command parameter '" + parm + "'."); 
    }

    private bool Get_PositionAtEnd()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
        if (parm.ToString().ToLower() == "end")
          return true;
      }

      return false;
    }

    private bool Get_Advance()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
        if (parm.ToString().ToLower() == "advance")
          return true;
      }

      return false;
    }

    private bool Get_UsePriorEnd()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
				if (parm.ToString().ToLower() == "$priorend")
          return true;
      }

      return false;
    }

    private bool Get_ExcludeLastToken()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
				if (parm.ToString().ToLower() == "exclude")
          return true;
      }

      return false;
    }

    private bool Get_RemoveStoredToken()
    {
      if (this.Parms == null || this.Parms.Length == 0)
        return false;

      foreach (var parm in this.Parms)
      {
				if (parm.ToString().ToLower() == "remove")
          return true;
      }

      return false;
    }

		private int Get_StoredTokenIndex()
    {
      string code = this.Code;

			if (this.Verb.ToLower() != "extractstoredtoken")
				return -1;

      if (this.Parms.Length < 2)
        throw new Exception("The index of the stored token must be supplied as the second parameter " +
                                   "of the ExtractStoredToken command. Command code is '" + this.Code + "'.");

      string parm = this.Parms[1];

			if (parm.ToLower() == "last")
				return 99999;

			if (parm.ToLower() == "join")
				return 99998;

      if (parm.IsNumeric())
        return parm.ToInt32();

      throw new Exception("The second parameter of the ExtractStoredToken command must be numeric - the value " +
                          "found is '" + parm + "'. The command code is '" + this.Code + "'.");
    }
    
    private string[] Get_Parms()
    {
			if (_parms != null)
				return _parms;

			if (this.Code.IsBlank())
			{
				_parms = new string[0];
				return _parms;
			}

      string parmString = this.Code.GetTextBetween(Constants.OpenParen, Constants.CloseParen);
			if (parmString.IsBlank())
			{
				_parms = new string[0];
				return _parms;
			}

      string[] parms = parmString.Split(Constants.CommaDelimiter);
      string[] trimmedParms = new string[parms.Length];
      for(int i = 0; i < parms.Length; i++)
        trimmedParms[i] = parms[i].Trim();

			_parms = trimmedParms;

      return _parms; 
    }

		private string Get_TextToFind()
		{
			if (this.Parms == null || this.Parms.Length == 0)
				return String.Empty;

			return this.Parms[0];
		}

		private string Get_ParmsString()
		{
			if (this.Parms == null || this.Parms.Length == 0)
				return String.Empty;

			var sb = new StringBuilder();

			foreach (string parm in this.Parms)
			{
				if (sb.Length > 0)
					sb.Append(" " + parm);
				else
					sb.Append(parm); 
			}

			return sb.ToString();
		}

    public Cmd Clone()
    {
      var clone = new Cmd();
      clone.Code = this.Code;
      return clone;
    }

  }
}
