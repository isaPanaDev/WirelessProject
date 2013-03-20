using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Toughbook.Gps;
using System.IO.Ports;

namespace WirelessProject
{
    public delegate void ComportStatusChangedEventHandler(object sender, bool comportStatus);

    public partial class GpsControl : UserControl
    {
        #region Variables
        public event ComportStatusChangedEventHandler ComportChanged;

        private System.Windows.Forms.Timer timer;
        public Gps gps = null; 
        private double longitude;
        private double latitude;
        private DebugControl debug;
        private Thread satThread = null;

        private int gpsEventID = 2; // GPS Event ID
        private int gpsDelay;
        private string gpsStatus; // long/lat validation

        private Bitmap defaultLEDimage;
        private Bitmap greenLEDimage;
        private Bitmap redLEDimage;
        private Bitmap orangeLEDimage;

        private LEDPictureBox comportLED;
        private LEDPictureBox gpsLED;
        #endregion
        public DebugControl Debug { set { debug = value; } }

        public GpsControl()
        {
            InitializeComponent();
            InitializeLEDIcon();

            gps = new Gps();
            gps.CoordinateChanged += new EventHandler<CoordinateEventArgs>(gps_CoordinateChanged);
        
            timer = new System.Windows.Forms.Timer();           
            timer.Tick += new EventHandler(timer_Tick);

            gpsDelay = Int32.Parse(gpsNumericUpDown.Value.ToString()) * 1000;
            timer.Interval = gpsDelay;

            //cmbPortName.SelectedIndex = 4;
            RefreshComPortList();                     
        }

        private void RefreshComPortList()
        {
            cmbPortName.Items.Clear();
            int num = 0;
            // add NONE port
            cmbPortName.Items.Add("NONE");
            foreach (string s in SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray() )
            {
                cmbPortName.Items.Add(s);
            }
            if (cmbPortName.Items.Count > 0)
            {
                cmbPortName.SelectedIndex = 0;

            }       
         
        }

        private void gps_CoordinateChanged(object sender, CoordinateEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate()
            {
                longitude = e.Coordinate.Longitude.DecimalDegrees;
                latitude = e.Coordinate.Latitude.DecimalDegrees;
            }));
            Console.WriteLine("----- long: " + longitude.ToString() + " -- lang: " + latitude.ToString());
        }
        private void InitializeLEDIcon()
        {
            greenLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.green);
            redLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.red);
            orangeLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.orange);
            defaultLEDimage = new Bitmap(global::WirelessProject.Properties.Resources._default);

            comportLED = new LEDPictureBox(this, comportBox, defaultLEDimage);
            comportLED.LedOn();
            comportLED.BlinkEnable();

            gpsLED = new LEDPictureBox(this, gpsBox, defaultLEDimage);
            gpsLED.LedOn();
            gpsLED.BlinkEnable();
        }
        private bool SelectDevice(string port, int baudrate)
        {
            try
            {
                string com = port.Replace("COM", "");
                Device dev = new SerialDevice("COM" + com, baudrate);
                gps.Open(dev);

                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        internal bool StartTest()
        {
            try
            {
                // check if COM port is selected
                if (cmbPortName.Text == "NONE")
                {                    
                    return false;
                }

                // check if COM port is a GPS port
                // TBD

                timer.Start();
                SelectDevice(cmbPortName.Text, Int32.Parse(baudRate.Text));
               
                if (satThread == null)
                {
                    satThread = new Thread(new ThreadStart(GetSatelliteData));
                }

                if ((satThread.ThreadState == System.Threading.ThreadState.Unstarted) ||
                    (satThread.ThreadState == System.Threading.ThreadState.Stopped))
                {
                    debug.ConnectionStatus = "GPS enabled";
                    satThread.Start();
                }

                comportLED.ChangeColor(greenLEDimage);

                return true;
            }
            catch (Exception ex)
            {
                debug.ConnectionStatus = "GPS failed to enable.\n";
                debug.ConnectionStatus += ex.Message;
                Wlog.log.Debug("FAIL GPSEnable: " + ex.Message);

                comportLED.ChangeColor(redLEDimage);
            }
            return false;
        }
        private void GetSatelliteData()
        {
            while (true)
            {
                debug.Grid.Rows[0].Cells[0].Value = longitude;
                debug.Grid.Rows[0].Cells[1].Value = latitude;

                // Verify range is valid
                gpsStatus = "1"; // error
                if (latitude >= -90 && latitude <= 90 && longitude >= -180 && longitude <= 180)
                {
                    gpsStatus = "0"; // success - valid range!
                }
                
                Console.WriteLine("LATITUDE: " + latitude.ToString() + " LONGITUDE: " + longitude.ToString());

                // Write coordinates to Log file
                string newGpsData = +gpsEventID + "," + gpsStatus + "," + latitude.ToString() + "," + longitude.ToString();
                if (gpsStatus == "1")
                {
                    //string debugErrorMsg = "Dummy error message in GetSatelliteData - delete this later";
                    Wlog.log.Error(newGpsData); // will be in red color!
                }
                else
                {
                    Wlog.log.Debug(newGpsData);
                }

                Console.WriteLine("GPS Delay: " + gpsDelay.ToString());
                Thread.Sleep(gpsDelay); // Delay according to the numeric value
            }
        }

        internal bool StopTest()
        {
            try
            {
                if (satThread != null)
                {
                    // tbGPS.GPSDisable(); // FAILS!
                    satThread.Abort();
                    satThread = null;

                    timer.Stop();

                    ClearCoordinates();
                    debug.ConnectionStatus = "GPS disabled";

                    gpsLED.ChangeColor(orangeLEDimage);

                    if (!gpsBox.Visible) // If invisible, show it!
                        gpsBox.Visible = true;
                }
                return true;
            }
            catch (InvalidOperationException ex)
            {
                debug.ConnectionStatus = "GPS failed to disable.\n";
                debug.ConnectionStatus += ex.ToString();
                Wlog.log.Debug("FAIL GPSDisable: " + ex.Message);
            }
            return false;
        }

        private void ClearCoordinates()
        {
            debug.Grid.Rows[0].Cells[0].Value = debug.Grid.Rows[0].Cells[1].Value = "0.00";
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Update GPS LED status every 1 sec!
            if (gpsStatus == "0") // coordinates valid
            {
                gpsLED.ChangeColor(greenLEDimage);
            }
            else
            {
                gpsLED.ChangeColor(redLEDimage);
            }

            gpsBox.Visible = !gpsBox.Visible; // blink it!
        }

        private void numericUpDownDelay_ValueChanged(object sender, EventArgs e)
        {
            gpsDelay = Int32.Parse(gpsNumericUpDown.Value.ToString()) * 1000;
            timer.Interval = gpsDelay;
        }
    }
}
