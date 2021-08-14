using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controller;
using Model;

namespace View
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }       
        public FrmMain(string title)
        {
            InitializeComponent();
            this.Text = "Welcome " + title;            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cbxTestName.Text.Equals(""))
            {
                MessageBox.Show("Text name must be not empty!", "Show fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgMarkTest.DataSource = new MainController().getAllResultByID(cbxTestName.SelectedValue.ToString());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cbxTestName.DataSource = new MainController().getAllTestName();
            cbxTestName.DisplayMember = "TestName";
            cbxTestName.ValueMember = "TestID";
        }

        private void dgMarkTest_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            if (dgMarkTest.CurrentCell == null || dgMarkTest.CurrentCell.Value == null || e.RowIndex == -1) return;
            if (dgMarkTest.CurrentCell.ColumnIndex.Equals(0))
            {
                string studentName = dgMarkTest.CurrentCell.Value.ToString();
                Student st = new MainController().getStudentByID(studentName,cbxTestName.SelectedValue.ToString());
                FrmMarkDetail f = new FrmMarkDetail(st);
                f.ShowDialog();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {            
            FrmMarkTest f = new FrmMarkTest(this.Text);
            f.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals(""))
            {
                MessageBox.Show("StudentRoll must be not empty!", "Search fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgMarkTest.DataSource = new MainController().getResultByStudentID(txtSearch.Text,cbxTestName.SelectedValue.ToString());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cbxTestName.Text = "";
            dgMarkTest.DataSource = null;
                dgMarkTest.Rows.Clear();
            
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.BackColor = Color.Pink;
        }

        private void FrmMain_Click(object sender, EventArgs e)
        {
            txtSearch.BackColor = Color.White;
            txtSearch.Enabled = false;
            txtSearch.Enabled = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = this.Text.Split(' ');
            FrmLogin f = new FrmLogin(s[1],"");
            f.Show();
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!txtSearch.Text.Trim().Equals(""))
            {
                btnReset.Enabled = true;
            }
            else
            {
                btnReset.Enabled = false;
            }
        }

        private void cbxTestName_TextChanged(object sender, EventArgs e)
        {
            if (!cbxTestName.Text.Trim().Equals(""))
            {
                btnReset.Enabled = true;
            }
            else
            {
                btnReset.Enabled = false;
            }
        }
    }
}
