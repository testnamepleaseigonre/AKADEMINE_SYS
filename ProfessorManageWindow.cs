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
    public partial class ProfessorManageWindow : Form
    {
        ItemsRepository repository = new ItemsRepository();
        UsersRepository uRep = new UsersRepository();

        public ProfessorManageWindow()
        {
            InitializeComponent();
        }

        private void ProfessorManageWindow_Load(object sender, EventArgs e)
        {
            foreach (User user in repository.getProfessors())
            {
                dataGridView1.Rows.Add(user.getID(), user.getName(), user.getSurname(), user.getUsername());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProfessorForm adp = new AddProfessorForm();
            adp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                try
                {
                    string id = dataGridView1.Rows[item.Index].Cells["profID"].Value.ToString();
                    uRep.removeProfessor(id);
                    dataGridView1.Rows.RemoveAt(item.Index);
                    MessageBox.Show("Professor successfully deleted!");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
