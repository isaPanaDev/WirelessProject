using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WirelessProject
{
    public partial class OutputForm : Form
    {
        /**
         * Path where all logs are saved
         */
        private string logpath;
        private HtmlAssembler html_assembler;

        public OutputForm(string logpath)
        {
            InitializeComponent();
            init();
            this.logpath = logpath;
            this.populateListBox(getCSVFiles(this.logpath));
        }

        private void init()
        {
            textBox1.Size = new System.Drawing.Size(584, 537);
            webBrowser1.Size = new System.Drawing.Size(584, 537);
            textBox1.ReadOnly = true;
            rb_text.Checked = true; //Text is the default output
            html_assembler = new HtmlAssembler();
        }

        private FileInfo[] getCSVFiles(string logpath)
        {
            if (Directory.Exists(logpath))
            {
                DirectoryInfo dinfo = new DirectoryInfo(logpath);
                FileInfo[] files = dinfo.GetFiles("*.csv");
                return files;
            }
            return null;
        }

        private void populateListBox(FileInfo[] files)
        {
            if (files != null)
            {
                foreach (FileInfo file in files)
                    this.listBox1.Items.Add(file.Name);
            }
        }

        private void showTextOutput()
        {
            webBrowser1.Hide();
            textBox1.Visible = true;
            textBox1.Location = new System.Drawing.Point(0, 0);
            textBox1.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                string file = logpath + "\\" + listBox1.SelectedItem.ToString();
                string content = File.ReadAllText(file);
                //Console.WriteLine(content);
                textBox1.Text = content;
            }
        }

        private void showHtmlOutput()
        {
            textBox1.Hide();
            webBrowser1.Visible = true;
            webBrowser1.Location = new System.Drawing.Point(0, 0);
            if (listBox1.SelectedIndex != -1)
            {
                string filename = listBox1.SelectedItem.ToString();
                string filepath = logpath + "\\" + filename;
                webBrowser1.DocumentText = html_assembler.getHtmlOutput(filepath);
            }
        }

        private void showOutput()
        {
            if (rb_text.Checked)
                showTextOutput();
            else
                showHtmlOutput();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showOutput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rb_text_CheckedChanged(object sender, EventArgs e)
        {
            showOutput();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            showOutput();
        }
    }
}
