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
    public partial class frmChangeAdmin : Form
    {
        public frmChangeAdmin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _global.tmp = txtCurPassword.Text.Trim() + "|" + txtNewPassword.Text.Trim() + "|" + txtConfirmPassword.Text.Trim();
        }
    }
}
