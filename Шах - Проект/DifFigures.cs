using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;


namespace Шах___Проект
{
    public class DifFigures
    {
        public enum tip
        {
            [Description("Коњ")] Rook,
            Bishop,
            Knight,
            King,
            Queen,
            Pawn,
            None

        }

        public bool White { get; set; }
        public tip t { get; set; }

        public DifFigures()
        {
        }
        

        public DifFigures(bool W, tip t)
        {
            this.t = t;
            this.White = W;
        }


    }
}
