using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAO
{
    public class DAOUser
    {
        public bool checkUser(string account, string pass)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string str = "select * from [user] where account='" + account + "' and pass='" + pass + "'";
            dt = data.executeQuery(str);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool changePassword(User user, string text)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string script = "update [User] set [pass] = '" + user.Pass + "' where account = '" + user.Account + "'";
            string str = "select * from [user] where account='" + user.Account + "' and pass = '" + text + "' ";
            dt = data.executeQuery(str);
            if (dt.Rows.Count > 0)
            {
                return data.executeNonQuery(script);
            }
            else
            {
                return false;
            }
        }

        public bool forgotPassword(User user)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string script = "update [User] set [pass] = '"+user.Pass+"' where account = '"+user.Account+"'";
            string str = "select * from [user] where account='" + user.Account + "' and question = '"+user.Question+"' and answer= '"+user.Answer+"'";
            dt = data.executeQuery(str);            
            if (dt.Rows.Count > 0)
            {
                return data.executeNonQuery(script);
            }
            else
            {
                return false;
            }
           
        }

        public bool insertUser(User user)
        {
            DataTable dt = new DataTable();
            DataProvider data = new DataProvider("PRN292_Project");
            data.connect();
            string script = "insert into [User] values ('" + user.Account + "','" + user.Pass + "','" + user.Name + "','" + user.Gender + "','" + user.Address + "','" + user.Dateofbirth + "','"+user.Question+"','"+user.Answer+"')";
            string str = "select * from [user] where account='" + user.Account + "'";
            Console.WriteLine(script);
            dt = data.executeQuery(str);
            if (user.Account.Equals("")&&user.Pass.Equals(""))
            {
                return false;
            }
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            return data.executeNonQuery(script);
        }
    }
}
