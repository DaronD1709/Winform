﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListAndEditForm1
{
    public partial class Managestudentform : Form
    {
        public Managestudentform()
        {
            InitializeComponent();
        }
        Student student = new Student();

        private void btTotal_Click(object sender, EventArgs e)
        {

        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID = Convert.ToInt32(txtStudentID.Text);


                //display a confirmation message before the delete 
                if ((MessageBox.Show("Are You Sure You Want To Delete This Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    if (student.deleteStudent(studentID))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new SqlCommand("SELECT * FROM std"));
                        // clear/reset fields after delete
                        txtStudentID.Text = "";
                        TextBoxFname.Text =
                        TextBoxLname.Text = "";
                        TextBoxAddress.Text = "";
                        TextBoxPhone.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        PictureBoxStudentImage.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Student Not Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please Enter A Valid ID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM std WHERE CONCAT(fname,lname,address)LIKE '%" + textboxsearch.Text + "%'");
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
            LabelTotalStudent.Text = ("Total Student: " + dataGridView1.RowCount);
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            txtStudentID.Clear();
            TextBoxFname.Clear();
            TextBoxLname.Clear();
            rbuttonMale.Checked = true;
            TextBoxPhone.Clear();
            TextBoxAddress.Clear();
            PictureBoxStudentImage.Image = null;
            dateTimePicker1.Value = DateTime.Now;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm upDestudent = new UpdateDeleteStudentForm();
            upDestudent.tb_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            upDestudent.tb_fname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            upDestudent.tb_lname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            upDestudent.dtP.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            // GENDER
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Female")
            {
                upDestudent.rb_Female.Checked = true;
            }
            else
            {
                upDestudent.rb_Male.Checked = true;
            }
            upDestudent.tb_Phone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            upDestudent.richtb_address.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            //IMAGE 
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            upDestudent.picUp.Image = Image.FromStream(picture);

            upDestudent.Show();
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void btDowload_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = ("Student_" + txtStudentID.Text);
            if ((PictureBoxStudentImage.Image == null))
            {
                MessageBox.Show("No Image In The PictureBox");
            }
            else if ((svf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image.Save((svf.FileName + (" . " + ImageFormat.Jpeg.ToString())));
            }
        }
        bool verif()
        {
            if ((TextBoxFname.Text.Trim() == "")
            || (TextBoxLname.Text.Trim() == "")
            || (TextBoxAddress.Text.Trim() == "")
            || (TextBoxPhone.Text.Trim() == "")
            || (PictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            int id = Convert.ToInt32(txtStudentID.Text);
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = TextBoxPhone.Text;
            string adrs = TextBoxAddress.Text;
            string gender = "Male";

            if (rbuttonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if ((this_year - born_year) < 10 || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                if (student.insertStudent(id, fname, lname, bdate, gender, phone, adrs, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LabelTotalStudent_Click(object sender, EventArgs e)
        {

        }

        private void Managestudentform_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    
        

     

