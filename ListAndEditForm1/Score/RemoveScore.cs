using ListAndEditForm1.CourseNScore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListAndEditForm1.Score
{
    public partial class RemoveScore : Form
    {
        public RemoveScore()
        {
            InitializeComponent();
        }
        SCORE score = new SCORE();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            SCORE score = new SCORE();
            try
            {
                int n = dataGridView1.SelectedRows.Count;
                MessageBox.Show("Are you sure you want to Delete This Course", "Remove Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (score.deletescore(n))
                {
                    MessageBox.Show("Score Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Score Not Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Enter A Valid Numeric ID", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
         
        }

        private void RemoveScore_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLSVDBDataSet2.score' table. You can move, or remove it, as needed.
            this.scoreTableAdapter.Fill(this.qLSVDBDataSet2.score);

        }
    }
}
