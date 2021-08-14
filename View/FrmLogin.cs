using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;

namespace View
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        public FrmLogin(string account , string pass)
        {
            InitializeComponent();
            txtAccount.Text = account;
            txtPass.Text = pass;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if ((new LoginController()).checkUser(txtAccount.Text, txtPass.Text))
            {
                MessageBox.Show("Login successful");
                FrmMain f = new FrmMain(txtAccount.Text);
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login fail");
            }
        }

        private void txtAccount_Click(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.Pink;
            txtPass.BackColor = Color.White;
        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.Pink;
        }

        private void FrmLogin_Click(object sender, EventArgs e)
        {
            txtPass.BackColor = Color.White;
            txtAccount.BackColor = Color.White;
            txtAccount.Enabled = false;
            txtPass.Enabled = false;
            txtAccount.Enabled = true;
            txtPass.Enabled = true;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtAccount.Text = "";
            txtPass.Text = "";
        }     
        private void enabledButtonReset()
        {
            if (txtAccount.Text.Trim().Equals("") && txtPass.Text.Trim().Equals(""))
            {
                btnReset.Enabled = false;
            }else if (txtAccount.Text.Trim().Equals("") || txtPass.Text.Trim().Equals(""))
            {
                btnReset.Enabled = true;
            }           
        }
       

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            enabledButtonReset();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            enabledButtonReset();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister f = new FrmRegister();
            f.Show();
            this.Hide();
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmForgetPassword f = new FrmForgetPassword(txtAccount.Text);
            f.Show();
            this.Hide();
        }

        private void linkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmChangePassword f = new FrmChangePassword(txtAccount.Text);
            f.Show();
            this.Hide();
        }
    }
    
}
