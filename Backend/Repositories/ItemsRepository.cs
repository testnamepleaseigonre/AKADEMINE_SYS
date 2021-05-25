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
    public class ItemsRepository
    {
        private SqlConnection conn;

        public ItemsRepository()
        {
            conn = new SqlConnection(@"Server=.;Database=AkademineSys;User Id=admin;Password=admin");
        }

        public List<Group> getGroups()
        {
            List<Group> groupsList = new List<Group>();
            try
            {
                string sql = "select id, title from groups";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string title = reader["title"].ToString();
                    groupsList.Add(new Group(id, title));
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }

            return groupsList;
        }

        public void addGroup(string name)
        {
            try
            {
                string sql = "insert into groups (title) values (@title)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", name);
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

        public List<Subject> getSubjects()
        {
            List<Subject> subjectList = new List<Subject>();
            try
            {
                string sql = "select id, name, professorID from subjects";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string profID = reader["professorID"].ToString();
                    subjectList.Add(new Subject(id, name, profID));

                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }

            return subjectList;
        }

        public List<User> getProfessors()
        {
            List<User> professorList = new List<User>();
            try
            {
                string sql = "select id, name, surname, username from users where userType=@userType";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userType", "proff");
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string surname = reader["surname"].ToString();
                    string username = reader["username"].ToString();
                    professorList.Add(new User(id.ToString(), name, surname, username, "", "proff"));
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }

            return professorList;
        }

        public List<User> getStudents()
        {
            List<User> studentsList = new List<User>();
            try
            {
                string sql = "select id, name, surname, username, userType from users where userType=@userType";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userType", "sdent");
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string surname = reader["surname"].ToString();
                    string username = reader["username"].ToString();
                    string userType = reader["userType"].ToString();
                    studentsList.Add(new User(id.ToString(), name, surname, username, "", userType));
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

        public List<User> getStudentsInSelectedGroup(string groupName)
        {
            string groupID = getGroupIDByName(groupName);
            List<string> sList = getStudentIDByGroupID(groupID);
            List<User> studentsList = new List<User>();
            foreach (String std in sList)
            {
                try
                {
                    string sql = "select id, name, surname, username, userType from users where id=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", std);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["name"].ToString();
                        string surname = reader["surname"].ToString();
                        string username = reader["username"].ToString();
                        string userType = reader["userType"].ToString();
                        studentsList.Add(new User(id.ToString(), name, surname, username, " ", userType));
                    }
                    conn.Close();
                }
                catch (Exception exc)
                {
                    conn.Close();
                    Debug.WriteLine(exc.Message);
                }
            }
            return studentsList;
        }

        private List<string> getStudentIDByGroupID(string groupID)
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
                    string id = reader["userID"].ToString();
                    sList.Add(id.ToString());
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

        private string getGroupIDByName(string groupName)
        {
            try
            {
                string sql = "select id from groups where title=@title";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", groupName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    conn.Close();
                    return id;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }
            throw new Exception("Error!");
        }

        public void addSubject(string name)
        {
            try
            {
                string sql = "insert into subjects (name) values (@name)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
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

        public void removeSubject(string id)
        {
            removeSubjectFrom_SubjectsInGroups(id);
            removeSubjectMarks(id);
            try
            {
                string sql = "delete from subjects where id=@id";
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

        private void removeSubjectMarks(string id)
        {
            try
            {
                string sql = "delete from grades where subjectID=@id";
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

        private void removeSubjectFrom_SubjectsInGroups(string id)
        {
            try
            {
                string sql = "delete from subjects_in_groups where subjectID=@id";
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

        public void assignToSubject(string subjectID, string profID)
        {
            try
            {
                string sql = "update subjects set professorID=@profID where id=@subID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@profID", profID);
                cmd.Parameters.AddWithValue("@subID", subjectID);
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

        public void removeGroup(string id)
        {
            try
            {
                removeGroupFromSubjects_in_groups(id);
                removeGroupFromUsers_in_groups(id);
                removeGroupGrades(id);
                string sql = "delete from groups where id=@id";
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

        private void removeGroupGrades(string id)
        {
            try
            {
                string sql = "delete from grades where groupID=@id";
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

        private void removeGroupFromUsers_in_groups(string id)
        {
            try
            {
                string sql = "delete from users_in_groups where groupID=@id";
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

        private void removeGroupFromSubjects_in_groups(string id)
        {
            try
            {
                string sql = "delete from subjects_in_groups where groupID=@id";
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

        public List<Subject> getSubjectsInGroups(string groupID)
        {
            List<string> sList = getSubjectsInGroups_1(groupID); ;
            List<Subject> subjectsList = new List<Subject>();
            try
            {
                foreach (String gID in sList)
                {
                    string sql = "select id, name, professorID from subjects where id=@subjectID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@subjectID", gID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string professorID = reader["professorID"].ToString();
                        subjectsList.Add(new Subject(int.Parse(id), name, professorID));
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

            return subjectsList;
        }

        private List<string> getSubjectsInGroups_1(string groupID)
        {
            List<string> sList = new List<string>();
            try
            {
                string sql = "select subjectID from subjects_in_groups where groupID=@groupID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", groupID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string uid = reader["subjectID"].ToString();
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

        public void removeStudentFromGroup(string groupID, string studID)
        {
            try
            {
                string sql = "delete from users_in_groups where (groupID=@id and userID=@uid)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", groupID);
                cmd.Parameters.AddWithValue("@uid", studID);
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

        public void removeSubjectFromGroup(string groupID, string subID)
        {
            try
            {
                string sql = "delete from subjects_in_groups where (groupID=@id and subjectID=@uid)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", groupID);
                cmd.Parameters.AddWithValue("@uid", subID);
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

        public List<Subject> getNotAddedSubjects(string groupID)
        {
            List<string> sList = getSubjectsInGroups_1(groupID);
            List<Subject> subjectsList = getSubjects();
            List<Subject> finalList = new List<Subject>();
            if (sList.Count != 0)
            {
                foreach (Subject subject in subjectsList)
                {
                    bool check = false;
                    foreach (String subID in sList)
                    {
                        if(subID.ToString() == subject.Id.ToString())
                        {
                            check = true;
                        }
                    }
                    if(check == false)
                    {
                        finalList.Add(subject);
                    }
                }
            }
            else
            {
                finalList = subjectsList;
            }
            return finalList;
        }

        public void addSubjectToGroup(string groupID, string subjectName)
        {
            try
            {
                string sql = "insert into subjects_in_groups (groupID, subjectID) values (@groupID, @subjectID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", groupID);
                cmd.Parameters.AddWithValue("@subjectID", getID_bySubjectName(subjectName));
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
        
        private string getID_bySubjectName(string subName)
        {
            try
            {
                string sql = "select id from subjects where name=@subname";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subname", subName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ID = reader["id"].ToString();
                    conn.Close();
                    return ID;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }
            conn.Close();
            throw new Exception("No such subject!");
        }

        public List<User> getNotAddedStudents(string groupID)
        {
            List<string> sList = getStudentsInGroups(groupID);
            List<User> studentsList = getStudents();
            List<User> finalList = new List<User>();
            if (sList.Count != 0)
            {
                foreach (User user in studentsList)
                {
                    bool check = false;
                    foreach (String subID in sList)
                    {
                        if (subID == user.getID())
                        {
                            check = true;
                        }
                    }
                    if (check == false)
                    {
                        finalList.Add(user);
                    }
                }
            }
            else
            {
                finalList = studentsList;
            }
            return finalList;
        }
        private List<string> getStudentsInGroups(string groupID)
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

        public void addStudentToGroup(string groupID, string studentName)
        {
            try
            {
                checkIfStudentInGroup(getStudentIDbyNameAndSurname(studentName));
                try
                {
                    string sql = "insert into users_in_groups (groupID, userID) values (@groupID, @userID)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@groupID", groupID);
                    cmd.Parameters.AddWithValue("@userID", getStudentIDbyNameAndSurname(studentName));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch
                {
                    conn.Close();
                    throw new Exception("Errorasdsad!");
                }
            }
            catch(Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        private void checkIfStudentInGroup(string studID)
        {
            try
            {
                string sql = "select groupID from users_in_groups where userID=@studID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studID", studID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ID = reader["groupID"].ToString();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception("Student is already in another group!");
            }
            conn.Close();
        }

        private string getStudentIDbyNameAndSurname(string name)
        {
            try
            {
                string sql = "select id from users where name+' '+surname=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ID = reader["id"].ToString();
                    conn.Close();
                    return ID;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }
            conn.Close();
            throw new Exception("No such user!");
        }

        public List<Subject> getSubjectsInSelectedGroup(string groupName, string professorID)
        {
            string groupID = getGroupIDByName(groupName);
            List<string> sList = new List<string>();
            List<Subject> subjectsList = new List<Subject>();
            foreach (String std in getSubjectsByGroupID(groupID))
            {
                if(checkIfProffRuns(std, professorID) == true)
                {
                    sList.Add(std);
                }
            }
            foreach(String std in sList)
            {
                try
                {
                    string sql = "select id, name, professorID from subjects where id=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", std);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["id"].ToString());
                        string name = reader["name"].ToString();
                        string profID = reader["professorID"].ToString();
                        subjectsList.Add(new Subject(id, name, profID));
                    }
                    conn.Close();
                }
                catch (Exception exc)
                {
                    conn.Close();
                    Debug.WriteLine(exc.Message);
                }
            }
            return subjectsList;
        }

        private bool checkIfProffRuns(string subID, string profID)
        {
            bool check = false;
            try
            {
                string sql = "select professorID from subjects where id=@subID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subID", subID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["professorID"].ToString();
                    if(id == profID)
                    {
                        check = true;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                Debug.WriteLine(exc.Message);
            }
            return check;
        }

        private List<string> getSubjectsByGroupID(string groupID)
        {
            List<string> sList = new List<string>();
            try
            {
                string sql = "select subjectID from subjects_in_groups where groupID=@groupID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", groupID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["subjectID"].ToString();
                    sList.Add(id.ToString());
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

        public bool  markIsTrue(string groupName, string subjectName, string studentName)
        {
            try
            {
                string sql = "select grade from grades where (groupID=@groupID and subjectID=@subjectID and studentID=@studentID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", getGroupIDByName(groupName));
                cmd.Parameters.AddWithValue("@subjectID", getID_bySubjectName(subjectName));
                cmd.Parameters.AddWithValue("@studentID", getStudentIDbyNameAndSurname(studentName));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string mark = reader["grade"].ToString();
                    conn.Close();
                    return true;
                }
            }
            catch
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return false;
        }

        public string getMarkInfo(string groupName, string subjectName, string studentName)
        {
            try
            {
                string sql = "select grade from grades where (groupID=@groupID and subjectID=@subjectID and studentID=@studentID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupID", getGroupIDByName(groupName));
                cmd.Parameters.AddWithValue("@subjectID", getID_bySubjectName(subjectName));
                cmd.Parameters.AddWithValue("@studentID", getStudentIDbyNameAndSurname(studentName));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string mark = reader["grade"].ToString();
                    conn.Close();
                    return mark;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            conn.Close();
            throw new Exception("Error!");
        }

        public void changeMark(string number, string groupName, string subjectName, string studentName)
        {
            try
            {
                string sql = "update grades set grade=@grade where (groupID=@groupID and subjectID=@subjectID and studentID=@studentID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@grade", number);
                cmd.Parameters.AddWithValue("@groupID", getGroupIDByName(groupName));
                cmd.Parameters.AddWithValue("@subjectID", getID_bySubjectName(subjectName));
                cmd.Parameters.AddWithValue("@studentID", getStudentIDbyNameAndSurname(studentName));
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

        public void addNewMark(string number, string groupName, string subjectName, string studentName)
        {
            try
            {
                string sql = "insert into grades (date, groupID, subjectID, studentID, grade) values (@date, @groupID, @subjectID, @studentID, @grade)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@groupID", getGroupIDByName(groupName));
                cmd.Parameters.AddWithValue("@subjectID", getID_bySubjectName(subjectName));
                cmd.Parameters.AddWithValue("@studentID", getStudentIDbyNameAndSurname(studentName));
                cmd.Parameters.AddWithValue("@grade", number);
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

        public string getGroupNameByUserID(string userID)
        {
            try
            {
                string sql = "select title from groups where id=(select groupID from users_in_groups where userID=@id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["title"].ToString();
                    conn.Close();
                    return id;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                return "Not in group";
            }
            conn.Close();
            throw new Exception("Error!");
        }
        public string getGroupsIDByName(string subName)
        {
            try
            {
                string sql = "select id from groups where title=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", subName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    conn.Close();
                    return id;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            conn.Close();
            throw new Exception("Error!");
        }

        public string getSubjectIDByName(string subName)
        {
            try
            {
                string sql = "select id from subjects where name=@name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", subName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["id"].ToString();
                    conn.Close();
                    return id;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            conn.Close();
            throw new Exception("Error!");
        }

        public string getGrade(string studID, string subID)
        {
            try
            {
                string sql = "select grade from grades where (subjectID=@subID and studentID=@studID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subID", subID);
                cmd.Parameters.AddWithValue("@studID", studID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader["grade"].ToString();
                    conn.Close();
                    return id;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            conn.Close();
            throw new Exception("Error!");
        }
    }
}
