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
    
    public partial class ImeiPrezime : Form
    {
        public String cuvam = "";
        
        public ImeiPrezime(String t)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(233, 233, 250);
            label1.Text = "Внеси име и презиме на " + t + " играч";
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            cuvam = textBox1.Text;
            String[] p = cuvam.Split();
            if (cuvam.Trim().Length == 0)
            {
                errorProvider1.SetError(button1, "Внесете валидно име");
                button1.Enabled = true;
            }
            
            else if (p.Length==1 || p.Length>2 || p[0].Trim().Equals("") || p[1].Trim().Equals(""))
            {
                errorProvider1.SetError(button1, "Мора да внесете точно едно празно место");
                button1.Enabled = true;
            }
            
            else
            {
                errorProvider1.Clear();
                button1.Enabled = false;
                this.Close();
            }

        }
        public bool isDisabled()
        {
            return button1.Enabled;
        }
        public String igrac()
        {
            
            return cuvam;
        }
      
      
    }
}
