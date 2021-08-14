using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Model;
using DAO;
namespace Controller
{
    public class MarkTestController
    {
        MarkTestDAO markTestDAO = new MarkTestDAO();
        IFrmMarkTest frmMarkTest;
        IFrmSave frmSave;
        Dictionary<string, Student> dicStudent;
        Dictionary<string, QuestionDetail> dicStudentAnswer;
        Dictionary<string, QuestionDetail> dicQuestion;
        private int numberQuestion;

      
        public MarkTestController(IFrmMarkTest frmMarkTest, Dictionary<string, Student> dicStudent)
        {
            this.frmMarkTest = frmMarkTest;
            this.dicStudent = dicStudent;
        }

        public MarkTestController(IFrmMarkTest frmMarkTest, Dictionary<string, QuestionDetail> dicQuestion, int question)
        {
            this.frmMarkTest = frmMarkTest;
            this.dicQuestion = dicQuestion;
            numberQuestion = question;
        }
        public MarkTestController(IFrmMarkTest frmMarkTest, Dictionary<string, QuestionDetail> dicQuestion, Dictionary<string, Student> dicStudent)
        {
            this.frmMarkTest = frmMarkTest;
            this.dicQuestion = dicQuestion;
            this.dicStudent = dicStudent;
        }



        public MarkTestController(IFrmSave frmSave, Dictionary<string, Student> dicStudent)
        {
            this.frmSave = frmSave;
            this.dicStudent = dicStudent;
        }


        public Student showStudentDetail(string studentName)
        {
            return dicStudent[studentName];
        }

        public void inputStudent()
        {
            if (dicStudent.Count > 0)
            {
                dicStudent.Clear();
                frmMarkTest.clearRowGrid();
            }

            string folderStudentPath = frmMarkTest.getFolderPath();
            if (folderStudentPath.Equals(""))
            {
                return;
            }
            DirectoryInfo d = new DirectoryInfo(folderStudentPath);
            if (!d.Name.Equals("Student"))
            {
                frmMarkTest.showMessageBox("Must be Student folder!");
                return;
            }
            DirectoryInfo[] diArr = getDirectories(folderStudentPath);

            int i = 0;
            foreach (DirectoryInfo dri in diArr)
            {
                string studentName = dri.Name;
                if (!isCorectFileName(dri.Name))
                {
                    frmMarkTest.addStudentToGrid(studentName, false, i);
                }
                else
                {
                    frmMarkTest.addStudentToGrid(studentName, true, i);
                }
                FileInfo[] file = dri.GetFiles();
                dicStudentAnswer = new Dictionary<string, QuestionDetail>();
                foreach (FileInfo item in file)
                {
                    string studentPath = item.FullName;
                    if (!isCorectQuestionName(Path.GetFileNameWithoutExtension(item.Name)))
                    {
                        continue;
                    }
                    string questionName = Path.GetFileNameWithoutExtension(item.Name);
                    dicStudentAnswer.Add(questionName, new QuestionDetail(questionName, studentPath));
                }
                dicStudent.Add(studentName, new Student(studentName, dicStudentAnswer));
                i++;
            }
            frmMarkTest.setBtnReset(true);
            frmMarkTest.setBtnMark(true);
        }

        public void inputQuestion()
        {
            if (dicQuestion.Count > 0)
            {
                frmMarkTest.clearColumnGrid(dicQuestion.Count);
                dicQuestion.Clear();
                frmMarkTest.clearScoreColumn();
            }
            string folderQuestionPath = frmMarkTest.getFolderPath();
            if (folderQuestionPath.Equals(""))
            {
                return;
            }
            DirectoryInfo d = new DirectoryInfo(folderQuestionPath);
            if (!d.Name.Equals("Question"))
            {
                frmMarkTest.showMessageBox("Must be Question folder!");
                return;
            }
            DirectoryInfo[] diArr = getDirectories(folderQuestionPath);
            if (!numberQuestion.Equals(diArr.Length))
            {
                frmMarkTest.showMessageBox("Question folder must has " + numberQuestion + " Q !");
                return;
            }
            int i = 1;
            bool isValidName = true;
            foreach (DirectoryInfo item in diArr)
            {
                string questionName = item.Name;
                if (!isCorectQuestionName(questionName))
                {
                    frmMarkTest.showMessageBox("'" + questionName + "' is not a correct folder format. Please check again.");
                    isValidName = false;
                    break;
                }
            }
            if (isValidName)
            {
               
                foreach (DirectoryInfo dri in diArr)
                {
                    string questionName = dri.Name;

                    frmMarkTest.addQuestionToGrid(questionName, i);
                    FileInfo[] file = dri.GetFiles();
                    foreach (FileInfo item in file)
                    {
                        string questionPath = item.FullName;
                        dicQuestion.Add(questionName, new QuestionDetail(questionName, questionPath));
                    }
                    i++;
                }

                frmMarkTest.setBtnReset(true);
                frmMarkTest.setBtnReset(true);
                frmMarkTest.setBtnMark(true);
            }

        }

        public Student findStudent(string studentName)
        {
            return dicStudent[studentName];
        }

        public void markTest()
        {
            DataTable dt1;
            DataTable dt2;
            string dbName = frmMarkTest.DbName;
            if (dicStudent.Count == 0)
            {
                frmMarkTest.showMessageBox("Please input students!");
                return;
            }
            if (dicQuestion.Count == 0)
            {
                frmMarkTest.showMessageBox("Please input questions!");
                return;
            }
            if (dbName.Equals("") || !markTestDAO.connectDB(dbName))
            {
                frmMarkTest.showMessageBox("Data Base error!");
                return;
            }
            fillEmptyAnswer();
            int i = 0;
            foreach (var student in dicStudent)
            {
                string markDetail = "";
                double score = 0;
                foreach (var item in student.Value.ListQuestion)
                {
                    markDetail += "[" + item.Value.QuestionName + "]:\n";
                    try
                    {
                        if (item.Value.FilePath.Equals(""))
                        {
                            frmMarkTest.setDataGridValue(i, item.Value.QuestionName, "Empty Answer");
                            markDetail += "Empty Answer\nMark=0\n";
                            continue;
                        }
                        string studentScript = File.ReadAllText(item.Value.FilePath);
                        if (!dicQuestion.ContainsKey(item.Key))
                        {
                            continue;
                        }
                        string questionScript = File.ReadAllText(dicQuestion[item.Key].FilePath);
                        dt1 = markTestDAO.getTableResult(studentScript, dbName);
                        dt2 = markTestDAO.getTableResult(questionScript, dbName);

                        if (AreTablesTheSame(dt1, dt2))
                        {

                            frmMarkTest.setDataGridValue(i, item.Value.QuestionName, "Correct");
                            score++;
                            markDetail += "Passed\nMark=" + dicQuestion.Count / 10 + "\n";

                        }
                        else
                        {
                            frmMarkTest.setDataGridValue(i, item.Value.QuestionName, "False");
                            markDetail += "Wrong data table\nMark=0\n";
                        }
                    }
                    catch (Exception)
                    {
                        frmMarkTest.setDataGridValue(i, item.Value.QuestionName, "Error");
                        markDetail += "Error. Can not execute script\nMark=0\n";
                    }
                }
                student.Value.MarkDetail = markDetail;
                if (!isCorectFileName(student.Value.StudentName + ""))
                {
                    score = 0;
                }
                student.Value.TotalScore = Math.Round((score / dicQuestion.Count) * 10, 1);
                frmMarkTest.setDataGridValue(i, "Score", Math.Round((score / dicQuestion.Count) * 10, 1) + "");
                i++;

            }
            frmMarkTest.setBtnSave(true);
            frmMarkTest.setBtnExport(true);
            frmMarkTest.setBtnMark(false);
        }
        public bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                for (int j = 0; j < tbl1.Columns.Count; j++)
                {
                    if (!Equals(tbl1.Rows[i][j], tbl2.Rows[i][j]))
                        return false;
                }
            }
            return true;
        }
        public void fillEmptyAnswer()
        {
            foreach (var question in dicQuestion)
            {
                foreach (var student in dicStudent)
                {
                    if (!student.Value.ListQuestion.ContainsKey(question.Key))
                    {
                        student.Value.ListQuestion.Add(question.Key, new QuestionDetail(question.Key, ""));
                    }
                }
            }
        }

        public void resetForm()
        {
            frmMarkTest.clearRowGrid();
            frmMarkTest.clearColumnGrid(dicQuestion.Count);
            frmMarkTest.clearDbName();
            dicQuestion.Clear();
            dicStudent.Clear();
            frmMarkTest.setBtnReset(false);
            frmMarkTest.setBtnSave(false);
            frmMarkTest.setBtnExport(false);
            frmMarkTest.setBtnMark(true);
        }


        public bool isCorectFileName(string fileName)
        {
            string pattern = "^[a-z]{1,}\\d{5,6}$";
            bool result = Regex.IsMatch(fileName, pattern);
            return result;
        }
        public bool isCorectQuestionName(string fileName)
        {
            string pattern = "[Q][1-9][0-9]*$";
            bool result = Regex.IsMatch(fileName, pattern);
            return result;
        }

        public DirectoryInfo[] getDirectories(string selectedPath)
        {
            DirectoryInfo di = new DirectoryInfo(selectedPath);
            return di.GetDirectories();
        }

        public void saveTest()
        {
            string testName = frmSave.TestName;
            if (testName.Equals(""))
            {
                frmSave.showMessageBox("Please input again Test Name it's empty!");
                return;
            }
            if (markTestDAO.insertTest(testName))
            {
                string testId = markTestDAO.getTestID(testName);
                foreach (var item in dicStudent)
                {
                    markTestDAO.insertStudentScore(item.Value, testId);
                }
                frmSave.showMessageBox("Save successfully.");
            }
            else
            {
                frmSave.showMessageBox("Please input again Test Name it's already exist!");
            }
        }
    }
}
