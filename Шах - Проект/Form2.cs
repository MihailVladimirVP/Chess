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
    public partial class PawnPromotion : Form
    {
        Form1 f;
        bool White;
        int index;
        List<DifFigures.tip> Taken;
        public PawnPromotion(Form1 forma, bool White, int index, List<DifFigures.tip> Taken)
        {

            InitializeComponent();

            this.f = forma;
            this.White = White;
            this.index = index;
            this.Taken = Taken;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String k = listBox1.Text;
            DifFigures zamena = new DifFigures();
            zamena.White = this.White;

            if (!White)
            {
                if (String.Compare(k, "Кралица") == 0)
                {
                    zamena.t = DifFigures.tip.Queen;
                    f.figures[index].Image = Properties.Resources.Piece3;
                    f.BlackTaken.Remove(DifFigures.tip.Queen);
                    f.Ostanati[index].t = DifFigures.tip.Queen;
                    f.removeItem1(DifFigures.tip.Queen);

                    this.Close();
                }

                else if (String.Compare(k, "Топ") == 0)
                {
                    zamena.t = DifFigures.tip.Rook;

                    f.figures[index].Image = Properties.Resources.Piece0;
                    f.Ostanati[index].t = DifFigures.tip.Rook;
                    f.BlackTaken.Remove(DifFigures.tip.Rook);
                    f.removeItem1(DifFigures.tip.Rook);

                    this.Close();
                }

                else if (String.Compare(k, "Ловец") == 0)
                {
                    zamena.t = DifFigures.tip.Bishop;
                    f.Ostanati[index].t = DifFigures.tip.Bishop;
                    f.figures[index].Image = Properties.Resources.Piece2;

                    f.BlackTaken.Remove(DifFigures.tip.Bishop);
                    f.removeItem1(DifFigures.tip.Bishop);
                    this.Close();
                }

                else if (String.Compare(k, "Коњ") == 0)
                {
                    zamena.t = DifFigures.tip.Knight;
                    f.Ostanati[index].t = DifFigures.tip.Knight;
                    f.figures[index].Image = Properties.Resources.Piece1;

                    f.BlackTaken.Remove(DifFigures.tip.Knight);
                    f.removeItem1(DifFigures.tip.Knight);

                    this.Close();
                }

                else
                {
                    this.Close();
                }
            }

            else if (White)
            {
                if (String.Compare(k, "Кралица") == 0)
                {
                    zamena.t = DifFigures.tip.Queen;
                    f.Ostanati[index].t = DifFigures.tip.Queen;
                    f.figures[index].Image = Properties.Resources.Piece59;
                    f.removeItem2(DifFigures.tip.Queen);
                    f.WhiteTaken.Remove(DifFigures.tip.Queen);

                    this.Close();
                }

                else if (String.Compare(k, "Топ") == 0)
                {
                    zamena.t = DifFigures.tip.Rook;
                    f.Ostanati[index].t = DifFigures.tip.Rook;
                    f.figures[index].Image = Properties.Resources.Piece63;
                    f.removeItem2(DifFigures.tip.Rook);
                    f.WhiteTaken.Remove(DifFigures.tip.Rook);
                    this.Close();
                }
                else if (String.Compare(k, "Ловец") == 0)
                {
                    zamena.t = DifFigures.tip.Bishop;
                    f.Ostanati[index].t = DifFigures.tip.Bishop;
                    f.figures[index].Image = Properties.Resources.Piece61;
                    f.removeItem2(DifFigures.tip.Bishop);
                    f.WhiteTaken.Remove(DifFigures.tip.Bishop);

                    this.Close();
                }
                else if (String.Compare(k, "Коњ") == 0)
                {
                    zamena.t = DifFigures.tip.Knight;
                    f.Ostanati[index].t = DifFigures.tip.Knight;
                    f.figures[index].Image = Properties.Resources.Piece62;
                    f.removeItem2(DifFigures.tip.Knight);
                    f.WhiteTaken.Remove(DifFigures.tip.Knight);

                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
