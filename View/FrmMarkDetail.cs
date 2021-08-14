using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Controller;
namespace View
{
    public partial class FrmMarkDetail : Form
    {
        Student student;

        public FrmMarkDetail()
        {
            InitializeComponent();
        }

        public FrmMarkDetail(Student student)
        {
            InitializeComponent();
            this.student = student;
        }
        public void initForm()
        {
            lblStudent.Text = student.StudentName;
            lblScore.Text = student.TotalScore + "";
            txtMarkDetail.Text = student.MarkDetail.Replace("\n", Environment.NewLine);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMarkDetail_Load(object sender, EventArgs e)
        {
            initForm();
        }
    }
}
