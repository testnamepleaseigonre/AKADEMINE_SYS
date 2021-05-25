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
using AKADEMINE_SYS.Backend.Models;

namespace AKADEMINE_SYS
{
    public partial class StudentWindow : Form
    {
        private ItemsRepository repository = new ItemsRepository();

        public StudentWindow()
        {
            InitializeComponent();
            label2.Text = User.LoggedInUser.getName() + " " + User.LoggedInUser.getSurname();
            try
            {
                label3.Text = repository.getGroupNameByUserID(User.LoggedInUser.getID());
                foreach (Subject subject in repository.getSubjectsInGroups(repository.getGroupsIDByName(label3.Text.ToString())))
                {
                    comboBox2.Items.Add(subject.name);
                }
            }
            catch
            {
                label3.Text = "Not in group!";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label7.Text = repository.getGrade(User.LoggedInUser.getID(), repository.getSubjectIDByName(comboBox2.SelectedItem.ToString()));
            }
            catch
            {
                label7.Text = "No grade yet.";
            }
            
        }
    }
}
