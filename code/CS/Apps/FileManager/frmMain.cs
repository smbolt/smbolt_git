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
using Org.GS;
using Org.ZIP;

namespace Org.FileManager
{
  public partial class frmMain : Form
    
  {
    private a a;

    private string _targetPath = String.Empty;

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
        case "CreateArchive":
          CreateArchive();
          break;

        case "ExtractArchive":
          ExtractArchive();
          break;

        case "SegmentFile":
          SegmentFile();
          break;

        case "DesegmentFile":
          DesegmentFile();
          break;

        case "CreateDummySegmentationData":
          CreateDummySegmentationData();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void CreateArchive()
    {
      Archiver archiver = new Archiver();

      string sourceDirectory = g.CI("ArchiveSourceFolder");

      int seq = 0;
      string targetPath = g.CI("ArchiveTargetFolder") + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + seq.ToString("000") + ".zip";

      while (File.Exists(targetPath))
      {
        seq++;
        targetPath = g.CI("ArchiveTargetFolder") + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + seq.ToString("000") + ".zip";
      }
      
      _targetPath = targetPath;

      archiver.CreateArchive(sourceDirectory, targetPath);

      txtOut.Text = "Running CreateArchive.";
    }
    
    
    private void ExtractArchive()
    {
      try
      {
        Archiver archiver = new Archiver();

        string sourcePath = _targetPath;

        string targetDirectory = g.CI("ExtractTargetFolder");

        archiver.ExtractArchive(sourcePath, targetDirectory);

        txtOut.Text = "Running ExtractArchive.";
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during archive processing." + g.crlf2 +
                        "Exception Message follows:" + g.crlf2 + ex.ToReport(), 
                        "File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }

    }

    private void SegmentFile()
    {      
      string segmentationSourcePath = lblSegmentationSourceValue.Text; 
      List<string> segmentationFiles = Directory.GetFiles(segmentationSourcePath).ToList(); 
      
      if (segmentationFiles.Count > 1)
      {
        MessageBox.Show("Multiple files exist in the segmentation source path." + g.crlf2 + 
                        "Place single file for segmentation in path '" + segmentationSourcePath + "' and try again.", 
                        "File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        txtOut.Text = "Multiple files exist in segmentation source path '" + segmentationSourcePath + "'."; 
        return; 
      }

      if (segmentationFiles.Count == 0)
      {
        MessageBox.Show("No files exist in the segmentation source path." + g.crlf2 + 
                        "Place single file for segmentation in path '" + segmentationSourcePath + "' and try again.", 
                        "File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        txtOut.Text = "No files exist in segmentation source path '" + segmentationSourcePath + "'."; 
        return; 
      }


      string segmentationFileFullPath = segmentationFiles.First();

      int segmentSize = cboSegmentSize.Text.Split(Constants.SpaceDelimiter).First().Replace(",", String.Empty).ToInt32() * 1000;

      FileSystemUtility fsu = new FileSystemUtility();

      fsu.SegmentizeFile(segmentationFileFullPath, segmentSize); 

      txtOut.Text = "Running SegmentFile.";
    }

    private void DesegmentFile()
    {
      string segmentationFileFullPath = lblSegmentationSourceValue.Text + @"\SegmentationSource.txt";

      if (!File.Exists(segmentationFileFullPath))
      {
        MessageBox.Show("File for desegmenting does not exist." + g.crlf2 +
                        "Create file '" + segmentationFileFullPath + "' and try again.",
                        "File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtOut.Text = "File '" + segmentationFileFullPath + "' does not exist.";
        return;
      }

      FileSystemUtility fsu = new FileSystemUtility();

      fsu.DesegmentizeFile(segmentationFileFullPath);

      txtOut.Text = "Running DesegmentFile.";
    }

    private void CreateDummySegmentationData()
    {
      // compose full file path for segmentation source file
      string segmentationFileFullPath = lblSegmentationSourceValue.Text + @"\SegmentationSource.txt";

      // if the directory does not exist, create it
      if (!Directory.Exists(lblSegmentationSourceValue.Text))
        Directory.CreateDirectory(lblSegmentationSourceValue.Text);

      // delete any previously existing files in the directory
      List<string> existingFiles = Directory.GetFiles(lblSegmentationSourceValue.Text).ToList();
      foreach (string existingFile in existingFiles)
        File.Delete(existingFile); 

      // build a "dummy string" and put it in the file
      StringBuilder sb = new StringBuilder();

      int fileSize = cboFileSize.Text.Split(Constants.SpaceDelimiter).First().Replace(",", String.Empty).ToInt32() * 1000;

      while (sb.Length < fileSize)
      {
        sb.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz"); 
      }

      sb.Append(g.crlf); 
      File.WriteAllText(segmentationFileFullPath, sb.ToString());       

      txtOut.Text = "Running CreateDummySegmentationData.";
    }



    private void InitializeApplication()
    {
      try
      {
        a = new a();
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred during initialization." + g.crlf2 + ex.ToReport(), "File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        return;
      }

      cboFileSize.SelectedIndex = 0;
      cboSegmentSize.SelectedIndex = 0; 


      int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
      int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                           Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

      lblStatus.Text = String.Empty;

      lblArchiveSourceValue.Text = g.CI("ArchiveSourceFolder");
      lblArchiveTargetValue.Text = g.CI("ArchiveTargetPath");
      lblExtractTargetValue.Text = g.CI("ExtractTargetFolder");
      lblSegmentationSourceValue.Text = g.CI("SegmentationSourceFolder");
      lblSegmentationTargetValue.Text = g.CI("SegmentationTargetFolder");
      lblDesegmentationTargetValue.Text = g.CI("DesegmentationTargetFolder");
    }
  }
}
