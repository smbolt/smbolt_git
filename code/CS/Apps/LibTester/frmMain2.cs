using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using Org.GS;
using Org.GS.TextProcessing;
using Org.Pdfx;

namespace Org.LibTester
{
  public partial class frmMain2 : Form
  {
    private a a;

    private FormatSpecSet _formatSpecSet;

    public frmMain2()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ParseReport":
          ParseReport();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void ParseReport()
    {
      LoadFormatSpecSet();

      Text text;
      string fullFilePath = @"C:\_work\WL_Temp\4.5.2 NonOperated\01 Non-Op Well Files A-D\BRANCH_1H-16\BRANCH 1H-16_Asset Partner Drilling Report 1-57_Final.pdf";
      using (var textExtractor = new TextExtractor())
        text = textExtractor.ExtractTextFromPdf(fullFilePath);

      var formatSpec = _formatSpecSet["Format1"];

      formatSpec.TextStructureDefinition.SetTextStructure(text, formatSpec.RecogSpecSet);
    }

    private void LoadFormatSpecSet()
    {
      try
      {
        _formatSpecSet = new FormatSpecSet();

        var formatSpecFiles = Directory.GetFiles(g.ImportsPath, "*.xml").ToList();

        using (var f = new ObjectFactory2())
        {
          foreach (var formatSpecFile in formatSpecFiles)
          {
            var xml = XElement.Parse(File.ReadAllText(formatSpecFile));
            var formatSpecSet = f.Deserialize(xml) as FormatSpecSet;
            foreach (var kvp in formatSpecSet)
            {
              if (_formatSpecSet.ContainsKey(kvp.Key))
                throw new Exception("FormatSpec named '" + kvp.Key + "' already exists in the FormatSpecSet.  FormatSpec names " +
                                    "must be unique across all files imported.  The duplicate name exists in the file '" + formatSpecFile + "'.");
              _formatSpecSet.Add(kvp.Key, kvp.Value);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the FormatSpecSet.", ex);
      }
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "Library Tester - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}
