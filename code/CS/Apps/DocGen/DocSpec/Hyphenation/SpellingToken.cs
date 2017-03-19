using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class SpellingToken
    {
        public string Text { get; set; }
        public string TextChecked { get; set; }
        public bool BeginOfSentence { get; set; }
        public bool IsSpellingChecked { get; set; }
        public bool IsSpellingCorrect { get; set; }
        public bool IsSpellingError { get; set; }
        public List<string> SpellingSuggestions { get; set; }

        public bool SpellCheckThisToken { get { return Get_SpellCheckThisToken(); } }

        public SpellingToken(string rawToken)
        {
            this.Text = rawToken.Trim();
            this.TextChecked = String.Empty;
            this.BeginOfSentence = false;
            this.IsSpellingChecked = false;
            this.IsSpellingCorrect = false;
            this.IsSpellingError = false;
            this.SpellingSuggestions = new List<string>();
        }

        private bool Get_SpellCheckThisToken()
        {
            string prepToken = this.Text.PrepForSpellCheck();

            if (prepToken.IsBlank())
                return false;

            return true;
        }
    }
}
