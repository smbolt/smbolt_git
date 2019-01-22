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
using COKC.Counting.Business;
using COKC.Counting.Business.Models;
using Org.TOCR;
using Org.IMG;
using Org.GS;
using Org.GS.Configuration;

namespace Org.ImageTest
{
  public partial class frmMain : Form
  {
    private a a;
    private string _imagesFolder;
    private bool _processImages;
    private string _initialTab;
    private bool _connectToDb;
    private string _currentImagesFolder;
    private string _currentFmtFolder;
    private Image _checkImage;
    private string _imgFmt;
    private string _imgExt;
    private bool _allFilesReady = false;
    private ConfigDbSpec _configDbSpec;
    private int _orgId;
    private int _selectedTagId;
    private CountingRepository _countingRepo;
    
    private TagSet _tagSet;
    private TagMatchSet _tagMatchSet;

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
        case "GetOCRText":
          if (cboImage.Text.IsNotBlank())
          {
            lblOcr.Text = String.Empty;
            Application.DoEvents();
            lblOcr.Text = GetOcrText(_imagesFolder + @"\" + cboImage.Text, _imgFmt);
          }
          break;

        case "MatchOcrTextToTag":
          MatchOcrTextToTag();
          break;

        case "ComputeMatchScores":
          ComputeMatchScores();
          break;

        case "ComputeScores":
          ComputeScores();
          break;

        case "Prev":
        case "Next":
          NavigateImage(action);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    public void ComputeMatchScores()
    {
      DateTime dtBegin = DateTime.Now;
      int tagCount = 0;

      StringBuilder sb = new StringBuilder();

      SortedList<float, TagMatch> scoredList = new SortedList<float, TagMatch>();

      foreach (var kvpTagMatch in _tagMatchSet)
      {
        int key = kvpTagMatch.Key;
        string tokens = kvpTagMatch.Value.TagMatchTokens;
        float score = TagMatchEngine.ComputeTagMatchScore(tokens, lblOcr.Text);
        tagCount++;

        if (score > 0)
        {
          while (scoredList.ContainsKey(score))
            score = score * .999999F;

          scoredList.Add(score, kvpTagMatch.Value);
        }
      }

      var ts = DateTime.Now - dtBegin;
      float ms = ts.Ticks / 10000000F;

      for (int i = scoredList.Count - 1; i > -1; i--)
      {
        var kvpScoredMatch = scoredList.ElementAt(i);

        sb.Append(kvpScoredMatch.Key.ToString("000.0000000") + "  " +
                  kvpScoredMatch.Value.TagId.ToString("000000") + "  " +
                  kvpScoredMatch.Value.TagMatchTokens.PadTo(55) + "      " +
                  _tagSet[kvpScoredMatch.Value.TagId].TagValue + g.crlf); 
      }

      var duration = "Scoring Duration: " + ms.ToString("##0.0000000") + "   tags: " + tagCount.ToString(); 

      txtComputedScores.Text = duration + g.crlf + sb.ToString();
    }

    private void ComputeScores()
    {

      lblScores.Text = "Score: 100.00";

      txtMatchOut.Text = "Grading matches.";
    }

    private string GetOcrText(string path, string imgFmt)
    {
      string ocrText = String.Empty;

      using (var ocr = new OCR())
      {
        ocrText = ocr.GetText(path, imgFmt);
      }


      string alphaTokens = ocrText.ToUpper().ToAlphaTokens(2);
      int payToken = alphaTokens.IndexOf(" PAY ", 10);
      if (payToken > -1)
        alphaTokens = alphaTokens.Substring(0, payToken).Trim();

      int dollarsToken = alphaTokens.IndexOf(" DOLLAR");
      if (dollarsToken > -1)
        alphaTokens = alphaTokens.Substring(0, dollarsToken);

      alphaTokens = alphaTokens.Replace(" DATE ", " ");
      alphaTokens = alphaTokens.Replace(" THE ", " ");
      if (alphaTokens.StartsWith("THE "))
        alphaTokens = alphaTokens.Substring(3).Trim();

      if (alphaTokens.EndsWith(" DATE"))
        alphaTokens = alphaTokens.Substring(0, alphaTokens.Length - 5).Trim();

      alphaTokens = alphaTokens.Replace("DOLLARS", " ");

      return alphaTokens;
    }

    private void NavigateImage(string direction)
    {
      txtComputedScores.Text = String.Empty;
      Application.DoEvents();

      switch (direction)
      {
        case "Next":
          if (cboImage.SelectedIndex < cboImage.Items.Count - 1)
          {
            if (cboImage.SelectedIndex == -1)
              cboImage.SelectedIndex = 1;
            else
              cboImage.SelectedIndex++;
          }
          break;


        case "Prev":
          if (cboImage.SelectedIndex > 1)
            cboImage.SelectedIndex--;
          break;
      }
    }

    private void MatchOcrTextToTag()
    {
      if (_selectedTagId == -1)
        return;

      string ocrText = lblOcr.Text;

      try
      {
        int newTagMatchId = _countingRepo.InserTagMatch(_orgId, _selectedTagId, ocrText.Trim());

        if (newTagMatchId > 0)
          MessageBox.Show("TagMatch added.", "ImageTest");
        else
          MessageBox.Show("TagMatch already exists.", "ImageTest"); 
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occured during application initialization." + g.crlf2 +
                        ex.ToReport(), "Image Test - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

        btnMatchOcrTextToTag.Enabled = false;

        _connectToDb = g.CI("ConnectToDb").ToBoolean();
        _processImages = g.CI("ProcessImages").ToBoolean();
        _initialTab = g.CI("InitialTab");


        _orgId = g.CI("OrgId").ToInt32();
        string dbSpecPrefix = g.CI("DbSpecPrefix");
        _configDbSpec = g.GetDbSpec(dbSpecPrefix);

        _countingRepo = new CountingRepository(_configDbSpec);

        _imgFmt = g.CI("SelectedImageFormat");
        _imgExt = "." + _imgFmt;

        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

        _imagesFolder = g.CI("ImagesFolder");

        if (_processImages)
        {
          using (var fme = new FileMgmtEngine())
          {
            fme.AssertDirectoryStructure(_imagesFolder);
            fme.CleanUp(_imagesFolder, _imgFmt);
            _currentImagesFolder = fme.MigrateImages(_imagesFolder, _imgFmt);
            _currentFmtFolder = _currentImagesFolder + @"\" + _imgFmt.ToUpper();
          }

          using (var imgEngine = new ImageEngine())
          {
            imgEngine.ClipImages(_currentFmtFolder, _imgFmt);
          }

       
          LoadImages();
        }

        if (_connectToDb)
        {
          _tagSet = GetTags();
          _tagMatchSet = GetTagMatchSet();

          LoadTags();
        }

        switch (_initialTab)
        {
          case "Images":
            tabMain.SelectedTab = tabPageImages;
            break;

          case "Match":
            tabMain.SelectedTab = tabPageTagMatch;
            break;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occured during application initialization." + g.crlf2 +
                        ex.ToReport(), "Image Test - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private TagSet GetTags()
    {
      try
      {
        var tagSet = new TagSet();

        var tags = _countingRepo.GetTags(_orgId);
        foreach (var tag in tags)
        {
          if (!tagSet.ContainsKey(tag.TagId))
            tagSet.Add(tag.TagId, tag);
        }

        return tagSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to retrieve the tags from the database.", ex);
      }
    }

    private TagMatchSet GetTagMatchSet()
    {
      try
      {
        var set = new TagMatchSet();
        var tagMatches = _countingRepo.GetTagMatchesForOrg(_orgId);

        foreach (var tagMatch in tagMatches)
        {
          if (!set.ContainsKey(tagMatch.TagMatchId))
            set.Add(tagMatch.TagMatchId, tagMatch);
        }

        return set;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to retrieve the TagMatchSet from the database.", ex);
      }
    }

    private void LoadTags()
    {
      lbTags.Items.Clear();
      foreach (var tag in _tagSet.Values)
      {
        lbTags.Items.Add(tag.TagValue); 
      }
    }

    private void LoadImages()
    {
      List<string> images = Directory.GetFiles(_currentFmtFolder, "*.*").ToList();

      cboImage.Items.Clear();
      cboImage.Items.Add(String.Empty);
      foreach (var image in images)
      {
        if (image.Contains("_clip"))
          cboImage.Items.Add(Path.GetFileName(image));
      }
    }

    private void cboImage_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboImage.Text.IsBlank())
        return;

      LoadImage(cboImage.Text);
    }

    private void LoadImage(string imageName)
    {
      this.Cursor = Cursors.WaitCursor;

      lblOcr.Text = String.Empty;
      Application.DoEvents();

      string imagePath = _currentFmtFolder + @"\" + imageName;
      Image img;

      using (var fs = new FileStream(imagePath, FileMode.Open))
      {
        img = Image.FromStream(fs);
        fs.Close();
      }

      Image bmp = (Bitmap)img;
      pbMain.Image = bmp;

      Application.DoEvents();

      lblOcr.Text = GetOcrText(_currentFmtFolder + @"\" + imageName, _imgFmt);

      if (ckComputeScores.Checked)
      {
        ComputeMatchScores();
      }

      this.Cursor = Cursors.Default;
    }

    private void MatchElementsTextChanged(object sender, EventArgs e)
    {
      int tagIx = lbTags.SelectedIndex;
      if (tagIx == -1)
      {
        _selectedTagId = -1;
        lblTagId.Text = "TagID:";
      }
      else
      {
        _selectedTagId = _tagSet.ElementAt(tagIx).Key;
        lblTagId.Text = "TagID: " + _selectedTagId.ToString();
      }

      var selectedTag = lbTags.SelectedItem.ObjectToTrimmedString();

      if (selectedTag.IsNotBlank() && lblOcr.Text.IsNotBlank())
        btnMatchOcrTextToTag.Enabled = true;
      else
        btnMatchOcrTextToTag.Enabled = false;
    }
  }
}
