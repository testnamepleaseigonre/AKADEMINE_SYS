using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using AKADEMINE_SYS.Backend.Models;
using System.Diagnostics;

namespace AKADEMINE_SYS.Backend.Repositories
{
    class UsersRepository
    {
        private SqlConnection conn;

        public UsersRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=AkademineSys;User Id=admin;Password=admin");
        }

        public void addStudent(string name, string surname)
        {
            try
            {
                string sql = "insert into users (name, surname, username, password, userType) values (@name, @surname, @username, @password, @userType)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@username", name);
                cmd.Parameters.AddWithValue("@password", surname);
                cmd.Parameters.AddWithValue("@userType", "sdent");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                conn.Close();
                throw new Exception("Error!");
            }
        }

        public void removeStudent(string id)
        {
            try
            {
                removeStudentFromGroups(id);
                removeStudentMarks(id);
                string sql = "delete from users where id=@userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
        }

        private void removeStudentMarks(string id)
        {
            try
            {
                string sql = "delete from grades where studentID=@userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
        }

        private void removeStudentFromGroups(string id)
        {
            try
            {
                string sql = "delete from users_in_groups where userID=@userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
        }

        public void addProfessor(string name, string surname)
        {
            try
            {
                string sql = "insert into users (name, surname, username, password, userType) values (@name, @surname, @username, @password, @userType)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@surname", surname);
                cmd.Parameters.AddWithValue("@username", name);
                cmd.Parameters.AddWithValue("@password", surname);
                cmd.Parameters.AddWithValue("@userType", "proff");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                conn.Close();
                throw new Exception("Error!");
            }
        }

        public void removeProfessor(string id)
        {
            try
            {
                unassignFromSubject(id);
                string sql = "delete from users where id=@userid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
        }

        private void unassignFromSubject(string id)
        {
            try
            {
                string sql = "update subjects set professorID=NULL where professorID=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
        }

        public List<User> getStudentsInGroups(string groupID)
        {
            List<User> studentsList = new List<User>();
            List<string> sList = new List<string>();
            sList = getStudentsInGroups_1(groupID);
            try
            {
                foreach(String studID in sList)
                {
                    string sql = "select id, name, surname, username, userType from users where id=@userID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", studID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string surname = reader["surname"].ToString();
                        string username = reader["username"].ToString();
                        string userType = reader["userType"].ToString();
                        studentsList.Add(new User(id, name, surname, username, "", userType));
                    }
                    conn.Close();
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }

            return studentsList;
        }
        private List<string> getStudentsInGroups_1(string groupID)
        {
            List<string> sList = new List<string>();
            try
            {
                string sql = "select userID from users_in_groups where groupID=@groupID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", groupID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string uid = reader["userID"].ToString();
                    sList.Add(uid);
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }

            return sList;
        }
    }
}
