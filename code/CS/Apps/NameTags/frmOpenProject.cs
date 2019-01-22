using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NameTags
{
    public partial class frmOpenProject : Form
    {
        private Project _project;


        public frmOpenProject(Project project)
        {
            InitializeComponent();

            _project = project;
            InitializeForm();

        }

        private void InitializeForm()
        {
            btnOpenProject.Enabled = false;

            //ImageList imageList = new ImageList();
            //System.Drawing.Icon largeIcon = new Icon(ResMain.TagIcon,new Size(64,64));
            //imageList.Images.Add(largeIcon);
            //lvProjects.SmallImageList = imageList;
            //lvProjects.LargeImageList = imageList;
            lvProjects.View = View.Details;

            ColumnHeader ch = new ColumnHeader();
            ch.Text = "File Name";
            ch.Width = 300;
            lvProjects.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Date Modified";
            ch.Width = 150;
            lvProjects.Columns.Add(ch);

            if (!Directory.Exists(ConfigHelper.DefaultProjectPath))
                Directory.CreateDirectory(ConfigHelper.DefaultProjectPath);

            string[] files = Directory.GetFiles(ConfigHelper.DefaultProjectPath, "*.tag");

            foreach(string fileName in files)
            {
                FileInfo fi = new FileInfo(fileName);
                ListViewItem item = new ListViewItem();
                item.Text = Path.GetFileNameWithoutExtension(fileName);
                item.SubItems.Add(fi.LastWriteTime.ToString());
                item.Tag = fileName;
                
                lvProjects.Items.Add(item);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _project.Name = "NO_PROJECT";
            this.Close();
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            SelectProjectAndClose();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvProjects_DoubleClick(object sender, EventArgs e)
        {
            SelectProjectAndClose();
        }

        private void lvProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lvProjects.SelectedItems.Count)
            {
                case 0:
                    btnOpenProject.Enabled = false;
                    break;

                default:
                    btnOpenProject.Enabled = true;
                    break;
            }
        }

        private void SelectProjectAndClose()
        {
            _project.Name = lvProjects.SelectedItems[0].Text;
            _project.FullFileName = lvProjects.SelectedItems[0].Tag.ToString();
            this.Close();
        }
    }
}