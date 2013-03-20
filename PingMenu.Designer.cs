namespace WirelessProject
{
    partial class PingMenu
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
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOk = new System.Windows.Forms.Button();
            this.hostName = new System.Windows.Forms.TextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.hostIp = new System.Windows.Forms.TextBox();
            this.radioName = new System.Windows.Forms.RadioButton();
            this.radioIP = new System.Windows.Forms.RadioButton();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.packetSize = new System.Windows.Forms.NumericUpDown();
            this.pLabelPacket = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeout = new System.Windows.Forms.NumericUpDown();
            this.interval = new System.Windows.Forms.NumericUpDown();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(149, 258);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 23;
            this._btnCancel.Text = "&Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Location = new System.Drawing.Point(68, 258);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(75, 23);
            this._btnOk.TabIndex = 22;
            this._btnOk.Text = "&OK";
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // hostName
            // 
            this.hostName.Location = new System.Drawing.Point(98, 8);
            this.hostName.Name = "hostName";
            this.hostName.Size = new System.Drawing.Size(98, 20);
            this.hostName.TabIndex = 1;
            this.hostName.Text = "google.com";
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(207, 8);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "&Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // hostIp
            // 
            this.hostIp.Enabled = false;
            this.hostIp.Location = new System.Drawing.Point(98, 37);
            this.hostIp.Name = "hostIp";
            this.hostIp.Size = new System.Drawing.Size(98, 20);
            this.hostIp.TabIndex = 4;
            // 
            // radioName
            // 
            this.radioName.AutoSize = true;
            this.radioName.Checked = true;
            this.radioName.Location = new System.Drawing.Point(13, 11);
            this.radioName.Name = "radioName";
            this.radioName.Size = new System.Drawing.Size(81, 17);
            this.radioName.TabIndex = 26;
            this.radioName.TabStop = true;
            this.radioName.Text = "Host Name:";
            this.radioName.UseVisualStyleBackColor = true;
            this.radioName.CheckedChanged += new System.EventHandler(this.radioName_CheckedChanged);
            // 
            // radioIP
            // 
            this.radioIP.AutoSize = true;
            this.radioIP.Location = new System.Drawing.Point(13, 35);
            this.radioIP.Name = "radioIP";
            this.radioIP.Size = new System.Drawing.Size(63, 17);
            this.radioIP.TabIndex = 27;
            this.radioIP.Text = "Host IP:";
            this.radioIP.UseVisualStyleBackColor = true;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.packetSize);
            this.gbOptions.Controls.Add(this.pLabelPacket);
            this.gbOptions.Controls.Add(this.label5);
            this.gbOptions.Controls.Add(this.label4);
            this.gbOptions.Controls.Add(this.timeout);
            this.gbOptions.Controls.Add(this.interval);
            this.gbOptions.Location = new System.Drawing.Point(5, 76);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(191, 147);
            this.gbOptions.TabIndex = 97;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Ping Settings";
            // 
            // packetSize
            // 
            this.packetSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.packetSize.Location = new System.Drawing.Point(93, 66);
            this.packetSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.packetSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.packetSize.Name = "packetSize";
            this.packetSize.Size = new System.Drawing.Size(63, 20);
            this.packetSize.TabIndex = 106;
            this.packetSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // pLabelPacket
            // 
            this.pLabelPacket.AutoSize = true;
            this.pLabelPacket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLabelPacket.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pLabelPacket.Location = new System.Drawing.Point(20, 68);
            this.pLabelPacket.Name = "pLabelPacket";
            this.pLabelPacket.Size = new System.Drawing.Size(67, 13);
            this.pLabelPacket.TabIndex = 105;
            this.pLabelPacket.Text = "Packet Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 99;
            this.label5.Text = "&Timeout:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "Inter&val:";
            // 
            // timeout
            // 
            this.timeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeout.Location = new System.Drawing.Point(93, 92);
            this.timeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.timeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(63, 20);
            this.timeout.TabIndex = 100;
            this.timeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // interval
            // 
            this.interval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.interval.Location = new System.Drawing.Point(93, 40);
            this.interval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.interval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.interval.Name = "interval";
            this.interval.Size = new System.Drawing.Size(63, 20);
            this.interval.TabIndex = 102;
            this.interval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // PingMenu
            // 
            this.AcceptButton = this._btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(286, 293);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.radioIP);
            this.Controls.Add(this.radioName);
            this.Controls.Add(this.hostIp);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.hostName);
            this.Controls.Add(this._btnOk);
            this.Controls.Add(this._btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PingMenu";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Host Options";
            this.Load += new System.EventHandler(this.HostOptions_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.interval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Button _btnCancel;
		private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.TextBox hostName;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.TextBox hostIp;
        private System.Windows.Forms.RadioButton radioName;
        private System.Windows.Forms.RadioButton radioIP;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Label pLabelPacket;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.NumericUpDown interval;
        private System.Windows.Forms.NumericUpDown packetSize;
	}
}