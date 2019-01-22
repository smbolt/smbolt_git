namespace CSharpChartExplorer
{
    partial class ParamViewer
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
            this.listView = new System.Windows.Forms.ListView();
            this.Key = new System.Windows.Forms.ColumnHeader();
            this.Value = new System.Windows.Forms.ColumnHeader();
            this.OKPB = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Key,
            this.Value});
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(8, 52);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(304, 173);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // Key
            // 
            this.Key.Text = "Key";
            this.Key.Width = 80;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 220;
            // 
            // OKPB
            // 
            this.OKPB.Location = new System.Drawing.Point(124, 232);
            this.OKPB.Name = "OKPB";
            this.OKPB.Size = new System.Drawing.Size(72, 21);
            this.OKPB.TabIndex = 1;
            this.OKPB.Text = "OK";
            this.OKPB.Click += new System.EventHandler(this.OKPB_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(8, 7);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(304, 42);
            this.label.TabIndex = 2;
            this.label.Text = "This is to demonstrate that ChartDirector charts are clickable. In this demo prog" +
                "ram, we just display the information provided to the ClickHotSpot event handler." +
                " ";
            // 
            // ParamViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(320, 261);
            this.Controls.Add(this.label);
            this.Controls.Add(this.OKPB);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ParamViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hot Spot Parameters";
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button OKPB;
		private System.Windows.Forms.ColumnHeader Key;
		private System.Windows.Forms.ColumnHeader Value;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.ListView listView;
    }
}