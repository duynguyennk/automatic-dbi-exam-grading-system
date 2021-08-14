using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Model;
using Controller;
namespace View
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FrmMarkTest frmMarkTest = new FrmMarkTest();
            //FrmMarkDetail frmMarkDetail = new FrmMarkDetail();
            //Dictionary<string, Student> dicStudent = new Dictionary<string, Student>();
            //Dictionary<string, QuestionDetail> dicStudentAnswer = new Dictionary<string, QuestionDetail>();
            //Dictionary<string, QuestionDetail> dicQuestion = new Dictionary<string, QuestionDetail>();
            //MarkTestController markTestController = new MarkTestController(frmMarkTest,dicStudent, dicStudentAnswer, dicQuestion);
            //frmMarkTest.ShowDialog();
            Application.Run(new FrmLogin());

        }
    }
}
