using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Шах___Проект
{

    class DrawValid
    {
        public DifFigures[] Ostanati;
        public int idx1;
        public int idx2;
        public TakenPlaces Zafateni;
        public bool[][] Enpassant;

        public DrawValid(DifFigures[] Ostanati, int idx1, int idx2, TakenPlaces Zafateni, bool[][] Enpassant)
        {
            this.Ostanati = Ostanati;
            this.idx1 = idx1;
            this.idx2 = idx2;
            this.Zafateni = Zafateni;
            this.Enpassant = Enpassant;

        }
        public void OnPaint(PaintEventArgs e)
        {
            Graphics et = e.Graphics;
            ValidMoves k = new ValidMoves(Ostanati[idx1], idx1, Zafateni, Enpassant);
            Brush pinkt = new SolidBrush(Color.FromArgb(255, 182, 193));

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    {
                        if (k.Valid[i][j])
                        {
                            et.FillRectangle(pinkt, i * 50, j * 50, 50, 50);
                        }
                    }
                }
            }
        }
    }
}

