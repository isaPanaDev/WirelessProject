namespace WirelessProject
{
    partial class PingControl
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
            this.pingGroupBox = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this._cbShowErrorMessages = new System.Windows.Forms.CheckBox();
            this.listViewPING = new WirelessProject.ListViewDB();
            this._colIpAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colPacketSent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colPacketReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colPacketReceivedPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colPacketLost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colMaxConsecutivePacketsLost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colRecPacketLost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._colTestDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pingBox = new System.Windows.Forms.PictureBox();
            this.pingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pingGroupBox
            // 
            this.pingGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pingGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.pingGroupBox.Controls.Add(this.btnRemove);
            this.pingGroupBox.Controls.Add(this.btnAdd);
            this.pingGroupBox.Controls.Add(this.btnOptions);
            this.pingGroupBox.Controls.Add(this._cbShowErrorMessages);
            this.pingGroupBox.Controls.Add(this.listViewPING);
            this.pingGroupBox.Controls.Add(this.pingBox);
            this.pingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pingGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingGroupBox.ForeColor = System.Drawing.Color.RoyalBlue;
            this.pingGroupBox.Location = new System.Drawing.Point(0, 0);
            this.pingGroupBox.Name = "pingGroupBox";
            this.pingGroupBox.Size = new System.Drawing.Size(849, 348);
            this.pingGroupBox.TabIndex = 86;
            this.pingGroupBox.TabStop = false;
            this.pingGroupBox.Text = "PING";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = global::WirelessProject.Properties.Resources.remove;
            this.btnRemove.Location = new System.Drawing.Point(707, 20);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(65, 54);
            this.btnRemove.TabIndex = 98;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this._tbRemoveHost_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::WirelessProject.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(636, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 54);
            this.btnAdd.TabIndex = 97;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions.Enabled = false;
            this.btnOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptions.Image = global::WirelessProject.Properties.Resources.options;
            this.btnOptions.Location = new System.Drawing.Point(778, 20);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(65, 54);
            this.btnOptions.TabIndex = 96;
            this.btnOptions.Text = "Columns";
            this.btnOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // _cbShowErrorMessages
            // 
            this._cbShowErrorMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbShowErrorMessages.AutoSize = true;
            this._cbShowErrorMessages.Enabled = false;
            this._cbShowErrorMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cbShowErrorMessages.ForeColor = System.Drawing.Color.Crimson;
            this._cbShowErrorMessages.Location = new System.Drawing.Point(740, 80);
            this._cbShowErrorMessages.Name = "_cbShowErrorMessages";
            this._cbShowErrorMessages.Size = new System.Drawing.Size(93, 19);
            this._cbShowErrorMessages.TabIndex = 95;
            this._cbShowErrorMessages.Text = "Show &Errors";
            this._cbShowErrorMessages.UseVisualStyleBackColor = true;
            this._cbShowErrorMessages.CheckedChanged += new System.EventHandler(this._cbShowErrorMessages_CheckedChanged);
            // 
            // listViewPING
            // 
            this.listViewPING.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPING.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._colIpAddr,
            this._colHostName,
            this._colStatus,
            this._colPacketSent,
            this._colPacketReceived,
            this._colPacketReceivedPercent,
            this._colPacketLost,
            this._colMaxConsecutivePacketsLost,
            this._colRecPacketLost,
            this._colTestDuration});
            this.listViewPING.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPING.FullRowSelect = true;
            this.listViewPING.Location = new System.Drawing.Point(3, 111);
            this.listViewPING.MultiSelect = false;
            this.listViewPING.Name = "listViewPING";
            this.listViewPING.Size = new System.Drawing.Size(843, 234);
            this.listViewPING.TabIndex = 93;
            this.listViewPING.UseCompatibleStateImageBehavior = false;
            this.listViewPING.View = System.Windows.Forms.View.Details;
            this.listViewPING.SelectedIndexChanged += new System.EventHandler(this.listViewPING_SelectedIndexChanged);
            // 
            // _colIpAddr
            // 
            this._colIpAddr.Tag = "1";
            this._colIpAddr.Text = "IP Address";
            this._colIpAddr.Width = 102;
            // 
            // _colHostName
            // 
            this._colHostName.Tag = "1";
            this._colHostName.Text = "Host Name";
            this._colHostName.Width = 90;
            // 
            // _colStatus
            // 
            this._colStatus.Tag = "1";
            this._colStatus.Text = "Status";
            this._colStatus.Width = 70;
            // 
            // _colPacketSent
            // 
            this._colPacketSent.Tag = "1";
            this._colPacketSent.Text = "Sent";
            // 
            // _colPacketReceived
            // 
            this._colPacketReceived.Tag = "1";
            this._colPacketReceived.Text = "Received";
            this._colPacketReceived.Width = 75;
            // 
            // _colPacketReceivedPercent
            // 
            this._colPacketReceivedPercent.Tag = "1";
            this._colPacketReceivedPercent.Text = "Received %";
            this._colPacketReceivedPercent.Width = 78;
            // 
            // _colPacketLost
            // 
            this._colPacketLost.Tag = "1";
            this._colPacketLost.Text = "Lost";
            this._colPacketLost.Width = 50;
            // 
            // _colMaxConsecutivePacketsLost
            // 
            this._colMaxConsecutivePacketsLost.Tag = "1";
            this._colMaxConsecutivePacketsLost.Text = "Consecutive Lost";
            this._colMaxConsecutivePacketsLost.Width = 110;
            // 
            // _colRecPacketLost
            // 
            this._colRecPacketLost.Tag = "1";
            this._colRecPacketLost.Text = "Lost (Recent)";
            this._colRecPacketLost.Width = 100;
            // 
            // _colTestDuration
            // 
            this._colTestDuration.Tag = "1";
            this._colTestDuration.Text = "Test Duration";
            this._colTestDuration.Width = 90;
            // 
            // pingBox
            // 
            this.pingBox.Location = new System.Drawing.Point(19, 35);
            this.pingBox.Name = "pingBox";
            this.pingBox.Size = new System.Drawing.Size(22, 22);
            this.pingBox.TabIndex = 89;
            this.pingBox.TabStop = false;
            // 
            // PingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pingGroupBox);
            this.Name = "PingControl";
            this.Size = new System.Drawing.Size(849, 348);
            this.pingGroupBox.ResumeLayout(false);
            this.pingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pingGroupBox;
        private System.Windows.Forms.PictureBox pingBox;
        private System.Windows.Forms.ColumnHeader _colRecPacketLost;
        private System.Windows.Forms.ColumnHeader _colMaxConsecutivePacketsLost;
        private System.Windows.Forms.ColumnHeader _colTestDuration;
        private System.Windows.Forms.ColumnHeader _colIpAddr;
        private System.Windows.Forms.ColumnHeader _colPacketLost;
        private System.Windows.Forms.ColumnHeader _colHostName;
        private System.Windows.Forms.ColumnHeader _colPacketReceivedPercent;
        private System.Windows.Forms.ColumnHeader _colPacketReceived;
        private System.Windows.Forms.ColumnHeader _colPacketSent;
        private System.Windows.Forms.ColumnHeader _colStatus;
        private ListViewDB listViewPING;
        private System.Windows.Forms.CheckBox _cbShowErrorMessages;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;

    }
}
