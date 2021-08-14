using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class QuestionDetail
    {
        private string questionName;
        private string filePath;

        public QuestionDetail() { }
        public QuestionDetail(string questionName, string filePath)
        {
            this.questionName = questionName;
            this.filePath = filePath;
        }

       

        public string QuestionName { get => questionName; set => questionName = value; }
        public string FilePath { get => filePath; set => filePath = value; }
    }
}
