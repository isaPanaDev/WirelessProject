using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirelessProject
{
    class HtmlTransforms
    {
        /** HTML tags **/
        string openErrorRow = "<tr class=\"error-tr\">";
        string openNoErrorRow = "<tr class=\"noerror-tr\">";
        string closeRow = "</tr>";

        //note: do no user openErrorCell or openNoErrorCell directly, use them getTableCell method
        string openErrorCell = "<td id=\"error-td\" ";
        string openNoErrorCell = "<td ";
        string closeCell = "</td>";
        /** End of HTML tags **/


        public string getTransformations(string[] data)
        {
            try
            {
                if (isHeader(data))
                {
                    return createRow(data, getCellSpacing(data), false);
                }
                else
                {
                    if (isErrorMessage(data))
                        return createRow(data, getCellSpacing(data), true);
                    else
                        return createRow(data, getCellSpacing(data), false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private string createRow(string[] data, int[] cspace, bool isError)
        {
            StringBuilder sb_row = new StringBuilder();
            string openRow = (isError) ? openErrorRow : openNoErrorRow;
            string openCell = (isError) ? openErrorCell : openNoErrorCell;
            sb_row.Append(openRow);
            for (int cnt = 0; cnt < data.Length; cnt++)
            {
                //Console.WriteLine("Cell: " + data[cnt] + " Space: " + cspace[cnt]);
                sb_row.Append(getTableCell(openCell, cspace[cnt])).Append(data[cnt]).Append(closeCell);
            }
            sb_row.Append(closeRow);
            return sb_row.ToString();
        }

        private string getTableCell(string cellType, int colspan)
        {
            return cellType + "colspan="+colspan+">";
        }

        private bool isErrorMessage(string[] data)
        {
            if(data.Length > 2)
            {
                string thirdmsg = data[2];
                if(thirdmsg == "1")
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        private bool isHeader(string[] data)
        {
            if (data.Length > 2)
            {
                string thirdcol = data[2];
                if (thirdcol == "1" || thirdcol == "0")
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * Simplify later
         */

        private int[] getCellSpacing(string[] data)
        {
            string d = string.Concat(data);
            Console.WriteLine("data: " + d);
            Console.WriteLine("No of items in data[]: " + data.Length);
            
            int arraylen = data.Length;
            int max_cells = 5;
            int avglen;
            bool perfect_avg = true;
            int[] colspans = new int[data.Length];
            if (arraylen >= max_cells)
            {
                avglen = 1;
            }
            else
            {
                avglen = max_cells / arraylen;
                perfect_avg = (max_cells % arraylen == 0) ? true : false;
            }
            for(int cnt=0; cnt<data.Length; cnt++)
            {
                if (perfect_avg)
                {
                    Console.WriteLine("is a perfect average");
                    for (int i = 0; i < data.Length; i++)
                    {
                        colspans[i] = avglen;
                        Console.WriteLine("count: " + i + " value " + avglen);
                    }
                    break;
                }
                else
                {
                    colspans[cnt] = (cnt == (arraylen - 1)) ? max_cells - (cnt * avglen) : avglen;
                }
            }
            return colspans;
        }

    }
}
