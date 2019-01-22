using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace NameTags
{
	/// <summary>
	/// Summary description for frmNames.
	/// </summary>
	/// 


	public class frmNames : System.Windows.Forms.Form
	{
		private bool IsUpdateInProgress = false;
		private bool IsUpdateAll = false;
		private string holdFirstName = "";
		private string holdLastName = "";
		private string holdGrade = "";
		private string holdGroup = "";
		private int holdRow = -1;
		private int holdPersonIndex = -1;
		private Enums.EditMode editMode = Enums.EditMode.None;
		private PersonSet _persons;
		private Project _project;

		private System.Windows.Forms.Label lblNameList;
		private System.Windows.Forms.ListView lvNames;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label lblFullName;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.Label lblFullNameLit;
		private System.Windows.Forms.ComboBox cboGrade;
		private System.Windows.Forms.Label lblGrade;
		private System.Windows.Forms.Label lblGroup;
		private System.Windows.Forms.ComboBox cboGroup;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbPersonEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblPersonCount;
		private System.Windows.Forms.Button btnSelectAll;
		private System.Windows.Forms.Button btnUnselectAll;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnReload;
		private Button btnDeleteAll;
		private Button btnExport;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNames(Project project)
		{
			InitializeComponent();
			Application.DoEvents();
			InitializeForm();
			_project = project;
			_persons = project.Persons;
			LoadPersons();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblNameList = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.lvNames = new System.Windows.Forms.ListView();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.lblFullName = new System.Windows.Forms.Label();
			this.lblFirstName = new System.Windows.Forms.Label();
			this.lblLastName = new System.Windows.Forms.Label();
			this.lblFullNameLit = new System.Windows.Forms.Label();
			this.cboGrade = new System.Windows.Forms.ComboBox();
			this.lblGrade = new System.Windows.Forms.Label();
			this.lblGroup = new System.Windows.Forms.Label();
			this.cboGroup = new System.Windows.Forms.ComboBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbPersonEdit = new System.Windows.Forms.GroupBox();
			this.btnDeleteAll = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.lblPersonCount = new System.Windows.Forms.Label();
			this.btnSelectAll = new System.Windows.Forms.Button();
			this.btnUnselectAll = new System.Windows.Forms.Button();
			this.btnReload = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.gbPersonEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblNameList
			// 
			this.lblNameList.Location = new System.Drawing.Point(8, 8);
			this.lblNameList.Name = "lblNameList";
			this.lblNameList.Size = new System.Drawing.Size(280, 24);
			this.lblNameList.TabIndex = 0;
			this.lblNameList.Text = "Make desired changes to the name list.";
			this.lblNameList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(424, 464);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(80, 24);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "OK";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lvNames
			// 
			this.lvNames.CheckBoxes = true;
			this.lvNames.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvNames.FullRowSelect = true;
			this.lvNames.GridLines = true;
			this.lvNames.HideSelection = false;
			this.lvNames.Location = new System.Drawing.Point(8, 32);
			this.lvNames.MultiSelect = false;
			this.lvNames.Name = "lvNames";
			this.lvNames.Size = new System.Drawing.Size(512, 224);
			this.lvNames.TabIndex = 0;
			this.lvNames.UseCompatibleStateImageBehavior = false;
			this.lvNames.View = System.Windows.Forms.View.Details;
			this.lvNames.SelectedIndexChanged += new System.EventHandler(this.lvNames_SelectedIndexChanged);
			this.lvNames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvNames_ItemCheck);
			// 
			// txtFirstName
			// 
			this.txtFirstName.Location = new System.Drawing.Point(16, 40);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(128, 21);
			this.txtFirstName.TabIndex = 3;
			this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
			// 
			// txtLastName
			// 
			this.txtLastName.Location = new System.Drawing.Point(152, 40);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(128, 21);
			this.txtLastName.TabIndex = 4;
			this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
			// 
			// lblFullName
			// 
			this.lblFullName.BackColor = System.Drawing.Color.White;
			this.lblFullName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFullName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFullName.Location = new System.Drawing.Point(296, 40);
			this.lblFullName.Name = "lblFullName";
			this.lblFullName.Size = new System.Drawing.Size(192, 21);
			this.lblFullName.TabIndex = 5;
			// 
			// lblFirstName
			// 
			this.lblFirstName.Location = new System.Drawing.Point(16, 24);
			this.lblFirstName.Name = "lblFirstName";
			this.lblFirstName.Size = new System.Drawing.Size(128, 16);
			this.lblFirstName.TabIndex = 5;
			this.lblFirstName.Text = "First Name:";
			// 
			// lblLastName
			// 
			this.lblLastName.Location = new System.Drawing.Point(152, 24);
			this.lblLastName.Name = "lblLastName";
			this.lblLastName.Size = new System.Drawing.Size(128, 16);
			this.lblLastName.TabIndex = 5;
			this.lblLastName.Text = "Last Name:";
			// 
			// lblFullNameLit
			// 
			this.lblFullNameLit.Location = new System.Drawing.Point(296, 24);
			this.lblFullNameLit.Name = "lblFullNameLit";
			this.lblFullNameLit.Size = new System.Drawing.Size(128, 16);
			this.lblFullNameLit.TabIndex = 5;
			this.lblFullNameLit.Text = "Full Name:";
			// 
			// cboGrade
			// 
			this.cboGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGrade.Items.AddRange(new object[] {
			"",
			"K",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6"});
			this.cboGrade.Location = new System.Drawing.Point(16, 80);
			this.cboGrade.Name = "cboGrade";
			this.cboGrade.Size = new System.Drawing.Size(48, 21);
			this.cboGrade.TabIndex = 5;
			this.cboGrade.SelectedValueChanged += new System.EventHandler(this.cboGrade_SelectedValueChanged);
			// 
			// lblGrade
			// 
			this.lblGrade.Location = new System.Drawing.Point(16, 64);
			this.lblGrade.Name = "lblGrade";
			this.lblGrade.Size = new System.Drawing.Size(48, 16);
			this.lblGrade.TabIndex = 5;
			this.lblGrade.Text = "Grade:";
			// 
			// lblGroup
			// 
			this.lblGroup.Location = new System.Drawing.Point(152, 64);
			this.lblGroup.Name = "lblGroup";
			this.lblGroup.Size = new System.Drawing.Size(48, 16);
			this.lblGroup.TabIndex = 5;
			this.lblGroup.Text = "Group:";
			// 
			// cboGroup
			// 
			this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroup.Items.AddRange(new object[] {
			"",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6"});
			this.cboGroup.Location = new System.Drawing.Point(152, 80);
			this.cboGroup.Name = "cboGroup";
			this.cboGroup.Size = new System.Drawing.Size(48, 21);
			this.cboGroup.TabIndex = 6;
			this.cboGroup.SelectedValueChanged += new System.EventHandler(this.cboGroup_SelectedValueChanged);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(102, 128);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(80, 24);
			this.btnUpdate.TabIndex = 8;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(16, 128);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(80, 24);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Add Person";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(190, 128);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// gbPersonEdit
			// 
			this.gbPersonEdit.Controls.Add(this.lblGroup);
			this.gbPersonEdit.Controls.Add(this.lblGrade);
			this.gbPersonEdit.Controls.Add(this.lblFullName);
			this.gbPersonEdit.Controls.Add(this.lblFullNameLit);
			this.gbPersonEdit.Controls.Add(this.cboGrade);
			this.gbPersonEdit.Controls.Add(this.cboGroup);
			this.gbPersonEdit.Controls.Add(this.txtFirstName);
			this.gbPersonEdit.Controls.Add(this.txtLastName);
			this.gbPersonEdit.Controls.Add(this.lblFirstName);
			this.gbPersonEdit.Controls.Add(this.lblLastName);
			this.gbPersonEdit.Controls.Add(this.btnCancel);
			this.gbPersonEdit.Controls.Add(this.btnUpdate);
			this.gbPersonEdit.Controls.Add(this.btnAdd);
			this.gbPersonEdit.Controls.Add(this.btnDeleteAll);
			this.gbPersonEdit.Controls.Add(this.btnDelete);
			this.gbPersonEdit.Location = new System.Drawing.Point(8, 288);
			this.gbPersonEdit.Name = "gbPersonEdit";
			this.gbPersonEdit.Size = new System.Drawing.Size(512, 168);
			this.gbPersonEdit.TabIndex = 7;
			this.gbPersonEdit.TabStop = false;
			this.gbPersonEdit.Text = "Edit person data below:";
			// 
			// btnDeleteAll
			// 
			this.btnDeleteAll.Location = new System.Drawing.Point(330, 128);
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.Size = new System.Drawing.Size(80, 24);
			this.btnDeleteAll.TabIndex = 10;
			this.btnDeleteAll.Text = "Delete All";
			this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(416, 128);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(80, 24);
			this.btnDelete.TabIndex = 10;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(110, 464);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(80, 24);
			this.btnImport.TabIndex = 7;
			this.btnImport.Text = "Import";
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// lblPersonCount
			// 
			this.lblPersonCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPersonCount.Location = new System.Drawing.Point(8, 264);
			this.lblPersonCount.Name = "lblPersonCount";
			this.lblPersonCount.Size = new System.Drawing.Size(280, 16);
			this.lblPersonCount.TabIndex = 5;
			this.lblPersonCount.Text = "List contains 0 persons.";
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.Location = new System.Drawing.Point(352, 264);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(80, 24);
			this.btnSelectAll.TabIndex = 1;
			this.btnSelectAll.Text = "Select All";
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// btnUnselectAll
			// 
			this.btnUnselectAll.Location = new System.Drawing.Point(440, 264);
			this.btnUnselectAll.Name = "btnUnselectAll";
			this.btnUnselectAll.Size = new System.Drawing.Size(80, 24);
			this.btnUnselectAll.TabIndex = 2;
			this.btnUnselectAll.Text = "Unselect All";
			this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
			// 
			// btnReload
			// 
			this.btnReload.Location = new System.Drawing.Point(24, 464);
			this.btnReload.Name = "btnReload";
			this.btnReload.Size = new System.Drawing.Size(80, 24);
			this.btnReload.TabIndex = 8;
			this.btnReload.Text = "Re-Sort";
			this.btnReload.Visible = false;
			this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(198, 464);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(80, 24);
			this.btnExport.TabIndex = 7;
			this.btnExport.Text = "Export";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// frmNames
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(530, 496);
			this.ControlBox = false;
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnReload);
			this.Controls.Add(this.gbPersonEdit);
			this.Controls.Add(this.lvNames);
			this.Controls.Add(this.lblNameList);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblPersonCount);
			this.Controls.Add(this.btnSelectAll);
			this.Controls.Add(this.btnUnselectAll);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNames";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Names / Select for Printing";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmNames_KeyUp);
			this.gbPersonEdit.ResumeLayout(false);
			this.gbPersonEdit.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion


		private void InitializeForm()
		{
			IsUpdateInProgress = false;
			btnAdd.Enabled = true;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
			btnCancel.Enabled = false;
			btnClose.Enabled = true;
			btnImport.Visible = false;
			DisableFields();
		}

		private void DisableFields()
		{
			txtFirstName.Text = "";
			txtFirstName.Enabled = false;
			txtLastName.Text = "";
			txtLastName.Enabled = false;
			lblFullName.Text = "";
			lblFullName.BackColor = this.BackColor;
			cboGrade.Text = "";
			cboGrade.Enabled = false;
			cboGroup.Text = "";
			cboGroup.Enabled = false;
		}

		private void ClearHoldFields()
		{
			holdFirstName = "";
			holdLastName = "";
			holdGrade = "";
			holdGroup = "";
			holdRow = -1;
		}

		private void ClearFields()
		{
			txtFirstName.Text = "";
			txtLastName.Text = "";
			lblFullName.Text = "";
			cboGrade.Text = "";
			cboGroup.Text = "";
		}

		private void EnableFields()
		{
			txtFirstName.Enabled = true;
			txtLastName.Enabled = true;
			lblFullName.BackColor = Color.White;
			cboGrade.Enabled = true;
			cboGroup.Enabled = true;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void LoadPersons()
		{	
			lvNames.Clear();

			ColumnHeader ch = new ColumnHeader();
			ch.Text = "Full Name";
			ch.Width = 150;	
			lvNames.Columns.Add(ch);

			ch = new ColumnHeader();
			ch.Text = "First Name";
			ch.Width = 100;
			lvNames.Columns.Add(ch);

			ch = new ColumnHeader();
			ch.Text = "Last Name";
			ch.Width = 100;
			lvNames.Columns.Add(ch);

			ch = new ColumnHeader();
			ch.Text = "Grade";
			ch.Width = 60;
			ch.TextAlign = HorizontalAlignment.Right;
			lvNames.Columns.Add(ch);

			ch = new ColumnHeader();
			ch.Text = "Group";
			ch.Width = 60;
			ch.TextAlign = HorizontalAlignment.Right;
			lvNames.Columns.Add(ch);
			
			LoadPersonsToListView();
		}

		private void LoadPersonsToListView()
		{
			lvNames.Items.Clear();

			for(int n = 0; n < _persons.Count; n++)
			{
				Person person = (Person)_persons.Values[n];
				ListViewItem i = new ListViewItem();
				i.Text = person.FullName;
				i.SubItems.Add(person.FirstName);
				i.SubItems.Add(person.LastName);
				i.SubItems.Add(person.Grade);
				i.SubItems.Add(person.Group);
				i.Tag = person.FullName;
				if(person.Selected)
				{
					i.Checked = true;
				}
				else
				{
					i.Checked = false;
				}
			
				lvNames.Items.Add(i);
			}

			lblPersonCount.Text = "List contains " +
				lvNames.Items.Count.ToString().Trim() + " persons - " +
				_project.SelectedPersonCount().ToString() + " selected to print.";
		}

		private bool CheckUpdateInProgress()
		{
			if((txtFirstName.Text.Trim().CompareTo(holdFirstName) == 0)
				& (txtLastName.Text.Trim().CompareTo(holdLastName) == 0)
				& (cboGrade.Text.Trim().CompareTo(holdGrade) == 0)
				& (cboGroup.Text.Trim().CompareTo(holdGroup) == 0))
			{
				return false;
			}
			return true;
		}

		private void lvNames_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lvNames.SelectedItems.Count != 1)
				return;

			EnableFields();
			ListViewItem i = lvNames.SelectedItems[0];
			txtFirstName.Text = i.SubItems[1].Text;
			txtLastName.Text = i.SubItems[2].Text;
			cboGrade.Text = i.SubItems[3].Text;
			cboGroup.Text = i.SubItems[4].Text;
			lblFullName.Text = (txtFirstName.Text + " " + txtLastName.Text).Trim();

			holdPersonIndex = _persons.IndexOfKey(i.Tag.ToString());
			holdRow = lvNames.SelectedIndices[0];
			
			holdFirstName = txtFirstName.Text;
			holdLastName = txtLastName.Text;
			holdGrade = cboGrade.Text;
			holdGroup = cboGroup.Text;

			IsUpdateInProgress = false;
			btnUpdate.Enabled = false;
			btnDelete.Enabled = true;
			btnAdd.Enabled = false;
			btnCancel.Enabled = true;
			editMode = Enums.EditMode.Update;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			editMode = Enums.EditMode.None;
			IsUpdateInProgress = false;
			lvNames.SelectedItems.Clear();
			btnDelete.Enabled = true;
			btnCancel.Enabled = false;
			btnAdd.Enabled = true;
			lvNames.Focus();
			DisableFields();
			ClearHoldFields();
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			editMode = Enums.EditMode.Add;
			IsUpdateInProgress = false;
			EnableFields();
			ClearFields();
			txtFirstName.Focus();

			btnAdd.Enabled = false;
			btnDelete.Enabled = false;
			btnCancel.Enabled = true;
			btnUpdate.Enabled = false;

			cboGrade.SelectedIndex = 1;
			cboGroup.SelectedIndex = 1;
		}

		private void txtFirstName_TextChanged(object sender, System.EventArgs e)
		{
			CheckForChanges();
		}

		private void txtLastName_TextChanged(object sender, System.EventArgs e)
		{
			CheckForChanges();
		}

		private void cboGrade_SelectedValueChanged(object sender, System.EventArgs e)
		{
			CheckForChanges();
		}

		private void cboGroup_SelectedValueChanged(object sender, System.EventArgs e)
		{
			CheckForChanges();
		}

		private void CheckForChanges()
		{
			if (editMode == Enums.EditMode.None)
				return;

			if(IsUpdateInProgress)
				return;

			if(CheckUpdateInProgress())
			{
				btnUpdate.Enabled = true;
			}
			else
			{
				btnUpdate.Enabled = false;
			}
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			switch(editMode)
			{
				case Enums.EditMode.Add:					
					Person p1 = new Person();
					p1.FirstName = txtFirstName.Text.Trim();
					p1.LastName = txtLastName.Text.Trim();
					p1.Grade = cboGrade.Text;
					p1.Group = cboGroup.Text;
					_persons.Add(p1.FullName, p1);
					LoadPersonsToListView();
					break;

				case Enums.EditMode.Update:
					IsUpdateInProgress = true;
					Person p2 = (Person) _persons.Values[holdPersonIndex];
					p2.FirstName = txtFirstName.Text.Trim();
					p2.LastName = txtLastName.Text.Trim();
					p2.Grade = cboGrade.Text;
					p2.Group = cboGroup.Text;
					LoadPersonsToListView();
					break;

				default:
					break;
			}

			btnUpdate.Enabled = false;
			btnDelete.Enabled = false;
			btnAdd.Enabled = true;
			btnCancel.Enabled = false;
			btnAdd.Focus();
			DisableFields();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete the selected person?", "Delete selected person?", MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes)
			{
				_persons.Remove(lblFullName.Text);
				LoadPersonsToListView();

				btnUpdate.Enabled = false;
				btnDelete.Enabled = false;
				btnAdd.Enabled = true;
				btnCancel.Enabled = false;
				DisableFields();
			}
		}

		private void btnDeleteAll_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete all the persons in the list?", "Delete all persons?", MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes)
			{
				_persons.Clear();
				LoadPersonsToListView();
				btnUpdate.Enabled = false;
				btnDelete.Enabled = false;
				btnAdd.Enabled = true;
				btnCancel.Enabled = false;
				DisableFields();
			}
		}

		private void lvNames_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if(IsUpdateAll)
				return;

			ListViewItem item = lvNames.Items[e.Index];
			string fullName = (item.SubItems[1].Text + " " + item.SubItems[2].Text).Trim();
      
      


			Person p = (Person) _persons[fullName];

			if(e.CurrentValue == CheckState.Checked)
			{
				e.NewValue = CheckState.Unchecked;
				p.Selected = false;
			}
			else
			{
				e.NewValue = CheckState.Checked;
				p.Selected = true;
			}

			lblPersonCount.Text = "List contains " +
				lvNames.Items.Count.ToString().Trim() + " persons - " +
				_project.SelectedPersonCount().ToString() + " selected to print.";
		}

		private void btnSelectAll_Click(object sender, System.EventArgs e)
		{
			IsUpdateAll = true;

			for(int n = 0; n < lvNames.Items.Count; n ++)
			{
				ListViewItem item = lvNames.Items[n];
				string fullName = (item.SubItems[1].Text + " " + item.SubItems[2].Text).Trim();
				Person p = (Person) _persons[fullName];
				p.Selected = true;
				item.Checked = true;
			}

			IsUpdateAll = false;

			lblPersonCount.Text = "List contains " +
				lvNames.Items.Count.ToString().Trim() + " persons - " +
				_project.SelectedPersonCount().ToString() + " selected to print.";
		}

		private void btnUnselectAll_Click(object sender, System.EventArgs e)
		{
			IsUpdateAll = true;

			for(int n = 0; n < lvNames.Items.Count; n ++)
			{
				ListViewItem item = lvNames.Items[n];
				string fullName = (item.SubItems[1].Text + " " + item.SubItems[2].Text).Trim();
				Person p = (Person) _persons[fullName];
				p.Selected = false;
				item.Checked = false;
			}

			IsUpdateAll = false;

			lblPersonCount.Text = "List contains " +
				lvNames.Items.Count.ToString().Trim() + " persons - " +
				_project.SelectedPersonCount().ToString() + " selected to print.";
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			_persons.Clear();
			char[] delim = {','};
			StreamReader sr = new StreamReader(ConfigHelper.DefaultImportFileFullPath);
			while (sr.Peek() != -1)
			{
				string line = sr.ReadLine();
				string[] s = line.Split(delim, 5);
				string s1 = s[0];
				Person p = new Person();
				p.LastName = s[0].Trim();
				p.FirstName = s[1].Trim();
				p.Grade = s[3].Trim();
				p.Group = s[4].Trim();
				_persons.Add(p.FullName, p);
			}
			sr.Close();
			LoadPersonsToListView();
		}

		private void btnReload_Click(object sender, System.EventArgs e)
		{
			SortedList sl = new SortedList();
			
			for(int n = 0; n < _persons.Count; n++)
			{
				Person person = (Person) _persons.Values[n];
				sl.Add(person.FullName, person);				
			}

			_persons.Clear();

			for(int n = 0; n < sl.Count; n++)
			{
				Person person = (Person) sl.GetByIndex(n);
				_persons.Add(person.FullName, person);				
			}
			
			LoadPersonsToListView();
		}

		private void frmNames_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
				btnImport.Visible = !btnImport.Visible;

      if (e.KeyCode == Keys.F7)
      {
        switch (editMode)
        {
          case Enums.EditMode.Add:
            Person p1 = new Person();
            p1.FirstName = txtFirstName.Text.Trim();
            p1.LastName = txtLastName.Text.Trim();
            p1.Grade = cboGrade.Text;
            p1.Group = cboGroup.Text;
            _persons.Add(p1.FullName, p1);
            LoadPersonsToListView();
            break;

          case Enums.EditMode.Update:
            IsUpdateInProgress = true;
            Person p2 = (Person)_persons.Values[holdPersonIndex];
            p2.FirstName = txtFirstName.Text.Trim();
            p2.LastName = txtLastName.Text.Trim();
            p2.Grade = cboGrade.Text;
            p2.Group = cboGroup.Text;
            LoadPersonsToListView();
            break;

          default:
            break;
        }

        btnUpdate.Enabled = false;
        btnDelete.Enabled = false;
        btnAdd.Enabled = true;
        btnCancel.Enabled = false;
        btnAdd.Focus();
        DisableFields();

        editMode = Enums.EditMode.Add;
        IsUpdateInProgress = false;
        EnableFields();
        ClearFields();
        txtFirstName.Focus();

        btnAdd.Enabled = false;
        btnDelete.Enabled = false;
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;

        cboGrade.SelectedIndex = 1;
        cboGroup.SelectedIndex = 1;

        e.Handled = true;
      }
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			StreamWriter sw = new StreamWriter(ConfigHelper.DefaultImportFileFullPath);

			foreach(KeyValuePair<string, Person> kvp in _persons)
			{
				sw.WriteLine(kvp.Value.LastName + "," +
					kvp.Value.FirstName + ",X," +
					kvp.Value.Grade + "," +
					kvp.Value.Group);
			}

			sw.Close();
		}





	}
}
