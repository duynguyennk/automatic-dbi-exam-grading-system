using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class LoginController
    {
        public bool checkUser(string account, string pass)
        {
            return (new DAOUser()).checkUser(account, pass);
        }
    }
}
