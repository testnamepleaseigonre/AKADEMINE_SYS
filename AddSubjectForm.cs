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
    public partial class AddSubjectForm : Form
    {
        ItemsRepository repository = new ItemsRepository();

        public AddSubjectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                repository.addSubject(textBox1.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            this.Close();
        }
    }
}
