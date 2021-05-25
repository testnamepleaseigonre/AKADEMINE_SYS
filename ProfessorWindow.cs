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
    public partial class ProfessorWindow : Form
    {
        ItemsRepository repository = new ItemsRepository();

        public ProfessorWindow()
        {
            InitializeComponent();
            label2.Text = User.LoggedInUser.getName() + " " + User.LoggedInUser.getSurname();
            foreach(Group group in repository.getGroups())
            {
                comboBox1.Items.Add(group.Title);
            }
            markLabel.Visible = false;
            label6.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
        }

        private void index_change(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            foreach(User user in repository.getStudentsInSelectedGroup(comboBox1.SelectedItem.ToString()))
            {
                comboBox3.Items.Add(user.getName() + " " + user.getSurname());
            }
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            foreach (Subject subject in repository.getSubjectsInSelectedGroup(comboBox1.SelectedItem.ToString(), User.LoggedInUser.getID()))
            {
                comboBox2.Items.Add(subject.name);
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                DialogResult result = MessageBox.Show($"Please choose a group!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                DialogResult result = MessageBox.Show($"Please choose a subject!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(repository.markIsTrue(comboBox1.Text, comboBox2.Text, comboBox3.Text) == true)
            {
                markLabel.Visible = true;
                label6.Visible = true;
                markLabel.Text = repository.getMarkInfo(comboBox1.Text, comboBox2.Text, comboBox3.Text);
                button1.Visible = true;
                button2.Visible = false;
            }
            else
            {
                markLabel.Visible = false;
                label6.Visible = false;
                button1.Visible = false;
                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeMark chm = new ChangeMark(comboBox1.Text, comboBox2.Text, comboBox3.Text);
            chm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNewMark chm = new AddNewMark(comboBox1.Text, comboBox2.Text, comboBox3.Text);
            chm.ShowDialog();
        }
    }
}
