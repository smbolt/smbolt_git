using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
	public class SearchText
	{
		private string _text;
		public int _parmIndex;
		public string[] _parms;
		public int[] _startPos;
		public int[] _endPos;

		public SearchText(string text, string[] parms)
		{
			_text = text;
			_parms = parms;
			_parmIndex = 0;
			_startPos = new int[parms.Length];
			_endPos = new int[parms.Length];
		}

		public string GetFirstMatchingString(int startPosition)
		{
			if (_text.IsBlank() || _parms == null || _parms.Length == 0)
				return String.Empty;

			if (startPosition >= _text.Length)
				return String.Empty;

			string textToSearch = String.Empty;
			if (startPosition > 0)
				textToSearch = _text.Substring(startPosition);
			else
				textToSearch = _text;

			for (int i = 0; i < _parms.Length; i++)
			{
				_startPos[i] = -1;
				_endPos[i] = -1;

				bool tokenIsPlaceholder = false;
				string searchToken = _parms[i];
				if (searchToken.StartsWith("`"))
				{
					tokenIsPlaceholder = true;
					searchToken = searchToken.Substring(1);
				}

				if (tokenIsPlaceholder)
				{
					int beg = 0;
					if (i > 0)
						beg = _endPos[i - 1] + 1;

					string token = textToSearch.GetNextToken(beg);
					if (token.IsBlank())
						return String.Empty;

					if (!token.IsTokenType(searchToken))
						return String.Empty;

					int foundPos = textToSearch.IndexOf(token, beg, StringComparison.CurrentCultureIgnoreCase);
					if (foundPos == -1)
						return String.Empty;

					_startPos[i] = foundPos;
					_endPos[i] = foundPos + token.Length - 1; 
				}
				else
				{
					int beg = 0;
					if (i > 0)
						beg = _endPos[i - 1];

					int foundPos = textToSearch.IndexOf(searchToken, beg, StringComparison.CurrentCultureIgnoreCase);
					if (foundPos == -1)
						return String.Empty;

					_startPos[i] = foundPos;
					_endPos[i] = foundPos + searchToken.Length - 1; 
				}
			}

      if (_startPos.Length > 0)
        textToSearch = textToSearch.Substring(_startPos[0]); 

			return textToSearch;
		}
	}
}
