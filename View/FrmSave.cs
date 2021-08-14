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
    public partial class FrmSave : Form, IFrmSave
    {
        Dictionary<string, Student> dicStudent;
        public FrmSave()
        {
            InitializeComponent();
        }

        public FrmSave(Dictionary<string, Student> dicStudent)
        {
            InitializeComponent();
            this.dicStudent = dicStudent;
        }

        public string TestName { get => txtTestName.Text.Trim(); set => txtTestName.Text = value; }

        public void showMessageBox(string message)
        {
            MessageBox.Show(message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            new MarkTestController(this,dicStudent).saveTest();
            this.Close();
        }
    }
}
