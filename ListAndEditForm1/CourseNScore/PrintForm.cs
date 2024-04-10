using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ListAndEditForm1.CourseNScore
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }
        Student student = new Student();
        private System.Data.DataTable dataTable;

        private void PrintForm_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM std");
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
            }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = ("Print Document");
            printDlg.Document = (printDoc);
            printDlg.AllowSelection = (true);
            printDlg.AllowSomePages = (true);

            if (printDlg.ShowDialog() == DialogResult.OK) printDoc.Print();
        }

        private void ImportFile_Click(object sender, EventArgs e)
        {

            ImportFromExcel();

        }


        private void ImportFromExcel()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Select an Excel file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Create an Excel application
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;

                    // Open the selected Excel file
                    Excel.Workbook excelWB = excelApp.Workbooks.Open(filePath);
                    Excel._Worksheet excelWS = excelWB.Sheets[1];
                    Excel.Range exRange = excelWS.UsedRange;

                    // Create a DataTable to store the imported data
                    dataTable = new System.Data.DataTable();

                    // Add columns to the DataTable
                    for (int col = 1; col <= exRange.Columns.Count; col++)
                    {
                        dataTable.Columns.Add("Column " + col);
                    }

                    // Read data from Excel and populate DataTable
                    for (int exRow = 2; exRow <= exRange.Rows.Count; exRow++)
                    {
                        DataRow newRow = dataTable.NewRow();
                        for (int exCol = 1; exCol <= exRange.Columns.Count; exCol++)
                        {
                            newRow[exCol - 1] = exRange.Cells[exRow, exCol].Text;
                        }
                        dataTable.Rows.Add(newRow);
                    }

                    // Bind DataTable to DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Close Excel
                    excelWB.Close(false);
                    excelApp.Quit();

                    MessageBox.Show("Data imported from Excel successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data from Excel: " + ex.Message);
            }
        }
    }
    }

