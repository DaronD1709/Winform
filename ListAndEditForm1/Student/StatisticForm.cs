using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListAndEditForm1
{
    public partial class StatisticForm : Form
    {
        public StatisticForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void panelFemale_Paint(object sender, PaintEventArgs e)
        {

        }
        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;
        private void StatisticForm_Load(object sender, EventArgs e)
        {
            panTotalColor = panelTotal.BackColor;
            panMaleColor = panelMale.BackColor;
            panFemaleColor = panelFemale.BackColor;

            Student student = new Student();
            double total = Convert.ToDouble(student.totalStudent());
            double totalMale = Convert.ToDouble(student.totalMaleStudent());
            double totalFemale = Convert.ToDouble(student.totalFemaleStudent());


            double maleStudentPercentage = (totalMale * (100 / total));
            double femaleStudentPercentage = (totalFemale * (100 / total));
            labelTotal.Text = ("Total Students:" + total.ToString());
            labelMale.Text = ("Male :" + (maleStudentPercentage.ToString("0.00") + "%"));
            labelFemale.Text = ("Female :" + (femaleStudentPercentage.ToString("0.00") + "%"));
        }
        private void labelTotal_MouseEnter(object sender, EventArgs e)
        {
            labelTotal.ForeColor = panTotalColor;
            panelTotal.BackColor = Color.White;
        }
        private void labelTotal_MouseLeave(object sender, EventArgs e)
        {
            labelTotal.ForeColor = Color.White;
            panelTotal.BackColor = panTotalColor;

        }
        private void labelMale_MouseEnter(object sender, EventArgs e)
        {
            labelMale.ForeColor = panMaleColor;
            panelMale.BackColor = Color.White;
        }
        private void labelMale_MouseLeave(object sender, EventArgs e)
        {
            labelMale.ForeColor = Color.White;
            panelMale.BackColor = panMaleColor;
        }
        private void labelFemale_MouseEnter(object sender, EventArgs e)
        {
            labelFemale.ForeColor = panFemaleColor;
            panelFemale.BackColor = Color.White;
        }
        private void labelFemale_MouseLeave(object sender, EventArgs e)
        {
            labelFemale.ForeColor = Color.White;
            panelFemale.BackColor = panFemaleColor;
        }
    }
}
