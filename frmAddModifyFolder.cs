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
    public partial class frmAddModifyFolder : Form
    {
        public frmAddModifyFolder()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _recordfolder.name = txtName.Text.Trim().Replace('\'', '`');// 기호렬속에 "'"이 들어있으면 "`"으로 교체
            _recordfolder.comment = richTextBox.Text.Trim().Replace('\'', '`');// 기호렬속에 "'"이 들어있으면 "`"으로 교체
        }

        private void frmAddModifyFolder_Load(object sender, EventArgs e)
        {
            txtName.Text = _recordfolder.name;
            richTextBox.Text = _recordfolder.comment;
        }
    }
}
