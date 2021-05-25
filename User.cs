using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKADEMINE_SYS
{
    public class User
    {
        public static User LoggedInUser = new User(" ", " ", " ", " ", " ", "anonymous");

        protected string id;
        protected string name;
        protected string surname;
        protected string username;
        protected string password;
        protected string userType;

        public User(string id, string name, string surname, string username, string password, string userType)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }

        public string getName()
        {
            return name;
        }

        public string getUsername()
        {
            return username;
        }

        public string getID()
        {
            return id;
        }

        public string getUserType()
        {
            return userType;
        }

        public string getSurname()
        {
            return surname;
        }

        public void setUserType(string userType)
        {
            this.userType = userType;
        }



    }
}
