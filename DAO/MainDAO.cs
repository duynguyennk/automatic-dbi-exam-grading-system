using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO
{
    public class MainDAO
    {
        public DataTable getAllTestName()
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string sql = "select * from Test";
            dt = data.executeQuery(sql);
            return dt;
        }

        public DataTable getAllResultByID(string testID)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string sql = "select studentID, TotalScore from Student where TestID = '"+testID+"'";
            dt = data.executeQuery(sql);
            return dt;
        }

        public DataTable getResultByStudentID(string studentID, string testID)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string sql = "select StudentID, TotalScore from Student where StudentID like '%" + studentID + "%' and TestID="+testID;
            dt = data.executeQuery(sql);
            return dt;
        }

        public Student getStudentByID(string studentName, string testID)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string sql = "select studentID, TotalScore ,MarkDetail from Student where TestID = '" + testID + "' and StudentID = '" + studentName + "'";
            Student st = new Student();
            dt = data.executeQuery(sql);
            st.StudentName = dt.Rows[0][0].ToString();
            st.TotalScore = Convert.ToDouble(dt.Rows[0][1].ToString());
            st.MarkDetail = dt.Rows[0][2].ToString();
            return st;
        }
    }
}
