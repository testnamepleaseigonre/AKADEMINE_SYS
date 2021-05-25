using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AKADEMINE_SYS.UsersData;

namespace AKADEMINE_SYS
{
    public partial class LoginWindow : Form
    {
        private UsersRepository repository = new UsersRepository();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                User.LoggedInUser = repository.Login(usernameTxtBox.Text, passwordTxtBox.Text);
                //this.Close();
                if (User.LoggedInUser.getUserType() == "admin")
                {
                    AdminWindow uw = new AdminWindow();
                    uw.ShowDialog();
                }
                else if(User.LoggedInUser.getUserType() == "proff")
                {
                    ProfessorWindow prof = new ProfessorWindow();
                    prof.ShowDialog();
                }
                else if (User.LoggedInUser.getUserType() == "sdent")
                {
                    StudentWindow std = new StudentWindow();
                    std.ShowDialog();
                }
                else
                {

                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
