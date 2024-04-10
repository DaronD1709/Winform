using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using ListAndEditForm1.CourseNScore;

namespace ListAndEditForm1
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }
        

        private void UpdateDeleteStudentForm_Load(object sender, EventArgs e)
        {

        }
        Student student = new Student();
        private void bt_Find_Click(object sender, EventArgs e)
        {
            int id = int.Parse(tb_id.Text);
            SqlCommand command = new SqlCommand("SELECT id, fname, lname,bdate, gender, phone, address, picture FROM std WHERE id = " + id);
            DataTable table = student.getStudents(command);
            if (table.Rows.Count > 0)
            {
                tb_fname.Text = table.Rows[0]["fname"].ToString();
                tb_lname.Text = table.Rows[0]["lname"].ToString();
                dtP.Value = (DateTime)table.Rows[0]["bdate"];
                if (table.Rows[0]["gender"].ToString() == "Female")
                {
                    rb_Female.Checked = true;
                }
                else
                {
                    rb_Male.Checked = true;
                }
                tb_Phone.Text = table.Rows[0]["phone"].ToString();
                richtb_address.Text = table.Rows[0]["address"].ToString();
                byte[] pic;
                pic = (byte[])table.Rows[0]["picture"];
                MemoryStream picture = new MemoryStream(pic);
                picUp.Image = Image.FromStream(picture);
            }
            else
            {
                MessageBox.Show("Not found", "Find Student ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void TexBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bt_Edit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_id.Text))
            {
                int id = int.Parse(tb_id.Text);

                // Tạo SqlCommand để cập nhật thông tin sinh viên dựa trên ID
                SqlCommand updateCommand = new SqlCommand("UPDATE std SET fname = @fname, lname = @lname, bdate = @bdate, gender = @gender, phone = @phone, address = @address, picture = @picture WHERE id = @id");

                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.Parameters.AddWithValue("@fname", tb_fname.Text);
                updateCommand.Parameters.AddWithValue("@lname", tb_lname.Text);
                updateCommand.Parameters.AddWithValue("@bdate", dtP.Value);
                updateCommand.Parameters.AddWithValue("@gender", rb_Female.Checked ? "Female" : "Male");
                updateCommand.Parameters.AddWithValue("@phone", tb_Phone.Text);
                updateCommand.Parameters.AddWithValue("@address", richtb_address.Text);

                // Chuyển hình ảnh thành byte array để lưu trữ trong cơ sở dữ liệu
                MemoryStream ms = new MemoryStream();
                picUp.Image.Save(ms, picUp.Image.RawFormat);
                byte[] picture = ms.ToArray();
                updateCommand.Parameters.AddWithValue("@picture", picture);

                // Thực hiện lệnh UPDATE trực tiếp từ đối tượng updateCommand
                int result;

                using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSVDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))

                {
                    connection.Open();
                    updateCommand.Connection = connection;

                    // ExecuteNonQuery trả về số dòng bị ảnh hưởng
                    result = updateCommand.ExecuteNonQuery();
                }

                // Kiểm tra và xử lý kết quả
                if (result > 0)
                {
                    MessageBox.Show("Student information updated successfully!", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update student information. ID not found.", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please enter the student ID to edit.", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bt_Remove_Click(object sender, EventArgs e)
        {
            int id = int.Parse(tb_id.Text);

            // Tạo chuỗi kết nối
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QLSVDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu lệnh SQL để xóa sinh viên
                string query = "DELETE FROM std WHERE id=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student removed successfully", "Remove Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Xóa dữ liệu hiển thị nếu cần
                        //...
                    }
                    else
                    {
                        MessageBox.Show("Error removing student", "Remove Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void picUp_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                picUp.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCourseFr addCourseFr = new AddCourseFr();
            addCourseFr.Show(this);
        }
    }
}
