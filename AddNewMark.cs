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
    public partial class AddNewMark : Form
    {
        ItemsRepository repository = new ItemsRepository();

        private string groupName;
        private string subjectName;
        private string studentName;

        public AddNewMark()
        {
            InitializeComponent();
        }

        public AddNewMark(string groupName, string subjectName, string studentName)
        {
            InitializeComponent();
            this.groupName = groupName;
            this.subjectName = subjectName;
            this.studentName = studentName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int.Parse(textBox1.Text);
                }
                catch
                {
                    throw new Exception("Must be a number!");
                }
                int number = int.Parse(textBox1.Text);
                if (number < 0 || number > 10)
                {
                    MessageBox.Show("Number ( 0 - 10)!");
                }
                else
                {
                    repository.addNewMark(number.ToString(), groupName, subjectName, studentName);
                    MessageBox.Show("Mark sucessfully added!");
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
