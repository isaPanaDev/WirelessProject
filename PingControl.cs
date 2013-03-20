using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace WirelessProject
{
    public delegate void PingStatusChangedEventHandler(object sender, bool pingStatus);

    public partial class PingControl : UserControl
    {
        #region Variables 
        public event PingStatusChangedEventHandler PingFailed;

        // LED related
        private LEDPictureBox pingLED;
        private Bitmap greenLEDimage;
        private Bitmap redLEDimage;
        private Bitmap defaultLEDimage;
        private Bitmap orangeLEDimage;
        private bool failPingStatus; // Used to set the correct LED depending on all pings
        private bool testStarted; 
        private DebugControl debug;
        private int pingEventID = 1; // Ping Event ID

        private PingObject _selectedPinger = null;
        private List<PingObject> _hosts = new List<PingObject>();
        private Hashtable _table = new Hashtable();
        #endregion

        public ListViewDB PingListView { get { return listViewPING; } }
        public DebugControl Debug { set { debug = value; } }

        public PingControl()
        {
            InitializeComponent();
            InitializeLEDIcon();

            testStarted = false;
        }

        private void InitializeLEDIcon()
        {
            greenLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.green);
            redLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.red);
            orangeLEDimage = new Bitmap(global::WirelessProject.Properties.Resources.orange);
            defaultLEDimage = new Bitmap(global::WirelessProject.Properties.Resources._default);
            pingLED = new LEDPictureBox(this, pingBox, defaultLEDimage);
            pingLED.LedOn();
            pingLED.BlinkEnable();
            pingLED.Visible = true;
            pingBox.Visible = true;
            failPingStatus = false;
        }

        internal void StartTest()
        {
            foreach (PingObject pingInList in _hosts)
            {
                pingInList.OnPing += new OnPingDelegate(OnHostPing);
                pingInList.OnStopPinging += new OnHostPingerCommandDelegate(OnStopPinging);
                pingInList.OnStartPinging += new OnHostPingerCommandDelegate(OnStartPinging);

                pingInList.Start();
            }
            testStarted = true;

            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
        }

        internal void StopTest()
        {
            foreach (PingObject pingInList in _hosts)
            {
                pingInList.Stop();
                // Keep the order!!
                pingInList.OnPing -= new OnPingDelegate(OnHostPing);
                pingInList.OnStopPinging -= new OnHostPingerCommandDelegate(OnStopPinging);
                pingInList.OnStartPinging -= new OnHostPingerCommandDelegate(OnStartPinging);
            }

            pingBox.Visible = true;
            failPingStatus = false;
            pingLED.ChangeColor(orangeLEDimage); 
            pingBox.Visible = true;

            testStarted = false;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
        }

        private void OnPingFailed(bool e)
        {
            if (PingFailed != null)
                PingFailed(this, e);
        }

        private void _cbShowErrorMessages_CheckedChanged(object sender, EventArgs e)
        {
            Options.Instance.ShowErrorMessages = !Options.Instance.ShowErrorMessages;
        }

        // Called when pinging to update the LED
        void UpdateLED(PingObject host)
        {
            switch (host.Status)
            {
                case HostStatus.Dead:
                     pingLED.ChangeColor(redLEDimage);
                     pingBox.Visible = true;
                     failPingStatus = true;
                    break;

                case HostStatus.DnsError:
                    pingLED.ChangeColor(redLEDimage);
                    failPingStatus = true;
                    break;

                case HostStatus.Alive:
                    if (failPingStatus)  
                        pingLED.ChangeColor(redLEDimage);
                    else
                        pingLED.ChangeColor(greenLEDimage); 
                    pingBox.Visible = !pingBox.Visible; // blink it!
                    break;

                case HostStatus.Stopped:
                    pingLED.ChangeColor(orangeLEDimage);
                    pingBox.Visible = true; // Don't need to blink when stopped
                    break;

                case HostStatus.Unknown:
                    pingLED.ChangeColor(orangeLEDimage);
                    if (!testStarted) break;
                    if (_selectedPinger != null && _selectedPinger.IsRunning) // only blink when running
                    {
                        pingBox.Visible = !pingBox.Visible;
                    }
                    break;
            }
        }

        void OnStartPinging(PingObject host)
        {
            Console.WriteLine("On start pinging");
            if (InvokeRequired)
            {
                Invoke(new OnPingDelegate(OnHostPing), new object[] { host });
                return;
            }

            lock (_table)
            {
                if ((ListViewItem)_table[host.ID] != null && _selectedPinger == host)
                {
                    OnPingFailed(false);

                   // if (_selectedPinger.HostName == host.hostNoname)
                   //     host.Status = HostStatus.Dead;
                }

                UpdateLED(host);
            }
        }

        void OnStopPinging(PingObject host)
        {
            Console.WriteLine("On stop pinging");
            if (InvokeRequired)
            {
                Invoke(new OnPingDelegate(OnHostPing), new object[] { host });
                return;
            }

            lock (_table)
            {
                ListViewItem item = (ListViewItem)_table[host.ID];
                if (item != null)
                {
                    item.BackColor = Color.SandyBrown;
                    item.ForeColor = Color.Black;
                    item.SubItems[2].Text = host.StatusName;

                    pingLED.ChangeColor(orangeLEDimage);

                    if (_selectedPinger == host)
                    {
                        OnPingFailed(true);
                    }
                }

                UpdateLED(host);
            }
        }

        string PercentToString(float percent)
        {
            return String.Format("{0:P}", percent / 100);
        }
        string DurationToString(TimeSpan duration)
        {
            StringBuilder builder = new StringBuilder();
            if (duration.Days > 0)
            {
                builder.Append(duration.Days);
                builder.Append(duration.Days > 1 ? " days " : " day ");
            }

            builder.AppendFormat("{0:d2} : {1:d2} : {2:d2}", duration.Hours, duration.Minutes, duration.Seconds);

            return builder.ToString();
        }
        void OnHostPing(PingObject host)
        {
            if (InvokeRequired)
            {
                Invoke(new OnPingDelegate(OnHostPing), new object[] { host });
                return;
            }
            UpdateList(host);
          
        }
        private void UpdateList(PingObject host)
        {
            lock (_table)
            {
                ListViewItem item = (ListViewItem)_table[host.ID];
                if (item != null)
                {
                    item.SubItems[0].Text = host.HostIP.ToString();
                    item.SubItems[1].Text = host.HostName;
                    item.SubItems[2].Text = host.StatusName;
                    item.SubItems[3].Text = host.SentPackets.ToString();
                    item.SubItems[4].Text = host.ReceivedPackets.ToString();
                    item.SubItems[5].Text = PercentToString(host.ReceivedPacketsPercent);
                    item.SubItems[6].Text = host.LostPackets.ToString();
                    item.SubItems[7].Text = host.MaxConsecutivePacketsLost.ToString();                    
                    item.SubItems[8].Text = host.RecentlyLostPackets.ToString();
                    item.SubItems[9].Text = DurationToString(host.CurrentTestDuration);
                }
                else
                {
                    item = new ListViewItem(new string[] 
					{ 
						host.HostIP.ToString(), host.HostName,
						host.StatusName,
						host.SentPackets.ToString(), host.ReceivedPackets.ToString(), PercentToString(host.ReceivedPacketsPercent),
						host.LostPackets.ToString(), host.MaxConsecutivePacketsLost.ToString(),
						host.RecentlyLostPackets.ToString(), 
						DurationToString(host.CurrentTestDuration)
					});
						
                    _table.Add(host.ID, item);
                    item.Tag = host;
                    listViewPING.Items.Insert(0, item);
                }

                _selectedPinger = host;
                Console.WriteLine("in update list " + host.Status.ToString());
                switch (host.Status)
                {
                    case HostStatus.Dead:
                        item.BackColor = Color.Red;
                        item.ForeColor = Color.White;

                        
                         // Keep the order!!
                        host.OnPing -= new OnPingDelegate(OnHostPing);
                        host.OnStopPinging -= new OnHostPingerCommandDelegate(OnStopPinging);
                        host.OnStartPinging -= new OnHostPingerCommandDelegate(OnStartPinging);
                        host.Stop();

                        Wlog.log.Error(+pingEventID + ",1," + host.ID.ToString() + "," + host.HostInfo);
                        break;

                    case HostStatus.DnsError:
                        item.BackColor = Color.OrangeRed;
                        item.ForeColor = Color.White;

                        Wlog.log.Error(+pingEventID + ",1," + host.ID.ToString() + "," + host.HostInfo);
                        break;

                    case HostStatus.Alive:
                        item.BackColor = Color.LightGreen;
                        item.ForeColor = Color.Black;

                        Wlog.log.Debug(+pingEventID + ",0," + host.ID.ToString() + "," + host.HostInfo);
                        break;

                    case HostStatus.Stopped:
                        item.BackColor = Color.Turquoise;
                        item.ForeColor = Color.Purple;

                        Wlog.log.Debug(+pingEventID + ",0," + host.ID.ToString() + "," + host.HostInfo);
                        break;

                    case HostStatus.Unknown:
                        item.BackColor = Color.Yellow;
                        item.ForeColor = Color.Black;

                        Wlog.log.Error(+pingEventID + ",1," + host.ID.ToString() + "," + host.HostInfo);
                        break;
                }

                UpdateLED(host);
            }
        }
        private void _tbRemoveHost_Click(object sender, EventArgs e)
        {
            if (_selectedPinger != null && !_selectedPinger.IsRunning)
            {
                if (MessageBox.Show("Remove host from list?", "Remove host",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                lock (_table)
                {
                    lock (_hosts)
                    {
                        _hosts.Remove(_selectedPinger);
                    }

                    _selectedPinger.OnPing -= new OnPingDelegate(OnHostPing);
                    _selectedPinger.OnStopPinging -= new OnHostPingerCommandDelegate(OnStopPinging);
                    _selectedPinger.OnStartPinging -= new OnHostPingerCommandDelegate(OnStartPinging);

                    ListViewItem hi = (ListViewItem)_table[_selectedPinger.ID];
                    _table.Remove(_selectedPinger.ID);
                    listViewPING.Items.Remove(hi);
                }
            }
        }

        private void _tbStartHost_Click(object sender, EventArgs e)
        {
            if (_selectedPinger != null)
                _selectedPinger.Start();
        }

        private void _tbStopHost_Click(object sender, EventArgs e)
        {
            if (_selectedPinger != null)
                _selectedPinger.Stop();
        }

        private void listViewPING_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPING.SelectedItems != null && listViewPING.SelectedItems.Count > 0)
                _selectedPinger = (PingObject)listViewPING.SelectedItems[0].Tag;
            else
                _selectedPinger = null;

            // Notify main to update its buttons states
        //    OnPingFailed(_selectedPinger != null && _selectedPinger.IsRunning);
        }

        #region Ping Menu Options
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewHost(null);
        }
        public void AddNewHost(PingObject host)
        {
            PingMenu dlg = new PingMenu();
            if (dlg.ShowDialog(this, host) == DialogResult.OK)
            {
                bool exists = false;
                lock (_hosts)
                {
                    foreach (PingObject hp in _hosts)
                    {
                        if (hp.HostIP != null && hp.HostIP == dlg.Host.HostIP)
                        {
                            exists = true;
                            break;
                        }
                    }
                }

                if (exists)
                {
                    MessageBox.Show("Host already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                lock (_hosts)
                {
                    _hosts.Add(dlg.Host);
                }

                UpdateList(dlg.Host);
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            PingColumnOptions dlg = new PingColumnOptions();

            for (int i = Options.NUMBER_OF_COLUMNS - 1; i >= 0; i--)
                dlg.SelectedColumns.SetItemChecked(i, Options.Instance.GetComlumnVisability(i));

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = Options.NUMBER_OF_COLUMNS - 1; i >= 0; i--)
                {
                    bool old = Options.Instance.GetComlumnVisability(i);

                    Options.Instance.SetColumnVisability(i, dlg.SelectedColumns.GetItemChecked(i));

                    if (!old)
                        listViewPING.Columns[i].Width = Options.Instance.GetColumnWidth(i);

                    if (!Options.Instance.GetComlumnVisability(i))
                        listViewPING.Columns[i].Width = 0;
                }
            }
        }

        #endregion        
    }
}
