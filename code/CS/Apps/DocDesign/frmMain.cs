using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using Org.SF;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.UI;
using Org.TW;
using Org.TW.ToolPanels;
using Org.TW.Forms;
using Ds = Org.DocGen.DocSpec;
using Org.DocGen;

namespace Org.DocDesign
{
  public partial class frmMain : frmToolWindowParent
  {
    private a a;

    // File Paths
    private string _imgPath;
    private string _docPath;
    private string _wordFilePath;
    private string _packagePath;
    private string _resumeDocs;
    private string _hyphenationData;
    private string _dictionaryData;
    private string _resumeOutXml;

    private Dictionary<string, frmToolWindowBase> toolWindows;
    private Dictionary<string, Panel> toolWindowDockingTargets;

    // Panels for Tool Windows
    private ToolPanelBase tpTreeView;
    private ToolPanelBase tpText;
    private ToolPanelBase tpInfo;
    private ToolPanelBase tpControlPanel;
    private ToolPanelBase tpProperties;
    private ToolPanelBase tpImage;
    private ToolPanelBase tpDebug;
    private Dictionary<string, ToolPanelBase> toolPanels = null;

    private UIState _uiState;
    private float _scale = 100.0F;
    private bool propertiesShown = false;
    private bool textPanelShown = false;
    private PictureBox pb;
    private Image _emptyCell;

    private string _docValidationReport;
    private string _documentXmlString;
    private XElement _documentXml;
    private XElement _stylesXml;
    private string _xmlMap = String.Empty;
    private string _map = String.Empty;
    private ImageEngine _imgEngine = null;
    private Ds.DocPackage _package = null;
    private Ds.DocumentElement selectedElement = null;
    private Ds.PageSet _pageSet = null;

    private frmTextDisplay fTextDisplay = null;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      if (action.StartsWith("TW_"))
      {
        ToolWindowAction(action);
        return;
      }

      switch (action)
      {
        case "GenerateDocument":
          this.GenerateDocument();
          break;

        case "ZoomIn":
        case "ZoomOut":
          this.Zoom(action);
          break;

        case "MarkOverlay":
          MarkOverlay();
          break;

        case "ClearOverlay":
          ClearOverlay();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void GenerateDocument()
    {
      lblStatus.Text = "Document generation in progress...";
      Application.DoEvents();

      _package = GeneratePackage(cboPackages.SelectedItem.ToString());

      if (_package == null)
        return;

      txtValidationErrors.Text = _docValidationReport;
      txtWordXml.Text = _documentXmlString;

      if (((ControlPanel)tpControlPanel).PrintToImage)
      {
        lblStatus.Text = "Document printing in progress...";
        Application.DoEvents();
        //SendDocumentToPrinter("resume1.docx");

        lblStatus.Text = "Image being loaded...";
        Application.DoEvents();
        LoadImage();
      }
      else
      {
        if (((ControlPanel)tpControlPanel).CreateDocument)
        {
          _imgEngine = new ImageEngine();
          _package.IsAdsdi = false;
          _package.DocControl = GetDocControl();
          _pageSet = _imgEngine.GenerateImageFromPackage(_package, true);
          LoadPageImages(_pageSet);
          _map = _package.DocOut.Map;
          _xmlMap = _package.DocOut.XmlMap.ToString();
          ShowCode(_package.DocOut.MetricsTrace);
          txtTagReport.Text = _package.DocOut.GetTagReport();
          txtRegionsOccupied.Text = _package.DocOut.GetRegionsReport();

          LoadRegions();
          ResetOverlay();
          LoadDocTree();

          if (((ControlPanel)tpControlPanel).ShowXmlMap || ((ControlPanel)tpControlPanel).ShowMap)
          {
            if (fTextDisplay == null)
            {
              fTextDisplay = new frmTextDisplay();
              fTextDisplay.Show();
              if (((ControlPanel)tpControlPanel).ShowXmlMap)
                fTextDisplay.SetText(_xmlMap);
              else
                fTextDisplay.SetText(_map);
              fTextDisplay.TopMost = true;
            }
            else
            {
              if (fTextDisplay.IsDisposed)
                fTextDisplay = new frmTextDisplay();
              fTextDisplay.Show();
              if (((ControlPanel)tpControlPanel).ShowXmlMap)
                fTextDisplay.SetText(_xmlMap);
              else
                fTextDisplay.SetText(_map);
              fTextDisplay.TopMost = true;
            }
          }
        }
      }

      lblStatus.Text = String.Empty;
      Application.DoEvents();
      pnlImage.Focus();
    }


    private Ds.DocPackage GeneratePackage(string packageName)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        float widthFactor = 0.15F;
        float spaceWidthFactor = 0.215F;

        ((TextPanel)tpText).Clear();

        widthFactor = ((ControlPanel)tpControlPanel).WidthFactor;
        spaceWidthFactor = ((ControlPanel)tpControlPanel).SpaceWidthFactor;

        Ds.DocPackage docPackage = new Ds.DocPackage();
        docPackage.DocControl = GetDocControl();
        docPackage.DocControl.DocInfo = new Ds.DocInfo(_docPath, _packagePath, packageName, "BuildMyResume.com");
        docPackage.Load();
        docPackage.IsAdsdi = true;

        File.Delete(docPackage.DocxFullFilePath);

        using (DocEngine docEngine = new DocEngine())
        {
          _docValidationReport = docEngine.GenerateDocument(docPackage, ((ControlPanel)tpControlPanel).WidthFactor,
                                 ((ControlPanel)tpControlPanel).SpaceWidthFactor, ((ControlPanel)tpControlPanel).LineFactor, _scale);
          _documentXml = docEngine.DocumentXml;
          _documentXmlString = _documentXml.ToString();
          _stylesXml = docEngine.StylesXml;

          docPackage.AddGenerated(docEngine.DocumentXml);
          docPackage.AddGenerated(docEngine.StylesXml);

          File.WriteAllText(docPackage.DocControl.DocInfo.WordXmlPath, docPackage.DocPartsOut["document"].DocPartXml.ToString());

          _xmlMap = docEngine.DocMap;
        }

        if (((ControlPanel)tpControlPanel).ShowXmlMap)
        {
          if (fTextDisplay == null)
          {
            fTextDisplay = new frmTextDisplay();
            fTextDisplay.Show();
            fTextDisplay.TopMost = true;
            fTextDisplay.SetText(_xmlMap);
          }
          else
          {
            if (fTextDisplay.IsDisposed)
              fTextDisplay = new frmTextDisplay();
            fTextDisplay.Show();
            fTextDisplay.TopMost = true;
            fTextDisplay.SetText(_xmlMap);
          }
        }

        this.Cursor = Cursors.Default;
        return docPackage;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while generating document at '" + packageName + "'." + g.crlf2 + ex.ToReport(),
                        "ResumeImageTest Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Cursor = Cursors.Default;
        return null;
      }

    }

    private void LoadDocTree()
    {
      TreeViewPanel tvPanel = (TreeViewPanel)tpTreeView;
      tvPanel.IsLoading = true;

      TreeView tvDoc = ((TreeViewPanel)tpTreeView).TreeView;

      tvDoc.Nodes.Clear();
      TreeNode n = new TreeNode();
      n.Text = "root";
      n.Name = "root";
      n.Tag = "root";
      tvDoc.Nodes.Add(n);

      Ds.DocumentElement e = _package.DocOut.GetRoot(_package.DocOut, "tc0");

      LoadTreeView(e, n);
      tvDoc.ExpandAll();

      tvPanel.IsLoading = false;
    }

    private void LoadTreeView(Ds.DocumentElement d, TreeNode n)
    {
      TreeNode newNode = new TreeNode();
      newNode.Name = d.Name;
      newNode.Tag = d.Tag;
      newNode.Text = d.DeType.ToString() + " (" + d.Name + @"/" + d.Tag + ")";
      string tag = d.Tag;
      if (tag.IsNotBlank())
        tag = " / " + tag;
      newNode.ToolTipText = d.OxName + " ( " + d.Name + tag + " )";
      n.Nodes.Add(newNode);

      foreach (Ds.DocumentElement de in d.ChildElements)
        LoadTreeView(de, newNode);
    }

    private void LoadPageImages(Ds.PageSet ps)
    {
      this.Cursor = Cursors.WaitCursor;
      pnlImage.Controls.Clear();

      int minPanelWidth = ps.CurrentPageSize.Width + 40;
      if (pnlImage.Width < minPanelWidth)
        pnlImage.Width = minPanelWidth;

      int minPanelHeight = 40 + (ps.Count * (ps.CurrentPageSize.Height + 20));
      pnlImage.Height = minPanelHeight;

      int pbCount = 0;

      foreach (Ds.Page page in ps.Values)
      {
        pb = new PictureBox();
        pb.Tag = page.PageNumber;
        pb.MouseMove += PictureBox_MouseMove;
        pb.MouseWheel += PictureBox_MouseWheel;
        pb.MouseClick += PictureBox_MouseClick;
        pb.Size = page.Image.Size;
        pb.Image = page.Image;
        pnlImage.Controls.Add(pb);
        pb.Parent = pnlImage;
        pb.BackColor = System.Drawing.Color.White;
        pb.Left = pnlImage.Width / 2 - pb.Width / 2;
        pb.Top = 20 + pbCount * (pb.Height + 20);
        pbCount++;
        pb.Controls.Add(pbOverlay);
        pbOverlay.Visible = false;
        pbOverlay.BackColor = Color.Transparent;

        pb.Invalidate();
      }

      this.Cursor = Cursors.Default;
    }

    private void LoadImage()
    {
      this.Cursor = Cursors.WaitCursor;

      pnlImage.Controls.Clear();

      int i = 0;

      while (i < 20)
      {
        if (File.Exists(_imgPath + @"\page1.png"))
        {
          break;
        }
        else
        {
          i++;
          System.Threading.Thread.Sleep(100);
        }
      }

      if (!File.Exists(_imgPath + @"\page1.png"))
      {
        this.Cursor = Cursors.Default;
        lblStatus.Text = "Image file not found.";
        return;
      }

      Image img = Image.FromFile(_imgPath + @"\page1.png");

      int minPanelWidth = img.Width + 40;
      if (pnlImage.Width < minPanelWidth)
        pnlImage.Width = minPanelWidth;

      int minPanelHeight = 40 + img.Height;
      pnlImage.Height = minPanelHeight;

      PictureBox pb = new PictureBox();
      pb.Tag = 1;
      pb.MouseMove += PictureBox_MouseMove;
      pb.MouseWheel += PictureBox_MouseWheel;
      pb.Size = img.Size;
      pb.Image = img;
      pnlImage.Controls.Add(pb);
      pb.Left = pnlImage.Width / 2 - pb.Width / 2;
      pb.Top = 20;
      pb.Invalidate();

      this.Cursor = Cursors.Default;
    }

    private void ClearImage()
    {
      this.Cursor = Cursors.WaitCursor;
      pnlImage.Controls.Clear();
      this.Cursor = Cursors.Default;
    }


    private void PictureBox_MouseClick(object sender, MouseEventArgs e)
    {
      PictureBox p = (PictureBox)sender;

      if (p.Image == null)
        return;

      int x = e.X;
      if (x > p.Image.Width - 1)
        x = p.Image.Width - 1;

      int y = e.Y;
      if (y > p.Image.Height - 1)
        y = p.Image.Height - 1;

      int scaledX = Convert.ToInt32((float)e.X / _scale);
      int scaledY = Convert.ToInt32((float)e.Y / _scale);

      Color c = ((Bitmap)p.Image).GetPixel(x, y);

      Ds.DocumentElement de = GetDocumentElementAt(scaledX, scaledY);
      if (de != null)
        selectedElement = de;

      if (toolWindows["ToolWindow_Info"].Visible)
      {
        if (de == null)
        {
          ((InfoPanel)tpInfo).Clear();
        }
        else
        {
          ((InfoPanel)tpInfo).Source = "I";
          ((InfoPanel)tpInfo).MousePos = scaledX.ToString("000") + "," + scaledY.ToString("000");
          ((InfoPanel)tpInfo).MouseRaw = e.X.ToString("000") + "," + e.Y.ToString("000");
          ((InfoPanel)tpInfo).Color = c.R.ToString() + "." + c.G.ToString() + "." + c.B.ToString();
          DisplayInfo("I", de);
        }
      }

      if (toolWindows["ToolWindow_Text"].Visible)
      {
        ((TextPanel)tpText).SetText(selectedElement.GetXmlMap(((ControlPanel)tpControlPanel).IncludeProperties).ToString());
      }

      if (toolWindows["ToolWindow_Properties"].Visible)
        ShowProperties();

      if (de != null)
      {
        if (toolWindows["ToolWindow_Image"].Visible)
        {
          if (!splitterImageAndCode.Panel2Collapsed || tpText.IsDockedInToolWindow)
          {
            PictureBox tpImagePb = ((ImagePanel)tpImage).PictureBox;
            Ds.DrawingMode drawingModeHold = _package.DocOut.DrawingMode;
            _package.DocOut.DrawingMode = Ds.DrawingMode.DocumentPortion;
            _imgEngine.DrawDocumentElement(tpImagePb, selectedElement.Parent);
            _package.DocOut.DrawingMode = drawingModeHold;
          }
        }

      }
    }

    private void ShowProperties()
    {
      if (selectedElement == null)
        return;

      ((PropertiesPanel)tpProperties).LoadElement(selectedElement);

      toolWindows["ToolWindow_Properties"].Visible = true;

      propertiesShown = true;
    }

    private void ShowCode(string code)
    {
      ((TextPanel)tpText).SetText(code);

      toolWindows["ToolWindow_Text"].Visible = true;

      textPanelShown = true;
    }

    private void PictureBox_MouseMove(object sender, MouseEventArgs e)
    {
      PictureBox p = (PictureBox)sender;

      if (p.Image == null)
        return;

      int x = e.X;
      if (x > p.Image.Width - 1)
        x = p.Image.Width - 1;

      int y = e.Y;
      if (y > p.Image.Height - 1)
        y = p.Image.Height - 1;

      int scaledX = Convert.ToInt32((float)e.X / _scale);
      int scaledY = Convert.ToInt32((float)e.Y / _scale);

      Color c = ((Bitmap)p.Image).GetPixel(x, y);

      if (g.AppConfig.GetBoolean("DebugElementsAtPoint"))
      {
        string elementsAtPoint = GetDocumentElementsAt(scaledX, scaledY);
        SetDebugText(elementsAtPoint);
      }

      Ds.DocumentElement de = GetDocumentElementAt(scaledX, scaledY);
      if (de == null)
      {
        ClearOverlay();
        selectedElement = null;
      }
      else
      {
        if (selectedElement == null)
        {
          selectedElement = de;
          ClearOverlay();
          MarkOverlay(de);
        }
        else
        {
          if (de.Name != selectedElement.Name)
          {
            selectedElement = de;
            ClearOverlay();
            MarkOverlay(de);
          }
        }
      }

      if (de != null)
      {
        DisplayInfo("I", de);
        UpdateProperties(de);
      }

      ((InfoPanel)tpInfo).Source = "I";
      ((InfoPanel)tpInfo).MousePos = scaledX.ToString("000") + "," + scaledY.ToString("000");
      ((InfoPanel)tpInfo).MouseRaw = e.X.ToString("000") + "," + e.Y.ToString("000");
      ((InfoPanel)tpInfo).Color = c.R.ToString() + "." + c.G.ToString() + "." + c.B.ToString();
    }

    private void UpdateProperties(Ds.DocumentElement de)
    {
      if (de == null)
        return;

      if (!toolWindows["ToolWindow_Properties"].Visible)
        return;

      ((PropertiesPanel)tpProperties).LoadElement(de);

      toolWindows["ToolWindow_Properties"].Visible = true;
    }

    private void DisplayInfo(string source, Ds.DocumentElement de)
    {
      if (de == null)
        return;

      if (!toolWindows["ToolWindow_Info"].Visible)
        return;

      ((InfoPanel)tpInfo).Source = source;
      ((InfoPanel)tpInfo).ElementName = de.Name;
      ((InfoPanel)tpInfo).ElementTag = de.Tag;
      ((InfoPanel)tpInfo).ElementType = de.DeType.ToString();
      ((InfoPanel)tpInfo).Level = de.Level.ToString();
      RectangleF rectF = de.RawMetrics.GetRectangleF();
      ((InfoPanel)tpInfo).Offset = rectF.X.ToString("000.000") + "," + rectF.Y.ToString("000.000");
      ((InfoPanel)tpInfo).ElementSize = rectF.Width.ToString("000.000") + "," + rectF.Height.ToString("000.000");

      lblStatus.Text = de.Text;
    }

    private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
    {
      pnlImage.HorizontalScroll.Enabled = true;

      int smoothingFactor = -20;
      int value = Convert.ToInt32(e.Delta / smoothingFactor);
      value += pnlImage.VerticalScroll.Value;

      if (e.Delta < 0)
      {
        if (value > pnlImage.VerticalScroll.Maximum)
          value = pnlImage.VerticalScroll.Maximum;
      }
      else
      {
        if (value < pnlImage.VerticalScroll.Minimum)
          value = pnlImage.VerticalScroll.Minimum;
      }

      pnlImage.VerticalScroll.Value = value;
    }


    private string GetDocumentElementsAt(int x, int y)
    {
      if (_package == null)
        return "Package is null";

      if (_package.DocOut == null)
        return "DocOut is null";

      StringBuilder sb = new StringBuilder();

      Ds.OccupiedRegionSet ors = _package.DocOut.RegionSet.GetRegionSetAt(x, y);

      // display all elements at this point
      foreach (Ds.OccupiedRegion or in ors.Values)
      {
        sb.Append("Level:" + or.DocumentElement.Level.ToString("000") + "   " +
                  "Depth:" + or.DocumentElement.Depth.ToString("000") + "   " +
                  "X:" + or.RectF.X.ToString("0000.0000") + "   " +
                  "Y:" + or.RectF.X.ToString("0000.0000") + "   " +
                  "W:" + or.RectF.Width.ToString("0000.0000") + "   " +
                  "H:" + or.RectF.Height.ToString("0000.0000") + "   " +
                  "Name:" + or.DocumentElement.Name + g.crlf);
      }

      // display the selected element at this point
      Ds.OccupiedRegion selectedOr = null;

      foreach (Ds.OccupiedRegion or in ors.Values)
      {
        if (selectedOr == null)
          selectedOr = or;
        else if (or.DocumentElement.Level > selectedOr.DocumentElement.Level)
          selectedOr = or;
      }

      if (selectedOr != null)
      {
        sb.Append(g.crlf2 + "SELECTED:"  + g.crlf +
                  "Level:" + selectedOr.DocumentElement.Level.ToString("000") + "   " +
                  "Depth:" + selectedOr.DocumentElement.Depth.ToString("000") + "   " +
                  "X:" + selectedOr.RectF.X.ToString("0000.0000") + "   " +
                  "Y:" + selectedOr.RectF.X.ToString("0000.0000") + "   " +
                  "W:" + selectedOr.RectF.Width.ToString("0000.0000") + "   " +
                  "H:" + selectedOr.RectF.Height.ToString("0000.0000") + "   " +
                  "Name:" + selectedOr.DocumentElement.Name + g.crlf);
      }

      string report = sb.ToString();
      return report;
    }

    private Ds.DocumentElement GetDocumentElementAt(int x, int y)
    {
      if (_package == null)
        return null;

      if (_package.DocOut == null)
        return null;

      Ds.DocumentElement de = null;

      Ds.OccupiedRegionSet ors = _package.DocOut.RegionSet.GetRegionSetAt(x, y);

      foreach (Ds.OccupiedRegion or in ors.Values)
      {
        if (de == null)
          de = or.DocumentElement;
        else if (or.DocumentElement.Level > de.Level)
          de = or.DocumentElement;
      }

      return de;
    }

    public void LoadRegions()
    {
      ((ControlPanel)tpControlPanel).ClearRegionList();

      if (_package == null)
        return;

      foreach (Ds.OccupiedRegion or in _package.DocOut.RegionSet.Values)
      {
        ((ControlPanel)tpControlPanel).AddRegion(or.RegionName);
      }
    }

    private void ResetOverlay()
    {
      if (pb == null)
        return;

      pbOverlay.Location = new Point(0, 0);
      pbOverlay.Size = pb.Size;

      if (pbOverlay.Image == null)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      if (pbOverlay.Image.Size != pbOverlay.Size)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      pbOverlay.Visible = true;
      pbOverlay.BringToFront();

      Application.DoEvents();
    }

    private void MarkOverlay()
    {
      if (pbOverlay == null)
        return;

      if (pbOverlay.Image == null)
        return;

      if (pbOverlay.Image.Size != pbOverlay.Size)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      Font f = new Font("Tahoma", 7.0F);

      Graphics gr = Graphics.FromImage(pbOverlay.Image);
      gr.ScaleTransform(_scale, _scale);
      gr.TextContrast = 3;
      gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gr.TextRenderingHint = TextRenderingHint.AntiAlias;

      RectangleF rectF = new RectangleF(0, 0, (float)(pbOverlay.Width - 1) / _scale, (float)(pbOverlay.Height - 1) / _scale);

      Rectangle r1 = new Rectangle(pb.Left, pb.Top, pb.Width, pb.Height);
      Rectangle r2 = new Rectangle(pbOverlay.Left, pbOverlay.Top, pbOverlay.Width, pbOverlay.Height);

      gr.DrawRectangle(Pens.Blue, rectF.X, rectF.Y, rectF.Width, rectF.Height);

      gr.DrawString("pb raw :", f, Brushes.Black, new PointF(10, 10));
      gr.DrawString(r1.Left.ToString() + "," + r1.Top.ToString() + "  " + r1.Width.ToString() + "," + r1.Height.ToString(), f, Brushes.Black, new PointF(80, 10));

      gr.DrawString("pbO raw :", f, Brushes.Black, new PointF(10, 20));
      gr.DrawString(r2.Left.ToString() + "," + r2.Top.ToString() + "  " + r2.Width.ToString() + "," + r2.Height.ToString(), f, Brushes.Black, new PointF(80, 20));

      gr.DrawString("pbO scaled :", f, Brushes.Black, new PointF(10, 30));
      gr.DrawString(rectF.Left.ToString() + "," + rectF.Top.ToString() + "  " + rectF.Width.ToString() + "," + rectF.Height.ToString(), f, Brushes.Black, new PointF(80, 30));

      gr.Dispose();
      pbOverlay.Invalidate();

      if (g.AppConfig.GetBoolean("DebugElementsAtPoint"))
      {
        string overlayReport = g.crlf + "Selected overlay rectangle   X:" + rectF.X.ToString("0000.0000") + "  " +
                               "Y:" + rectF.Y.ToString("0000.0000") + "  " +
                               "W:" + rectF.Width.ToString("0000.0000") + "  " +
                               "H:" + rectF.Height.ToString("0000.0000") + g.crlf2;
        AppendDebugText(overlayReport);
      }
    }

    private void MarkOverlay(Ds.DocumentElement de)
    {
      if (de == null)
        return;

      if (pbOverlay == null)
        return;

      if (pbOverlay.Image == null)
        return;

      if (pbOverlay.Image.Size != pbOverlay.Size)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      Graphics gr = Graphics.FromImage(pbOverlay.Image);
      gr.ScaleTransform(_scale, _scale);
      gr.TextContrast = 3;
      gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gr.TextRenderingHint = TextRenderingHint.AntiAlias;
      RectangleF rectF = de.RawMetrics.GetRectangleF();
      gr.DrawRectangle(Pens.Magenta, rectF.X, rectF.Y, rectF.Width, rectF.Height);

      gr.Dispose();
      pbOverlay.Invalidate();

      if (g.AppConfig.GetBoolean("DebugElementsAtPoint"))
      {
        string overlayReport = g.crlf + "Selected overlay rectangle   X:" + rectF.X.ToString("0000.0000") + "  " +
                               "Y:" + rectF.Y.ToString("0000.0000") + "  " +
                               "W:" + rectF.Width.ToString("0000.0000") + "  " +
                               "H:" + rectF.Height.ToString("0000.0000") + g.crlf2;
        AppendDebugText(overlayReport);
      }

    }

    private void MarkOverlay(Ds.OccupiedRegion or)
    {
      if (or == null)
        return;

      if (pbOverlay == null)
        return;

      if (pbOverlay.Image == null)
        return;

      if (pbOverlay.Image.Size != pbOverlay.Size)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      Graphics gr = Graphics.FromImage(pbOverlay.Image);
      gr.ScaleTransform(_scale, _scale);
      gr.TextContrast = 3;
      gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gr.TextRenderingHint = TextRenderingHint.AntiAlias;
      gr.DrawRectangle(Pens.Magenta, or.RectF.X, or.RectF.Y, or.RectF.Width, or.RectF.Height);

      gr.Dispose();
      pbOverlay.Invalidate();

      if (g.AppConfig.GetBoolean("DebugElementsAtPoint"))
      {
        string overlayReport = g.crlf + "Selected overlay rectangle   X:" + or.RectF.X.ToString("0000.0000") + "  " +
                               "Y:" + or.RectF.Y.ToString("0000.0000") + "  " +
                               "W:" + or.RectF.Width.ToString("0000.0000") + "  " +
                               "H:" + or.RectF.Height.ToString("0000.0000") + g.crlf2;
        AppendDebugText(overlayReport);
      }
    }

    private void ClearOverlay()
    {
      if (pbOverlay == null)
        return;

      if (pbOverlay.Image == null)
        return;

      if (pbOverlay.Image.Size != pbOverlay.Size)
        pbOverlay.Image = new Bitmap(pbOverlay.Width, pbOverlay.Height);

      Graphics gr = Graphics.FromImage(pbOverlay.Image);
      gr.ScaleTransform(_scale, _scale);
      gr.TextContrast = 3;
      gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gr.TextRenderingHint = TextRenderingHint.AntiAlias;

      gr.Clear(Color.Transparent);

      pbOverlay.Invalidate();
    }

    private void RefreshImage()
    {
      ClearImage();
      lblStatus.Text = "Refreshing image...";
      Application.DoEvents();
      _imgEngine = new ImageEngine();

      _package.DocControl = GetDocControl();
      _pageSet = _imgEngine.GenerateImageFromPackage(_package, false);

      LoadPageImages(_pageSet);
      ResetOverlay();

      lblStatus.Text = String.Empty;
      Application.DoEvents();
      pnlImage.Focus();
    }

    private Ds.DocControl GetDocControl()
    {
      Ds.DocControl dc = new Ds.DocControl();

      dc.DebugControl.CreateMap = ((ControlPanel)tpControlPanel).ShowMap;
      dc.DebugControl.CreateXmlMap = ((ControlPanel)tpControlPanel).ShowXmlMap;
      dc.DebugControl.IncludeProperties = ((ControlPanel)tpControlPanel).IncludeProperties;
      dc.DebugControl.InDiagnosticsMode = ((ControlPanel)tpControlPanel).DiagnosticsMode;
      dc.DebugControl.ShowScale = ((ControlPanel)tpControlPanel).ShowScale;
      dc.DebugControl.DiagnosticsLevel = ((ControlPanel)tpControlPanel).DiagnosticsLevel;

      dc.PrintControl.CreateDocument = ((ControlPanel)tpControlPanel).CreateDocument;
      dc.PrintControl.Scale = _scale;
      dc.PrintControl.LineFactor = ((ControlPanel)tpControlPanel).LineFactor;
      dc.PrintControl.WidthFactor = ((ControlPanel)tpControlPanel).WidthFactor;
      dc.PrintControl.SpaceWidthFactor = ((ControlPanel)tpControlPanel).SpaceWidthFactor;
      dc.PrintControl.TextContrast = 3;

      return dc;
    }


    private void InitializeApplication()
    {
      a = new a();

      InitializeToolWindowForms();

      _imgPath = g.AppConfig.GetCI("ImagePath");
      _docPath = g.AppConfig.GetCI("DocPath");
      _wordFilePath = g.AppConfig.GetCI("WordFilePath");
      _packagePath = g.AppConfig.GetCI("PackagePath");
      _resumeDocs = g.AppConfig.GetCI("ResumeDocs");
      _resumeOutXml = g.AppConfig.GetCI("ResumeOutXml");
      _hyphenationData = g.AppConfig.GetCI("HyphenationData");
      _dictionaryData = g.AppConfig.GetCI("DictionaryData");

      string[] packagesList = Directory.GetDirectories(_packagePath);

      List<string> packages = Directory.GetDirectories(_packagePath).ToList();
      cboPackages.Items.Clear();
      foreach (string package in packages)
        cboPackages.Items.Add(Path.GetFileName(package));

      string lastPackageUsed = g.AppConfig.GetCI("LastPackageUsed");

      if (lastPackageUsed.IsNotBlank())
      {
        for (int i = 0; i < cboPackages.Items.Count; i++)
        {
          if (cboPackages.Items[i].ToString() == lastPackageUsed)
          {
            cboPackages.SelectedIndex = i;
            break;
          }
        }
      }
      else
      {
        if (cboPackages.Items.Count > 0)
          cboPackages.SelectedIndex = 0;
      }

      ToolStripComboBox cb = (ToolStripComboBox) toolStripMain.Items["cboPackages"];
      if (cb != null)
      {
        cb.SelectedIndexChanged += cb_SelectedIndexChanged;
      }

      lblScale.Text = "100%";
      if (g.AppConfig.ContainsKey("InitialScale"))
        lblScale.Text = g.AppConfig.GetCI("InitialScale") + "%";

      _scale = ((float)((float)Int32.Parse(lblScale.Text.Replace("%", String.Empty)) / 100)) * 0.96F;
    }

    private void cb_SelectedIndexChanged(object sender, EventArgs e)
    {
      ToolStripComboBox cb = (ToolStripComboBox) sender;
      g.AppConfig.SetCI("LastPackageUsed", cb.Text);
    }

    private void InitializeToolWindowForms()
    {
      int secondScreenWidth = 0;
      Rectangle primaryScreenRectangle = new Rectangle(new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);
      if (Screen.AllScreens.Count() > 1)
      {
        System.Windows.Forms.Screen screen2 = null;
        if (screen2 != null)
          secondScreenWidth = screen2.Bounds.Width;
      }
      Rectangle totalScreenArea = new Rectangle(new Point(0, 0), new Size(primaryScreenRectangle.Width + secondScreenWidth, primaryScreenRectangle.Height));

      List<string> splitterPanelsManaged = new List<string>();

      Point initialLocation = new Point(650, 250);

      // Create toolWindows collection and the tool windows.
      toolWindows = new Dictionary<string, frmToolWindowBase>();

      Dictionary<string, string> toolWindowSpec = new Dictionary<string, string>();
      toolWindowSpec.Add("TreeView", "DocumentTree");
      toolWindowSpec.Add("Text", "Document Xml");
      toolWindowSpec.Add("Debug", "Debug");
      toolWindowSpec.Add("Info", "Info");
      toolWindowSpec.Add("ControlPanel", "Control Panel");
      toolWindowSpec.Add("Image", "Document Image");
      toolWindowSpec.Add("Properties", "Properties");

      foreach (KeyValuePair<string, string> kvpToolWindow in toolWindowSpec)
      {
        frmToolWindowBase fToolWindow = new frmToolWindowBase(this, kvpToolWindow.Value);
        fToolWindow.Owner = this;
        fToolWindow.Tag = "ToolWindow_" + kvpToolWindow.Key;
        if (kvpToolWindow.Key == "Info" || kvpToolWindow.Key == "ControlPanel")
          fToolWindow.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        toolWindows.Add(fToolWindow.Tag.ToString(), fToolWindow);
      }

      // Get or create the UIState information for the tool windows
      MainFormHelper.InitializeUIState(this, toolWindows);
      _uiState = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState;

      // Create the tool panels and dock them in the tool windows or main UI
      InitializeToolPanels();

      // Establish docking targets (panels) for floated and docked locations
      toolWindowDockingTargets = new Dictionary<string, Panel>();
      foreach (KeyValuePair<string, frmToolWindowBase> kvpToolWindow in toolWindows)
        toolWindowDockingTargets.Add(kvpToolWindow.Key.Replace("ToolWindow", "FloatTarget"), kvpToolWindow.Value.DockPanel);

      toolWindowDockingTargets.Add("DockedTarget_TreeView", splitterTv.Panel1);
      toolWindowDockingTargets.Add("DockedTarget_Text", splitterImageAndCode.Panel2);

      foreach (UIWindow uiWindow in _uiState.UIWindowSet.Values)
      {
        if (uiWindow.IsMainForm)
        {
          MainFormHelper.ManageInitialSize(this, uiWindow);
        }
        else
        {
          string toolName = uiWindow.Name.Replace("ToolWindow_", String.Empty);
          ToolPanelBase tpBase;

          if (uiWindow.WindowLocation.IsDocked)
          {
            frmToolWindowBase toolWindowBase = toolWindows[uiWindow.Name];
            toolWindowBase.Visible = false;
            string toolPanelName = uiWindow.Name.Replace("ToolWindow", "ToolPanel");
            tpBase = toolPanels[toolPanelName];
            toolWindowBase.DockPanel.Controls.Remove(tpBase);
            string docketTargetName = uiWindow.Name.Replace("ToolWindow", "DockedTarget");
            Panel dockingTarget = toolWindowDockingTargets[docketTargetName];
            dockingTarget.Controls.Clear();
            dockingTarget.Controls.Add(tpBase);
            tpBase.Dock = DockStyle.Fill;
            tpBase.NotifyHostEvent += ToolPanel_NotifyHostEvent;

            switch (toolName)
            {
              case "TreeView":
                splitterTv.Panel1Collapsed = false;
                splitterPanelsManaged.Add(toolName);
                break;

              case "Text":
                splitterImageAndCode.Panel2Collapsed = false;
                splitterPanelsManaged.Add(toolName);
                break;
            }
          }
          else
          {
            Point defaultLocation = new Point(200, 200);
            Panel floatPanel = toolWindowDockingTargets[uiWindow.Name.Replace("ToolWindow", "FloatTarget")];
            floatPanel.Controls.Clear();
            tpBase = toolPanels[uiWindow.Name.Replace("ToolWindow", "ToolPanel")];
            Size tpSize = tpBase.Size;
            floatPanel.Controls.Add(tpBase);
            tpBase.Dock = DockStyle.Fill;
            tpBase.NotifyHostEvent += ToolPanel_NotifyHostEvent;
            frmToolWindowBase toolWindowBase = toolWindows[uiWindow.Name];
            if (toolWindowBase.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
              toolWindowBase.Size = tpSize.Inflate(14, 14);
            else
              toolWindowBase.Size = uiWindow.WindowLocation.Size.ToSize();

            toolWindowBase.WireUpNotifyHost();
            Rectangle toolWindowRectangle = new Rectangle(uiWindow.WindowLocation.Location, toolWindowBase.Size);
            Rectangle visibleOverlap = totalScreenArea;
            visibleOverlap.Intersect(toolWindowRectangle);

            if (visibleOverlap.IsEmpty)
            {
              uiWindow.WindowLocation.Location = defaultLocation;
              defaultLocation.Offset(50, 50);
            }

            toolWindowBase.Location = uiWindow.WindowLocation.Location;
            toolWindowBase.Visible = uiWindow.WindowLocation.IsVisible;

            switch (toolName)
            {
              case "TreeView":
                splitterTv.Panel1Collapsed = true;
                splitterPanelsManaged.Add(toolName);
                break;

              case "Text":
                splitterImageAndCode.Panel2Collapsed = true;
                splitterPanelsManaged.Add(toolName);
                break;
            }
          }
        }
      }

      if (!splitterPanelsManaged.Contains("TreeView"))
        splitterTv.Panel1Collapsed = true;

      if (!splitterPanelsManaged.Contains("Text"))
        splitterImageAndCode.Panel2Collapsed = true;

      if (!splitterPanelsManaged.Contains("ControlPanel"))
        splitterMain.Panel2Collapsed = true;

      foreach (frmToolWindowBase toolWindowBase in toolWindows.Values)
      {
        toolWindowBase.ToolAction += ToolWindow_ToolAction;
      }

      base.SetToolWindows(toolWindows);
    }

    private void ToolPanel_NotifyHostEvent(Org.TW.ToolPanels.ToolPanelNotifyEvent e)
    {
      if (e.Message.IsBlank())
        return;

      string tag = e.Message;

      if (!_package.DocOut.Tags.ContainsKey(tag))
      {
        lblStatus.Text = "Tag '" + tag + "' not found";
        return;
      }

      Ds.DocumentElement de = _package.DocOut.Tags[tag] as Ds.DocumentElement;
      UpdateProperties(de);

      lblStatus.Text = e.Message;
    }

    private void InitializeToolPanels()
    {
      tpTreeView = new TreeViewPanel();
      tpText = new TextPanel();
      tpInfo = new InfoPanel();
      tpControlPanel = new ControlPanel();
      tpProperties = new PropertiesPanel();
      tpImage = new ImagePanel();
      tpDebug = new DebugPanel();

      toolPanels = new Dictionary<string, ToolPanelBase>();
      toolPanels.Add(tpTreeView.Tag.ToString(), tpTreeView);
      toolPanels.Add(tpText.Tag.ToString(), tpText);
      toolPanels.Add(tpInfo.Tag.ToString(), tpInfo);
      toolPanels.Add(tpControlPanel.Tag.ToString(), tpControlPanel);
      toolPanels.Add(tpProperties.Tag.ToString(), tpProperties);
      toolPanels.Add(tpImage.Tag.ToString(), tpImage);
      toolPanels.Add(tpDebug.Tag.ToString(), tpDebug);
    }

    private void ToolWindowAction(string action)
    {
      string[] tokens = action.Split('_');

      frmToolWindowBase toolWindow = null;
      ToolPanelBase toolPanel = null;
      Panel dockingTarget = null;

      if (tokens.Length != 3)
        return;

      if (tokens[0] != "TW")
        return;

      string toolWindowAction = tokens[1];
      string toolWindowTarget = tokens[2];

      List<string> toolTargets = new List<string>();

      if (toolWindowTarget == "All")
      {
        toolTargets.Add("TreeView");
        toolTargets.Add("Text");
        toolTargets.Add("Info");
        toolTargets.Add("ControlPanel");
        toolTargets.Add("Image");
        toolTargets.Add("Properties");
      }
      else
      {
        toolTargets.Add(toolWindowTarget);
      }

      foreach (string toolTarget in toolTargets)
      {
        string uiWindowName = "ToolWindow_" + toolTarget;
        UIWindow uiWindow = _uiState.UIWindowSet[uiWindowName];

        toolWindow = toolWindows["ToolWindow_" + toolTarget];
        string dockingTargetName = "DockedTarget_" + toolTarget;

        switch (toolWindowAction)
        {
          case "Dock":
            if (toolWindowDockingTargets.ContainsKey(dockingTargetName))
            {
              toolWindow.Visible = false;
              toolPanel = toolPanels["ToolPanel_" + toolTarget];
              toolWindow.DockPanel.Controls.Remove(toolPanel);
              dockingTarget = toolWindowDockingTargets[dockingTargetName];
              dockingTarget.Controls.Clear();
              dockingTarget.Controls.Add(toolPanel);
              toolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = true;

              switch (toolTarget)
              {
                case "TreeView":
                  splitterTv.Panel1Collapsed = false;
                  break;

                case "Text":
                  splitterImageAndCode.Panel2Collapsed = false;
                  break;
              }
              break;
            }
            else
            {
              if (toolWindow.Visible)
                toolWindow.Visible = false;
            }
            break;

          case "Float":
            if (toolWindowDockingTargets.ContainsKey(dockingTargetName))
            {
              toolPanel = toolPanels["ToolPanel_" + toolTarget];
              dockingTargetName = "DockedTarget_" + toolTarget;
              dockingTarget = toolWindowDockingTargets[dockingTargetName];
              dockingTarget.Controls.Remove(toolPanel);
              toolWindow.DockPanel.Controls.Clear();
              toolWindow.DockPanel.Controls.Add(toolPanel);
              toolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = false;

              switch (toolTarget)
              {
                case "TreeView":
                  splitterTv.Panel1Collapsed = true;
                  break;

                case "Text":
                  splitterImageAndCode.Panel2Collapsed = true;
                  break;
              }
              toolWindow.Visible = true;
            }
            else
            {
              if (!toolWindow.Visible)
                toolWindow.Visible = true;
            }
            break;

          case "Hide":
            toolWindow.Visible = false;
            uiWindow.WindowLocation.IsVisible = toolWindow.Visible;
            break;

          case "Show":
            toolWindow.Visible = true;
            uiWindow.WindowLocation.IsVisible = toolWindow.Visible;
            break;

          case "Toggle":
            toolWindow.Visible = !toolWindow.Visible;
            uiWindow.WindowLocation.IsVisible = toolWindow.Visible;
            break;
        }
      }
    }



    public void ToolWindow_NotifyHostEvent(Org.TW.ToolPanels.ToolPanelNotifyEvent e)
    {
      if (e == null)
        return;
    }

    private void ToolWindow_ToolAction(ToolActionEventArgs e)
    {
      if (e == null)
        return;

      frmToolWindowBase toolWindow = e.ToolWindow as frmToolWindowBase;
      string name = toolWindow.Tag.ToString();

      switch (e.ToolActionEvent)
      {
        case ToolActionEvent.ToolWindowMoved:
          _uiState.UIWindowSet[name].WindowLocation.Location = toolWindow.Location;
          break;

        case ToolActionEvent.ToolWindowResized:
          _uiState.UIWindowSet[name].WindowLocation.Size = toolWindow.Size;
          break;

        case ToolActionEvent.ToolWindowVisibleChanged:
          _uiState.UIWindowSet[name].WindowLocation.IsVisible = toolWindow.Visible;
          break;
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (g.AppConfig.IsUpdated)
        g.AppConfig.Save();
    }


    private void Zoom(string action)
    {
      int oldScale = Int32.Parse(lblScale.Text.Replace("%", String.Empty));
      int newScale = MainFormHelper.GetNewScale(oldScale, action);

      lblScale.Text = newScale.ToString() + "%";
      _scale = ((float)((float)newScale / 100)) * 0.96F;

      RefreshImage();
    }

    private void SetDebugText(string text)
    {
      if (text.IsBlank())
        return;

      if (!tpDebug.Visible)
        return;


      ((DebugPanel)tpDebug).SetText(text);
    }

    private void AppendDebugText(string text)
    {
      if (text.IsBlank())
        return;

      if (!tpDebug.Visible)
        return;


      ((DebugPanel)tpDebug).AppendText(text);
    }
  }
}
