using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using WirelessProject.Properties;

namespace WirelessProject
{
    public partial class DebugControl : UserControl
    {
        #region Properties
        public string ConnectionStatus
        {
            set { lblConnStatus.Text = value; }
            get { return lblConnStatus.Text; }
        }
        public DataGridView Grid { get { return dataGridCoord; } }
        #endregion

        public DebugControl()
        {
            InitializeComponent();
        }
    }
}
