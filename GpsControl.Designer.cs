namespace WirelessProject
{
    partial class GpsControl
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
            this.gpsGroupBox = new System.Windows.Forms.GroupBox();
            this.gpsBox = new System.Windows.Forms.PictureBox();
            this.labelDelay = new System.Windows.Forms.Label();
            this.gpsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.baudRate = new System.Windows.Forms.TextBox();
            this.lblbaudrate = new System.Windows.Forms.Label();
            this.comportBox = new System.Windows.Forms.PictureBox();
            this.lblCOM = new System.Windows.Forms.Label();
            this.gpsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpsNumericUpDown)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comportBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gpsGroupBox
            // 
            this.gpsGroupBox.Controls.Add(this.gpsBox);
            this.gpsGroupBox.Controls.Add(this.labelDelay);
            this.gpsGroupBox.Controls.Add(this.gpsNumericUpDown);
            this.gpsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpsGroupBox.ForeColor = System.Drawing.Color.RoyalBlue;
            this.gpsGroupBox.Location = new System.Drawing.Point(16, 11);
            this.gpsGroupBox.Name = "gpsGroupBox";
            this.gpsGroupBox.Size = new System.Drawing.Size(288, 62);
            this.gpsGroupBox.TabIndex = 62;
            this.gpsGroupBox.TabStop = false;
            this.gpsGroupBox.Text = "GPS";
            // 
            // gpsBox
            // 
            this.gpsBox.Location = new System.Drawing.Point(15, 26);
            this.gpsBox.Name = "gpsBox";
            this.gpsBox.Size = new System.Drawing.Size(22, 22);
            this.gpsBox.TabIndex = 74;
            this.gpsBox.TabStop = false;
            // 
            // labelDelay
            // 
            this.labelDelay.AutoSize = true;
            this.labelDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDelay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDelay.Location = new System.Drawing.Point(55, 29);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(125, 15);
            this.labelDelay.TabIndex = 56;
            this.labelDelay.Text = "Update Interval (secs)";
            // 
            // gpsNumericUpDown
            // 
            this.gpsNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpsNumericUpDown.Location = new System.Drawing.Point(186, 28);
            this.gpsNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.gpsNumericUpDown.Name = "gpsNumericUpDown";
            this.gpsNumericUpDown.ReadOnly = true;
            this.gpsNumericUpDown.Size = new System.Drawing.Size(53, 20);
            this.gpsNumericUpDown.TabIndex = 55;
            this.gpsNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gpsNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.gpsNumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDownDelay_ValueChanged);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.cmbPortName);
            this.settingsGroupBox.Controls.Add(this.baudRate);
            this.settingsGroupBox.Controls.Add(this.lblbaudrate);
            this.settingsGroupBox.Controls.Add(this.comportBox);
            this.settingsGroupBox.Controls.Add(this.lblCOM);
            this.settingsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsGroupBox.ForeColor = System.Drawing.Color.RoyalBlue;
            this.settingsGroupBox.Location = new System.Drawing.Point(310, 11);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(304, 62);
            this.settingsGroupBox.TabIndex = 63;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cmbPortName.Location = new System.Drawing.Point(96, 24);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(67, 24);
            this.cmbPortName.TabIndex = 81;
            // 
            // baudRate
            // 
            this.baudRate.Enabled = false;
            this.baudRate.Location = new System.Drawing.Point(230, 25);
            this.baudRate.Name = "baudRate";
            this.baudRate.Size = new System.Drawing.Size(44, 22);
            this.baudRate.TabIndex = 80;
            this.baudRate.Text = "4800";
            // 
            // lblbaudrate
            // 
            this.lblbaudrate.AutoSize = true;
            this.lblbaudrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbaudrate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblbaudrate.Location = new System.Drawing.Point(169, 31);
            this.lblbaudrate.Name = "lblbaudrate";
            this.lblbaudrate.Size = new System.Drawing.Size(55, 13);
            this.lblbaudrate.TabIndex = 79;
            this.lblbaudrate.Text = "BaudRate";
            this.lblbaudrate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comportBox
            // 
            this.comportBox.Location = new System.Drawing.Point(19, 26);
            this.comportBox.Name = "comportBox";
            this.comportBox.Size = new System.Drawing.Size(22, 22);
            this.comportBox.TabIndex = 73;
            this.comportBox.TabStop = false;
            // 
            // lblCOM
            // 
            this.lblCOM.AutoSize = true;
            this.lblCOM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCOM.Location = new System.Drawing.Point(65, 30);
            this.lblCOM.Name = "lblCOM";
            this.lblCOM.Size = new System.Drawing.Size(31, 13);
            this.lblCOM.TabIndex = 76;
            this.lblCOM.Text = "COM";
            this.lblCOM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // GpsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.gpsGroupBox);
            this.Name = "GpsControl";
            this.Size = new System.Drawing.Size(673, 77);
            this.gpsGroupBox.ResumeLayout(false);
            this.gpsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpsNumericUpDown)).EndInit();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comportBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpsGroupBox;
        private System.Windows.Forms.PictureBox gpsBox;
        private System.Windows.Forms.Label labelDelay;
        private System.Windows.Forms.NumericUpDown gpsNumericUpDown;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.PictureBox comportBox;
        private System.Windows.Forms.Label lblCOM;
        private System.Windows.Forms.Label lblbaudrate;
        private System.Windows.Forms.TextBox baudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
    }
}
