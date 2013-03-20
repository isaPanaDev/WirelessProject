using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WirelessProject.Properties;
using System.IO;

namespace WirelessProject
{
    public partial class OutputControl : UserControl
    {
        private Settings settings = Settings.Default;

        public OutputControl()
        {
            InitializeComponent();

            logPathLabel.Text = getGPSFolder();
        }

        private string getGPSFolder()
        {
            string logPath = Settings.Default.GPSLogFolder;
            if (logPath.Length == 0)
            {
                logPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                Settings.Default.GPSLogFolder = logPath;
            }
            return logPath;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            //Get path from log4net config if possible.
            string logpath = Path.GetDirectoryName(Application.ExecutablePath).ToString()+"\\logs";
            OutputForm outputform = new OutputForm(logpath);
            outputform.Visible = true;


            /**
            string gpsFile = Wlog.FileName(); // Gets the file from log4net configuration
            Process.Start("notepad.exe", gpsFile);
            //*/
        }
    }
}
