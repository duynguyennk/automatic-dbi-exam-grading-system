using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        private string account;
        private string pass;
        private string name;
        private bool gender;
        private string address;
        private DateTime dateofbirth;
        private string question;

        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }



        public User() { }
        public string Account { get => account; set => account = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Name { get => name; set => name = value; }
        public bool Gender { get => gender; set => gender = value; }        
        public string Address { get => address; set => address = value; }
        public DateTime Dateofbirth { get => dateofbirth; set => dateofbirth = value; }

    }
}
