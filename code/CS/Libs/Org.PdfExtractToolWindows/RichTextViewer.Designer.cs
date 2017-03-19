namespace Org.PdfExtractToolWindows
{
	partial class RichTextViewer
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextViewer));
			this.txtData = new FastColoredTextBoxNS.FastColoredTextBox();
			((System.ComponentModel.ISupportInitialize)(this.txtData)).BeginInit();
			this.SuspendLayout();
			// 
			// txtData
			// 
			this.txtData.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.txtData.AutoScrollMinSize = new System.Drawing.Size(25, 13);
			this.txtData.BackBrush = null;
			this.txtData.CharHeight = 13;
			this.txtData.CharWidth = 7;
			this.txtData.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtData.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtData.Font = new System.Drawing.Font("Courier New", 9F);
			this.txtData.IsReplaceMode = false;
			this.txtData.Location = new System.Drawing.Point(0, 26);
			this.txtData.Margin = new System.Windows.Forms.Padding(2);
			this.txtData.Name = "txtData";
			this.txtData.Paddings = new System.Windows.Forms.Padding(0);
			this.txtData.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtData.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtData.ServiceColors")));
			this.txtData.Size = new System.Drawing.Size(518, 468);
			this.txtData.TabIndex = 4;
			this.txtData.Tag = "";
			this.txtData.Zoom = 100;
			// 
			// RichTextViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtData);
			this.Name = "RichTextViewer";
			this.Size = new System.Drawing.Size(518, 494);
			this.Tag = "ToolPanel_RichTextViewer";
			this.Controls.SetChildIndex(this.txtData, 0);
			((System.ComponentModel.ISupportInitialize)(this.txtData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private FastColoredTextBoxNS.FastColoredTextBox txtData;
	}
}
