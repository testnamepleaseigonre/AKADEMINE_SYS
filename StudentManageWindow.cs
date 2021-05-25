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
    public partial class StudentManageWindow : Form
    {
        ItemsRepository repository = new ItemsRepository();
        UsersRepository uRep = new UsersRepository();

        public StudentManageWindow()
        {
            InitializeComponent();
        }

        private void StudentManageWindow_Load(object sender, EventArgs e)
        {
            foreach (User user in repository.getStudents())
            {
                dataGridView1.Rows.Add(user.getID(), user.getName(), user.getSurname(), user.getUsername());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudentForm asd = new AddStudentForm();
            asd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                try
                {
                    string id = dataGridView1.Rows[item.Index].Cells["studID"].Value.ToString();
                    uRep.removeStudent(id);
                    dataGridView1.Rows.RemoveAt(item.Index);
                    MessageBox.Show("Student successfully deleted!");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
