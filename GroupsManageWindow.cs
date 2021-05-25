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
using AKADEMINE_SYS.Backend.Models;

namespace AKADEMINE_SYS
{
    public partial class GroupsManageWindow : Form
    {

        ItemsRepository repository = new ItemsRepository();
        UsersRepository uRes = new UsersRepository();

        public GroupsManageWindow()
        {
            InitializeComponent();
        }

        private void StudentGroupsWindow_Load(object sender, EventArgs e)
        {
            foreach (Group group in repository.getGroups())
            {
                dataGridView1.Rows.Add(group.Id, group.Title);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AddGroupForm gf = new AddGroupForm(dataGridView1);
                gf.ShowDialog();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DialogResult result = MessageBox.Show($"Do you really want to delete this group ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                    {
                        try
                        {
                            string id = dataGridView1.Rows[item.Index].Cells["groupID"].Value.ToString();
                            repository.removeGroup(id);
                            dataGridView1.Rows.RemoveAt(item.Index);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    MessageBox.Show("Group-(s) successfully removed!");
                }
            }
            else
            {
                DialogResult result = MessageBox.Show($"Please choose a group!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void row_selected(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string selectedGroupID = null;
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                   selectedGroupID = dataGridView1.Rows[item.Index].Cells["groupID"].Value.ToString();
                }
                row_selected_update_students(selectedGroupID);
                row_selected_update_subjects(selectedGroupID);
            }
            catch
            {

            }
        }

        private void row_selected_update_students(string selectedGroupID)
        {
            dataGridView2.Rows.Clear();
            foreach (User user in uRes.getStudentsInGroups(selectedGroupID))
            {
                dataGridView2.Rows.Add(user.getID(), user.getName(), user.getSurname(), user.getUsername());
            }
        }

        private void row_selected_update_subjects(string selectedGroupID)
        {
            dataGridView3.Rows.Clear();
            foreach (Subject subject in repository.getSubjectsInGroups(selectedGroupID))
            {
                dataGridView3.Rows.Add(subject.Id, subject.name, subject.proffesorID);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if(dataGridView2.SelectedRows.Count != 0)
                {
                    DialogResult result = MessageBox.Show($"Do you really want to delete this student from the group ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in this.dataGridView2.SelectedRows)
                        {
                            try
                            {
                                string id = dataGridView2.Rows[item.Index].Cells["studentID"].Value.ToString();
                                repository.removeStudentFromGroup(getSelectedGroupID(), id);
                                dataGridView2.Rows.RemoveAt(item.Index);
                                
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message);
                            }
                        }
                        MessageBox.Show("Student successfully removed from the group!");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Please select student!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if(dataGridView3.SelectedRows.Count != 0)
                {
                    DialogResult result = MessageBox.Show($"Do you really want to delete this subject from the group ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in this.dataGridView3.SelectedRows)
                        {
                            try
                            {
                                string id = dataGridView3.Rows[item.Index].Cells["SubjectID"].Value.ToString();
                                repository.removeSubjectFromGroup(getSelectedGroupID(), id);
                                dataGridView3.Rows.RemoveAt(item.Index);
                                
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show(exc.Message);
                            }
                        }
                        MessageBox.Show("Subject successfully removed from the group!");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Please select subject!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count != 0)
            {
                AddSubjectToGroup astg = new AddSubjectToGroup(getSelectedGroupID());
                astg.ShowDialog();
            }
        }

        private string getSelectedGroupID()
        {
            string selectedGroupID = null;
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                selectedGroupID = dataGridView1.Rows[item.Index].Cells["GroupID"].Value.ToString();
            }
            return selectedGroupID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                AddStudentToGroup astg = new AddStudentToGroup(getSelectedGroupID());
                astg.ShowDialog();
            }
        }
    }
}
