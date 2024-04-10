using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ListAndEditForm1.CourseNScore
{
    public partial class AddCourseFr : Form
    {
        MY_DB mydb = new MY_DB();
        private SqlConnection connection;
        private SqlCommand command;


        public AddCourseFr()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (!int.TryParse(textBox.Text, out _))
                {
                    MessageBox.Show("Invalid, please enter only number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = string.Empty;
                }
            }
        }

        private void AddCourseFr_Load(object sender, EventArgs e)
        {

        }



        private void InitializeDatabase()
        {
            string connectionString = "YourConnectionString";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
            try
            {
                connection.Open();
                // Create table if not exists
                command.CommandText = @"CREATE TABLE IF NOT EXISTS ComboData (
                                            ComboValue NVARCHAR(50),
                                            TextValue NVARCHAR(255)
                                        )";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database: " + ex.Message);
            }
        }

        private void PopulateComboBox()
        {
            try
            {
                // Populate combo box with values from database
                command.CommandText = "SELECT ComboValue FROM ComboData";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    selectcombox.Items.Add(reader["ComboValue"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating combo box: " + ex.Message);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Retrieve text value corresponding to the selected combo value
                command.CommandText = "SELECT TextValue FROM ComboData WHERE ComboValue = @ComboValue";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ComboValue", selectcombox.SelectedItem.ToString());
                string textValue = command.ExecuteScalar()?.ToString() ?? "";
                availablebox.Text = textValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving text value: " + ex.Message);
            }
        }

        private void selectcombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = selectcombox.SelectedItem.ToString();
            string command1 = "SELECT label FROM Course WHERE semester = 1";
            string command2 = "SELECT label FROM Course WHERE semester = 2";
            string command3 = "SELECT label FROM Course WHERE semester = 3";
            Course course = new Course();

            

            switch (selectedValue)
            {
                case "1":
                    {
                        availablebox.Items.Add(course.getlabelCourses(command1));

                    }
                    break;
                case "2":
                    {

                    }
                    break;
                case "3":
                    {

                    }
                    break;
                default:

                    break;
            }
        }
    }
}
