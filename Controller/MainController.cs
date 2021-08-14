using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Controller
{
    public class MainController
    {
        public DataTable getAllTestName()
        {
            return new MainDAO().getAllTestName();
        }

        public DataTable getAllResultByID(string testID)
        {
            return new MainDAO().getAllResultByID(testID);
        }

        public Student getStudentByID(string studentName, string testID)
        {
            return new MainDAO().getStudentByID(studentName,testID);
        }

        public DataTable getResultByStudentID(string studentID, string testID)
        {
            return new MainDAO().getResultByStudentID(studentID,testID);
        }
    }
}
