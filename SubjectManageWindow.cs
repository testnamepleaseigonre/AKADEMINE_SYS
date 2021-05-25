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
    public partial class SubjectManageWindow : Form
    {
        ItemsRepository repository = new ItemsRepository();

        public SubjectManageWindow()
        {
            InitializeComponent();
            
        }

        private void SubjectManageWindow_Load(object sender, EventArgs e)
        {
            foreach (Subject subject in repository.getSubjects())
            {
               
                if(subject.proffesorID.ToString() == "")
                {
                    dataGridView1.Rows.Add(subject.Id, subject.name, "Not assigned!");
                }
                else
                {
                    dataGridView1.Rows.Add(subject.Id, subject.name, subject.proffesorID);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSubjectForm ads = new AddSubjectForm();
            ads.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                try
                {
                    string id = dataGridView1.Rows[item.Index].Cells["subjectID"].Value.ToString();
                    repository.removeSubject(id);
                    dataGridView1.Rows.RemoveAt(item.Index);
                    MessageBox.Show("Subject successfully removed!");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                try
                {
                    string id = dataGridView1.Rows[item.Index].Cells["subjectID"].Value.ToString();
                    AssignProfessorForm apf = new AssignProfessorForm(id);
                    apf.ShowDialog();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
