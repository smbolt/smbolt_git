using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adsdi.GS;
using Adsdi.DCT;
using Adsdi.DocGen;
using Adsdi.DocGen.DocSpec;

namespace DictionaryManager
{
    public partial class frmMain : Form
    {
        private a a;

        private string _dctPath;
        private Dct _dctA;
        private Dct _dctB;

        private int spellCheckCount = 0;
        private int correctCount = 0;
        private int errorCount = 0;
        private float percentCorrect = 0F;

        private SpellingEngine _spellingEngine;

        delegate double MathFunction(double x);


        public frmMain()
        {
            InitializeComponent();
            InitializeApplication();
        }


        private void Action(object sender, EventArgs e)
        {
            string action = g.GetActionFromEvent(sender);

            switch (action)
            {
                case "LOAD_DCT_A":
                case "LOAD_DCT_B":
                    if (ckSimpleList.Checked)
                        ListFile(action);
                    else
                        if (ckFilterSpacesAndBadSpelling.Checked)
                            FilterOutSpadesAndBadSpelling(action);
                        else
                            LoadDictionary(action);
                    break;

                case "COMPARE_AB":
                case "COMPARE_BA":
                    CompareDictionaries(action);
                    break;

                case "FIND_IN_A":
                case "FIND_IN_B":
                    SearchDictionaries(action);
                    break;

                case "COMBINE_AB":
                    Combine();
                    break;

                case "VALIDATE_ENTRIES":
                    ValidateEntries();
                    break;

                case "GET_AUTO_CORRECT_ENTRIES":
                    GetAutoCorrectEntries();
                    break;

                case "RELOAD_CBO":
                    ReloadCbo();
                    break;

                case "LOAD_TO_TRINODE":
                    LoadToTriNode();
                    break;

                case "GET_VARIANTS":
                    GetVariants();
                    break;

                case "RUN_FUNCTIONAL":
                    RunFunctional();
                    break;

                case "RANDOMIZE":
                    Randomize();
                    break;

                case "CLEAR_REPORTS":
                    int pos = txtReports.Text.IndexOf("Spell Check Report");
                    if (pos != -1)
                    {
                        txtReports.Text = txtReports.Text.Substring(0, pos).Trim();
                        return;
                    }

                    txtReports.Clear();
                    break;

                case "SPELL_CHECK":
                    SpellCheck();
                    break;

                case "ADD_TO_DICTIONARY":
                    AddToDictionary();
                    break;

                case "RANDOMIZE_FROM_FILE":
                    RandomizeFromFile();
                    break;

                case "EXIT":
                    TerminateApplication();
                    break;
            }

            this.Cursor = Cursors.Default;
        }

        private void InitializeApplication()
        {
            a = new a();
            _dctPath = g.AppConfig.GetCI("DctPath"); 

            _spellingEngine = new SpellingEngine();

            ReloadCbo();
            ckLimitSpellCheck.Checked = true;
            txtSpellCheckLimit.Text = "99";
            txtSearchText.Text = "variants";
        }

        private void ReloadCbo()
        {
            cboDctA.Items.Clear();
            cboDctB.Items.Clear();
            cboDctA.Items.Add(String.Empty);
            cboDctB.Items.Add(String.Empty);
            List<string> filePaths = Directory.GetFiles(_dctPath).ToList();

            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                if (fileName.Substring(0, 1).IsNumeric())
                {
                    cboDctA.Items.Add(fileName);
                    cboDctB.Items.Add(fileName);
                }
            }
        }

        private void LoadDictionary(string command)
        {
            this.Cursor = Cursors.WaitCursor;

            Dct d = null;
            ComboBox c = null;
            TextBox t = null;
            TextBox st = null;
            string origSrc = "A";
            string dctName = "A";

            switch (command)
            {
                case "LOAD_DCT_A":
                    d = _dctA;
                    c = cboDctA;
                    t = txtDctA;
                    st = txtSourceLetterA;
                    dctName = "A";
                    break;

                case "LOAD_DCT_B":
                    d = _dctB;
                    c = cboDctB;
                    t = txtDctB;
                    st = txtSourceLetterB;
                    dctName = "B";
                    break;
            }


            origSrc = dctName;
            string srcLtr = st.Text.Trim();

            string fileName = c.Text;
            if (fileName.IsBlank())
            {
                MessageBox.Show("Please choose a dictionary file.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileLetter = fileName.Substring(0, 1);

            string fullPath = _dctPath + @"\" + fileName;

            d = new Dct();
            if (srcLtr.IsNotBlank())
                dctName = srcLtr;

            if (fileLetter.IsNumeric())
                dctName = fileLetter;

            d.Name = dctName;
            d.Load(fullPath, dctName);

            t.Text = d.GetList();
            t.SelectionStart = 0;
            t.SelectionLength = 0;

            lblStatus.Text = "Dictionary " + d.Name + "  Entries loaded: " + d.Count.ToString("#,###,##0") + 
                             "  Correct Spelling: " + d.CorrectSpellingCount.ToString("#,###,##0") + "  Load Time: " + d.LoadSeconds.ToString("00.000") + "   Errors Found: " + d.ErrorsFound.ToString();

            if (origSrc == "A")
            {
                _dctA = d;
                tabMain.SelectedTab = tabPageDctA;
            }
            else
            {
                _dctB = d;
                tabMain.SelectedTab = tabPageDctB;
            }

            this.Cursor = Cursors.Default;
        }
        
        private void LoadToTriNode()
        {
            this.Cursor = Cursors.WaitCursor;
            string fileName = cboDctA.Text;

            if (_dctA == null)
            {
                MessageBox.Show("Dictionary A must be loaded first.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _spellingEngine.Tree = new TriNode(null, null, 'm');
            _spellingEngine.Tree.NodeAdded += Tree_NodeAdded;
            _spellingEngine.Tree.TotalNodeCount = 1;
            _spellingEngine.Tree.TotalLowNodeCount = 0;
            _spellingEngine.Tree.TotalEqualNodeCount = 1;
            _spellingEngine.Tree.TotalHighNodeCount = 0;
            _spellingEngine.Tree.InDiagnosticsMode = false;

            Random r = new Random();
            string nodeBeingAdded = String.Empty;
            int count = 0;
            List<string> dummyPatterns = new List<string>();

            SortedList<string, Adsdi.DCT.Entry> randomOrder = new SortedList<string, Adsdi.DCT.Entry>();

            lblStatus.Text = "Loading random table to optimize TriNode entry sequence."; 
            Application.DoEvents();

            foreach (KeyValuePair<string, Adsdi.DCT.Entry> kvp in _dctA)
            {
                count++;
                int rnd = r.Next(100, 999);
                string rndStr = rnd.ToString("000");
                string key = rndStr + "~" + kvp.Key;
                randomOrder.Add(key, kvp.Value);
                if (count % 500 == 0)
                {
                    lblStatus.Text = "Loading random table to optimize TriNode entry sequence - loaded " + count.ToString("###,##0") + " items."; 
                    Application.DoEvents();
                }
            }

            count = 0;
            foreach (KeyValuePair<string, Adsdi.DCT.Entry> kvp in randomOrder)
            {
                string fullKey = kvp.Key;
                string actualKey = fullKey.Split('~').Last();
                nodeBeingAdded = actualKey;
                _spellingEngine.Tree.NodeBeingAdded = nodeBeingAdded;
                TriNode addedNode = _spellingEngine.Tree.InsertNode(_spellingEngine.Tree, actualKey, dummyPatterns, false);
                _spellingEngine.Tree.IncludedLevels = String.Empty;
                count++;
                if (count % 500 == 0)
                {
                    lblStatus.Text = "Loading TriNode item " + count.ToString("###,##0") + " : " + actualKey;
                    Application.DoEvents();
                }
            }

            StringBuilder sb = new StringBuilder();

            int foundCount = 0;
            int searchCount = 0;
            bool inDiagnosticsMode = ckInDiagnosticsMode.Checked;

            DateTime beginDT = DateTime.Now;

            foreach (KeyValuePair<string, Adsdi.DCT.Entry> kvp in _dctA)
            {
                searchCount++;
                TriNode t = _spellingEngine.Tree.SearchForNode(kvp.Key);
                if (t != null)
                {
                    //sb.Append(t.Level.ToString("000") + "  " + t.Value.PadTo(30) + " FOUND" + g.crlf);
                    foundCount++;
                    if (searchCount % 1000 == 0)
                    {
                        TimeSpan ts = DateTime.Now - beginDT;
                        float tps = (float)searchCount / (float) ts.TotalSeconds;
                        lblStatus.Text = "Found     " + foundCount.ToString("###,##0") + "/" + searchCount.ToString("###,##0") + " TPS: " + tps.ToString("000.000") + "  " + kvp.Value;
                        Application.DoEvents();
                    }
                }
                else
                {
                    sb.Append("XXX  " + kvp.Key.PadTo(30) + " NOT FOUND" + g.crlf);
                }
            }

            TimeSpan tsEnd = DateTime.Now - beginDT;

            lblStatus.Text = "Found     " + foundCount.ToString("###,##0") + "/" + searchCount.ToString("###,##0") + "  Elapsed time: " + tsEnd.TotalSeconds.ToString("00.000000");
            Application.DoEvents();

            txtReports.Text = sb.ToString();
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            this.Cursor = Cursors.Default;
        }

        private void Randomize()
        {
            this.Cursor = Cursors.WaitCursor;
            string fileName = cboDctA.Text;

            if (_dctA == null)
            {
                MessageBox.Show("Dictionary A must be loaded first.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _spellingEngine.Tree = new TriNode(null, null, 'm');
            _spellingEngine.Tree.NodeAdded += Tree_NodeAdded;
            _spellingEngine.Tree.TotalNodeCount = 1;
            _spellingEngine.Tree.TotalLowNodeCount = 0;
            _spellingEngine.Tree.TotalEqualNodeCount = 1;
            _spellingEngine.Tree.TotalHighNodeCount = 0;
            _spellingEngine.Tree.InDiagnosticsMode = false;

            Random r = new Random();
            string nodeBeingAdded = String.Empty;
            int count = 0;
            List<string> dummyPatterns = new List<string>();

            SortedList<string, Adsdi.DCT.Entry> randomOrder = new SortedList<string, Adsdi.DCT.Entry>();

            lblStatus.Text = "Loading random table to optimize create random file."; 
            Application.DoEvents();

            foreach (KeyValuePair<string, Adsdi.DCT.Entry> kvp in _dctA)
            {
                count++;
                int rnd = r.Next(1000, 9999);
                string rndStr = rnd.ToString("0000");
                string key = rndStr + "~" + kvp.Key;
                randomOrder.Add(key, kvp.Value);
                if (count % 500 == 0)
                {
                    lblStatus.Text = "Loading random table to optimize create random file - loaded " + count.ToString("###,##0") + " items."; 
                    Application.DoEvents();
                }
            }

            count = 0;
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, Adsdi.DCT.Entry> kvp in randomOrder)
            {              
                string line = kvp.Value.Text.Trim() + "|" + kvp.Value.Source.Trim() + "|" + kvp.Value.Flags + "|" + kvp.Value.IsSpellingChecked.ToString().Substring(0, 1) +
                                                      "|" + kvp.Value.SpellingIsValid.ToString().Substring(0, 1);
                sb.Append(line + g.crlf);
                count++;
                if (count % 500 == 0)
                {
                    lblStatus.Text = "Loading item " + count.ToString("###,##0") + " : " + kvp.Value.Text;
                    Application.DoEvents();
                }
            }

            string fileOut = sb.ToString();
            File.WriteAllText(_dctPath + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-Random.txt", fileOut);

            txtReports.Text = fileOut;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            this.Cursor = Cursors.Default;
        }

        private void RandomizeFromFile()
        {
            this.Cursor = Cursors.WaitCursor;
            string fileName = cboDctA.Text;
            string fullPath = _dctPath + @"\" + fileName;

            if (!File.Exists(fullPath))
            {
                MessageBox.Show("File chosen '" + fullPath + "' does not exist.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _spellingEngine.Tree = new TriNode(null, null, 'm');
            _spellingEngine.Tree.NodeAdded += Tree_NodeAdded;
            _spellingEngine.Tree.TotalNodeCount = 1;
            _spellingEngine.Tree.TotalLowNodeCount = 0;
            _spellingEngine.Tree.TotalEqualNodeCount = 1;
            _spellingEngine.Tree.TotalHighNodeCount = 0;
            _spellingEngine.Tree.InDiagnosticsMode = false;

            List<string> dummyPatterns = new List<string>();

            StreamReader sr = new StreamReader(fullPath);

            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.IsNotBlank())
                {
                    string[] tokens = line.Split(Adsdi.GS.Constants.PipeDelimiter);
                    if (tokens[0] != null)
                    {
                        string key = tokens[0];
                        _spellingEngine.Tree.NodeBeingAdded = key;
                        TriNode addedNode = _spellingEngine.Tree.InsertNode(_spellingEngine.Tree, key, dummyPatterns, false);
                        _spellingEngine.Tree.IncludedLevels = String.Empty;
                        count++;
                        if (count % 500 == 0)
                        {
                            lblStatus.Text = "Loading TriNode item " + count.ToString("###,##0") + " : " + key;
                            Application.DoEvents();
                        }
                    }
                }
            }

            sr.Close();
            
            int foundCount = 0;
            int searchCount = 0;

            DateTime beginDT = DateTime.Now;

            sr = new StreamReader(fullPath);

            count = 0;
            while (!sr.EndOfStream)
            {
                count++;
                string line = sr.ReadLine();
                if (line.IsNotBlank())
                {
                    string[] tokens = line.Split(Adsdi.GS.Constants.PipeDelimiter);
                    if (tokens[0] != null)
                    {
                        searchCount++;
                        string key = tokens[0];
                        TriNode t = _spellingEngine.Tree.SearchForNode(key);
                        if (t != null)
                        {
                            foundCount++;
                            //if (searchCount % 1000 == 0)
                            //{
                            //    lblStatus.Text = "Found     " + foundCount.ToString("###,##0") + "/" + searchCount.ToString("###,##0") + "  " + key;
                            //    Application.DoEvents();
                            //}
                        }
                    }
                }
            }

            sr.Close();
            sr.Dispose();

            TimeSpan tsEnd = DateTime.Now - beginDT;

            lblStatus.Text = "Found     " + foundCount.ToString("###,##0") + "/" + searchCount.ToString("###,##0") + "  Elapsed time: " + tsEnd.TotalSeconds.ToString("00.000000");
            Application.DoEvents();

            txtReports.Text = File.ReadAllText(fullPath);
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            this.Cursor = Cursors.Default;
        }

        private void Tree_NodeAdded(TriNode triNode)
        {
        }

        private void ListFile(string command)
        {
            this.Cursor = Cursors.WaitCursor;

            ComboBox c;

            switch (command)
            {
                case "LOAD_DCT_B":
                    c = cboDctB;
                    break;

                default:
                    c = cboDctA;
                    break;
            }

            string fileName = c.Text;
            if (fileName.IsBlank())
            {
                MessageBox.Show("Please choose a dictionary file.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullPath = _dctPath + @"\" + fileName;
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(fullPath);

            int lineCount = 0;
            while (!sr.EndOfStream)
            {
                lineCount++;
                string line = sr.ReadLine();
                if (line.IsNotBlank())
                {
                    sb.Append(line + g.crlf);
                }
            }

            sr.Close();
            sr.Dispose();

            txtReports.Text = sb.ToString();

            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;

            tabMain.SelectedTab = tabPageReports;
            lblStatus.Text = "Entries: " + lineCount.ToString("#,###,##0");

            this.Cursor = Cursors.Default;
        }

        private void FilterOutSpadesAndBadSpelling(string command)
        {
            this.Cursor = Cursors.WaitCursor;

            Dct d = null;
            ComboBox c = null;
            TextBox t = null;
            TextBox st = null;
            string origSrc = "A";
            string dctName = "A";

            switch (command)
            {
                case "LOAD_DCT_A":
                    d = _dctA;
                    c = cboDctA;
                    t = txtDctA;
                    st = txtSourceLetterA;
                    dctName = "A";
                    break;

                case "LOAD_DCT_B":
                    d = _dctB;
                    c = cboDctB;
                    t = txtDctB;
                    st = txtSourceLetterB;
                    dctName = "B";
                    break;
            }


            origSrc = dctName;
            string srcLtr = st.Text.Trim();

            string fileName = c.Text;
            if (fileName.IsBlank())
            {
                MessageBox.Show("Please choose a dictionary file.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileLetter = fileName.Substring(0, 1);

            string fullPath = _dctPath + @"\" + fileName;

            d = new Dct();
            if (srcLtr.IsNotBlank())
                dctName = srcLtr;

            if (fileLetter.IsNumeric())
                dctName = fileLetter;

            d.Name = dctName;
            d.Load(fullPath, dctName);


            string fileOut = d.WriteFilteredToString();
            File.WriteAllText(_dctPath + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt", fileOut);

            txtReports.Text = fileOut;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            lblStatus.Text = "Dictionary " + d.Name + " Total: " + d.Count.ToString("#,###,##0") + "  FilteredOut: " + d.FilteredOutCount.ToString("#,###,##0");

            this.Cursor = Cursors.Default;
        }

        private void CompareDictionaries(string command)
        {
            this.Cursor = Cursors.WaitCursor;

            Dct dctBase = null;
            Dct dctComp = null;

            switch (command)
            {
                case "COMPARE_AB":
                    dctBase = _dctA;
                    dctComp = _dctB;
                    break;

                case "COMPARE_BA":
                    dctBase = _dctB;
                    dctComp = _dctA;
                    break;
            }

            if (dctBase == null || dctComp == null)
            {
                MessageBox.Show("Both dictionaries (A & B) must be loaded first.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtReports.Text = dctBase.Compare(dctComp);

            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            this.Cursor = Cursors.Default;
        }

        private void Combine()
        {
            this.Cursor = Cursors.WaitCursor;

            Dct dctBase = _dctA;
            Dct dctComp = _dctB;

            string srcA = txtSourceLetterA.Text;
            string srcB = txtSourceLetterB.Text;

            if (dctBase == null || dctComp == null)
            {
                MessageBox.Show("Both dictionaries (A & B) must be loaded first.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dct dctCombined = dctBase.Combine(dctComp, srcA, srcB);

            string fileOut = dctCombined.WriteToString();

            txtReports.Text = dctCombined.GetList() + g.crlf2 +
                "----------------------------------------------------------------------------" + g.crlf + fileOut;

            File.WriteAllText(_dctPath + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt", fileOut);

            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            int aCount = dctCombined.Where(e => e.Value.Source == "A").Count();
            int bCount = dctCombined.Where(e => e.Value.Source == "B").Count();

            lblStatus.Text = "Dictionary " + dctCombined.Name + "  'A' Entries: " + aCount.ToString("#,###,##0") + "  'B' Entries: " + bCount.ToString("#,###,##0") +
                             "  Total: " + dctCombined.Count.ToString("#,###,##0");

            this.Cursor = Cursors.Default;
        }

        private void SearchDictionaries(string command)
        {
            this.Cursor = Cursors.WaitCursor;

            Dct dct= null;
            string searchText = txtSearchText.Text;

            switch (command)
            {
                case "FIND_IN_A":
                    dct = _dctA;
                    break;

                default:
                    dct = _dctB;
                    break;
            }

            if (dct == null)
            {
                MessageBox.Show("Dictionary must be loaded first.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (searchText.IsBlank())
            {
                MessageBox.Show("Enter a non-blank search string.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dct.ContainsKey(searchText))
                lblFindResult.Text = "FOUND: " + searchText;
            else
                lblFindResult.Text = "NOT FOUND: " + searchText;

            this.Cursor = Cursors.Default;
        }

        private void GetVariants()
        {
            if (txtSearchText.Text.IsBlank())
                return;
             
            this.Cursor = Cursors.WaitCursor;

            string variant = String.Empty;
            string t = txtSearchText.Text.Trim().ToLower();
            List<string> variants = _spellingEngine.GetVariants(t);

            StringBuilder sb = new StringBuilder();
            foreach (string s in variants)
                sb.Append(s + g.crlf);

            string results = sb.ToString();

            txtReports.Text = results;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            lblStatus.Text = "Variant words created for '" + t + "' is " + variants.Count.ToString("##,##0") + ".";
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void RunFunctional()
        {
            this.Cursor = Cursors.WaitCursor;

            StringBuilder sb = new StringBuilder();

            MathFunction f = Math.Sin;
            double y = f(4);

            Func<double, double> f2 = Math.Sin;
            y = f2(4);

            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            Predicate<int> predicate = new Predicate<int>(i => i < 2);
            List<int> list2 = list.FindAll(predicate);

            List<int> list3 = list.FindAll(x => x > 3);

            sb.Append("Run functional.");            


            string results = sb.ToString();

            txtReports.Text = results;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            lblStatus.Text = "Functional code executed.";
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }

        private void ValidateEntries()
        {
            correctCount = 0;
            errorCount = 0;
            spellCheckCount = 0;

            int spellCheckLimit = 0;

            if (ckLimitSpellCheck.Checked)
            {
                if (!txtSpellCheckLimit.Text.IsNumeric())
                {
                    MessageBox.Show("Spell check limit must be a valid number.", "DictionaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    spellCheckLimit = Int32.Parse(txtSpellCheckLimit.Text);
                }
            }

            Adsdi.DCT.WordEngine wordEngine = new Adsdi.DCT.WordEngine();
            wordEngine.SpellingChecked += wordEngine_SpellingChecked;

            foreach (Adsdi.DCT.Entry e in _dctA.Values)
            {
                SpellingResult spellingResult = wordEngine.CheckSpelling(e.Text, ++spellCheckCount);
                e.IsSpellingChecked = true;
                e.SpellingIsValid = spellingResult.IsCorrect;
                e.SpellingSuggestions = spellingResult.Suggestions;

                if (e.SpellingIsValid)
                    correctCount++;
                else
                    errorCount++;

                percentCorrect = (float)correctCount / spellCheckCount * 100;

                if (ckLimitSpellCheck.Checked)
                {
                    if (spellCheckLimit > 0)
                        if (spellCheckCount > spellCheckLimit)
                            break;
                }

                System.Threading.Thread.Sleep(10);
            }

            txtReports.Text = _dctA.GetList();
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            wordEngine.Dispose();


            string stop = "stop here";

            List<string> suggestions = new List<string>();

            int suggestionAddedCount = 0;
            int suggestionCount = 0;

            foreach (Adsdi.DCT.Entry e in _dctA.Values)
            {
                foreach (string suggestion in e.SpellingSuggestions)
                {
                    suggestionCount++;
                    if (!_dctA.ContainsKey(suggestion) && !suggestions.Contains(suggestion))
                    {
                        System.Threading.Thread.Sleep(10);
                        SpellingResult spellingResult = wordEngine.CheckSpelling(suggestion, ++spellCheckCount);
                        if (spellingResult.IsCorrect)
                        {
                            suggestionAddedCount++;
                            suggestions.Add(suggestion);
                            lblFindResult.Text = suggestions.Count.ToString("###,##0") + "  " + e.Text;
                            Application.DoEvents();
                        }
                    }
                }
            }

            string stop2 = "stop";

            int toAddCount = suggestions.Count;
            int addedCount = 0;

            foreach (string s in suggestions)
            {
                if (!_dctA.ContainsKey(s))
                {
                    string[] tokens = new string[3];
                    tokens[0] = s;
                    tokens[1] = "S";
                    tokens[2] = String.Empty;
                    Adsdi.DCT.Entry e = new Adsdi.DCT.Entry(tokens, "S", 0);
                    e.IsSpellingChecked = true;
                    e.SpellingIsValid = true;
                    _dctA.Add(e.Text, e);
                    addedCount++;
                    lblFindResult.Text = addedCount.ToString("###,##0") + "/" + toAddCount.ToString("###,##0") + "  " + e.Text;
                    Application.DoEvents();
                }
            }

            string stop3 = "stop";


            string fileOut = _dctA.WriteToString();
            txtReports.Text = fileOut;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;
            File.WriteAllText(_dctPath + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt", fileOut);
        }

        private void GetAutoCorrectEntries()
        {
            this.Cursor = Cursors.WaitCursor;

            Adsdi.DCT.WordEngine wordEngine = new Adsdi.DCT.WordEngine();
            
            string entries = wordEngine.GetAutoCorrectEntries();

            txtReports.Text = entries;
            txtReports.SelectionStart = 0;
            txtReports.SelectionLength = 0;
            tabMain.SelectedTab = tabPageReports;

            this.Cursor = Cursors.Default;
        }

        private void SpellCheck()
        {
            if (_spellingEngine.Tree == null)
            {
                MessageBox.Show("The TriNode Spell Check collection must be loaded first.", "Dictionary Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            StringBuilder sb = new StringBuilder();

            string textToSpellCheck = txtReports.Text;
            SpellingPassage passage = new SpellingPassage(textToSpellCheck); 

            foreach (SpellingToken spellingToken in passage.SpellingTokens)
            {
                if (spellingToken.SpellCheckThisToken)
                {
                    _spellingEngine.CheckSpelling(spellingToken);
                    if (spellingToken.IsSpellingError)
                    {
                        if (spellingToken.SpellingSuggestions.Count == 0)
                            sb.Append("NOT CORRECT : " + spellingToken.TextChecked + "  - no suggestions" + g.crlf);
                        else
                            sb.Append("NOT CORRECT : " + spellingToken.TextChecked + "  (" + spellingToken.SpellingSuggestions.ToDelimitedList(",") + ")" + g.crlf);
                    }
                }
            }

            txtReports.Text = textToSpellCheck + g.crlf2 + "Spell Check Report" + g.crlf2 + sb.ToString();
        }

        private void wordEngine_SpellingChecked(string message)
        {
            lblStatus.Text = "Correct: " + percentCorrect.ToString("#00.00") + "%   " + correctCount.ToString("###,##0") + "/" + spellCheckCount.ToString("###,##0") + "  " + message;
            Application.DoEvents();
        }

        private void TerminateApplication()
        {
            this.Close();
        }

        private void ctxMenuAddToDictionary_Opening(object sender, CancelEventArgs e)
        {
            string word = txtReports.SelectedText.Trim();
            if (word.IsBlank())
            {
                e.Cancel = true;
                return;
            }
        }

        private void AddToDictionary()
        {
            string word = txtReports.SelectedText.Trim();

            if (word.IsBlank())
                return;

            this.Cursor = Cursors.WaitCursor;
            string fileName = cboDctA.Text;
            string fullPath = _dctPath + @"\" + fileName;

            if (!File.Exists(fullPath))
            {
                MessageBox.Show("Dictionary file '" + fullPath + "' could not be found.", "DictonaryManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lineToAdd = word + "||T|T" + g.crlf;

            File.AppendAllText(fullPath, lineToAdd);

            List<string> dummyPatterns = new List<string>();
            _spellingEngine.Tree.NodeBeingAdded = word;
            TriNode addedNode = _spellingEngine.Tree.InsertNode(_spellingEngine.Tree, word, dummyPatterns, false);

            this.Cursor = Cursors.Default;
        }

    }
}
