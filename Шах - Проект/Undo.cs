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
    public partial class Undo : Form
    {
        public Undo()
        {
            InitializeComponent();
            this.BackColor = Color.Wheat;
            PictureBox cheat = new PictureBox();
            cheat.Image = Properties.Resources.Undo;
            cheat.SetBounds(0, 0, 197, 248);
            Controls.Add(cheat);
        }

    }
}
