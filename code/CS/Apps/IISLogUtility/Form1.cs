using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IISLogUtility
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      var sb = new StringBuilder();
      var userList = new List<string>();

      var fileList = Directory.GetFiles(@"C:\_work\IIS_Log").ToList();

      foreach (var file in fileList)
      {
        lblStatus.Text = file;
        Application.DoEvents();

        var sr = new StreamReader(file);

        while (sr.Peek() != -1)
        {
          string line = sr.ReadLine().ToLower();
          int pos = line.IndexOf(@"gpnet\");
          if (pos > -1)
          {
            int end = line.IndexOf(' ', pos + 1);
            if (end == -1)
              end = line.Length - 1;
            string user = line.Substring(pos, end - pos).Trim();
            if (!userList.Contains(user))
              userList.Add(user);
          }
        }

        sr.Close();
        sr.Dispose();


        sb.Append(file + Environment.NewLine);
      }

      userList.Sort();

      foreach (var user in userList)
        sb.Append(user + Environment.NewLine);

      string report = sb.ToString();

      textBox1.Text = report;

      this.Cursor = Cursors.Default;
    }
  }
}
