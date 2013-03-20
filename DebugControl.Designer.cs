namespace WirelessProject
{
    partial class DebugControl
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
            this.debugSettings = new System.Windows.Forms.GroupBox();
            this.lblConnStatus = new System.Windows.Forms.Label();
            this.dataGridCoord = new System.Windows.Forms.DataGridView();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debugSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCoord)).BeginInit();
            this.SuspendLayout();
            // 
            // debugSettings
            // 
            this.debugSettings.Controls.Add(this.lblConnStatus);
            this.debugSettings.Controls.Add(this.dataGridCoord);
            this.debugSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugSettings.ForeColor = System.Drawing.Color.RoyalBlue;
            this.debugSettings.Location = new System.Drawing.Point(3, 3);
            this.debugSettings.Name = "debugSettings";
            this.debugSettings.Size = new System.Drawing.Size(501, 105);
            this.debugSettings.TabIndex = 61;
            this.debugSettings.TabStop = false;
            this.debugSettings.Text = "GPS";
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.AutoSize = true;
            this.lblConnStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnStatus.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.lblConnStatus.Location = new System.Drawing.Point(268, 47);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(83, 15);
            this.lblConnStatus.TabIndex = 81;
            this.lblConnStatus.Text = "GPS disabled";
            // 
            // dataGridCoord
            // 
            this.dataGridCoord.AllowUserToDeleteRows = false;
            this.dataGridCoord.AllowUserToResizeColumns = false;
            this.dataGridCoord.AllowUserToResizeRows = false;
            this.dataGridCoord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCoord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Longitude,
            this.Latitude});
            this.dataGridCoord.Location = new System.Drawing.Point(6, 31);
            this.dataGridCoord.MultiSelect = false;
            this.dataGridCoord.Name = "dataGridCoord";
            this.dataGridCoord.ReadOnly = true;
            this.dataGridCoord.RowHeadersVisible = false;
            this.dataGridCoord.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridCoord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridCoord.Size = new System.Drawing.Size(247, 46);
            this.dataGridCoord.TabIndex = 80;
            // 
            // Longitude
            // 
            this.Longitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.Name = "Longitude";
            this.Longitude.ReadOnly = true;
            this.Longitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Latitude
            // 
            this.Latitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.Name = "Latitude";
            this.Latitude.ReadOnly = true;
            this.Latitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DebugControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.debugSettings);
            this.Name = "DebugControl";
            this.Size = new System.Drawing.Size(514, 267);
            this.debugSettings.ResumeLayout(false);
            this.debugSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCoord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox debugSettings;
        private System.Windows.Forms.Label lblConnStatus;
        private System.Windows.Forms.DataGridView dataGridCoord;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;


    }
}
