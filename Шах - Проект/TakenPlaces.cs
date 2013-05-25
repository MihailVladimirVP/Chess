using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Шах___Проект
{
    public class TakenPlaces
    {
        public bool[][] ZafateniMesta = new bool[8][];
        public bool[][] Whitte = new bool[8][];

        public TakenPlaces()
        {
          
            for (int i = 0; i < 8; i++)
            {
                ZafateniMesta[i] = new bool[8];
                Whitte[i] = new bool[8];
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0 || i == 1)
                    {
                        Whitte[i][j] = false;
                        ZafateniMesta[i][j] = true;
                    }
                    if (i == 6 || i == 7)
                    {
                        Whitte[i][j] = true;
                        ZafateniMesta[i][j] = true;
                    }
                    if (i==4 || i==5 || i==3 || i==2)
                    {
                        ZafateniMesta[i][j] = false;
                        Whitte[i][j] = false;
                    }
                }
            }
        }
        public TakenPlaces(bool [][]ZafateniMesta, bool [][] Whitte)
        {

            this.Whitte = Whitte;
            this.ZafateniMesta = ZafateniMesta;
        }

    }
}
