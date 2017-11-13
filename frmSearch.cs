using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simri
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strSearch = txtSearch.Text.Trim();
            if (strSearch.Length == 0)
            {
                _global.tmp =  "";
                return;
            }
            string[] arryKey = strSearch.Split(' ');
            string strString = "";
            string strNumber = "";
            int len = arryKey.Length;
            for (int i = 0; i < len; i++ )
            {
                string tmp = arryKey[i].Trim();
                if (tmp.Length != 0)
                {
                    try
                    {
                        strNumber += Int32.Parse(tmp).ToString() + ",";
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.Message);
                        strString += tmp + ",";
                    }
                }
            }
            if (strNumber.EndsWith(","))
            {
                strNumber = strNumber.Substring(0, strNumber.Length - 1);
            }
            if (strString.EndsWith(","))
            {
                strString = strString.Substring(0, strString.Length - 1);
            }
            _global.tmp = strString + "|" + strNumber;
        }
    }
}
