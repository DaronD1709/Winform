using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListAndEditForm1.CourseNScore
{
    public partial class ManageCourseForm : Form
    {
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        Course course = new Course();
        int pos;

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        void reloadListBoxData()
        {
            ListBoxCourses.DataSource = course.getAllCourses();
            ListBoxCourses.ValueMember = "id";
            ListBoxCourses.DisplayMember = "label";
            ListBoxCourses.SelectedItem = null;
            LabelTotalCourses.Text = ("Total Courses: " + course.totalCourse());
        }

        void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];
            ListBoxCourses.SelectedIndex = index;
            TextBoxID.Text = dr.ItemArray[0].ToString();
            TextBoxCourseName.Text = dr.ItemArray[1].ToString();
            NumericUpDownHours.Value = int.Parse(dr.ItemArray[2].ToString());
            TextBoxDescription.Text = dr.ItemArray[3].ToString();
        }

        private void ListBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListBoxCourses_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)ListBoxCourses.SelectedItem;
            pos = ListBoxCourses.SelectedIndex;
            showData(pos);
        }

        private void ButtonAddCourse_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxID.Text);
            string name = TextBoxCourseName.Text;
            int hrs = (int)NumericUpDownHours.Value;
            string descr = TextBoxDescription.Text;
            if (name.Trim() == "")
            {
                MessageBox.Show("Add A Course Name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (course.checkCourseName(name))
            {
                if (course.insertCourse(id ,name, hrs, descr))
                {
                    MessageBox.Show("New Course Inserted" ,"Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            string name = TextBoxCourseName.Text;
            int hrs = (int)NumericUpDownHours.Value;
            string descr = TextBoxDescription.Text;
            int id = int.Parse(TextBoxID.Text);
            if (!course.checkCourseName(name, Convert.ToInt32(TextBoxID.Text)))
            {
                MessageBox.Show("This Course Name Already Exist", "Edit Course", MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
            }
            else if (course.updateCourse(id, name ,hrs ,descr))
            {
                MessageBox.Show("Course Updated", "Edit Course" ,MessageBoxButtons.OK ,MessageBoxIcon.Information);
                reloadListBoxData();
            }
            else
            {
                MessageBox.Show("Course Not Updated" ,"Edit Course" ,MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }
            pos = 0;
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int courseID = Convert.ToInt32(TextBoxID.Text);
                if ((MessageBox.Show("Are Yousure You Want To Delete This Course" ,"Remove Course" ,MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    if (course.deleteCourse(courseID))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK ,MessageBoxIcon.Information);
                        TextBoxID.Text = "";
                        TextBoxCourseName.Text = "";
                        NumericUpDownHours.Value = 10;
                        TextBoxDescription.Text = "";
                        reloadListBoxData();
                    }
                    else
                    {
                        MessageBox.Show("Course Not Deleted", "Remove Course", MessageBoxButtons.OK ,MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Enter A Valid Numeric ID", "Remove Course" ,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pos = 0;
        }

        private void ButtonFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(pos);
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if( pos < (course.getAllCourses().Rows.Count - 1))
            {
                pos = pos + 1;
                showData(pos);
            }
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if ( pos > 0 )
            {
                pos = pos - 1;
                showData(pos);
            }
        }

        private void ButtonLast_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count - 1;
            showData(pos);
        }

        private void LabelTotalCourses_Click(object sender, EventArgs e)
        {

        }
    }
}
