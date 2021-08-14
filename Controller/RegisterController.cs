using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public class RegisterController
    {
        public bool insertUser(User user)
        {
            return (new DAOUser()).insertUser(user);
        }

        public bool forgotPassword(User user)
        {
            return (new DAOUser()).forgotPassword(user);
        }

        public bool changePassword(User user, string text)
        {
            return (new DAOUser()).changePassword(user,text);
        }
    }
}
