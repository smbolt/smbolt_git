namespace Teleflora.Operations.MetricView
{
  partial class frmMetricProperties
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblEnvironmentPrompt = new System.Windows.Forms.Label();
      this.lblEnvironmentID = new System.Windows.Forms.Label();
      this.txtGraphLegendLabel = new System.Windows.Forms.TextBox();
      this.lblEnvironmentDesc = new System.Windows.Forms.Label();
      this.lblMetricTypePrompt = new System.Windows.Forms.Label();
      this.lblMetricTypeID = new System.Windows.Forms.Label();
      this.lblMetricTypeDesc = new System.Windows.Forms.Label();
      this.lblTargetSystemPrompt = new System.Windows.Forms.Label();
      this.lblTargetSystemID = new System.Windows.Forms.Label();
      this.lblTargetSystemDesc = new System.Windows.Forms.Label();
      this.lblTargetApplicationPrompt = new System.Windows.Forms.Label();
      this.lblTargetApplicationID = new System.Windows.Forms.Label();
      this.lblTargetApplicationDesc = new System.Windows.Forms.Label();
      this.lblMetricStatePrompt = new System.Windows.Forms.Label();
      this.lblMetricStateID = new System.Windows.Forms.Label();
      this.lblMetricStateDesc = new System.Windows.Forms.Label();
      this.lblAggregateTypePrompt = new System.Windows.Forms.Label();
      this.lblAggregateTypeID = new System.Windows.Forms.Label();
      this.lblAggregateTypeDesc = new System.Windows.Forms.Label();
      this.lblMetricValueTypePrompt = new System.Windows.Forms.Label();
      this.lblMetricValueTypeID = new System.Windows.Forms.Label();
      this.lblMetricValueTypeDesc = new System.Windows.Forms.Label();
      this.lblIntervalTypePrompt = new System.Windows.Forms.Label();
      this.lblIntervalTypeID = new System.Windows.Forms.Label();
      this.lblIntervalTypeDesc = new System.Windows.Forms.Label();
      this.lblObserverSystemPrompt = new System.Windows.Forms.Label();
      this.lblObserverSystemID = new System.Windows.Forms.Label();
      this.lblObserverSystemDesc = new System.Windows.Forms.Label();
      this.lblObserverApplicationPrompt = new System.Windows.Forms.Label();
      this.lblObserverApplicationID = new System.Windows.Forms.Label();
      this.lblObserverApplicationDesc = new System.Windows.Forms.Label();
      this.lblObserverServerPrompt = new System.Windows.Forms.Label();
      this.lblObserverServerID = new System.Windows.Forms.Label();
      this.lblObserverServerDesc = new System.Windows.Forms.Label();
      this.lblMetricPrompt = new System.Windows.Forms.Label();
      this.lblMetricID = new System.Windows.Forms.Label();
      this.lblMetricDesc = new System.Windows.Forms.Label();
      this.lblGraphLegendPrompt = new System.Windows.Forms.Label();
      this.gbSetLegendLabel = new System.Windows.Forms.GroupBox();
      this.cboGraphLabelStyles = new System.Windows.Forms.ComboBox();
      this.lblSelectLegendStyle = new System.Windows.Forms.Label();
      this.gbMetricProperties = new System.Windows.Forms.GroupBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtSQLCommand = new System.Windows.Forms.TextBox();
      this.gbForecastFromFile = new System.Windows.Forms.GroupBox();
      this.btnGetFile = new System.Windows.Forms.Button();
      this.ckUseYOYData = new System.Windows.Forms.CheckBox();
      this.ckForecastFromFile = new System.Windows.Forms.CheckBox();
      this.lblForecastFileName = new System.Windows.Forms.Label();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.lblMultiplier = new System.Windows.Forms.Label();
      this.txtMultiplier = new System.Windows.Forms.TextBox();
      this.gbSetLegendLabel.SuspendLayout();
      this.gbMetricProperties.SuspendLayout();
      this.gbForecastFromFile.SuspendLayout();
      this.SuspendLayout();
      //
      // lblEnvironmentPrompt
      //
      this.lblEnvironmentPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEnvironmentPrompt.Location = new System.Drawing.Point(49, 20);
      this.lblEnvironmentPrompt.Name = "lblEnvironmentPrompt";
      this.lblEnvironmentPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblEnvironmentPrompt.TabIndex = 0;
      this.lblEnvironmentPrompt.Text = "Environment ID / Desc:";
      this.lblEnvironmentPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblEnvironmentID
      //
      this.lblEnvironmentID.BackColor = System.Drawing.Color.White;
      this.lblEnvironmentID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblEnvironmentID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEnvironmentID.Location = new System.Drawing.Point(248, 20);
      this.lblEnvironmentID.Name = "lblEnvironmentID";
      this.lblEnvironmentID.Size = new System.Drawing.Size(53, 21);
      this.lblEnvironmentID.TabIndex = 1;
      this.lblEnvironmentID.Text = "1";
      this.lblEnvironmentID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // txtGraphLegendLabel
      //
      this.txtGraphLegendLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtGraphLegendLabel.Location = new System.Drawing.Point(52, 95);
      this.txtGraphLegendLabel.Name = "txtGraphLegendLabel";
      this.txtGraphLegendLabel.Size = new System.Drawing.Size(249, 21);
      this.txtGraphLegendLabel.TabIndex = 2;
      //
      // lblEnvironmentDesc
      //
      this.lblEnvironmentDesc.BackColor = System.Drawing.Color.White;
      this.lblEnvironmentDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblEnvironmentDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEnvironmentDesc.Location = new System.Drawing.Point(302, 20);
      this.lblEnvironmentDesc.Name = "lblEnvironmentDesc";
      this.lblEnvironmentDesc.Size = new System.Drawing.Size(188, 21);
      this.lblEnvironmentDesc.TabIndex = 1;
      this.lblEnvironmentDesc.Text = "EnvironmentDesc";
      this.lblEnvironmentDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricTypePrompt
      //
      this.lblMetricTypePrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricTypePrompt.Location = new System.Drawing.Point(49, 41);
      this.lblMetricTypePrompt.Name = "lblMetricTypePrompt";
      this.lblMetricTypePrompt.Size = new System.Drawing.Size(193, 21);
      this.lblMetricTypePrompt.TabIndex = 0;
      this.lblMetricTypePrompt.Text = "Metric Type ID / Desc:";
      this.lblMetricTypePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricTypeID
      //
      this.lblMetricTypeID.BackColor = System.Drawing.Color.White;
      this.lblMetricTypeID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricTypeID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricTypeID.Location = new System.Drawing.Point(248, 41);
      this.lblMetricTypeID.Name = "lblMetricTypeID";
      this.lblMetricTypeID.Size = new System.Drawing.Size(53, 21);
      this.lblMetricTypeID.TabIndex = 1;
      this.lblMetricTypeID.Text = "1";
      this.lblMetricTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblMetricTypeDesc
      //
      this.lblMetricTypeDesc.BackColor = System.Drawing.Color.White;
      this.lblMetricTypeDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricTypeDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricTypeDesc.Location = new System.Drawing.Point(302, 41);
      this.lblMetricTypeDesc.Name = "lblMetricTypeDesc";
      this.lblMetricTypeDesc.Size = new System.Drawing.Size(188, 21);
      this.lblMetricTypeDesc.TabIndex = 1;
      this.lblMetricTypeDesc.Text = "MetricTypeDesc";
      this.lblMetricTypeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblTargetSystemPrompt
      //
      this.lblTargetSystemPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetSystemPrompt.Location = new System.Drawing.Point(49, 62);
      this.lblTargetSystemPrompt.Name = "lblTargetSystemPrompt";
      this.lblTargetSystemPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblTargetSystemPrompt.TabIndex = 0;
      this.lblTargetSystemPrompt.Text = "Target System ID / Desc:";
      this.lblTargetSystemPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblTargetSystemID
      //
      this.lblTargetSystemID.BackColor = System.Drawing.Color.White;
      this.lblTargetSystemID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTargetSystemID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetSystemID.Location = new System.Drawing.Point(248, 62);
      this.lblTargetSystemID.Name = "lblTargetSystemID";
      this.lblTargetSystemID.Size = new System.Drawing.Size(53, 21);
      this.lblTargetSystemID.TabIndex = 1;
      this.lblTargetSystemID.Text = "1";
      this.lblTargetSystemID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblTargetSystemDesc
      //
      this.lblTargetSystemDesc.BackColor = System.Drawing.Color.White;
      this.lblTargetSystemDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTargetSystemDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetSystemDesc.Location = new System.Drawing.Point(302, 62);
      this.lblTargetSystemDesc.Name = "lblTargetSystemDesc";
      this.lblTargetSystemDesc.Size = new System.Drawing.Size(188, 21);
      this.lblTargetSystemDesc.TabIndex = 1;
      this.lblTargetSystemDesc.Text = "TargetSystemDesc";
      this.lblTargetSystemDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblTargetApplicationPrompt
      //
      this.lblTargetApplicationPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetApplicationPrompt.Location = new System.Drawing.Point(49, 83);
      this.lblTargetApplicationPrompt.Name = "lblTargetApplicationPrompt";
      this.lblTargetApplicationPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblTargetApplicationPrompt.TabIndex = 0;
      this.lblTargetApplicationPrompt.Text = "Target Application ID / Desc:";
      this.lblTargetApplicationPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblTargetApplicationID
      //
      this.lblTargetApplicationID.BackColor = System.Drawing.Color.White;
      this.lblTargetApplicationID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTargetApplicationID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetApplicationID.Location = new System.Drawing.Point(248, 83);
      this.lblTargetApplicationID.Name = "lblTargetApplicationID";
      this.lblTargetApplicationID.Size = new System.Drawing.Size(53, 21);
      this.lblTargetApplicationID.TabIndex = 1;
      this.lblTargetApplicationID.Text = "1";
      this.lblTargetApplicationID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblTargetApplicationDesc
      //
      this.lblTargetApplicationDesc.BackColor = System.Drawing.Color.White;
      this.lblTargetApplicationDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTargetApplicationDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetApplicationDesc.Location = new System.Drawing.Point(302, 83);
      this.lblTargetApplicationDesc.Name = "lblTargetApplicationDesc";
      this.lblTargetApplicationDesc.Size = new System.Drawing.Size(188, 21);
      this.lblTargetApplicationDesc.TabIndex = 1;
      this.lblTargetApplicationDesc.Text = "TargetApplicationDesc";
      this.lblTargetApplicationDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricStatePrompt
      //
      this.lblMetricStatePrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricStatePrompt.Location = new System.Drawing.Point(49, 104);
      this.lblMetricStatePrompt.Name = "lblMetricStatePrompt";
      this.lblMetricStatePrompt.Size = new System.Drawing.Size(193, 21);
      this.lblMetricStatePrompt.TabIndex = 0;
      this.lblMetricStatePrompt.Text = "Metric State ID / Desc:";
      this.lblMetricStatePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricStateID
      //
      this.lblMetricStateID.BackColor = System.Drawing.Color.White;
      this.lblMetricStateID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricStateID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricStateID.Location = new System.Drawing.Point(248, 104);
      this.lblMetricStateID.Name = "lblMetricStateID";
      this.lblMetricStateID.Size = new System.Drawing.Size(53, 21);
      this.lblMetricStateID.TabIndex = 1;
      this.lblMetricStateID.Text = "1";
      this.lblMetricStateID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblMetricStateDesc
      //
      this.lblMetricStateDesc.BackColor = System.Drawing.Color.White;
      this.lblMetricStateDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricStateDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricStateDesc.Location = new System.Drawing.Point(302, 104);
      this.lblMetricStateDesc.Name = "lblMetricStateDesc";
      this.lblMetricStateDesc.Size = new System.Drawing.Size(188, 21);
      this.lblMetricStateDesc.TabIndex = 1;
      this.lblMetricStateDesc.Text = "MetricStateDesc";
      this.lblMetricStateDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblAggregateTypePrompt
      //
      this.lblAggregateTypePrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAggregateTypePrompt.Location = new System.Drawing.Point(49, 146);
      this.lblAggregateTypePrompt.Name = "lblAggregateTypePrompt";
      this.lblAggregateTypePrompt.Size = new System.Drawing.Size(193, 21);
      this.lblAggregateTypePrompt.TabIndex = 0;
      this.lblAggregateTypePrompt.Text = "Aggregate Type ID / Desc:";
      this.lblAggregateTypePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblAggregateTypeID
      //
      this.lblAggregateTypeID.BackColor = System.Drawing.Color.White;
      this.lblAggregateTypeID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblAggregateTypeID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAggregateTypeID.Location = new System.Drawing.Point(248, 146);
      this.lblAggregateTypeID.Name = "lblAggregateTypeID";
      this.lblAggregateTypeID.Size = new System.Drawing.Size(53, 21);
      this.lblAggregateTypeID.TabIndex = 1;
      this.lblAggregateTypeID.Text = "1";
      this.lblAggregateTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblAggregateTypeDesc
      //
      this.lblAggregateTypeDesc.BackColor = System.Drawing.Color.White;
      this.lblAggregateTypeDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblAggregateTypeDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblAggregateTypeDesc.Location = new System.Drawing.Point(302, 146);
      this.lblAggregateTypeDesc.Name = "lblAggregateTypeDesc";
      this.lblAggregateTypeDesc.Size = new System.Drawing.Size(188, 21);
      this.lblAggregateTypeDesc.TabIndex = 1;
      this.lblAggregateTypeDesc.Text = "AggregateTypeDesc";
      this.lblAggregateTypeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricValueTypePrompt
      //
      this.lblMetricValueTypePrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricValueTypePrompt.Location = new System.Drawing.Point(49, 167);
      this.lblMetricValueTypePrompt.Name = "lblMetricValueTypePrompt";
      this.lblMetricValueTypePrompt.Size = new System.Drawing.Size(193, 21);
      this.lblMetricValueTypePrompt.TabIndex = 0;
      this.lblMetricValueTypePrompt.Text = "Metric Value Type ID / Desc:";
      this.lblMetricValueTypePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricValueTypeID
      //
      this.lblMetricValueTypeID.BackColor = System.Drawing.Color.White;
      this.lblMetricValueTypeID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricValueTypeID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricValueTypeID.Location = new System.Drawing.Point(248, 167);
      this.lblMetricValueTypeID.Name = "lblMetricValueTypeID";
      this.lblMetricValueTypeID.Size = new System.Drawing.Size(53, 21);
      this.lblMetricValueTypeID.TabIndex = 1;
      this.lblMetricValueTypeID.Text = "1";
      this.lblMetricValueTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblMetricValueTypeDesc
      //
      this.lblMetricValueTypeDesc.BackColor = System.Drawing.Color.White;
      this.lblMetricValueTypeDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricValueTypeDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricValueTypeDesc.Location = new System.Drawing.Point(302, 167);
      this.lblMetricValueTypeDesc.Name = "lblMetricValueTypeDesc";
      this.lblMetricValueTypeDesc.Size = new System.Drawing.Size(188, 21);
      this.lblMetricValueTypeDesc.TabIndex = 1;
      this.lblMetricValueTypeDesc.Text = "MetricValueTypeDesc";
      this.lblMetricValueTypeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblIntervalTypePrompt
      //
      this.lblIntervalTypePrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIntervalTypePrompt.Location = new System.Drawing.Point(49, 188);
      this.lblIntervalTypePrompt.Name = "lblIntervalTypePrompt";
      this.lblIntervalTypePrompt.Size = new System.Drawing.Size(193, 21);
      this.lblIntervalTypePrompt.TabIndex = 0;
      this.lblIntervalTypePrompt.Text = "IntervalType ID / Desc:";
      this.lblIntervalTypePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblIntervalTypeID
      //
      this.lblIntervalTypeID.BackColor = System.Drawing.Color.White;
      this.lblIntervalTypeID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblIntervalTypeID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIntervalTypeID.Location = new System.Drawing.Point(248, 188);
      this.lblIntervalTypeID.Name = "lblIntervalTypeID";
      this.lblIntervalTypeID.Size = new System.Drawing.Size(53, 21);
      this.lblIntervalTypeID.TabIndex = 1;
      this.lblIntervalTypeID.Text = "1";
      this.lblIntervalTypeID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblIntervalTypeDesc
      //
      this.lblIntervalTypeDesc.BackColor = System.Drawing.Color.White;
      this.lblIntervalTypeDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblIntervalTypeDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIntervalTypeDesc.Location = new System.Drawing.Point(302, 188);
      this.lblIntervalTypeDesc.Name = "lblIntervalTypeDesc";
      this.lblIntervalTypeDesc.Size = new System.Drawing.Size(188, 21);
      this.lblIntervalTypeDesc.TabIndex = 1;
      this.lblIntervalTypeDesc.Text = "IntervalTypeDesc";
      this.lblIntervalTypeDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverSystemPrompt
      //
      this.lblObserverSystemPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverSystemPrompt.Location = new System.Drawing.Point(49, 209);
      this.lblObserverSystemPrompt.Name = "lblObserverSystemPrompt";
      this.lblObserverSystemPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblObserverSystemPrompt.TabIndex = 0;
      this.lblObserverSystemPrompt.Text = "Observer System ID / Desc:";
      this.lblObserverSystemPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverSystemID
      //
      this.lblObserverSystemID.BackColor = System.Drawing.Color.White;
      this.lblObserverSystemID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverSystemID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverSystemID.Location = new System.Drawing.Point(248, 209);
      this.lblObserverSystemID.Name = "lblObserverSystemID";
      this.lblObserverSystemID.Size = new System.Drawing.Size(53, 21);
      this.lblObserverSystemID.TabIndex = 1;
      this.lblObserverSystemID.Text = "1";
      this.lblObserverSystemID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblObserverSystemDesc
      //
      this.lblObserverSystemDesc.BackColor = System.Drawing.Color.White;
      this.lblObserverSystemDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverSystemDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverSystemDesc.Location = new System.Drawing.Point(302, 209);
      this.lblObserverSystemDesc.Name = "lblObserverSystemDesc";
      this.lblObserverSystemDesc.Size = new System.Drawing.Size(188, 21);
      this.lblObserverSystemDesc.TabIndex = 1;
      this.lblObserverSystemDesc.Text = "ObserverSystemDesc";
      this.lblObserverSystemDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverApplicationPrompt
      //
      this.lblObserverApplicationPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverApplicationPrompt.Location = new System.Drawing.Point(49, 230);
      this.lblObserverApplicationPrompt.Name = "lblObserverApplicationPrompt";
      this.lblObserverApplicationPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblObserverApplicationPrompt.TabIndex = 0;
      this.lblObserverApplicationPrompt.Text = "Observer Application ID / Desc:";
      this.lblObserverApplicationPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverApplicationID
      //
      this.lblObserverApplicationID.BackColor = System.Drawing.Color.White;
      this.lblObserverApplicationID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverApplicationID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverApplicationID.Location = new System.Drawing.Point(248, 230);
      this.lblObserverApplicationID.Name = "lblObserverApplicationID";
      this.lblObserverApplicationID.Size = new System.Drawing.Size(53, 21);
      this.lblObserverApplicationID.TabIndex = 1;
      this.lblObserverApplicationID.Text = "1";
      this.lblObserverApplicationID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblObserverApplicationDesc
      //
      this.lblObserverApplicationDesc.BackColor = System.Drawing.Color.White;
      this.lblObserverApplicationDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverApplicationDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverApplicationDesc.Location = new System.Drawing.Point(302, 230);
      this.lblObserverApplicationDesc.Name = "lblObserverApplicationDesc";
      this.lblObserverApplicationDesc.Size = new System.Drawing.Size(188, 21);
      this.lblObserverApplicationDesc.TabIndex = 1;
      this.lblObserverApplicationDesc.Text = "ObserverApplicationDesc";
      this.lblObserverApplicationDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverServerPrompt
      //
      this.lblObserverServerPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverServerPrompt.Location = new System.Drawing.Point(49, 251);
      this.lblObserverServerPrompt.Name = "lblObserverServerPrompt";
      this.lblObserverServerPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblObserverServerPrompt.TabIndex = 0;
      this.lblObserverServerPrompt.Text = "Observer Server ID / Desc:";
      this.lblObserverServerPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblObserverServerID
      //
      this.lblObserverServerID.BackColor = System.Drawing.Color.White;
      this.lblObserverServerID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverServerID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverServerID.Location = new System.Drawing.Point(248, 251);
      this.lblObserverServerID.Name = "lblObserverServerID";
      this.lblObserverServerID.Size = new System.Drawing.Size(53, 21);
      this.lblObserverServerID.TabIndex = 1;
      this.lblObserverServerID.Text = "1";
      this.lblObserverServerID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblObserverServerDesc
      //
      this.lblObserverServerDesc.BackColor = System.Drawing.Color.White;
      this.lblObserverServerDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblObserverServerDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObserverServerDesc.Location = new System.Drawing.Point(302, 251);
      this.lblObserverServerDesc.Name = "lblObserverServerDesc";
      this.lblObserverServerDesc.Size = new System.Drawing.Size(188, 21);
      this.lblObserverServerDesc.TabIndex = 1;
      this.lblObserverServerDesc.Text = "ObserverServerDesc";
      this.lblObserverServerDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricPrompt
      //
      this.lblMetricPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricPrompt.Location = new System.Drawing.Point(49, 125);
      this.lblMetricPrompt.Name = "lblMetricPrompt";
      this.lblMetricPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblMetricPrompt.TabIndex = 0;
      this.lblMetricPrompt.Text = "Metric ID / Desc:";
      this.lblMetricPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblMetricID
      //
      this.lblMetricID.BackColor = System.Drawing.Color.White;
      this.lblMetricID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricID.Location = new System.Drawing.Point(248, 125);
      this.lblMetricID.Name = "lblMetricID";
      this.lblMetricID.Size = new System.Drawing.Size(53, 21);
      this.lblMetricID.TabIndex = 1;
      this.lblMetricID.Text = "1";
      this.lblMetricID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblMetricDesc
      //
      this.lblMetricDesc.BackColor = System.Drawing.Color.White;
      this.lblMetricDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblMetricDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMetricDesc.Location = new System.Drawing.Point(302, 125);
      this.lblMetricDesc.Name = "lblMetricDesc";
      this.lblMetricDesc.Size = new System.Drawing.Size(188, 21);
      this.lblMetricDesc.TabIndex = 1;
      this.lblMetricDesc.Text = "MetricDesc";
      this.lblMetricDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblGraphLegendPrompt
      //
      this.lblGraphLegendPrompt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblGraphLegendPrompt.Location = new System.Drawing.Point(49, 71);
      this.lblGraphLegendPrompt.Name = "lblGraphLegendPrompt";
      this.lblGraphLegendPrompt.Size = new System.Drawing.Size(193, 21);
      this.lblGraphLegendPrompt.TabIndex = 0;
      this.lblGraphLegendPrompt.Text = "Graph Legend Label:";
      this.lblGraphLegendPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gbSetLegendLabel
      //
      this.gbSetLegendLabel.Controls.Add(this.cboGraphLabelStyles);
      this.gbSetLegendLabel.Controls.Add(this.txtGraphLegendLabel);
      this.gbSetLegendLabel.Controls.Add(this.lblSelectLegendStyle);
      this.gbSetLegendLabel.Controls.Add(this.lblGraphLegendPrompt);
      this.gbSetLegendLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gbSetLegendLabel.Location = new System.Drawing.Point(12, 459);
      this.gbSetLegendLabel.Name = "gbSetLegendLabel";
      this.gbSetLegendLabel.Size = new System.Drawing.Size(512, 144);
      this.gbSetLegendLabel.TabIndex = 3;
      this.gbSetLegendLabel.TabStop = false;
      this.gbSetLegendLabel.Text = "Set Graph Legend Label";
      //
      // cboGraphLabelStyles
      //
      this.cboGraphLabelStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboGraphLabelStyles.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboGraphLabelStyles.FormattingEnabled = true;
      this.cboGraphLabelStyles.Items.AddRange(new object[] {
        "",
        "System + Application + Metric",
        "System + Server + Metric",
        "Application + Metric",
        "Custom - Enter Below"
      });
      this.cboGraphLabelStyles.Location = new System.Drawing.Point(52, 45);
      this.cboGraphLabelStyles.Name = "cboGraphLabelStyles";
      this.cboGraphLabelStyles.Size = new System.Drawing.Size(438, 21);
      this.cboGraphLabelStyles.TabIndex = 1;
      this.cboGraphLabelStyles.SelectedIndexChanged += new System.EventHandler(this.cboGraphLabelStyles_SelectedIndexChanged);
      //
      // lblSelectLegendStyle
      //
      this.lblSelectLegendStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSelectLegendStyle.Location = new System.Drawing.Point(49, 21);
      this.lblSelectLegendStyle.Name = "lblSelectLegendStyle";
      this.lblSelectLegendStyle.Size = new System.Drawing.Size(193, 21);
      this.lblSelectLegendStyle.TabIndex = 0;
      this.lblSelectLegendStyle.Text = "Select Graph Legend Label Style:";
      this.lblSelectLegendStyle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gbMetricProperties
      //
      this.gbMetricProperties.Controls.Add(this.lblEnvironmentPrompt);
      this.gbMetricProperties.Controls.Add(this.lblMetricTypePrompt);
      this.gbMetricProperties.Controls.Add(this.lblEnvironmentID);
      this.gbMetricProperties.Controls.Add(this.lblObserverServerDesc);
      this.gbMetricProperties.Controls.Add(this.lblTargetSystemPrompt);
      this.gbMetricProperties.Controls.Add(this.lblObserverApplicationDesc);
      this.gbMetricProperties.Controls.Add(this.lblTargetApplicationPrompt);
      this.gbMetricProperties.Controls.Add(this.lblObserverSystemDesc);
      this.gbMetricProperties.Controls.Add(this.lblMetricStatePrompt);
      this.gbMetricProperties.Controls.Add(this.lblIntervalTypeDesc);
      this.gbMetricProperties.Controls.Add(this.lblAggregateTypePrompt);
      this.gbMetricProperties.Controls.Add(this.lblMetricValueTypeDesc);
      this.gbMetricProperties.Controls.Add(this.lblMetricPrompt);
      this.gbMetricProperties.Controls.Add(this.lblAggregateTypeDesc);
      this.gbMetricProperties.Controls.Add(this.lblMetricValueTypePrompt);
      this.gbMetricProperties.Controls.Add(this.lblMetricDesc);
      this.gbMetricProperties.Controls.Add(this.lblIntervalTypePrompt);
      this.gbMetricProperties.Controls.Add(this.lblMetricStateDesc);
      this.gbMetricProperties.Controls.Add(this.lblObserverSystemPrompt);
      this.gbMetricProperties.Controls.Add(this.lblTargetApplicationDesc);
      this.gbMetricProperties.Controls.Add(this.lblObserverApplicationPrompt);
      this.gbMetricProperties.Controls.Add(this.lblTargetSystemDesc);
      this.gbMetricProperties.Controls.Add(this.lblObserverServerPrompt);
      this.gbMetricProperties.Controls.Add(this.lblMetricTypeDesc);
      this.gbMetricProperties.Controls.Add(this.lblMetricTypeID);
      this.gbMetricProperties.Controls.Add(this.lblObserverServerID);
      this.gbMetricProperties.Controls.Add(this.lblEnvironmentDesc);
      this.gbMetricProperties.Controls.Add(this.lblObserverApplicationID);
      this.gbMetricProperties.Controls.Add(this.lblTargetSystemID);
      this.gbMetricProperties.Controls.Add(this.lblObserverSystemID);
      this.gbMetricProperties.Controls.Add(this.lblTargetApplicationID);
      this.gbMetricProperties.Controls.Add(this.lblIntervalTypeID);
      this.gbMetricProperties.Controls.Add(this.lblMetricStateID);
      this.gbMetricProperties.Controls.Add(this.lblMetricValueTypeID);
      this.gbMetricProperties.Controls.Add(this.lblAggregateTypeID);
      this.gbMetricProperties.Controls.Add(this.lblMetricID);
      this.gbMetricProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gbMetricProperties.Location = new System.Drawing.Point(12, 15);
      this.gbMetricProperties.Name = "gbMetricProperties";
      this.gbMetricProperties.Size = new System.Drawing.Size(512, 296);
      this.gbMetricProperties.TabIndex = 4;
      this.gbMetricProperties.TabStop = false;
      this.gbMetricProperties.Text = "Metric Property Values and Descriptions";
      //
      // btnUpdate
      //
      this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnUpdate.Location = new System.Drawing.Point(12, 612);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(152, 25);
      this.btnUpdate.TabIndex = 3;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      //
      // btnCancel
      //
      this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCancel.Location = new System.Drawing.Point(372, 612);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(152, 25);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // txtSQLCommand
      //
      this.txtSQLCommand.Location = new System.Drawing.Point(542, 21);
      this.txtSQLCommand.Multiline = true;
      this.txtSQLCommand.Name = "txtSQLCommand";
      this.txtSQLCommand.Size = new System.Drawing.Size(285, 617);
      this.txtSQLCommand.TabIndex = 5;
      //
      // gbForecastFromFile
      //
      this.gbForecastFromFile.Controls.Add(this.btnGetFile);
      this.gbForecastFromFile.Controls.Add(this.ckUseYOYData);
      this.gbForecastFromFile.Controls.Add(this.ckForecastFromFile);
      this.gbForecastFromFile.Controls.Add(this.lblForecastFileName);
      this.gbForecastFromFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gbForecastFromFile.Location = new System.Drawing.Point(12, 317);
      this.gbForecastFromFile.Name = "gbForecastFromFile";
      this.gbForecastFromFile.Size = new System.Drawing.Size(511, 85);
      this.gbForecastFromFile.TabIndex = 6;
      this.gbForecastFromFile.TabStop = false;
      this.gbForecastFromFile.Text = "Forecast from File";
      //
      // btnGetFile
      //
      this.btnGetFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnGetFile.Location = new System.Drawing.Point(52, 43);
      this.btnGetFile.Name = "btnGetFile";
      this.btnGetFile.Size = new System.Drawing.Size(100, 23);
      this.btnGetFile.TabIndex = 1;
      this.btnGetFile.Text = "Get File";
      this.btnGetFile.UseVisualStyleBackColor = true;
      this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
      //
      // ckUseYOYData
      //
      this.ckUseYOYData.AutoSize = true;
      this.ckUseYOYData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckUseYOYData.Location = new System.Drawing.Point(347, 20);
      this.ckUseYOYData.Name = "ckUseYOYData";
      this.ckUseYOYData.Size = new System.Drawing.Size(149, 17);
      this.ckUseYOYData.TabIndex = 0;
      this.ckUseYOYData.Text = "Use Year-Over-Year Data";
      this.ckUseYOYData.UseVisualStyleBackColor = true;
      this.ckUseYOYData.CheckedChanged += new System.EventHandler(this.ckForecastFromFile_CheckedChanged);
      //
      // ckForecastFromFile
      //
      this.ckForecastFromFile.AutoSize = true;
      this.ckForecastFromFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckForecastFromFile.Location = new System.Drawing.Point(52, 20);
      this.ckForecastFromFile.Name = "ckForecastFromFile";
      this.ckForecastFromFile.Size = new System.Drawing.Size(265, 17);
      this.ckForecastFromFile.TabIndex = 0;
      this.ckForecastFromFile.Text = "Convert this actual metric to a forecast from a file";
      this.ckForecastFromFile.UseVisualStyleBackColor = true;
      this.ckForecastFromFile.CheckedChanged += new System.EventHandler(this.ckForecastFromFile_CheckedChanged);
      //
      // lblForecastFileName
      //
      this.lblForecastFileName.BackColor = System.Drawing.Color.White;
      this.lblForecastFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblForecastFileName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblForecastFileName.Location = new System.Drawing.Point(177, 45);
      this.lblForecastFileName.Name = "lblForecastFileName";
      this.lblForecastFileName.Size = new System.Drawing.Size(313, 21);
      this.lblForecastFileName.TabIndex = 1;
      this.lblForecastFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // dlgFileOpen
      //
      this.dlgFileOpen.Filter = "CSV files |*.csv";
      this.dlgFileOpen.Title = "Locate and select file... ";
      //
      // lblMultiplier
      //
      this.lblMultiplier.Location = new System.Drawing.Point(61, 421);
      this.lblMultiplier.Name = "lblMultiplier";
      this.lblMultiplier.Size = new System.Drawing.Size(182, 23);
      this.lblMultiplier.TabIndex = 7;
      this.lblMultiplier.Text = "Multiply the value for this metric by:";
      //
      // txtMultiplier
      //
      this.txtMultiplier.Location = new System.Drawing.Point(244, 419);
      this.txtMultiplier.Name = "txtMultiplier";
      this.txtMultiplier.Size = new System.Drawing.Size(100, 21);
      this.txtMultiplier.TabIndex = 8;
      //
      // frmMetricProperties
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(844, 659);
      this.ControlBox = false;
      this.Controls.Add(this.txtMultiplier);
      this.Controls.Add(this.lblMultiplier);
      this.Controls.Add(this.gbForecastFromFile);
      this.Controls.Add(this.txtSQLCommand);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnUpdate);
      this.Controls.Add(this.gbMetricProperties);
      this.Controls.Add(this.gbSetLegendLabel);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "frmMetricProperties";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Metric Properties";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMetricProperties_KeyDown);
      this.gbSetLegendLabel.ResumeLayout(false);
      this.gbSetLegendLabel.PerformLayout();
      this.gbMetricProperties.ResumeLayout(false);
      this.gbForecastFromFile.ResumeLayout(false);
      this.gbForecastFromFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblEnvironmentPrompt;
    private System.Windows.Forms.Label lblEnvironmentID;
    private System.Windows.Forms.TextBox txtGraphLegendLabel;
    private System.Windows.Forms.Label lblEnvironmentDesc;
    private System.Windows.Forms.Label lblMetricTypePrompt;
    private System.Windows.Forms.Label lblMetricTypeID;
    private System.Windows.Forms.Label lblMetricTypeDesc;
    private System.Windows.Forms.Label lblTargetSystemPrompt;
    private System.Windows.Forms.Label lblTargetSystemID;
    private System.Windows.Forms.Label lblTargetSystemDesc;
    private System.Windows.Forms.Label lblTargetApplicationPrompt;
    private System.Windows.Forms.Label lblTargetApplicationID;
    private System.Windows.Forms.Label lblTargetApplicationDesc;
    private System.Windows.Forms.Label lblMetricStatePrompt;
    private System.Windows.Forms.Label lblMetricStateID;
    private System.Windows.Forms.Label lblMetricStateDesc;
    private System.Windows.Forms.Label lblAggregateTypePrompt;
    private System.Windows.Forms.Label lblAggregateTypeID;
    private System.Windows.Forms.Label lblAggregateTypeDesc;
    private System.Windows.Forms.Label lblMetricValueTypePrompt;
    private System.Windows.Forms.Label lblMetricValueTypeID;
    private System.Windows.Forms.Label lblMetricValueTypeDesc;
    private System.Windows.Forms.Label lblIntervalTypePrompt;
    private System.Windows.Forms.Label lblIntervalTypeID;
    private System.Windows.Forms.Label lblIntervalTypeDesc;
    private System.Windows.Forms.Label lblObserverSystemPrompt;
    private System.Windows.Forms.Label lblObserverSystemID;
    private System.Windows.Forms.Label lblObserverSystemDesc;
    private System.Windows.Forms.Label lblObserverApplicationPrompt;
    private System.Windows.Forms.Label lblObserverApplicationID;
    private System.Windows.Forms.Label lblObserverApplicationDesc;
    private System.Windows.Forms.Label lblObserverServerPrompt;
    private System.Windows.Forms.Label lblObserverServerID;
    private System.Windows.Forms.Label lblObserverServerDesc;
    private System.Windows.Forms.Label lblMetricPrompt;
    private System.Windows.Forms.Label lblMetricID;
    private System.Windows.Forms.Label lblMetricDesc;
    private System.Windows.Forms.Label lblGraphLegendPrompt;
    private System.Windows.Forms.GroupBox gbSetLegendLabel;
    private System.Windows.Forms.ComboBox cboGraphLabelStyles;
    private System.Windows.Forms.Label lblSelectLegendStyle;
    private System.Windows.Forms.GroupBox gbMetricProperties;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtSQLCommand;
    private System.Windows.Forms.GroupBox gbForecastFromFile;
    private System.Windows.Forms.Button btnGetFile;
    private System.Windows.Forms.CheckBox ckForecastFromFile;
    private System.Windows.Forms.Label lblForecastFileName;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    private System.Windows.Forms.CheckBox ckUseYOYData;
    private System.Windows.Forms.Label lblMultiplier;
    private System.Windows.Forms.TextBox txtMultiplier;
  }
}