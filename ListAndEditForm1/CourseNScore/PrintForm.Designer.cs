namespace ListAndEditForm1.CourseNScore
{
    partial class PrintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImportFile = new System.Windows.Forms.Button();
            this.ButtonPrint = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportFile
            // 
            this.ImportFile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ImportFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportFile.Font = new System.Drawing.Font("Agency FB", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFile.Location = new System.Drawing.Point(34, 422);
            this.ImportFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImportFile.Name = "ImportFile";
            this.ImportFile.Size = new System.Drawing.Size(122, 45);
            this.ImportFile.TabIndex = 0;
            this.ImportFile.Text = "To File";
            this.ImportFile.UseVisualStyleBackColor = false;
            this.ImportFile.Click += new System.EventHandler(this.ImportFile_Click);
            // 
            // ButtonPrint
            // 
            this.ButtonPrint.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ButtonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrint.Font = new System.Drawing.Font("Agency FB", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPrint.Location = new System.Drawing.Point(189, 422);
            this.ButtonPrint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ButtonPrint.Name = "ButtonPrint";
            this.ButtonPrint.Size = new System.Drawing.Size(122, 45);
            this.ButtonPrint.TabIndex = 1;
            this.ButtonPrint.Text = "Print";
            this.ButtonPrint.UseVisualStyleBackColor = false;
            this.ButtonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 14);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(764, 384);
            this.dataGridView1.TabIndex = 2;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(793, 488);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ButtonPrint);
            this.Controls.Add(this.ImportFile);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PrintForm";
            this.Text = "PrintForm";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ImportFile;
        private System.Windows.Forms.Button ButtonPrint;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}