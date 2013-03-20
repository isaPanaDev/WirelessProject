using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace WirelessProject
{
	public delegate void UpdateIPAddressDelegate(IPHostEntry dnsEntry);
	public delegate void ShowErrorDelegate(string error);

	public partial class PingMenu : Form
	{
		public PingMenu()
		{
			InitializeComponent();
		}

        PingObject _host = null;
        public PingObject Host
		{
			get { return _host; }
		}

        public DialogResult ShowDialog(IWin32Window owner, PingObject host)
		{
			_host = host;

			DialogResult res = ShowDialog(owner);
			if (res == DialogResult.OK)
			{
				if (_host == null)
				{
                    try
                    {
                        if (radioIP.Checked)    // IP was entered
                        {
                            _host = new PingObject(IPAddress.Parse(hostIp.Text));
                        }
                        else if (radioName.Checked)
                        {    // Host Name was entered
                            _host = new PingObject(hostName.Text);
                        }
                        else
                        {
                            _host = new PingObject();
                        }
                    }
                    catch
                    {
                        _host = null;
                        return DialogResult.Cancel;
                    }
				}

                _host.HostName = _host.HostName;
                _host.HostIP = _host.HostIP;
				_host.Timeout = (int)timeout.Value;
				_host.PingInterval = (int)interval.Value;
                _host.SentPackets = (int)packetSize.Value;
			}

			return res;
		}

        private void HostOptions_Load(object sender, EventArgs e)
        {
            if (_host != null)
            {
                hostName.Text = _host.HostName;
                hostIp.Text = _host.HostIP.ToString();

                btnResolve.Enabled = false;
                hostIp.Enabled = false;

                timeout.Value = _host.Timeout;
                interval.Value = _host.PingInterval;
                packetSize.Value = _host.SentPackets;
            }
            else
            {
                timeout.Value = PingObject.DEFAULT_TIMEOUT;
                interval.Value = PingObject.DEFAULT_PING_INTERVAL;
                packetSize.Value = PingObject.DEFAULT_PACKET_SENT;
               
            }
        }

		private void btnResolve_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ParameterizedThreadStart(Resolve));
			thread.Start(hostName.Text);
		}

		private void UpdateIPAddress(IPHostEntry dnsEntry)
		{
			if (InvokeRequired)
			{
				Invoke(new UpdateIPAddressDelegate(UpdateIPAddress), dnsEntry);
				return;
			}

			if (dnsEntry.AddressList.Length > 1)
			{
                ChooseIPForm dlg = new ChooseIPForm();
                if (dlg.ShowDialog(this, dnsEntry.AddressList) == DialogResult.OK)
                    hostIp.Text = dlg.SelectedIPAddress.ToString();
			}
			else // Get the IP address from textbox
				hostIp.Text = dnsEntry.AddressList[0].ToString();
		}

		private void ShowError(string error)
		{
			if (InvokeRequired)
			{
				Invoke(new ShowErrorDelegate(ShowError), error);
				return;
			}

			MessageBox.Show(error, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void Resolve(object name)
		{
			try
			{
				UpdateIPAddress(Dns.GetHostEntry((string)name));
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
			}
		}
		
        private void radioName_CheckedChanged(object sender, EventArgs e)
        {
            btnResolve.Enabled = !btnResolve.Enabled;
            hostName.Enabled = !hostName.Enabled;
            hostIp.Enabled = !hostName.Enabled;
        }

	}
}