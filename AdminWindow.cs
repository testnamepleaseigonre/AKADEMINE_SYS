using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AKADEMINE_SYS
{
    public partial class AdminWindow : Form
    {
        public AdminWindow()
        {
            InitializeComponent();
            label2.Text = User.LoggedInUser.getName() + " " + User.LoggedInUser.getSurname();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroupsManageWindow sd = new GroupsManageWindow();
            sd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubjectManageWindow sd = new SubjectManageWindow();
            sd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfessorManageWindow sd = new ProfessorManageWindow();
            sd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StudentManageWindow sd = new StudentManageWindow();
            sd.ShowDialog();
        }
    }
}
