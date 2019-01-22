using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.CHCM.Business;
using Models = Org.CHCM.Business.Models;
using Org.GS.Configuration;
using Org.GS;

namespace NameTags
{
	public partial class frmChildren : Form
	{
		private List<Models.Person> _children;
		private ConfigDbSpec _configDbSpec;

		public frmChildren(ConfigDbSpec configDbSpec)
		{
			InitializeComponent();

			_configDbSpec = configDbSpec;

			InitializeForm();
		}


		private void InitializeForm()
		{
			InitializeGrid();
			_children = GetChildren();
			LoadGrid();
		}

		private void LoadGrid()
		{
			gvChildren.Rows.Clear();

			if (_children == null)
				return;

			foreach (var child in _children)
			{
				gvChildren.Rows.Add(new string[] {
					child.LastName,
					child.FirstName,
					(child.SchoolGrade.HasValue ? child.SchoolGrade.Value.ToString() : String.Empty),
					child.Birthday,
					String.Empty
				});
			}
		}


		private void InitializeGrid()
		{
			gvChildren.Columns.Clear();

			DataGridViewColumn col = new DataGridViewTextBoxColumn();
			col.Name = "LastName";
			col.HeaderText = "Last Name";
			col.Width = 120;
			col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			gvChildren.Columns.Add(col);

			col = new DataGridViewTextBoxColumn();
			col.Name = "FirstName";
			col.HeaderText = "First Name";
			col.Width = 120;
			col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			gvChildren.Columns.Add(col);

			col = new DataGridViewTextBoxColumn();
			col.Name = "Grade";
			col.HeaderText = "Grade";
			col.Width = 160;
			col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			gvChildren.Columns.Add(col);

			col = new DataGridViewTextBoxColumn();
			col.Name = "BirthDate";
			col.HeaderText = "Birth Date";
			col.Width = 300;
			col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			gvChildren.Columns.Add(col);

			col = new DataGridViewTextBoxColumn();
			col.Name = "Comments";
			col.HeaderText = "Comments";
			col.Width = 360;
			col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			gvChildren.Columns.Add(col);
		}

		private List<Models.Person> GetChildren()
		{
			try
			{
				List<Models.Person> persons = null;

				using (var repo = new CHCMRepository(_configDbSpec))
				{
					persons = repo.GetPersons();
				}

				return persons;
			}
			catch (Exception ex)
			{
				MessageBox.Show("An exception occurred while attempting to retrieve the list of children from the database." + g.crlf2 + ex.ToReport(),
												"Children List - Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return null;
			}
		}

	}
}
