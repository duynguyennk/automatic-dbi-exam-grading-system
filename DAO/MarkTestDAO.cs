using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Model;

namespace DAO
{
    public class MarkTestDAO
    {
        public bool connectDB(string dbName)
        {
            DataProvider data = new DataProvider(dbName);
            try
            {
                data.connect();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public DataTable getTableResult(string script, string dbName)
        {
            DataProvider data = new DataProvider(dbName);
            data.connect();
            return data.executeQuery(script);
        }

        public bool insertTest(string testName)
        {
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            
            string script = "insert into [Test] values('" + testName + "')";
            if (!checkTestName(testName))
            {
                return false;
            }
            return data.executeNonQuery(script);
        }
        public bool checkTestName(string testName)
        {
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string script = "select * from Test where TestName='" + testName + "'";
            if (data.executeQuery(script).Rows.Count>0)
            {
                return false;
            }
            return true;
        }

        public string getTestID(string testName)
        {
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            return data.executeQuery("select TestID from Test where TestName='" + testName + "'").Rows[0][0].ToString();
        }

        public void insertStudentScore(Student st, string testId)
        {
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string script = "insert into [Student] values ('" + st.StudentName + "','" + st.TotalScore + "','" + st.MarkDetail + "'," + testId + ")";
            data.executeNonQuery(script);
        }
    }
}
