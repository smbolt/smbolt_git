using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class SpellingPassage
    {
        public List<SpellingToken> SpellingTokens { get; set; }

        public SpellingPassage(string rawPassage)
        {
            this.SpellingTokens = new List<SpellingToken>();

            List<string> rawTokens = rawPassage.Split(Org.GS.Constants.WhiteSpaceDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string rawToken in rawTokens)
            {
                if (rawToken.Contains("—"))
                {
                    string[] dashSeparatedWords = rawToken.Split('—');
                    foreach (string dashSeparatedWord in dashSeparatedWords)
                    {
                        this.SpellingTokens.Add(new SpellingToken(dashSeparatedWord));
                    }
                }
                else
                {
                    this.SpellingTokens.Add(new SpellingToken(rawToken));
                }
            }

            for(int i = 0; i < this.SpellingTokens.Count; i++)
            {
                SpellingToken token = this.SpellingTokens[i];

                string tokenText = token.Text.Trim();

                if (i == 0)
                    token.BeginOfSentence = true;

                if (tokenText.HasSentenceEndingPunctuation())
                {
                    if (i < this.SpellingTokens.Count - 2)
                    {
                        this.SpellingTokens[i + 1].BeginOfSentence = true;
                    }
                }

            }            
        }


    }
}
