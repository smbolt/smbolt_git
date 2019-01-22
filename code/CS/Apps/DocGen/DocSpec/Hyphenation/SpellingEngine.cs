using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class SpellingEngine
    {
        public TriNode Tree { get; set; }


        public SpellingEngine()
        {
            this.Tree = null;
        }

        public void CheckSpelling(SpellingToken spellingToken)
        {
            string spellCheckText = spellingToken.Text.PrepForSpellCheck();

            spellingToken.IsSpellingChecked = true;
            spellingToken.IsSpellingError = false;

            if (spellCheckText.IsValidOrdinalNumber())
            {
                spellingToken.IsSpellingCorrect = true;
                return;
            }

            TriNode t = this.Tree.SearchForNode(spellCheckText);

            if (t != null)
            {
                if (t.ComputedValue.IsNotBlank())
                {
                    spellingToken.IsSpellingCorrect = true;
                    return;
                }
            }

            if (spellingToken.BeginOfSentence)
            {
                spellCheckText = spellCheckText.ToLower();
                t = this.Tree.SearchForNode(spellCheckText);
                if (t != null)
                {
                    if (t.ComputedValue.IsNotBlank())
                    {
                        spellingToken.IsSpellingCorrect = true;
                        return;
                    }
                }
            }

            spellCheckText = spellCheckText.Replace("’", "'");

            if (spellCheckText.Contains("'s"))
            {
                string preApostrophe = spellCheckText.Replace("'s", String.Empty);
                t = this.Tree.SearchForNode(preApostrophe);
                if (t != null)
                {
                    if (t.ComputedValue.IsNotBlank())
                    {
                        spellingToken.IsSpellingCorrect = true;
                        return;
                    }
                }
            }


            spellingToken.TextChecked = spellCheckText;

            string spellCheckLower = spellCheckText.ToLower();

            List<string> variants = this.GetVariants(spellCheckLower);

            if (Char.IsUpper(spellCheckText[0]))
            {
                if (variants.Contains(spellCheckLower))
                    variants.Remove(spellCheckLower);
                variants.Insert(0, spellCheckLower);
            }

            if (Char.IsLower(spellCheckText[0]))
            {
                string spellCheckUpper = spellCheckText.Substring(0, 1).ToUpper();
                if (spellCheckText.Length > 1)
                    spellCheckUpper += spellCheckText.Substring(1);

                if (spellCheckUpper.Contains("'s"))
                {
                    string preApostrophe = spellCheckUpper.Replace("'s", String.Empty);
                    t = this.Tree.SearchForNode(preApostrophe);
                    if (t != null)
                    {
                        if (t.ComputedValue.IsNotBlank())
                        {
                            if (variants.Contains(spellCheckUpper))
                                variants.Remove(spellCheckUpper);
                            variants.Insert(0, spellCheckUpper);
                        }
                    }
                }
                else
                {
                    if (variants.Contains(spellCheckUpper))
                        variants.Remove(spellCheckUpper);
                    variants.Insert(0, spellCheckUpper);
                }
            }            

            spellingToken.SpellingSuggestions = this.GetValidWordsFromList(variants);
            spellingToken.IsSpellingError = true;
            spellingToken.IsSpellingCorrect = false;
        }

        public List<string> GetValidWordsFromList(List<string> listIn)
        {
            List<string> validWords = new List<string>();

            foreach (string s in listIn)
            {
                TriNode t = this.Tree.SearchForNode(s);
                if (t != null)
                {
                    if (t.ComputedValue.IsNotBlank())
                    {
                        if (!validWords.Contains(s))
                            validWords.Add(s);
                    }
                }
                else
                {
                    if (s.Contains("'s"))
                    {
                        string preApostrophe = s.Replace("'s", String.Empty);
                        t = this.Tree.SearchForNode(preApostrophe);
                        if (t != null)
                        {
                            if (t.ComputedValue.IsNotBlank())
                            {
                                if (!validWords.Contains(s))
                                    validWords.Add(s);
                            }
                        }
                    }

                }
            }

            return validWords;
        }


        public List<string> GetVariants(string value)
        {
            List<string> variants = new List<string>();

            string t = value.ToString().ToLower();

            string variant = String.Empty;

            // get swaps
            string[,] swaps = new string[,] { { "f", "ph" }, { "ei", "ie" }, { "ee", "ea" }, { "au", "ua" }, { "ee", "ei" }, { "ly", "lly" } };
            for (int i = 0; i < swaps.Length / 2; i++)
            {
                if (t.Contains(swaps[i, 0]))
                {
                    variant = t.Replace(swaps[i, 0], swaps[i, 1]);
                    if (!variants.Contains(variant))
                        variants.Add(variant);
                }
                if (t.Contains(swaps[i, 1]))
                {
                    variant = t.Replace(swaps[i, 1], swaps[i, 0]);
                    if (!variants.Contains(variant))
                        variants.Add(variant);
                }
            }

            string doubles = "bcdfglmnpstv";
            foreach (char c in doubles)
            {
                int pos = 0;
                while (pos != -1)
                {
                    pos = t.IndexOfNoRepeatChar(c, pos);
                    if (pos > 0)  // don't double first character
                    {
                        variant = t.DoubleCharAtPos(pos);
                        if (!variants.Contains(variant))
                            variants.Add(variant);
                    }
                    if (pos > -1)
                        pos++;
                }
            }

            string letters = "abcdefghijklmnopqrstuvwxyz";
            char[] chars = t.ToCharArray();
            char[] wChars = new char[chars.Length];

            // get splits
            for (int i = 1; i < t.Length; i++)
            {
                variant = t.Substring(0, i) + " " + t.Substring(i, t.Length - i);
                if (!variants.Contains(variant))
                    variants.Add(variant);
            }

            // get transpositions
            wChars = new char[chars.Length];
            for (int i = 0; i < chars.Length - 1; i++)
            {
                chars.CopyTo(wChars, 0);
                int j = i + 1;
                if (j < chars.Length)
                {
                    wChars[i] = chars[j];
                    wChars[j] = chars[i];
                }
                variant = new string(wChars);
                if (!variants.Contains(variant))
                    variants.Add(variant);
            }

            //get omissions            
            for (int i = 0; i < chars.Length; i++)
            {
                wChars = new char[chars.Length - 1];
                int k = 0;
                for (int j = 0; j < chars.Length; j++)
                {
                    if (j != i)
                        wChars[k++] = chars[j];
                }

                variant = new string(wChars);
                if (!variants.Contains(variant))
                    variants.Add(variant);
            }

            //get insertions
            for (int i = 0; i < chars.Length + 1; i++)
            {
                wChars = new char[chars.Length + 1];

                int j = 0;
                int p = 0;

                while (j < i)                           // map any characters prior the insert position
                    wChars[j++] = chars[p++];

                if (j == i)                             // if this is the place to plug a character
                {
                    int k = 0;
                    wChars[j] = letters[k];             // start with the letter 'a'
                    int m = j + 1;                      // m indexes the destination 
                    int n = i;                          // n indexes the source

                    while (m < wChars.Length)           // map the remaining characters from source to dest
                        wChars[m++] = chars[n++];

                    variant = new string(wChars);
                    if (!variants.Contains(variant))
                        variants.Add(variant);

                    k = 1;
                    while (k < 26)
                    {
                        wChars[j] = letters[k++];
                        variant = new string(wChars);
                        if (!variants.Contains(variant))
                            variants.Add(variant);
                    }
                }
            }

            //get single wrong characters
            for (int i = 0; i < chars.Length; i++)
            {
                wChars = new char[chars.Length];
                chars.CopyTo(wChars, 0);

                for (int j = 0; j < 26; j++)
                {
                    wChars[i] = letters[j];
                    variant = new string(wChars);

                    if (!variants.Contains(variant))
                        variants.Add(variant);
                }
            }

            return variants;
        }
    }
}
