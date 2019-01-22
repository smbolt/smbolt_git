using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Org.GS.OrgScript.Compilation;
using Org.GS;

namespace Org.OrgScriptWorkbench
{
  public partial class frmMain : Form
  {
    private CompilationJob _compilationJob;

    private bool _firstShowing = true;
    protected static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();


    TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
    TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
    TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
    TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
    TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
    TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
    TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
    MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

    private Style StringStyle { get; set; }
    private Style CommentStyle { get; set; }
    private Style NumberStyle { get; set; }
    private Style KeywordStyle { get; set; }


    protected Regex JScriptCommentRegex1,
                  JScriptCommentRegex2,
                  JScriptCommentRegex3;

    protected Regex JScriptKeywordRegex;
    protected Regex JScriptNumberRegex;
    protected Regex JScriptStringRegex;


    public static RegexOptions RegexCompiledOption
    {
      get
      {
        if (platformType == Platform.X86)
          return RegexOptions.Compiled;
        else
          return RegexOptions.None;
      }
    }

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object c, EventArgs e)
    {
      switch (c.ActionTag())
      {
        case "Save":
          Save();
          break;

        case "Compile":
          Compile();
          break;

        case "DebugCompile":
          DebugCompile();
          break;

        case "StepForward":
          DebugCompileStepForward();
          break;

        case "Execute":
          Execute();
          break;

        case "Run":
          Run();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Save()
    {


      txtConsole.Text = "File saved.";
      tabDrawer.SelectedTab = tabPageConsole;
    }

    private void Compile()
    {
      MessageBox.Show("The 'Compile' process is not yet implemented.", g.AppInfo.AppName + " - Function Not Yet Implemented",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
      return;


      //string rawText = txtCode1.Text;

      //_compilationJob = new CompilationJob(rawText);


      //var compiler = new Org.GS.OrgScript.Compilation.Compiler(rawText);

      ////var result = compiler.Compile();




      //txtConsole.Text = compiler.SyntaxNodeReport;
      //tabDrawer.SelectedTab = tabPageConsole;
    }

    private void DebugCompile()
    {
      string rawText = txtCode1.Text;

      _compilationJob = new CompilationJob(rawText);

      txtConsole.Text = _compilationJob.Report;
      tabDrawer.SelectedTab = tabPageConsole;
    }

    private void DebugCompileStepForward()
    {
      try
      {
        _compilationJob.StepForward();


      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred while attempting to step forward in the compilation debug session." + ex.ToReport();
        txtConsole.Text = errorMessage;
        MessageBox.Show(errorMessage, g.AppInfo.AppName + " - Compilation Debug Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Execute()
    {

      txtConsole.Text = "Execution complete.";
      tabDrawer.SelectedTab = tabPageConsole;
    }

    private void Run()
    {

      txtConsole.Text = "Run complete.";
      tabDrawer.SelectedTab = tabPageConsole;
    }


    public void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + ex.ToReport(),
                        "OrgScript Workbench - Error Initializing of Application Object 'a'",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        InitJScriptRegex();
        txtCode1.TabLength = 2;


        this.SetInitialSizeAndLocation();

        splitMain.SplitterDistance = 210;
        splitCode.SplitterDistance = 500;

        txtCode1.Text = File.ReadAllText(g.ImportsPath + @"\index.js");

        btnStepForward.Visible = false;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + ex.ToReport(),
                        "OrgScript Workbench - Error During Application Initialization",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (_firstShowing)
        RunFirstShowing();
    }

    private void RunFirstShowing()
    {
      txtCode1.Focus();

      _firstShowing = false;
    }

    private void txtCode1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      CSharpSyntaxHighlight(e);
      //JScriptSyntaxHighlight(e.ChangedRange);
    }

    private void CSharpSyntaxHighlight(TextChangedEventArgs e)
    {
      txtCode1.LeftBracket = '(';
      txtCode1.RightBracket = ')';
      txtCode1.LeftBracket2 = '\x0';
      txtCode1.RightBracket2 = '\x0';
      //clear style of changed range
      e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

      //string highlighting
      e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
      //comment highlighting
      e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
      e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
      e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
      //number highlighting
      e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
      //attribute highlighting
      e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
      //class name highlighting
      e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
      //keyword highlighting
      e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

      //clear folding markers
      e.ChangedRange.ClearFoldingMarkers();

      //set folding markers
      e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
      e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
      e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
    }


    public virtual void JScriptSyntaxHighlight(Range range)
    {
      range.tb.CommentPrefix = "//";
      range.tb.LeftBracket = '(';
      range.tb.RightBracket = ')';
      range.tb.LeftBracket2 = '{';
      range.tb.RightBracket2 = '}';
      range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;

      range.tb.AutoIndentCharsPatterns
          = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;]+);
";

      //clear style of changed range
      range.ClearStyle(StringStyle, CommentStyle, NumberStyle, KeywordStyle);
      //
      if (JScriptStringRegex == null)
        InitJScriptRegex();
      //string highlighting
      range.SetStyle(StringStyle, JScriptStringRegex);
      //comment highlighting
      range.SetStyle(CommentStyle, JScriptCommentRegex1);
      range.SetStyle(CommentStyle, JScriptCommentRegex2);
      range.SetStyle(CommentStyle, JScriptCommentRegex3);
      //number highlighting
      range.SetStyle(NumberStyle, JScriptNumberRegex);
      //keyword highlighting
      range.SetStyle(KeywordStyle, JScriptKeywordRegex);
      //clear folding markers
      range.ClearFoldingMarkers();
      //set folding markers
      range.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
      range.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block
    }


    private void InitJScriptRegex()
    {
      JScriptStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'", RegexCompiledOption);
      JScriptCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
      JScriptCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline | RegexCompiledOption);
      JScriptCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                                       RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);
      JScriptNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                                     RegexCompiledOption);
      JScriptKeywordRegex =
          new Regex(
              @"\b(true|false|break|case|catch|const|continue|default|delete|do|else|export|for|function|if|in|instanceof|new|null|return|switch|this|throw|try|var|void|while|with|typeof)\b",
              RegexCompiledOption);
    }

  }
}
