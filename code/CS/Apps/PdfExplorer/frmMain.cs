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
using Org.Pdf;
using Org.GS;

namespace Org.PdfExplorer
{
  public partial class frmMain : Form
  {
    private a a;
    private bool _firstShowing = true;
    private string _appName = "PDF Explorer";
    private Document _doc;

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
        case "OpenDocument":
          OpenDocument();
          break;

        case "SwitchView":
          SwitchView();
          break;

        case "ExpandAll":
          ExpandAll();
          break;

        case "CollapseAll":
          CollapseAll();
          break;

        case "FilterObjects":
          LoadTreeView(_doc);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void OpenDocument()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        string documentPath = Directory.GetFiles(g.ImportsPath).ToList().First();

        _doc = new Document(documentPath, txtObjectNumberBreak.Text);
        LoadTreeView(_doc);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurrerd while attempting to open the PDF document." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Opening Document", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(Document doc)
    {
      try
      {
        tvMain.Nodes.Clear();
        TreeNode rootNode = null;
        Application.DoEvents();

        this.Cursor = Cursors.WaitCursor;

        tvMain.SuspendLayout();
        tvMain.BeginUpdate();

        if (doc == null)
        {
          rootNode = new TreeNode("No document loaded");
          tvMain.Nodes.Add(rootNode);
          return;
        }

        rootNode = new TreeNode("doc-root (doc)");
        rootNode.Tag = doc;

        tvMain.Nodes.Add(rootNode);

        foreach (var pageKvp in doc.PageSet)
        {
          LoadTreeView(rootNode, pageKvp.Value);
        }

        rootNode.ExpandAll();
        tvMain.EndUpdate();
        tvMain.ResumeLayout();

        tvMain.SelectedNode = rootNode;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurrerd while attempting to load the document to the TreeView." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Loading TreeView", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(TreeNode parentNode, PObject o)
    {
      string objectType = o.PObjectType.ToString();
      if (o.IsImage)
        objectType = "Image";

      var node = new TreeNode(o.PObjectNumber.ToString("00000") + " " + o.Name + " (" + objectType + ")");
      node.Tag = o;
      parentNode.Nodes.Add(node);

      if (o.HasIndirectReference)
      {
        var indirectNode = new TreeNode("=>" + o.IndirectReferenceDisplay);
        indirectNode.Tag = o;
        node.Nodes.Add(indirectNode);
      }

      foreach (var childObject in o.ChildObjects.Values)
      {
        if (childObject.Name.In("/Annots,/CropBox,/MediaBox") && ckOmitAnnotations.Checked)
          continue;

        if (ckOnlyImages.Checked && !(childObject.ContainsAnImage || childObject.IsDecendentOfImage))
          continue;

        LoadTreeView(node, childObject);
      }
    }

    private void ExpandAll()
    {
      this.Cursor = Cursors.WaitCursor;
      tvMain.SuspendLayout();
      tvMain.BeginUpdate();
      tvMain.ExpandAll();
      tvMain.EndUpdate();
      tvMain.ResumeLayout();
      this.Cursor = Cursors.Default;
    }

    private void CollapseAll()
    {
      this.Cursor = Cursors.WaitCursor;
      tvMain.CollapseAll();
      this.Cursor = Cursors.Default;
    }

    private void SwitchView()
    {
      if (tabMain.SelectedTab == tabPagePdfImage)
        tabMain.SelectedTab = tabPagePdfStructure;
      else
        tabMain.SelectedTab = tabPagePdfImage;
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        _appName + " - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);


        tabMain.SelectedTab = tabPagePdfStructure;
        txtPdfStructure.Text = "PDF Structure";

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        _appName + " - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void frmMain_ResizeEnd(object sender, EventArgs e)
    {
      tabMain.Location = new Point(-8, -8);
      tabMain.Size = new Size(splitterMain.Panel2.ClientRectangle.Width + 12,
                              splitterMain.Panel2.ClientRectangle.Height + 12);
    }

    private void ResizeAdjustments()
    {
      tabMain.Location = new Point(-8, -8);
      tabMain.Size = new Size(splitterMain.Panel2.ClientRectangle.Width + 12,
                              splitterMain.Panel2.ClientRectangle.Height + 12);
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_firstShowing)
        return;

      ResizeAdjustments();

      _firstShowing = false;
    }

    private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
    {
      var treeNode = e.Node as TreeNode;
      if (treeNode == null || treeNode.Tag == null)
      {
        txtPdfStructure.Text = "TreeNode is null.";
        return;
      }

      if (treeNode.Text.StartsWith("doc-root"))
      {
        txtPdfStructure.Text = "PDF Document";
        return;
      }

      var pdfObject = treeNode.Tag as PObject;

      txtPdfStructure.Text = pdfObject.DisplayText;

      if (pdfObject.IsImage)
      {
        var pdfImage = pdfObject.PdfImage;
        SetImageInTab(pdfImage);
      }
    }

    private void SetImageInTab(PdfImage pdfImage)
    {
      try
      {
        pbPdfImage.Size = pdfImage.Size;
        pnlPdfImageShadow.Size = pbPdfImage.Size;
        pbPdfImage.Image = pdfImage.Image;
        tabMain.SelectedTab = tabPagePdfImage;
        Application.DoEvents();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception while attempting to place an image in the tab control." + g.crlf2 + ex.ToReport(),
                        _appName + " - Error Processing Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
