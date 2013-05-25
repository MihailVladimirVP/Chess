using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Web;

namespace Шах___Проект
{
    public partial class HighScores : Form
    {
        public HighScores()
        {
            String[] Rezultat = new String[10];
            InitializeComponent();
            this.BackColor = Color.FromArgb(240, 243, 253);
            int counter = 0;
            string line;


            var reader = new StreamReader("HighScores.txt");
            while ((line = reader.ReadLine()) != null)
            {

                String[] t = line.Split();
                counter++;
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                if (t.Length >= 4)
                {
                    row.Cells[0].Value = counter;
                    row.Cells[1].Value = t[0] + " " + t[1];
                    row.Cells[2].Value = t[2];
                    row.Cells[3].Value = t[3] + " " + t[4];
                    row.Cells[4].Value = t[5];
                    dataGridView1.Rows.Add(row);
                }

            }

        
            dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
           

            
            dataGridView1.BackgroundColor = Color.FromArgb(240, 243, 253);
            reader.Close();
         
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            File.Delete("HighScores.txt");
            FileStream fs = new FileStream("HighScores.txt", FileMode.CreateNew);
            fs.Close();
            

        }
       
     

        
       

       

        
    }
}
