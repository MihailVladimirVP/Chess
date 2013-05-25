using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Шах___Проект
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            button1.BackColor =  Color.Transparent;
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            this.Focus();
            button4.Select();
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void mouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(238,221,180) ;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(238, 221, 180);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(238, 221, 180);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.StartPosition = FormStartPosition.CenterParent;
            af.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HighScores hs = new HighScores();
            hs.StartPosition = FormStartPosition.CenterParent;
            hs.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
        

    }
}
