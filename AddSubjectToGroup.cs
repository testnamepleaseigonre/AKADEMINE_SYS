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
    public partial class AddSubjectToGroup : Form
    {
        private string groupID;
        ItemsRepository repository = new ItemsRepository();

        public AddSubjectToGroup()
        {
            InitializeComponent();
        }

        public AddSubjectToGroup(string groupID)
        {
            InitializeComponent();
            this.groupID = groupID;
            foreach(Subject subject in repository.getNotAddedSubjects(groupID))
            {
                comboBox1.Items.Add(subject.name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null)
            {
                try
                {
                    repository.addSubjectToGroup(groupID, comboBox1.SelectedItem.ToString());
                    DialogResult result = MessageBox.Show($"Subject successfully added!", "", MessageBoxButtons.OK);
                    this.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show($"Please pick subject you want to add!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
