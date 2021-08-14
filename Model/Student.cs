using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Student
    {
        private string studentName;
        private string markDetail;
        private double totalScore;
        private Dictionary<string,QuestionDetail> listQuestion;

        public Student() { }
        public Student(string studentName, Dictionary<string,QuestionDetail> listQuestion)
        {
            this.studentName = studentName;
            this.listQuestion = listQuestion;
        }

        public string StudentName { get => studentName; set => studentName = value; }
        public string MarkDetail { get => markDetail; set => markDetail = value; }
        public double TotalScore { get => totalScore; set => totalScore = value; }
        public Dictionary<string, QuestionDetail> ListQuestion { get => listQuestion; set => listQuestion = value; }
    }
}
