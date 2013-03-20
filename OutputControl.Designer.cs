namespace WirelessProject
{
    partial class OutputControl
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
            this.logPathLabel = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logPathLabel
            // 
            this.logPathLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logPathLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.logPathLabel.Location = new System.Drawing.Point(3, 40);
            this.logPathLabel.Name = "logPathLabel";
            this.logPathLabel.Size = new System.Drawing.Size(396, 26);
            this.logPathLabel.TabIndex = 86;
            this.logPathLabel.Text = "Default Download Directory:";
            this.logPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOutput
            // 
            this.btnOutput.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOutput.Location = new System.Drawing.Point(330, 3);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(69, 41);
            this.btnOutput.TabIndex = 87;
            this.btnOutput.Text = "Output";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // OutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.logPathLabel);
            this.Name = "OutputControl";
            this.Size = new System.Drawing.Size(410, 66);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Label logPathLabel;

    }
}
