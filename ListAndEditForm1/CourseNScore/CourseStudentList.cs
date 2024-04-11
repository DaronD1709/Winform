using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListAndEditForm1.CourseNScore
{
    public partial class CourseStudentList : Form
    {
        public CourseStudentList()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void CourseStudentList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLSVDBDataSet3.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.qLSVDBDataSet3.std);

     
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand("SELECT * FROM std WHERE CONCAT(id,fname,lname,address,Selected Course)LIKE '%" + txtnamecourse.Text + "%'");
            
            fillGrid(command);
        }

        private void fillGrid(SqlCommand command)
        {

            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            // Dem sinh vien 
            semesterlabel.Text = ("Semester : " + );
        }
    }
}
