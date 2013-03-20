using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WirelessProject
{
    class HtmlAssembler
    {
        /**
         * Injects logged data from csv files into
         * log_template to create HTML based logs
         */
        string html_template;
        StringBuilder sb_row;
        StringBuilder sb_rows;
        HtmlTransforms ht;

        /**
         * HTML tags
         */
        string openRow = "<tr>";
        string closeRow = "</tr>";
        string openCell = "<td>";
        string closeCell = "</td>";

        public HtmlAssembler()
        {
            ht = new HtmlTransforms();
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WirelessProject.Resources.log_template.html"))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    html_template = reader.ReadToEnd();
                }
            }
        }

        public string getHtmlOutput(string csvfile)
        {
            using(StreamReader reader = new StreamReader(csvfile))
            {
                string line;
                sb_rows = new StringBuilder();
                string html_copy = html_template;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    sb_rows.Append(ht.getTransformations(data));
                }
                html_copy = html_copy.Replace("<!--@data-->",sb_rows.ToString());
                return html_copy;
            }
        }

        private String createRow(string[] data)
        {
            sb_row = new StringBuilder();
            sb_row.Append(openRow);
            foreach (string d in data)
            {
                sb_row.Append(openCell).Append(d).Append(closeCell);
            }
            sb_row.Append(closeRow);
            return sb_row.ToString();
        }

    }
}
