using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Шах___Проект
{
    class Castling
    {
        TakenPlaces ZafateniMesta;
        bool White = false;
        bool Lev;

        public Castling(TakenPlaces Zafateni, bool W, bool Lev)
        {
            this.ZafateniMesta = Zafateni;
            this.White = W;
            this.Lev = Lev;
        }

        public bool ValidirajCastle()
        {
            if (White && Lev)
            {
                for (int i = 1; i < 4; i++)
                {
                    
                        if (this.ZafateniMesta.ZafateniMesta[7][i])
                        {
                            return false;
                        }
                }
                return true;

            }
            if (White && !Lev)
            {
                for (int i = 5; i < 7; i++)
                {

                    if (ZafateniMesta.ZafateniMesta[7][i])
                    {
                        
                        return false;
                    }
                }
                return true;
            }
            if (!White && Lev)
            {
                for (int i = 1; i < 4; i++)
                {
                    if (ZafateniMesta.ZafateniMesta[0][i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = 5; i < 7; i++)
                {

                    if (ZafateniMesta.ZafateniMesta[0][i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
