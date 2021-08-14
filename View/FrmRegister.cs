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
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            User user = new User();
            DateTime d = DateTime.Now;
            if (txtAccount.Text.Trim().Equals(""))
            {
                MessageBox.Show("Account must be not empty!", "Register fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                user.Account = txtAccount.Text;
            }
            if (txtPass.Text.Trim().Equals(""))
            {
                MessageBox.Show("Password must be not empty!", "Register fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                user.Pass = txtPass.Text;
            }

            user.Name = txtName.Text;
            if (rdbMale.Checked)
            {
                user.Gender = true;
            }
            else
            {
                user.Gender = false;
            }

            if ((d.Year - dtpDOB.Value.Year) <= 18)
            {
                MessageBox.Show("Age must be greater than 18 years old!", "Register fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                user.Dateofbirth = dtpDOB.Value;
            }
            user.Answer = txtAnswer.Text;

            if (cbxQuestion.Text.Equals("") || txtAnswer.Text.Equals(""))
            {
                MessageBox.Show("Question and answer security must be not empty!", "Register fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            user.Question = cbxQuestion.Text;
            user.Address = txtAddress.Text;
            DialogResult result = MessageBox.Show("Do you want to register this account?", "Create Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if ((new RegisterController()).insertUser(user))
                {
                    FrmLogin f = new FrmLogin(txtAccount.Text, txtPass.Text);
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Account already exist or null!", "Register fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmLogin f = new FrmLogin();
            f.Show();
            this.Hide();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtAccount.Text = "";
            txtPass.Text = "";
            txtName.Text = "";
            rdbMale.Checked = true;
            txtAddress.Text = "";
            txtAnswer.Text = "";
            cbxQuestion.SelectedIndex = 0;
            dtpDOB.Value = DateTime.Today;
        }

        private void checkButtonReset()
        {
            if (txtName.Text.Equals("") && txtAddress.Text.Equals("") && txtAccount.Text.Equals("") && txtPass.Text.Equals("") && txtAnswer.Text.Equals(""))
            {
                btnReset.Enabled = false;
            }
            if (!txtName.Text.Equals("") || !txtAddress.Text.Equals("") || !txtAccount.Text.Equals("") || !txtPass.Text.Equals("") || !txtAnswer.Text.Equals(""))
            {
                btnReset.Enabled = true;
            }
        }
        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            checkButtonReset();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            checkButtonReset();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            checkButtonReset();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            checkButtonReset();
        }

        private void txtAccount_Click(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.White;
            txtAccount.BackColor = Color.Pink;
            txtPass.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAnswer.BackColor = Color.White;

        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.White;
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.Pink;
            txtName.BackColor = Color.White;
            txtAnswer.BackColor = Color.White;
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.White;
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.White;
            txtName.BackColor = Color.Pink;
            txtAnswer.BackColor = Color.White;

        }

        private void txtAddress_Click(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.Pink;
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAnswer.BackColor = Color.White;


        }

        private void FrmRegister_Click(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAddress.BackColor = Color.White;
            txtAnswer.BackColor = Color.White;
            txtAccount.Enabled = false;
            txtName.Enabled = false;
            txtAddress.Enabled = false;
            txtPass.Enabled = false;
            txtAccount.Enabled = true;
            txtPass.Enabled = true;
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtAnswer.Enabled = false;
            txtAnswer.Enabled = true;
        }

        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            checkButtonReset();
        }

        private void txtAnswer_Click(object sender, EventArgs e)
        {
            txtAddress.BackColor = Color.White;
            txtAccount.BackColor = Color.White;
            txtPass.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAnswer.BackColor = Color.Pink;

        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
