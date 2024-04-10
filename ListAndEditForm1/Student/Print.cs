using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ListAndEditForm1
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }
        Student student = new Student();
        

        private void Print_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM std");
            fillGrid(command);
            if(rButtonNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void rButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void rButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            SqlCommand command;
            String query;
            if (rButtonYes.Checked)
            {
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (rButtonMale.Checked)
                {
                    query = "SELECT * FROM std WHERE gender = 'Male' AND bdate BETWEEN ' " + date1 + " 'AND' " + date2 + "'";
                }
                else if (rButtonFemale.Checked)
                {
                    query = "SELECT * FROM std WHERE gender = 'FeMale' AND bdate BETWEEN ' " + date1 + " 'AND' " + date2 + "'";
                }
                else
                {
                    query = "SELECT * FROM std WHERE bdate BETWEEN ' " + date1 + " 'AND' " + date2 + "'";
                }
                command = new SqlCommand(query);
                fillGrid(command);
            }
            else
            {
                if (rButtonMale.Checked)
                {
                    query = " SELECT * FROM std WHERE gender = 'Male' ";
                }
                else if (rButtonFemale.Checked)
                {
                    query = " SELECT * FROM std WHERE gender = 'FeMale' ";
                }
                else
                {
                    query = "SELECT * FROM std";
                }
                command = new SqlCommand(query);
                fillGrid(command);
            }
        }

        private void ButtonPrinter_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = " Print Document";
            printDlg.Document = printDoc;
            printDlg.AllowSelection = true;
            printDlg.AllowSomePages = true;

            if (printDlg.ShowDialog() == DialogResult.OK) printDoc.Print();
        }

        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1);
        }

        private void ExportToExcel(DataGridView dataGridView)
        {
            try
            {
                // Create a new Excel application
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;

                // Create a new workbook
                Excel.Workbook excelWB = excelApp.Workbooks.Add("");
                Excel._Worksheet excelWS = excelWB.ActiveSheet;

                // Write DataGridView data to Excel
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        excelWS.Cells[i + 1, j + 1] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Save the Excel file
                excelWB.SaveCopyAs(@"C:\Users\HALO\Desktop\students_list.xlsx");
                excelWB.Close();
                excelApp.Quit();

                MessageBox.Show("Data exported to Excel successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to Excel: " + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTimePicker = sender as DateTimePicker;
            string shortDate = dateTimePicker.Value.ToString("d");
            dateTimePicker.Text = shortDate;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTimePicker = sender as DateTimePicker;
            string shortDate = dateTimePicker.Value.ToString("d");
            dateTimePicker.Text = shortDate;
        }
    }
}
