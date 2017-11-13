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
    public partial class frmAddModifyImage : Form
    {
        public frmAddModifyImage()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "图片(*.jpg)|*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strLife = txtLife.Text.Trim();
            string strFilename = txtFilename.Text.Trim();
            if (strLife.Equals(""))
            {
                MessageBox.Show("清输入显示时间！");
                this.DialogResult = DialogResult.None;
                return;
            }
            if (strFilename.Equals(""))
            {
                MessageBox.Show("请选择图片！");
                this.DialogResult = DialogResult.None;
                return;
            }
            float life = 0;
            if (!float.TryParse(strLife, out life))
            {
                MessageBox.Show("请输入正确的显示时间！");
                this.DialogResult = DialogResult.None;
                return;
            }
            _global.tmp = strFilename + "|" + life.ToString();
        }

        private void frmAddModifyImage_Load(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            if (!_global.tmp.Equals(""))
            {
                string[] arryImageInfo = _global.tmp.Split('|');
                txtFilename.Text = arryImageInfo[0];
                txtLife.Text = arryImageInfo[1];
            }
        }
    }
}
