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
    public partial class AddStudentForm : Form
    {
        UsersRepository repository = new UsersRepository();

        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                repository.addStudent(textBox1.Text, textBox2.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            this.Close();
        }
    }
}
