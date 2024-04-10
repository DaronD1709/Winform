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
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";
            ofd.ShowDialog();
            string filename = ofd.FileName;
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Open(filename);
            Excel.Worksheet exSheet;
            exSheet = exBook.Worksheets["Sheet1"];
            Excel.Range exRange = exSheet.UsedRange;
            if (filename != "")
            {
                for (int exRow = 2; exRow <= exRange.Rows.Count; exRow++)
                {
                    string email = exRange.Cells[exRow, 5].Text;
                    if (exRange.Cells[exRow, 5].Text != "")
                    {
                        email = exRange.Cells[exRow, 2].Text + "@student.hcmute.edu.vn";
                    }
                    dataGridView1.Rows.Add(
                                                  exRange.Cells[exRow, 1].Text,
                                                  exRange.Cells[exRow, 2].Text,
                                                  exRange.Cells[exRow, 3].Text,
                                                  exRange.Cells[exRow, 4].Text,
                                                  exRange.Cells[exRow, 5].Text,
                                                  email
                                                  );
                }

                exBook.Close(false);
                exApp.Quit();
            }


        }
    }
    }

