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
    public partial class frmAddModifyUser : Form
    {
        public frmAddModifyUser()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _record.username = txtUsername.Text.Trim();
            _record.password = txtPassword.Text.Trim();
            _global.tmp = txtConfirm.Text.Trim();
            _record.gender = cmbGender.Text.Trim();
            DateTime date = dateTimePicker.Value;
            _record.birthYear = date.Year;
            _record.birthMonth = date.Month;
            _record.birthDay = date.Day;
            _record.job = txtJob.Text.Trim();
        }

        private void frmAddModifyUser_Load(object sender, EventArgs e)
        {
            txtUsername.Text = _record.username;
            txtPassword.Text = _record.password;
            txtConfirm.Text = _record.password;
            cmbGender.Text = _record.gender;
            DateTime date = new DateTime(_record.birthYear, _record.birthMonth, _record.birthDay);
            dateTimePicker.Value = date;
            txtJob.Text = _record.job;
        }
    }
}
