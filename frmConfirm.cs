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
    public partial class frmConfirm : Form
    {
        public frmConfirm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _global.tmp = txtPassword.Text.Trim();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK_Click(this, e);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
