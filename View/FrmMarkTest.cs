using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Controller;
using Model;

namespace View
{
    public partial class FrmMarkTest : Form, IFrmMarkTest
    {
        public FrmMarkTest()
        {
            InitializeComponent();
        }
        public FrmMarkTest(string tilte)
        {
            InitializeComponent();
            this.Text = tilte;
        }
        private int num;



        public FrmMarkTest(int num)
        {
            InitializeComponent();
            this.Num = num;

        }

        Dictionary<string, Student> dicStudent = new Dictionary<string, Student>();
        Dictionary<string, QuestionDetail> dicQuestion = new Dictionary<string, QuestionDetail>();

        public string DbName { get => cbxDataName.Text; set => cbxDataName.Text = value; }
        public int Num { get => num; set => num = value; }

        private void btnInputStudent_Click(object sender, EventArgs e)
        {
            new MarkTestController(this, dicStudent).inputStudent();
        }

        private void btnInputQuestion_Click(object sender, EventArgs e)
        {
            FrmChooseQuestion f = new FrmChooseQuestion(this,dicQuestion);
            //this.Hide();
            f.ShowDialog();
            
            //Console.WriteLine(Num+"");
            //if (Num != 0)
            //{
            //    new MarkTestController(this, dicQuestion, Num).inputQuestion();
            //}                      
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            new MarkTestController(this, dicQuestion, dicStudent).markTest();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            new MarkTestController(this, dicQuestion, dicStudent).resetForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        public void clearScoreColumn()
        {
            foreach (DataGridViewRow item in dgView.Rows)
            {
                item.Cells["Score"].Value = null;
            }
        }
        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgView.CurrentCell == null || dgView.CurrentCell.Value == null || e.RowIndex == -1) return;
            if (dgView.CurrentCell.ColumnIndex.Equals(0))
            {
                if (dicQuestion.Count > 0)
                {
                    string studentName = dgView.CurrentCell.Value.ToString();
                    Student st = new MarkTestController(this, dicStudent).showStudentDetail(studentName);
                    FrmMarkDetail f = new FrmMarkDetail(st);
                    f.ShowDialog();
                }


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmSave frm = new FrmSave(dicStudent);
            frm.ShowDialog();
        }


        public string getFolderPath()
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                return folderBrowserDialog1.SelectedPath;
            }
            return "";
        }



        public void addStudentToGrid(string studentName, bool isCorrect, int i)
        {
            dgView.Rows.Add(studentName);
            if (!isCorrect)
            {
                dgView.Rows[i].Cells["Score"].Value = 0 + "";
                dgView.Rows[i].Cells["Note"].Value = "Wrong folder name";
            }
        }

        public void clearRowGrid()
        {
            dgView.Rows.Clear();
        }

        public void addQuestionToGrid(string questionName, int i)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = questionName;
            dgView.Columns.Insert(i, column);
        }

        public void clearColumnGrid(int totalColumn)
        {
            for (int i = 1; i <= totalColumn; i++)
            {
                dgView.Columns.Remove("Q" + i);
            }
        }

        public void showMessageBox(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void setBtnReset(bool isEnabled)
        {
            if (isEnabled)
            {
                btnReset.Enabled = true;
            }
            else
            {
                btnReset.Enabled = false;
            }
        }

        public void clearDbName()
        {
            cbxDataName.Text = "";
        }

        public string getDbName()
        {
            return cbxDataName.Text.Trim();
        }

        public void setDataGridValue(int i, string columnName, string value)
        {
            dgView.Rows[i].Cells[columnName].Value = value;
        }

        public void setBtnSave(bool isEnabled)
        {
            btnSave.Enabled = isEnabled;
        }

        public void setBtnMark(bool isEnabled)
        {
            btnMark.Enabled = isEnabled;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            String[] s = this.Text.Split(' ');
            FrmMain f = new FrmMain(s[1]);
            f.Show();
            this.Hide();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];
            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Records";
            try
            {
                for (int i = 1; i < dgView.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgView.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgView.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgView.Columns.Count; j++)
                    {
                        if (dgView.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgView.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = "";
                        }
                    }
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;
                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void setBtnExport(bool isEnabled)
        {
            btnExport.Enabled = isEnabled;
        }
    }
}

