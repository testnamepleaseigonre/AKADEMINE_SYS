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
    public partial class AddGroupForm : Form
    {
        ItemsRepository repository = new ItemsRepository();
        DataGridView dgv;
        public AddGroupForm(DataGridView dgv)
        {
            InitializeComponent();
            this.dgv = dgv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                repository.addGroup(textBox1.Text);
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
