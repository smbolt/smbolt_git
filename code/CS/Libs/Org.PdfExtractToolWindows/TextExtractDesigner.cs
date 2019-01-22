using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Dx.Business.TextProcessing;
using Org.TW.ToolPanels;
using Org.TW;
using Org.GS;

namespace Org.PdfExtractToolWindows
{
  public partial class TextExtractDesigner : ToolPanelBase
  {
    private Text _debugText;

    private FileFormat _fileFormat;
    private string _rawText;

    private RecogSpec _recogSpec;
    private ExtractSpec _extractSpec;
    private Text _text;

    private TreeNode _selectedNode;
    private Text _selectedTextObject;
    private bool _loadingTreeView;

    private bool _workingHere = false;
    private bool _errorOccurred = false;
    
    public TextExtractDesigner()
      : base("TextExtractDesigner")
    {
      InitializeComponent();
      InitializeControl();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "EditConfig":
        case "UpdateResults":
          RefreshViews();
          break;

        case "Reprocess":
          Reprocess();
          break;

        case "WorkHere":
          WorkHere();
          break;

        case "Init":
          Init();
          break;

        case "Step":
          Step();
          break;

        case "Run":
          Run();
          break;

        case "ShowTextOnly":
          ShowTextOnly();
          break;

        case "ShowAllNodes":
          ShowAllNodes();
          break;

        case "ShowOnlyThisNodeType":
          ShowOnlyThisNodeType(sender);
          break;
      }

    }

    private void Init()
    {
      if (tvDocStructure.SelectedNode == null || tvDocStructure.SelectedNode.Tag == null)
        return;

      var text = tvDocStructure.SelectedNode.Tag as Text;
      _debugText = text.Clone();

      txtTextValue.Text = _debugText.RawText;
      txtConfig.Text = _debugText.TsdCode; 
    }

    private void Step()
    {

    }

    private void Run()
    {
      this.Cursor = Cursors.WaitCursor;

      _text.ExtractData(10);

      var sb = new StringBuilder();
      var s = String.Empty;

      if (_text.CxExceptionList.Count > 0)
      {
        foreach (CxException cx in _text.CxExceptionList)
        {
          s = cx.ToReport();
          sb.Append(s);
        }
        _text.ExtractionErrorReport = sb.ToString();
      }

      LoadTextObjectToTreeView(_text, String.Empty, -1);

      this.Cursor = Cursors.Default;
    }

    public void LoadTextObject(RecogSpec recogSpec, ExtractSpec extractSpec, Text text)
    {
      _recogSpec = recogSpec;
      _extractSpec = extractSpec;
      _text = text;

      if (tvDocStructure.SelectedNode == null)
        return;

      TreeNode selectedNode = tvDocStructure.SelectedNode;
      TreeNode node = selectedNode;
      string treeNodePath = node.Text;
      while (node.Parent != null)
      {
        treeNodePath = node.Parent.Text + "/" + treeNodePath;
        node = node.Parent;
      }

      LoadTextObjectToTreeView(_text, String.Empty, -1);

      if (tvDocStructure.Nodes.Count == 0)
        return;

      string[] pathTokens = treeNodePath.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
      selectedNode = null;

      int selectionLevel = pathTokens.Length - 1;
      var nodeSet = tvDocStructure.Nodes;
      int currLevel = -1;

      while (currLevel < selectionLevel)
      {
        currLevel++;
        string nodeText = pathTokens[currLevel];
        TreeNode currLevelNode = null;
        foreach (TreeNode n in nodeSet)
        {
          if (n.Text.IsNotBlank() && n.Text == nodeText)
          {
            currLevelNode = n;
            break;
          }
        }

        if (currLevelNode == null)
          break;

        if (currLevel == selectionLevel)
        {
          tvDocStructure.SelectedNode = currLevelNode;
          break;
        }
        else
        {
          nodeSet = currLevelNode.Nodes;
        }
      }
    }

    private void Reprocess()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _errorOccurred = false;

        base.NotifyHost(this, ToolPanelHostCommand.ReloadConfigs, null);

        if (_extractSpec == null)
        {
          MessageBox.Show("The ExtractSpec is null - the format of the data may not be recognized or an ExtractSpec may not be defined for the format.",
                          "PDF Text Extraction - Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        this.Cursor = Cursors.WaitCursor;

        _text.ProcessStructureDefinition(_extractSpec);
        _text.ExtractData(10);

        var sb = new StringBuilder();
        var s = String.Empty;

        if(_text.CxExceptionList.Count > 0)
        {
          foreach(CxException cx in _text.CxExceptionList)
          {
            if (sb.Length > 0)
              sb.Append("**********************************************************************************************************************************************" +
                        "**********************************" + g.crlf2);
            s = cx.ToReport();
            sb.Append(s);
          }
          _text.ExtractionErrorReport = sb.ToString();
        }
        
        LoadTextObjectToTreeView(_text, String.Empty, -1);

        this.Cursor = Cursors.Default;
      }
      catch (CxException cx)
      {
        _errorOccurred = true;
        if (_text != null)
        {
          _text.ExtractionErrorReport = cx.ToReport();
          LoadTextObjectToTreeView(_text, String.Empty, -1);
        }

        this.Cursor = Cursors.Default;

      }
      catch (Exception ex)
      {
        _errorOccurred = true;
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load the text lines to the grid." + g.crlf2 +
                        ex.ToReport(), "Text Extract Designer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void WorkHere()
    {
      int availableHeight = splitterMain.Panel2.Height;
      int textPanelHeight = Convert.ToInt32(availableHeight * 0.32F);
      int configPanelHeight = Convert.ToInt32(availableHeight * 0.32F);
      splitterRight.Panel2Collapsed = false;
      splitterRight.SplitterDistance = textPanelHeight;
      splitterRightBottom.SplitterDistance = configPanelHeight;

      btnWorkHere.Text = "Show Text Only";
      btnWorkHere.Tag = "ShowTextOnly";

      _workingHere = true;

      RefreshViews();
    }

    private void ShowTextOnly()
    {
      splitterRight.Panel2Collapsed = true;

      btnWorkHere.Text = "Work Here";
      btnWorkHere.Tag = "WorkHere";
      _workingHere = false;

      RefreshViews();
    }


    private void InitializeControl()
    {
      _selectedNode = null;
      _selectedTextObject = null;

      splitterRight.Panel2Collapsed = true;
      
      InitializeTreeViewImageList();
    }

    private void ShowAllNodes()
    {
      if (_text == null)
        return;

      LoadTextObjectToTreeView(_text, String.Empty, -1); 
    }

    private void ShowOnlyThisNodeType(object sender)
    {
      if (_text == null)
        return;

      var node = tvDocStructure.SelectedNode;
      if (node == null)
        return;

      string nodeText = node.Text; 

      int level = 0;
      var theNode = node;
      while (theNode.Parent != null)
      {
        level++;
        theNode = theNode.Parent;
      }

      LoadTextObjectToTreeView(_text, nodeText, level); 
    }

    private void LoadTextObjectToTreeView(Text t, string nodeTextToShow, int level)
    {
      _loadingTreeView = true;

      tvDocStructure.Nodes.Clear();

      string rootName = "Root";
      if (t.Name.IsNotBlank())
        rootName = t.Name;
      
      TreeNode rootNode = new TreeNode(rootName, 0, 1);
      rootNode.Tag = _text;
      tvDocStructure.Nodes.Add(rootNode);

      foreach (var childText in t.TextSet.Values)
      {
        LoadTextObjectToTreeView(childText, rootNode, nodeTextToShow, level);
      }
      
      tvDocStructure.ExpandAll();

      TreeNode selectedNode = rootNode;
      if (rootNode.Nodes.Count > 0)
        selectedNode = rootNode.Nodes[0];

      tvDocStructure.SelectedNode = selectedNode;
      tvDocStructure.Nodes[0].EnsureVisible();
      
      _loadingTreeView = false;
    }

    private void LoadTextObjectToTreeView(Text t, TreeNode parentNode, string nodeText, int level)
    {
      string nodeName = "Text";
      if (t.Name.IsNotBlank())
        nodeName = t.Name;

      var treeNode = new TreeNode(nodeName, 0, 1);
      treeNode.Tag = t;

      string compareNodeText = nodeText.Split(Constants.OpenBracket).First();
      string compareNodeName = nodeName.Split(Constants.OpenBracket).First();

      if (level == -1 || compareNodeText == compareNodeName)
      {
        parentNode.Nodes.Add(treeNode);
        foreach (var childText in t.TextSet.Values)
        {
          LoadTextObjectToTreeView(childText, treeNode, nodeText, level);
        }
      }
      else
      {
        foreach (var childText in t.TextSet.Values)
        {
          LoadTextObjectToTreeView(childText, parentNode, nodeText, level);
        }
      }

    }

    private void InitializeTreeViewImageList()
    {
      try
      {
        imgListTreeView.Images.Clear();
        imgListTreeView.ImageSize = new Size(16, 16);


        var resourceManager = new ResourceManager("Org.PdfExtractToolWindows.Resource1", Assembly.GetExecutingAssembly());

        var textNormal = (Icon)resourceManager.GetObject("text_normal");
        imgListTreeView.Images.Add("text_normal", textNormal);
        var textSelected = (Icon)resourceManager.GetObject("text_selected");
        imgListTreeView.Images.Add("text_selectted", textSelected); 
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the image list for the tree view.", ex); 
      }
    }

    private void tvDocStructure_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Node == null)
        return;

      _selectedNode = e.Node;

      if (_selectedNode.Tag == null)
      {
        txtTextValue.Text = "No text value";
        return;
      }

      var text = _selectedNode.Tag as Text;
      if (text.ExtractSpec == null && _extractSpec != null)
        text.ExtractSpec = _extractSpec;

      lblTextStructure.Text = text.Description;

      if (_selectedNode.Text.StartsWith("ReportUnit[") || _selectedNode.Parent == null)
      {
        switch (text.FileType)
        {
          case FileType.PDF:
            txtTextValue.Text = text.Report.Replace("SEC_END", "SEC_END\n"); 
            break;

          case FileType.XML:
            if (text.XElement != null)
              txtTextValue.Text = text.XElement.ToString();
            else
              txtTextValue.Text = "XElement is null";
            break;
        }
      }
      else
      {
        txtTextValue.Text = text.Report;
      }

      string t = txtTextValue.Text.Replace("\r\n", "\n");
      string[] lines = t.Split(Constants.NewLineDelimiter);
      int lineNbr = 0;
      var sb = new StringBuilder();
      for (int i = 0; i < lines.Length; i++)
      {
        if (!lines[i].StartsWith("SEC_"))
        {
          lineNbr++;
          string[] tokens = lines[i].Split(Constants.SpaceDelimiter);
          sb.Append(lineNbr.ToString() + ":" + tokens.Length.ToString() + "  ");
        }
      }

      string tokenCounts = sb.ToString().Trim();

      lblTextStructure.Text += "    Line Tokens: " + tokenCounts;

      _selectedNode.BackColor = Color.DodgerBlue;
      _selectedNode.ForeColor = Color.White;

      RefreshViews(true);
    }
    
    private void tvDocStructure_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      lblTextStructure.Text = String.Empty;

      if (tvDocStructure.SelectedNode == null)
        return;

      tvDocStructure.SelectedNode.BackColor = Color.White;
      tvDocStructure.SelectedNode.ForeColor = Color.Black;
    }

    private void tvDocStructure_Click(object sender, EventArgs e)
    {
      string typeName = sender.GetType().Name;
      var mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs == null)
        return;

      var hitText = tvDocStructure.HitTest(mouseEventArgs.Location);

      if (hitText.Node == null)
        return;

      tvDocStructure.SelectedNode = hitText.Node;
    }

    private void RefreshViews(bool suppressSwitchToErrorTab = false)
    {
      if (tvDocStructure.SelectedNode == null || tvDocStructure.SelectedNode.Tag == null)
      {
        txtTextValue.Text = String.Empty;
        txtConfig.Text = String.Empty;
        txtExtract.Text = String.Empty;
      }
      else
      {
        var selectedNode = tvDocStructure.SelectedNode;
        var text = selectedNode.Tag as Text;
        var tsd = text.Tsd;

        if (_workingHere)
        {
          txtTextValue.Text = text.RawText;
          txtConfig.Text = text.TsdCode;
          txtExtract.Text = String.Empty;
        }
        else
        {
          var cfgToolPanelUpdateParms = new ToolPanelUpdateParms();
          cfgToolPanelUpdateParms.TextConfigType = TextConfigType.TextStructureAndExtract;
          cfgToolPanelUpdateParms.ConfigFileFullPath = _extractSpec.FullFilePath;
          cfgToolPanelUpdateParms.Command = "LoadTsd";
          cfgToolPanelUpdateParms.ConfigFullXmlPath = tsd == null ? "root:" + _extractSpec.Name : tsd.FullXmlPath;
          cfgToolPanelUpdateParms.ToolPanelName = "ConfigEdit";
          base.NotifyHost(this, ToolPanelHostCommand.UpdateToolWindow, cfgToolPanelUpdateParms);

          var resultsToolPanelUpdateParms = new ToolPanelUpdateParms();
          resultsToolPanelUpdateParms.Command = "LoadResults";
          resultsToolPanelUpdateParms.ConfigFullXmlPath = "root";
          resultsToolPanelUpdateParms.ToolPanelName = "TextExtractResults";
          base.NotifyHost(this, ToolPanelHostCommand.UpdateToolWindow, resultsToolPanelUpdateParms);

          var errorsToolPanelUpdateParms = new ToolPanelUpdateParms();
          errorsToolPanelUpdateParms.Command = "LoadErrors";
          errorsToolPanelUpdateParms.ToolPanelName = "TextExtractErrors";
          errorsToolPanelUpdateParms.SuppressSwitchToErrorTab = !_errorOccurred;
          base.NotifyHost(this, ToolPanelHostCommand.UpdateToolWindow, errorsToolPanelUpdateParms);
          _errorOccurred = false;
        }
      }
    }

    public void ManageBreakpoints(bool breakpointEnabled, bool keepBreakpointsEnabled)
    {
      Org.Dx.Business.TextProcessing.Text.BreakpointEnabled = breakpointEnabled;
      Org.Dx.Business.TextProcessing.Text.KeepBreakpointEnabled = keepBreakpointsEnabled;
    }


  }
}
