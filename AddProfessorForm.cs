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
    public partial class AddProfessorForm : Form
    {
        UsersRepository repository = new UsersRepository();

        public AddProfessorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                repository.addProfessor(textBox1.Text, textBox2.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            this.Close();
        }
    }
}
