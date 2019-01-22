using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.XmlUtility
{
  public partial class frmMain : Form
  {
    private string _folderPath;
    private string _fileName;
    private XElement _originalXml;
    private XElement _xml;
    private XElement _queryResult;

    private List<string> _xpathExpressions;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Browse":
          BrowseForFile();
          break;

        case "LoadFile":
          LoadFile();
          break;

        case "ClearXml":
          txtXmlFile.Text = String.Empty;
          break;

        case "RunQuery":
          RunQuery();
          break;

        case "Back":
          if (_originalXml != null)
          {
            _xml = XElement.Parse(_originalXml.ToString());
            LoadTreeView(_xml);
          }
          break;

        case "AddToList":
          AddToList();
          break;

        case "DeleteFromList":
          DeleteFromList();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunQuery()
    {
      try
      {
        var r = _xml.RunXPathQuery(cboXPathExpression.Text, true);

        switch (r.GetType().Name)
        {
          case "XElement":
            LoadTreeView((XElement)r);
            break;

          case "Exception":
            MessageBox.Show("An exception occurred while running the XPath query." + g.crlf2 + ((Exception)r).ToReport(),
                            g.AppInfo.AppName + " - XPath Query Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;

          default:
            MessageBox.Show("An unexpected result was returned from running the XPath query." + g.crlf2 +
                            "Return type is '" + r.GetType().Name + "'" + g.crlf2 +
                            "Object is '" + r.ToString() + "'.",
                            g.AppInfo.AppName + " - XPath Query Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            break;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to run the XPath expression." + g.crlf + ex.ToReport(),
                        g.AppInfo.AppName + " XPath Expression Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void LoadFile()
    {
      try
      {
        string xml = File.ReadAllText(txtXmlFile.Text);
        if (!xml.IsValidXml())
        {
          _xml = null;
          LoadTreeView(null);

          MessageBox.Show("The data in the file is not valid XML.",
                          g.AppInfo.AppName + " Error Loading XML File.", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        _xml = xml.ToXElement();
        _originalXml = XElement.Parse(_xml.ToString());

        LoadTreeView(_xml);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to load the XML file." + g.crlf + ex.ToReport(),
                        g.AppInfo.AppName + " Error Loading XML File.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void BrowseForFile()
    {
      string holdFolderPath = _folderPath;
      string holdFileName = _fileName;

      if (_folderPath.IsBlank())
        _folderPath = @"C:\";

      dlgFileOpen.InitialDirectory = _folderPath;
      dlgFileOpen.Title = "Locate XML File";

      if (dlgFileOpen.ShowDialog() == DialogResult.OK)
      {
        string fullPath = dlgFileOpen.FileName;
        _folderPath = Path.GetDirectoryName(fullPath);
        _fileName = Path.GetFileName(fullPath);

        if (_folderPath != holdFolderPath || _fileName != holdFileName)
        {
          g.AppConfig.SetCI("FolderPath", _folderPath);
          g.AppConfig.SetCI("FileName", _fileName);
          g.AppConfig.Save();
        }

        txtXmlFile.Text = _folderPath + @"\" + _fileName;
      }
    }


    private void LoadTreeView(XElement xml, string selectedName = "")
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        tvXml.Nodes.Clear();
        tvXml.Visible = false;
        Application.DoEvents();

        if (xml == null)
        {
          btnRunQuery.Enabled = false;
          return;
        }

        tvXml.BeginUpdate();

        XElement rootElement = xml;

        TreeNode rootNode = new TreeNode(rootElement.Expanded());
        rootNode.Tag = rootElement;
        tvXml.Nodes.Add(rootNode);

        foreach (var childElement in rootElement.Elements())
        {
          LoadTreeView(childElement, rootNode, 0);
        }

        foreach (XNode childNode in rootElement.Nodes())
        {
          if (childNode.NodeType == System.Xml.XmlNodeType.Text)
          {
            var textElement = new XElement("Text", ((XText)childNode).Value);
            LoadTreeView(textElement, rootNode, 0);
          }
        }

        tvXml.EndUpdate();
        tvXml.ExpandAll();
        rootNode.EnsureVisible();
        tvXml.Visible = true;

        Application.DoEvents();

        //if (selectedName.IsNotBlank())
        //  SelectTreeViewNode(tvXml.Nodes[0], selectedName);
        //else
        //  tvXml.SelectedNode = rootNode;

        btnRunQuery.Enabled = true;
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load the services objects to the TreeView." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - TreeView Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(XElement e,  TreeNode parentNode, int level)
    {
      TreeNode node = new TreeNode(e.Expanded());
      node.Tag = e;
      parentNode.Nodes.Add(node);

      foreach (var childElement in e.Elements())
      {
        LoadTreeView(childElement, node, 0);
      }
    }

    private void AddToList()
    {
      if (!_xpathExpressions.Contains(cboXPathExpression.Text))
      {
        _xpathExpressions.Add(cboXPathExpression.Text);
        cboXPathExpression.LoadItems(_xpathExpressions);
      }
    }

    private void DeleteFromList()
    {
      if (_xpathExpressions.Contains(cboXPathExpression.Text))
      {
        _xpathExpressions.Remove(cboXPathExpression.Text);
        cboXPathExpression.LoadItems(_xpathExpressions);
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to initialize the application 'a' object." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " Error Initializing Application Object.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
      int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                           Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

      _xpathExpressions = g.GetList("XPathExpressions");
      cboXPathExpression.LoadItems(_xpathExpressions, true);

      btnLoadFile.Enabled = false;
      btnRunQuery.Enabled = false;

      InitializeFilePath();
    }

    private void InitializeFilePath()
    {
      _folderPath = g.CI("FolderPath");
      _fileName = g.CI("FileName");

      if (_folderPath.IsBlank() && !Directory.Exists(_folderPath))
      {
        _folderPath = String.Empty;
        _fileName = String.Empty;
      }

      string fullFilePath = _folderPath + @"\" + _fileName;

      if (!File.Exists(fullFilePath))
      {
        _fileName = String.Empty;
      }

      if (_folderPath.IsBlank())
        _folderPath = @"C:\";

      g.AppConfig.SetCI("FolderPath", _folderPath);
      g.AppConfig.SetCI("FileName", _fileName);
      g.AppConfig.Save();

      if (_folderPath.IsNotBlank() && _fileName.IsNotBlank() && File.Exists(_folderPath + @"\" + _fileName))
        txtXmlFile.Text = _folderPath + @"\" + _fileName;
    }

    private void txtXmlFile_TextChanged(object sender, EventArgs e)
    {
      btnLoadFile.Enabled = File.Exists(txtXmlFile.Text);
    }
  }
}
