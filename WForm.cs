using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using log4net;

namespace WirelessProject
{ 
    public partial class WForm : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);

        private DebugControl debug;
        List<TabPage> tabPageList = new List<TabPage>(2);

        private bool Resizing = false;

        public WForm()
        {
            InitializeComponent();

            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                //Wlog.log.Info("Internet Connected !");
            }
            else
            {
                //Wlog.log.Error("Internet Not Connected !");
            }

            // Initialize Controls
            debug = new DebugControl();
            tabPageDebug.Controls.Add(debug);

            gpsControl.Debug = debug;
            gpsControl.ComportChanged += new ComportStatusChangedEventHandler(gpsControl_ComportChanged);

            pingControl.Debug = debug;
            pingControl.PingFailed += new PingStatusChangedEventHandler(pingControl_PingFailed);

            // Sizing the Ping ListView to fill whole form when resizing
            pingControl.PingListView.SizeChanged += new EventHandler(test_SizeChanged);
        }

        private void test_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (int i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            Resizing = false;
        }

        // Called if ping fails
        void pingControl_PingFailed(object sender, bool pingStatus)
        {
        }

        // Called if Comport has changed
        void gpsControl_ComportChanged(object sender, bool comportStatus)
        {
        }

        private void EnableControls()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = true;
        }
        private void DisableAll()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = false;
        }
        private void UpdateControl(bool startStatus)
        {
            if (startStatus)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Initilize log4net only when you want to start logging.
            log4net.Config.XmlConfigurator.Configure();

            // Start Ping Test
            pingControl.StartTest();

            // Start GPS Test
            gpsControl.StartTest();
            

            UpdateControl(true);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            pingControl.StopTest();
            gpsControl.StopTest();
            UpdateControl(false);
        }
       
    }
}
