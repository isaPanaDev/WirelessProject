namespace WirelessProject
{
	partial class PingColumnOptions
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
            this.columns = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // columns
            // 
            this.columns.CheckOnClick = true;
            this.columns.FormattingEnabled = true;
            this.columns.Items.AddRange(new object[] {
            "IP Address",
            "Name",
            "Description",
            "Status",
            "Sent Packets",
            "Received Packets",
            "Received Packets %",
            "Lost Packets",
            "Lost Packets %",
            "Last Packet Lost",
            "Consecutive Packets Lost",
            "Max Consecutive Packet Lost",
            "Received Packets (Recent)",
            "Received Packets % (Recent)",
            "Lost Packets (Recent)",
            "Lost Packets % (Recent)",
            "Current Response Time",
            "Average Response Time",
            "Min Response Time",
            "Max Response Time",
            "Current Status Duration",
            "Alive Status Duration",
            "Dead Status Duration",
            "DNS Error Status Duration",
            "Unknown Status Duration",
            "Host Availability",
            "Total Test Duration",
            "Current Test Duration"});
            this.columns.Location = new System.Drawing.Point(12, 25);
            this.columns.Name = "columns";
            this.columns.Size = new System.Drawing.Size(210, 139);
            this.columns.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Display Columns:";
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(113, 179);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 6;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOK
            // 
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(24, 179);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 5;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            // 
            // PingColumnOptions
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(243, 229);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.columns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PingColumnOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Column Options";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox columns;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
	}
}