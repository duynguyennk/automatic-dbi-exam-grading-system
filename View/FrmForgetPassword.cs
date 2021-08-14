using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public partial class FrmForgetPassword : Form
    {
        public FrmForgetPassword()
        {
            InitializeComponent();
        }
        public FrmForgetPassword(string account)
        {
            InitializeComponent();
            txtAccount.Text = account;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User();
            if (txtAccount.Text.Equals(""))
            {
                MessageBox.Show("Account is empty!", "Save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                user.Account = txtAccount.Text;
            }
            if (!txtNewPass.Text.Trim().Equals(txtConPass.Text)||(txtNewPass.Text.Trim().Equals("") || txtConPass.Text.Trim().Equals("")))
            {
                MessageBox.Show("New password or confrim password wrong!", "Save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            user.Pass = txtNewPass.Text;
            user.Question = cbxQuestion.Text;
            user.Answer = txtAnswer.Text;
            DialogResult result = MessageBox.Show("Do you want to save this password?", "SavePassword", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if ((new RegisterController()).forgotPassword(user))
                {
                    FrmLogin f = new FrmLogin(txtAccount.Text, txtNewPass.Text);
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Question or Answer security wrong!", "Save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmLogin f = new FrmLogin(txtAccount.Text, "");
            f.Show();
            this.Hide();
        }
    }
}
