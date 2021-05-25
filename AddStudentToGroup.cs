using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AKADEMINE_SYS.Backend.Models;
using AKADEMINE_SYS.Backend.Repositories;

namespace AKADEMINE_SYS
{
    public partial class AddStudentToGroup : Form
    {
        private string groupID;
        ItemsRepository repository = new ItemsRepository();

        public AddStudentToGroup()
        {
            InitializeComponent();
        }

        public AddStudentToGroup(string groupID)
        {
            InitializeComponent();
            this.groupID = groupID;
            foreach (User user in repository.getNotAddedStudents(groupID))
            {
                comboBox1.Items.Add(user.getName() + " " + user.getSurname());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                try
                {
                    repository.addStudentToGroup(groupID, comboBox1.SelectedItem.ToString());
                    DialogResult result = MessageBox.Show($"Student successfully added!", "", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show($"Please pick student you want to add!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
