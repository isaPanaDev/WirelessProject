using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WirelessProject
{
	public partial class PingColumnOptions : Form
	{
		public PingColumnOptions()
		{
			InitializeComponent();

		}

		public CheckedListBox SelectedColumns
		{
			get { return columns; }
		}

	}
}