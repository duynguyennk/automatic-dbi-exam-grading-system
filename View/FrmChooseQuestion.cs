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
    public partial class FrmChooseQuestion : Form
    {
        private FrmMarkTest frmMarkTest;
        private Dictionary<string, QuestionDetail> dicQuestion;

        public FrmChooseQuestion()
        {
            InitializeComponent();
        }

        public FrmChooseQuestion(FrmMarkTest frmMarkTest, Dictionary<string, QuestionDetail> dicQuestion)
        {
            InitializeComponent();
            this.frmMarkTest = frmMarkTest;
            this.dicQuestion = dicQuestion;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(Math.Round(numQuestion.Value, 0)) > 0))
            {
                //FrmMarkTest f = new FrmMarkTest(Convert.ToInt32(Math.Round(numQuestion.Value, 0)));
                MarkTestController markTestController = new MarkTestController(frmMarkTest,dicQuestion,Convert.ToInt32(Math.Round(numQuestion.Value, 0)));
                markTestController.inputQuestion();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Number Q must be greater than 0");
                return;
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
