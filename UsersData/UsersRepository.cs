using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AKADEMINE_SYS.UsersData
{
    class UsersRepository
    {
        private static List<User> usersList;
        private SqlConnection conn = new SqlConnection(@"Server=.;Database=AkademineSys;User Id=admin;Password=admin");

        public UsersRepository()
        {
            if (usersList == null)
            {
                usersList = new List<User>();
            }
        }

        public User Login(string username, string password)
        {
            try
            {
                string sql = "select id, name, surname, username, userType from users where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        string id = dataReader["id"].ToString();
                        string name = dataReader["name"].ToString();
                        string surname = dataReader["surname"].ToString();
                        string uname = dataReader["username"].ToString();
                        string userType = dataReader["userType"].ToString();
                        conn.Close();
                        return new User(id, name, surname, username, "", userType);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            conn.Close();
            throw new Exception("Bad Credentials!");
        }
    }
}
