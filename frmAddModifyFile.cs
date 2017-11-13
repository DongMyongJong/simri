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
    public partial class frmAddModifyFile : Form
    {
        public frmAddModifyFile()
        {
            InitializeComponent();
        }

        private void frmAddModifyFile_Load(object sender, EventArgs e)
        {
            txtName.Text = _recordFile.name;
            txtTitle.Text = _recordFile.title;
            openFileDialog.FileName = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _recordFile.name = txtName.Text;
            _recordFile.title = txtTitle.Text;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "음성 파일(*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtName.Text = openFileDialog.SafeFileName.Replace('\'', '`'); // 파일이름속에 "'"이 들어있으면 "`"으로 교체
                txtTitle.Text = txtName.Text.Trim().Replace('\'', '`');
                _global.tmp = openFileDialog.FileName;
            }
        }
    }
}
