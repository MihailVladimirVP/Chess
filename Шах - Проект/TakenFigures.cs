using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Шах___Проект
{
    
    public class TakenFigures
    {
        public DifFigures.tip v;
        public int taken;

        public TakenFigures(DifFigures.tip v, int t)
        {
            this.v = v;
            this.taken = t;
        }

        public override string ToString()
        {
            String s="";
            return s + Convert.ToString(this.v) + " X " + taken;
        }
        public TakenFigures()
        {
        }


    }
}
