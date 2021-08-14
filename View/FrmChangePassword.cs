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
    public partial class FrmChangePassword : Form
    {
        public FrmChangePassword()
        {
            InitializeComponent();
        }
        public FrmChangePassword(string account)
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
            if (txtOldPass.Text.Trim().Equals(""))
            {
                MessageBox.Show("Save fail. Old password not empty!");
                return;
            }
            if (!txtNewPass.Text.Trim().Equals(txtConPass.Text) || txtNewPass.Text.Trim().Equals("") || txtConPass.Text.Trim().Equals(""))
            {
                MessageBox.Show("Save fail. Confirm password or new password wrong!");
                return;
            }


            user.Pass = txtNewPass.Text;

            DialogResult result = MessageBox.Show("Do you want to save this password?", "SavePassword", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if ((new RegisterController()).changePassword(user, txtOldPass.Text))
                {
                    FrmLogin f = new FrmLogin(txtAccount.Text, txtNewPass.Text);
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Save fail. Old password wrong!");
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

        private void txtOldPass_TextChanged(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtNewPass.BackColor = Color.White;
            txtConPass.BackColor = Color.White;
            txtOldPass.BackColor = Color.Pink;

        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.Pink;
            txtNewPass.BackColor = Color.White;
            txtConPass.BackColor = Color.White;
            txtOldPass.BackColor = Color.White;
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtNewPass.BackColor = Color.Pink;
            txtConPass.BackColor = Color.White;
            txtOldPass.BackColor = Color.White;
        }

        private void txtConPass_TextChanged(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtNewPass.BackColor = Color.White;
            txtConPass.BackColor = Color.Pink;
            txtOldPass.BackColor = Color.White;
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtNewPass.BackColor = Color.White;
            txtConPass.BackColor = Color.White;
            txtOldPass.BackColor = Color.White;
            txtAccount.Enabled = false;
            txtOldPass.Enabled = false;
            txtNewPass.Enabled = false;
            txtConPass.Enabled = false;
            txtAccount.Enabled = true;
            txtOldPass.Enabled = true;
            txtNewPass.Enabled = true;
            txtConPass.Enabled = true;
        }
    }
}
