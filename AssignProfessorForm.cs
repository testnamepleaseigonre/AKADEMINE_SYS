using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AKADEMINE_SYS.Backend.Repositories;

namespace AKADEMINE_SYS
{
    public partial class AssignProfessorForm : Form
    {
        ItemsRepository repository = new ItemsRepository();
        private List<User> usersList;
        private string subjectID;

        public AssignProfessorForm()
        {
            InitializeComponent();
        }

        public AssignProfessorForm(string id)
        {
            InitializeComponent();
            subjectID = id;
            usersList = repository.getProfessors();
            foreach (User user in usersList)
            {
                comboBox1.Items.Add(user.getName() + " " + user.getSurname());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string profID = null;
                foreach (User user in usersList)
                {
                    if ((user.getName() + " " + user.getSurname()) == comboBox1.Text)
                    {
                        profID = user.getID();
                    }
                }
                repository.assignToSubject(subjectID, profID);
                MessageBox.Show("Professor successfully assiged to the subject!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            this.Close();
        }
    }
}
