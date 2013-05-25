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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
           
            InitializeComponent();
            this.Icon = new System.Drawing.Icon(Properties.Resources.icon, new Size(20, 20));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            BackColor = Color.FromArgb(255, 250, 240);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
