using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NameTags
{
  /// <summary>
  /// Summary description for frmProject.
  /// </summary>
  public class frmProject : System.Windows.Forms.Form
  {
    private Project _project;

    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblWidth;
    private System.Windows.Forms.Label lblNameExample;
    private System.Windows.Forms.Label lblInches1;
    private System.Windows.Forms.Label lblHeight;
    private System.Windows.Forms.Label lblInches2;
    private System.Windows.Forms.PictureBox pbPreview;
    private System.Windows.Forms.GroupBox gbPreview;
    private System.Windows.Forms.ComboBox cboPageSize;
    private System.Windows.Forms.Label lblPageSize;
    private System.Windows.Forms.Label lblOrientation;
    private System.Windows.Forms.ComboBox cboOrientation;
    private System.Windows.Forms.Label lblMargins;
    private System.Windows.Forms.Label lblLeftMargin;
    private System.Windows.Forms.TextBox txtLeftMargin;
    private System.Windows.Forms.Label lblRightMargin;
    private System.Windows.Forms.TextBox txtRightMargin;
    private System.Windows.Forms.Label lblTopMargin;
    private System.Windows.Forms.TextBox txtTopMargin;
    private System.Windows.Forms.TextBox txtBottomMargin;
    private System.Windows.Forms.Label lblBottomMargin;
    private System.Windows.Forms.Label lblSpacing;
    private System.Windows.Forms.Label lblVerticalSpacing;
    private System.Windows.Forms.TextBox txtVerticalSpacing;
    private System.Windows.Forms.TextBox txtHorizontalSpacing;
    private System.Windows.Forms.Label lblHorizontalSpacing;
    private System.Windows.Forms.ComboBox cboAlignment;
    private System.Windows.Forms.Label lblAlignment;
    private System.Windows.Forms.GroupBox gbPageProperties;
    private System.Windows.Forms.GroupBox gbMargins;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox gbPrinterAdjustments;
    private System.Windows.Forms.Label lblPrinterAdjHorizontal;
    private System.Windows.Forms.Label lblPrinterAdjVertical;
    private System.Windows.Forms.Label lblMarginInfo;
    private System.Windows.Forms.TextBox txtPrintAdjVertical;
    private System.Windows.Forms.TextBox txtPrintAdjHorizontal;
    private System.Windows.Forms.TextBox txtNameTagWidth;
    private System.Windows.Forms.TextBox txtNameTagHeight;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public frmProject(Project project)
    {
      InitializeComponent();

      _project = project;
      DisplayProjectData();
    }

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
      this.btnUpdate = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtName = new System.Windows.Forms.TextBox();
      this.lblName = new System.Windows.Forms.Label();
      this.lblNameExample = new System.Windows.Forms.Label();
      this.lblWidth = new System.Windows.Forms.Label();
      this.txtNameTagWidth = new System.Windows.Forms.TextBox();
      this.lblInches1 = new System.Windows.Forms.Label();
      this.lblHeight = new System.Windows.Forms.Label();
      this.txtNameTagHeight = new System.Windows.Forms.TextBox();
      this.lblInches2 = new System.Windows.Forms.Label();
      this.pbPreview = new System.Windows.Forms.PictureBox();
      this.gbPreview = new System.Windows.Forms.GroupBox();
      this.cboPageSize = new System.Windows.Forms.ComboBox();
      this.lblPageSize = new System.Windows.Forms.Label();
      this.lblOrientation = new System.Windows.Forms.Label();
      this.cboOrientation = new System.Windows.Forms.ComboBox();
      this.lblLeftMargin = new System.Windows.Forms.Label();
      this.lblMargins = new System.Windows.Forms.Label();
      this.txtLeftMargin = new System.Windows.Forms.TextBox();
      this.lblRightMargin = new System.Windows.Forms.Label();
      this.txtRightMargin = new System.Windows.Forms.TextBox();
      this.lblTopMargin = new System.Windows.Forms.Label();
      this.txtTopMargin = new System.Windows.Forms.TextBox();
      this.txtBottomMargin = new System.Windows.Forms.TextBox();
      this.lblBottomMargin = new System.Windows.Forms.Label();
      this.lblSpacing = new System.Windows.Forms.Label();
      this.lblVerticalSpacing = new System.Windows.Forms.Label();
      this.txtVerticalSpacing = new System.Windows.Forms.TextBox();
      this.txtHorizontalSpacing = new System.Windows.Forms.TextBox();
      this.lblHorizontalSpacing = new System.Windows.Forms.Label();
      this.cboAlignment = new System.Windows.Forms.ComboBox();
      this.lblAlignment = new System.Windows.Forms.Label();
      this.gbPageProperties = new System.Windows.Forms.GroupBox();
      this.gbMargins = new System.Windows.Forms.GroupBox();
      this.lblMarginInfo = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.gbPrinterAdjustments = new System.Windows.Forms.GroupBox();
      this.lblPrinterAdjHorizontal = new System.Windows.Forms.Label();
      this.txtPrintAdjHorizontal = new System.Windows.Forms.TextBox();
      this.lblPrinterAdjVertical = new System.Windows.Forms.Label();
      this.txtPrintAdjVertical = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
      this.gbPreview.SuspendLayout();
      this.gbPageProperties.SuspendLayout();
      this.gbMargins.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.gbPrinterAdjustments.SuspendLayout();
      this.SuspendLayout();
      //
      // btnUpdate
      //
      this.btnUpdate.Location = new System.Drawing.Point(8, 544);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(104, 24);
      this.btnUpdate.TabIndex = 19;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      //
      // btnCancel
      //
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(264, 544);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(104, 24);
      this.btnCancel.TabIndex = 20;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // txtName
      //
      this.txtName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtName.Location = new System.Drawing.Point(104, 24);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(264, 21);
      this.txtName.TabIndex = 0;
      //
      // lblName
      //
      this.lblName.Location = new System.Drawing.Point(8, 24);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(88, 23);
      this.lblName.TabIndex = 21;
      this.lblName.Text = "Project name:";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblNameExample
      //
      this.lblNameExample.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNameExample.Location = new System.Drawing.Point(104, 48);
      this.lblNameExample.Name = "lblNameExample";
      this.lblNameExample.Size = new System.Drawing.Size(176, 16);
      this.lblNameExample.TabIndex = 22;
      this.lblNameExample.Text = "example: Truth School Jr. - 2007";
      this.lblNameExample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblWidth
      //
      this.lblWidth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblWidth.Location = new System.Drawing.Point(24, 24);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(48, 23);
      this.lblWidth.TabIndex = 24;
      this.lblWidth.Text = "Width:";
      this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtNameTagWidth
      //
      this.txtNameTagWidth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtNameTagWidth.Location = new System.Drawing.Point(96, 24);
      this.txtNameTagWidth.Name = "txtNameTagWidth";
      this.txtNameTagWidth.Size = new System.Drawing.Size(56, 21);
      this.txtNameTagWidth.TabIndex = 14;
      this.txtNameTagWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblInches1
      //
      this.lblInches1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInches1.Location = new System.Drawing.Point(160, 24);
      this.lblInches1.Name = "lblInches1";
      this.lblInches1.Size = new System.Drawing.Size(184, 23);
      this.lblInches1.TabIndex = 26;
      this.lblInches1.Text = "inches ";
      this.lblInches1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblHeight
      //
      this.lblHeight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblHeight.Location = new System.Drawing.Point(24, 48);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(48, 23);
      this.lblHeight.TabIndex = 25;
      this.lblHeight.Text = "Height:";
      this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtNameTagHeight
      //
      this.txtNameTagHeight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtNameTagHeight.Location = new System.Drawing.Point(96, 48);
      this.txtNameTagHeight.Name = "txtNameTagHeight";
      this.txtNameTagHeight.Size = new System.Drawing.Size(56, 21);
      this.txtNameTagHeight.TabIndex = 15;
      this.txtNameTagHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblInches2
      //
      this.lblInches2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblInches2.Location = new System.Drawing.Point(160, 48);
      this.lblInches2.Name = "lblInches2";
      this.lblInches2.Size = new System.Drawing.Size(184, 23);
      this.lblInches2.TabIndex = 2;
      this.lblInches2.Text = "inches";
      this.lblInches2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pbPreview
      //
      this.pbPreview.BackColor = System.Drawing.Color.White;
      this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pbPreview.Location = new System.Drawing.Point(16, 24);
      this.pbPreview.Name = "pbPreview";
      this.pbPreview.Size = new System.Drawing.Size(298, 385);
      this.pbPreview.TabIndex = 28;
      this.pbPreview.TabStop = false;
      //
      // gbPreview
      //
      this.gbPreview.Controls.Add(this.pbPreview);
      this.gbPreview.Location = new System.Drawing.Point(384, 16);
      this.gbPreview.Name = "gbPreview";
      this.gbPreview.Size = new System.Drawing.Size(328, 552);
      this.gbPreview.TabIndex = 29;
      this.gbPreview.TabStop = false;
      this.gbPreview.Text = "Preview printed page";
      //
      // cboPageSize
      //
      this.cboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPageSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboPageSize.Items.AddRange(new object[] {
        "Letter (8.5 x 11 inches)",
        "Legal  (8.5 x 14 inches)"
      });
      this.cboPageSize.Location = new System.Drawing.Point(96, 24);
      this.cboPageSize.Name = "cboPageSize";
      this.cboPageSize.Size = new System.Drawing.Size(232, 21);
      this.cboPageSize.TabIndex = 3;
      //
      // lblPageSize
      //
      this.lblPageSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPageSize.Location = new System.Drawing.Point(16, 24);
      this.lblPageSize.Name = "lblPageSize";
      this.lblPageSize.Size = new System.Drawing.Size(48, 23);
      this.lblPageSize.TabIndex = 24;
      this.lblPageSize.Text = "Size:";
      this.lblPageSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblOrientation
      //
      this.lblOrientation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrientation.Location = new System.Drawing.Point(16, 48);
      this.lblOrientation.Name = "lblOrientation";
      this.lblOrientation.Size = new System.Drawing.Size(80, 23);
      this.lblOrientation.TabIndex = 24;
      this.lblOrientation.Text = "Orientation:";
      this.lblOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // cboOrientation
      //
      this.cboOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboOrientation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboOrientation.Items.AddRange(new object[] {
        "Portrait",
        "Landscape"
      });
      this.cboOrientation.Location = new System.Drawing.Point(96, 48);
      this.cboOrientation.Name = "cboOrientation";
      this.cboOrientation.Size = new System.Drawing.Size(232, 21);
      this.cboOrientation.TabIndex = 4;
      //
      // lblLeftMargin
      //
      this.lblLeftMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLeftMargin.Location = new System.Drawing.Point(24, 40);
      this.lblLeftMargin.Name = "lblLeftMargin";
      this.lblLeftMargin.Size = new System.Drawing.Size(48, 24);
      this.lblLeftMargin.TabIndex = 24;
      this.lblLeftMargin.Text = "Left:";
      this.lblLeftMargin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMargins
      //
      this.lblMargins.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMargins.Location = new System.Drawing.Point(16, 16);
      this.lblMargins.Name = "lblMargins";
      this.lblMargins.Size = new System.Drawing.Size(104, 23);
      this.lblMargins.TabIndex = 23;
      this.lblMargins.Text = "Margins:";
      this.lblMargins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtLeftMargin
      //
      this.txtLeftMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLeftMargin.Location = new System.Drawing.Point(96, 48);
      this.txtLeftMargin.Name = "txtLeftMargin";
      this.txtLeftMargin.Size = new System.Drawing.Size(56, 21);
      this.txtLeftMargin.TabIndex = 7;
      this.txtLeftMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblRightMargin
      //
      this.lblRightMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRightMargin.Location = new System.Drawing.Point(24, 64);
      this.lblRightMargin.Name = "lblRightMargin";
      this.lblRightMargin.Size = new System.Drawing.Size(48, 24);
      this.lblRightMargin.TabIndex = 24;
      this.lblRightMargin.Text = "Right:";
      this.lblRightMargin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtRightMargin
      //
      this.txtRightMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRightMargin.Location = new System.Drawing.Point(96, 72);
      this.txtRightMargin.Name = "txtRightMargin";
      this.txtRightMargin.Size = new System.Drawing.Size(56, 21);
      this.txtRightMargin.TabIndex = 8;
      this.txtRightMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblTopMargin
      //
      this.lblTopMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTopMargin.Location = new System.Drawing.Point(24, 88);
      this.lblTopMargin.Name = "lblTopMargin";
      this.lblTopMargin.Size = new System.Drawing.Size(48, 24);
      this.lblTopMargin.TabIndex = 24;
      this.lblTopMargin.Text = "Top:";
      this.lblTopMargin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtTopMargin
      //
      this.txtTopMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTopMargin.Location = new System.Drawing.Point(96, 96);
      this.txtTopMargin.Name = "txtTopMargin";
      this.txtTopMargin.Size = new System.Drawing.Size(56, 21);
      this.txtTopMargin.TabIndex = 9;
      this.txtTopMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // txtBottomMargin
      //
      this.txtBottomMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtBottomMargin.Location = new System.Drawing.Point(96, 120);
      this.txtBottomMargin.Name = "txtBottomMargin";
      this.txtBottomMargin.Size = new System.Drawing.Size(56, 21);
      this.txtBottomMargin.TabIndex = 10;
      this.txtBottomMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblBottomMargin
      //
      this.lblBottomMargin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblBottomMargin.Location = new System.Drawing.Point(24, 112);
      this.lblBottomMargin.Name = "lblBottomMargin";
      this.lblBottomMargin.Size = new System.Drawing.Size(56, 24);
      this.lblBottomMargin.TabIndex = 24;
      this.lblBottomMargin.Text = "Bottom:";
      this.lblBottomMargin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblSpacing
      //
      this.lblSpacing.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSpacing.Location = new System.Drawing.Point(192, 16);
      this.lblSpacing.Name = "lblSpacing";
      this.lblSpacing.Size = new System.Drawing.Size(144, 24);
      this.lblSpacing.TabIndex = 23;
      this.lblSpacing.Text = "Spacing between tags:";
      this.lblSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblVerticalSpacing
      //
      this.lblVerticalSpacing.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVerticalSpacing.Location = new System.Drawing.Point(200, 40);
      this.lblVerticalSpacing.Name = "lblVerticalSpacing";
      this.lblVerticalSpacing.Size = new System.Drawing.Size(56, 24);
      this.lblVerticalSpacing.TabIndex = 24;
      this.lblVerticalSpacing.Text = "Vertical:";
      this.lblVerticalSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtVerticalSpacing
      //
      this.txtVerticalSpacing.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtVerticalSpacing.Location = new System.Drawing.Point(272, 48);
      this.txtVerticalSpacing.Name = "txtVerticalSpacing";
      this.txtVerticalSpacing.Size = new System.Drawing.Size(56, 21);
      this.txtVerticalSpacing.TabIndex = 11;
      this.txtVerticalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // txtHorizontalSpacing
      //
      this.txtHorizontalSpacing.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtHorizontalSpacing.Location = new System.Drawing.Point(272, 72);
      this.txtHorizontalSpacing.Name = "txtHorizontalSpacing";
      this.txtHorizontalSpacing.Size = new System.Drawing.Size(56, 21);
      this.txtHorizontalSpacing.TabIndex = 12;
      this.txtHorizontalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblHorizontalSpacing
      //
      this.lblHorizontalSpacing.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblHorizontalSpacing.Location = new System.Drawing.Point(200, 64);
      this.lblHorizontalSpacing.Name = "lblHorizontalSpacing";
      this.lblHorizontalSpacing.Size = new System.Drawing.Size(72, 24);
      this.lblHorizontalSpacing.TabIndex = 24;
      this.lblHorizontalSpacing.Text = "Horizontal:";
      this.lblHorizontalSpacing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // cboAlignment
      //
      this.cboAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboAlignment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboAlignment.Items.AddRange(new object[] {
        "Align left",
        "Center on page"
      });
      this.cboAlignment.Location = new System.Drawing.Point(96, 72);
      this.cboAlignment.Name = "cboAlignment";
      this.cboAlignment.Size = new System.Drawing.Size(232, 21);
      this.cboAlignment.TabIndex = 5;
      //
      // lblAlignment
      //
      this.lblAlignment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAlignment.Location = new System.Drawing.Point(16, 72);
      this.lblAlignment.Name = "lblAlignment";
      this.lblAlignment.Size = new System.Drawing.Size(80, 23);
      this.lblAlignment.TabIndex = 24;
      this.lblAlignment.Text = "Alignment:";
      this.lblAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gbPageProperties
      //
      this.gbPageProperties.Controls.Add(this.cboAlignment);
      this.gbPageProperties.Controls.Add(this.lblAlignment);
      this.gbPageProperties.Controls.Add(this.cboPageSize);
      this.gbPageProperties.Controls.Add(this.lblPageSize);
      this.gbPageProperties.Controls.Add(this.lblOrientation);
      this.gbPageProperties.Controls.Add(this.cboOrientation);
      this.gbPageProperties.Location = new System.Drawing.Point(8, 80);
      this.gbPageProperties.Name = "gbPageProperties";
      this.gbPageProperties.Size = new System.Drawing.Size(360, 104);
      this.gbPageProperties.TabIndex = 2;
      this.gbPageProperties.TabStop = false;
      this.gbPageProperties.Text = "Enter page size, orientation and alignment";
      //
      // gbMargins
      //
      this.gbMargins.Controls.Add(this.lblMarginInfo);
      this.gbMargins.Controls.Add(this.txtHorizontalSpacing);
      this.gbMargins.Controls.Add(this.lblHorizontalSpacing);
      this.gbMargins.Controls.Add(this.lblLeftMargin);
      this.gbMargins.Controls.Add(this.lblMargins);
      this.gbMargins.Controls.Add(this.txtLeftMargin);
      this.gbMargins.Controls.Add(this.lblRightMargin);
      this.gbMargins.Controls.Add(this.txtRightMargin);
      this.gbMargins.Controls.Add(this.lblTopMargin);
      this.gbMargins.Controls.Add(this.txtTopMargin);
      this.gbMargins.Controls.Add(this.txtBottomMargin);
      this.gbMargins.Controls.Add(this.lblBottomMargin);
      this.gbMargins.Controls.Add(this.lblSpacing);
      this.gbMargins.Controls.Add(this.lblVerticalSpacing);
      this.gbMargins.Controls.Add(this.txtVerticalSpacing);
      this.gbMargins.Location = new System.Drawing.Point(8, 192);
      this.gbMargins.Name = "gbMargins";
      this.gbMargins.Size = new System.Drawing.Size(360, 152);
      this.gbMargins.TabIndex = 6;
      this.gbMargins.TabStop = false;
      this.gbMargins.Text = "Enter page margins and spacing between tags";
      //
      // lblMarginInfo
      //
      this.lblMarginInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMarginInfo.Location = new System.Drawing.Point(168, 96);
      this.lblMarginInfo.Name = "lblMarginInfo";
      this.lblMarginInfo.Size = new System.Drawing.Size(184, 40);
      this.lblMarginInfo.TabIndex = 28;
      this.lblMarginInfo.Text = "Margins and spacing measurements are in inches and hundredths - for example 0.25 " +
                                "is 1/4 inch.";
      //
      // groupBox1
      //
      this.groupBox1.Controls.Add(this.lblWidth);
      this.groupBox1.Controls.Add(this.txtNameTagWidth);
      this.groupBox1.Controls.Add(this.lblInches1);
      this.groupBox1.Controls.Add(this.lblHeight);
      this.groupBox1.Controls.Add(this.txtNameTagHeight);
      this.groupBox1.Controls.Add(this.lblInches2);
      this.groupBox1.Location = new System.Drawing.Point(8, 352);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(360, 120);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Enter name tag dimensions";
      //
      // gbPrinterAdjustments
      //
      this.gbPrinterAdjustments.Controls.Add(this.lblPrinterAdjHorizontal);
      this.gbPrinterAdjustments.Controls.Add(this.txtPrintAdjHorizontal);
      this.gbPrinterAdjustments.Controls.Add(this.lblPrinterAdjVertical);
      this.gbPrinterAdjustments.Controls.Add(this.txtPrintAdjVertical);
      this.gbPrinterAdjustments.Location = new System.Drawing.Point(8, 480);
      this.gbPrinterAdjustments.Name = "gbPrinterAdjustments";
      this.gbPrinterAdjustments.Size = new System.Drawing.Size(360, 56);
      this.gbPrinterAdjustments.TabIndex = 16;
      this.gbPrinterAdjustments.TabStop = false;
      this.gbPrinterAdjustments.Text = "Enter printer adjustments";
      //
      // lblPrinterAdjHorizontal
      //
      this.lblPrinterAdjHorizontal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPrinterAdjHorizontal.Location = new System.Drawing.Point(24, 24);
      this.lblPrinterAdjHorizontal.Name = "lblPrinterAdjHorizontal";
      this.lblPrinterAdjHorizontal.Size = new System.Drawing.Size(64, 24);
      this.lblPrinterAdjHorizontal.TabIndex = 24;
      this.lblPrinterAdjHorizontal.Text = "Horizontal:";
      this.lblPrinterAdjHorizontal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtPrintAdjHorizontal
      //
      this.txtPrintAdjHorizontal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPrintAdjHorizontal.Location = new System.Drawing.Point(96, 24);
      this.txtPrintAdjHorizontal.Name = "txtPrintAdjHorizontal";
      this.txtPrintAdjHorizontal.Size = new System.Drawing.Size(56, 21);
      this.txtPrintAdjHorizontal.TabIndex = 17;
      this.txtPrintAdjHorizontal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblPrinterAdjVertical
      //
      this.lblPrinterAdjVertical.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPrinterAdjVertical.Location = new System.Drawing.Point(200, 24);
      this.lblPrinterAdjVertical.Name = "lblPrinterAdjVertical";
      this.lblPrinterAdjVertical.Size = new System.Drawing.Size(56, 24);
      this.lblPrinterAdjVertical.TabIndex = 24;
      this.lblPrinterAdjVertical.Text = "Vertical:";
      this.lblPrinterAdjVertical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtPrintAdjVertical
      //
      this.txtPrintAdjVertical.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPrintAdjVertical.Location = new System.Drawing.Point(272, 24);
      this.txtPrintAdjVertical.Name = "txtPrintAdjVertical";
      this.txtPrintAdjVertical.Size = new System.Drawing.Size(56, 21);
      this.txtPrintAdjVertical.TabIndex = 18;
      this.txtPrintAdjVertical.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // frmProject
      //
      this.AcceptButton = this.btnUpdate;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(726, 576);
      this.Controls.Add(this.gbPrinterAdjustments);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.gbMargins);
      this.Controls.Add(this.gbPageProperties);
      this.Controls.Add(this.gbPreview);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.btnUpdate);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.lblNameExample);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmProject";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Project Properties";
      ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
      this.gbPreview.ResumeLayout(false);
      this.gbPageProperties.ResumeLayout(false);
      this.gbMargins.ResumeLayout(false);
      this.gbMargins.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.gbPrinterAdjustments.ResumeLayout(false);
      this.gbPrinterAdjustments.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    public void DisplayProjectData()
    {
      txtName.Text = _project.Name;

      if (_project.PrintPageSettings.PageSize == Enums.PageSize.Letter)
        cboPageSize.SelectedIndex = 0;
      else
        cboPageSize.SelectedIndex = 1;

      if (_project.PrintPageSettings.PageOrientation == Enums.PageOrientation.Portrait)
        cboOrientation.SelectedIndex = 0;
      else
        cboOrientation.SelectedIndex = 1;

      if (_project.PrintPageSettings.PageAlignment == Enums.PageAlignment.Left)
        cboAlignment.SelectedIndex = 0;
      else
        cboAlignment.SelectedIndex = 1;

      txtLeftMargin.Text = Convert.ToString(_project.PrintPageSettings.LeftMargin / 100);
      txtRightMargin.Text = Convert.ToString(_project.PrintPageSettings.RightMargin / 100);
      txtTopMargin.Text = Convert.ToString(_project.PrintPageSettings.TopMargin / 100);
      txtBottomMargin.Text = Convert.ToString(_project.PrintPageSettings.BottomMargin / 100);
      txtVerticalSpacing.Text = Convert.ToString(_project.PrintPageSettings.VerticalSpacing / 100);
      txtHorizontalSpacing.Text = Convert.ToString(_project.PrintPageSettings.HorizontalSpacing / 100);
      txtNameTagWidth.Text = Convert.ToString(_project.PrintPageSettings.NameTagWidth / 100);
      txtNameTagHeight.Text = Convert.ToString(_project.PrintPageSettings.NameTagHeight / 100);
      txtPrintAdjHorizontal.Text = Convert.ToString(_project.PrintPageSettings.PrintAdjustHorizontal / 100);
      txtPrintAdjVertical.Text = Convert.ToString(_project.PrintPageSettings.PrintAdjustVertical / 100);

    }

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
      CloseTheForm();
    }

    private void btnUpdate_Click(object sender, System.EventArgs e)
    {
      if(ValidateInput())
      {
        _project.Name = txtName.Text;

        if (cboPageSize.SelectedIndex == 0)
          _project.PrintPageSettings.PageSize = Enums.PageSize.Letter;
        else
          _project.PrintPageSettings.PageSize = Enums.PageSize.Legal;

        if (cboOrientation.SelectedIndex == 0)
          _project.PrintPageSettings.PageOrientation = Enums.PageOrientation.Portrait;
        else
          _project.PrintPageSettings.PageOrientation = Enums.PageOrientation.Landscape;

        if (cboAlignment.SelectedIndex == 0)
          _project.PrintPageSettings.PageAlignment = Enums.PageAlignment.Left;
        else
          _project.PrintPageSettings.PageAlignment = Enums.PageAlignment.Center;

        _project.PrintPageSettings.LeftMargin = float.Parse(txtLeftMargin.Text) * 100;
        _project.PrintPageSettings.RightMargin = float.Parse(txtRightMargin.Text) * 100;
        _project.PrintPageSettings.TopMargin = float.Parse(txtTopMargin.Text) * 100;
        _project.PrintPageSettings.BottomMargin = float.Parse(txtBottomMargin.Text) * 100;
        _project.PrintPageSettings.VerticalSpacing = float.Parse(txtVerticalSpacing.Text) * 100;
        _project.PrintPageSettings.HorizontalSpacing = float.Parse(txtHorizontalSpacing.Text) * 100;
        _project.PrintPageSettings.NameTagWidth = float.Parse(txtNameTagWidth.Text) * 100;
        _project.PrintPageSettings.NameTagHeight = float.Parse(txtNameTagHeight.Text) * 100;
        _project.PrintPageSettings.PrintAdjustHorizontal = float.Parse(txtPrintAdjHorizontal.Text) * 100;
        _project.PrintPageSettings.PrintAdjustVertical = float.Parse(txtPrintAdjVertical.Text) * 100;

        this.DialogResult = DialogResult.OK;
      }
      else
      {
        return;
      }

      CloseTheForm();
    }

    private void CloseTheForm()
    {
      this.Close();
    }

    private bool ValidateInput()
    {
      if (txtName.Text.Length == 0)
      {
        MessageBox.Show("Project Name must be entered.", "Project Properties Error");
        txtName.Focus();
        return false;
      }

      if (txtNameTagWidth.Text.Length == 0)
      {
        MessageBox.Show("The width of the name tag must be entered.", "Project Properties Error");
        txtNameTagWidth.Focus();
        return false;
      }

      if (txtNameTagHeight.Text.Length == 0)
      {
        MessageBox.Show("The height of the name tag must be entered.", "Project Properties Error");
        txtNameTagHeight.Focus();
        return false;
      }

      foreach(char c in txtNameTagWidth.Text)
      {
        if(!(Char.IsNumber(c) | c == '.'))
        {
          MessageBox.Show("Invalid decimal format in name tag width.", "Project Properties Error");
          txtNameTagWidth.SelectAll();
          txtNameTagWidth.Focus();
          return false;
        }
      }

      foreach(char c in txtNameTagHeight.Text)
      {
        if(!(Char.IsNumber(c) | c == '.'))
        {
          MessageBox.Show("Invalid decimal format in name tag height.", "Project Properties Error");
          txtNameTagHeight.SelectAll();
          txtNameTagHeight.Focus();
          return false;
        }
      }
      float fWidth = float.Parse(txtNameTagWidth.Text);
      float fHeight = float.Parse(txtNameTagHeight.Text);

      //if (fWidth < 1.0 | fWidth > 8.0)
      //{
      //    MessageBox.Show("Width invalid, must be from 1 inch to 8 inches", "Project Properties Error");
      //    txtNameTagWidth.SelectAll();
      //    txtNameTagWidth.Focus();
      //    return false;
      //}

      //if (fHeight < 1.0 | fHeight > 10.0)
      //{
      //    MessageBox.Show("Height invalid, must be from 1 inch to 10 inches", "Project Properties Error");
      //    txtNameTagHeight.SelectAll();
      //    txtNameTagHeight.Focus();
      //    return false;
      //}

      //double rem = fWidth % 0.125;
      //if (rem != 0)
      //{
      //    MessageBox.Show("Width must be divisible by 0.125 (eights of an inch) \n\r" +
      //        "(i.e. must have no decimal or end in one of the following: \n\r" +
      //        ".0125, .025, .0375, .5, .625, .75, or .875)", "Project Properties Error");
      //    txtNameTagWidth.SelectAll();
      //    txtNameTagWidth.Focus();
      //    return false;
      //}

      //rem = fHeight % 0.125;
      //if (rem != 0)
      //{
      //    MessageBox.Show("Height must be divisible by 0.125 (eights of an inch) \n\r" +
      //        "(i.e. must have no decimal or end in one of the following: \n\r" +
      //        ".0125, .025, .0375, .5, .625, .75, or .875)", "Project Properties Error");
      //    txtNameTagHeight.SelectAll();
      //    txtNameTagHeight.Focus();
      //    return false;
      //}

      return true;
    }





  }
}
