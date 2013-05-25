using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Шах___Проект
{
    public class ValidMoves
    {
        public DifFigures figura { get; set; }
        public int index { get; set; }
        public bool[][] Valid { get; set; }
        public TakenPlaces Zafateni { get; set; }
        public bool[][] Enpassant { get; set; }
        public bool IsEnpassant { get; set; }


        public ValidMoves(DifFigures f, int i, TakenPlaces Zafateni, bool[][] Enpassant)
        {
            Valid = new bool[8][];
            for (int rr = 0; rr < 8; rr++)
            {
                Valid[rr] = new bool[8];
            }
            figura = f;
            index = i;
            this.Zafateni = Zafateni;
            generatePotezi(f.t, i, f.White, Zafateni, Enpassant);
            IsEnpassant = false;
            Enpassant = new bool[2][];

            Enpassant[1] = new bool[8];
            Enpassant[0] = new bool[8];

        }


        public void generatePotezi(DifFigures.tip t, int index, bool White, TakenPlaces Zafateni, bool[][] Enpassant)
        {
            if (t.Equals(DifFigures.tip.Pawn) && !White)
            {
                // Поле напред
                if ((index / 8) + 1 < 8)
                {
                    if (!(Zafateni.ZafateniMesta[index / 8 + 1][index % 8]))
                    {
                        Valid[(index / 8) + 1][(index % 8)] = true;
                    }
                }
                // Поле лево напред, доколку е зафатено
                if (index / 8 + 1 < 8 && ((index % 8) - 1) >= 0)
                {
                    if (Zafateni.ZafateniMesta[index / 8 + 1][index % 8 - 1] && Zafateni.Whitte[index / 8 + 1][index % 8 - 1] == true)
                    {
                        Valid[(index / 8) + 1][(index % 8) - 1] = true;
                    }
                }
                // Поле десно напред, доколку е зафатено
                if ((index / 8) + 1 < 8 && ((index % 8) + 1) < 8)
                {
                    if (Zafateni.ZafateniMesta[index / 8 + 1][index % 8 + 1] && Zafateni.Whitte[index / 8 + 1][index % 8 + 1] == true)
                    {
                        Valid[(index / 8) + 1][(index % 8) + 1] = true;
                    }
                }
                // Поле две напред, доколку на почетна позиција && поле едно напред не е зафатено
                if (index / 8 == 1 && index / 8 + 2 < 8)
                {
                    if (!Zafateni.ZafateniMesta[index / 8 + 2][index % 8] && !Zafateni.ZafateniMesta[index / 8 + 1][index % 8])
                        Valid[(index / 8) + 2][index % 8] = true;
                }
                // Enpassantе валидност десно
                if (index % 8 + 1 < 8)
                {
                    if (index % 8 + 1 < 8 && index / 8 == 4 && Enpassant[0][index % 8 + 1])
                    {
                        Valid[5][index % 8 + 1] = true;
                        IsEnpassant = true;
                    }
                }
                // Enpassante лево
                if (index % 8 - 1 >= 0)
                {
                    if (index % 8 - 1 >= 0 && index / 8 == 4 && Enpassant[0][index % 8 - 1])
                    {
                        Valid[5][index % 8 - 1] = true;
                        IsEnpassant = true;
                    }
                }

            }
            // Исто за бел пион
            else if (t.Equals(DifFigures.tip.Pawn) && White)
            {
                if ((index / 8) - 1 >= 0)
                {
                    if (!Zafateni.ZafateniMesta[index / 8 - 1][index % 8] && Zafateni.Whitte[index / 8 - 1][index % 8] == false)
                    {
                        Valid[(index / 8) - 1][(index % 8)] = true;
                    }
                }
                if (index / 8 - 1 >= 0 && ((index % 8) - 1) >= 0)
                {
                    if (Zafateni.ZafateniMesta[index / 8 - 1][index % 8 - 1] && Zafateni.Whitte[index / 8 - 1][index % 8 - 1] == false)
                    {
                        Valid[(index / 8) - 1][(index % 8) - 1] = true;
                    }

                }
                if ((index / 8) - 1 >= 0 && ((index % 8) + 1) < 8)
                {
                    if (Zafateni.ZafateniMesta[index / 8 - 1][index % 8 + 1] && Zafateni.Whitte[index / 8 - 1][index % 8 + 1] == false)
                    {
                        Valid[(index / 8) - 1][(index % 8) + 1] = true;
                    }
                }
                if (index / 8 == 6 && index / 8 - 2 >= 0)
                {
                    if (!Zafateni.ZafateniMesta[index / 8 - 2][index % 8] && !Zafateni.ZafateniMesta[index / 8 - 1][index % 8])
                    {
                        Valid[(index / 8) - 2][index % 8] = true;
                    }
                }
                if (index % 8 + 1 < 8)
                {
                    if (index % 8 + 1 < 8 && index / 8 == 3 && Enpassant[1][index % 8 + 1])
                    {
                        Valid[2][index % 8 + 1] = true;
                        IsEnpassant = true;
                    }
                }
                if (index % 8 - 1 >= 0)
                {
                    if (index % 8 - 1 >= 0 && index / 8 == 3 && Enpassant[1][index % 8 - 1])
                    {
                        Valid[2][index % 8 - 1] = true;
                        IsEnpassant = true;
                    }
                }
            }

            // Валидни потези за кралот (Напред (лево, право, десно), лево, десно, Назад (лево, право, десно))
            else if (t.Equals(DifFigures.tip.King))
            {
                if (White)
                {
                    if (index / 8 + 1 < 8 && index % 8 + 1 < 8)
                    {
                        if (!Zafateni.Whitte[(index / 8) + 1][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) + 1])
                        {
                            Valid[(index / 8) + 1][(index % 8) + 1] = true;
                        }
                    }

                    if (index / 8 + 1 < 8 && index % 8 - 1 >= 0)
                    {
                        if (!Zafateni.Whitte[(index / 8) + 1][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) - 1])
                        {
                            Valid[(index / 8) + 1][(index % 8) - 1] = true;
                        }
                    }

                    if ((index / 8 - 1 >= 0 && index % 8 + 1 < 8))
                    {
                        if (!Zafateni.Whitte[(index / 8) - 1][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) + 1])
                        {
                            Valid[(index / 8) - 1][(index % 8) + 1] = true;
                        }
                    }

                    if ((index / 8 - 1 >= 0 && index % 8 - 1 >= 0))
                    {
                        if (!Zafateni.Whitte[(index / 8) - 1][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) - 1])
                        {
                            Valid[(index / 8) - 1][(index % 8) - 1] = true;
                        }
                    }

                    if (index % 8 + 1 < 8)
                    {
                        if (!Zafateni.Whitte[(index / 8)][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8)][(index % 8) + 1])
                        {
                            Valid[(index / 8)][(index % 8) + 1] = true;
                        }
                    }

                    if (index % 8 - 1 >= 0)
                    {
                        if (!Zafateni.Whitte[(index / 8)][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8)][(index % 8) - 1])
                        {
                            Valid[(index / 8)][(index % 8) - 1] = true;
                        }
                    }

                    if (index / 8 + 1 < 8)
                    {
                        if (!Zafateni.Whitte[(index / 8 + 1)][(index % 8)] || !Zafateni.ZafateniMesta[(index / 8 + 1)][(index % 8)])
                        {
                            Valid[(index / 8) + 1][(index % 8)] = true;
                        }
                    }
                    if (index / 8 - 1 >= 0)
                    {
                        if (!Zafateni.Whitte[(index / 8 - 1)][(index % 8)] || !Zafateni.ZafateniMesta[(index / 8 - 1)][(index % 8)])
                        {
                            Valid[(index / 8) - 1][(index % 8)] = true;
                        }
                    }
                }
                else if (!White)
                {

                    if (index / 8 + 1 < 8 && index % 8 + 1 < 8)
                    {
                        if (Zafateni.Whitte[(index / 8) + 1][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) + 1])
                        {
                            Valid[(index / 8) + 1][(index % 8) + 1] = true;
                        }
                    }

                    if (index / 8 + 1 < 8 && index % 8 - 1 >= 0)
                    {
                        if (Zafateni.Whitte[(index / 8) + 1][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) - 1])
                        {
                            Valid[(index / 8) + 1][(index % 8) - 1] = true;
                        }
                    }

                    if ((index / 8 - 1 >= 0 && index % 8 + 1 < 8))
                    {
                        if (Zafateni.Whitte[(index / 8) - 1][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) + 1])
                        {
                            Valid[(index / 8) - 1][(index % 8) + 1] = true;
                        }
                    }

                    if ((index / 8 - 1 >= 0 && index % 8 - 1 >= 0))
                    {
                        if (Zafateni.Whitte[(index / 8) - 1][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) - 1])
                        {
                            Valid[(index / 8) - 1][(index % 8) - 1] = true;
                        }
                    }

                    if (index % 8 + 1 < 8)
                    {
                        if (Zafateni.Whitte[(index / 8)][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8)][(index % 8) + 1])
                        {
                            Valid[(index / 8)][(index % 8) + 1] = true;
                        }
                    }

                    if (index % 8 - 1 >= 0)
                    {
                        if (Zafateni.Whitte[(index / 8)][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8)][(index % 8) - 1])
                        {
                            Valid[(index / 8)][(index % 8) - 1] = true;
                        }
                    }

                    if (index / 8 + 1 < 8)
                    {
                        if (Zafateni.Whitte[(index / 8 + 1)][(index % 8)] || !Zafateni.ZafateniMesta[(index / 8 + 1)][(index % 8)])
                        {
                            Valid[(index / 8) + 1][(index % 8)] = true;
                        }
                    }
                    if (index / 8 - 1 >= 0)
                    {
                        if (Zafateni.Whitte[(index / 8 - 1)][(index % 8)] || !Zafateni.ZafateniMesta[(index / 8 - 1)][(index % 8)])
                        {
                            Valid[(index / 8) - 1][(index % 8)] = true;
                        }
                    }
                }
            }
            else if (t.Equals(DifFigures.tip.Rook))
            {
                int[] Tocki = new int[4];
                Tocki[0] = -1;
                Tocki[1] = -1;
                Tocki[2] = -1;
                Tocki[3] = -1;
                int min = Int16.MinValue;


                for (int i = index / 8 - 1; i >= 0; i--)
                {
                    if (Zafateni.ZafateniMesta[i][index % 8])
                    {
                        if (min < i)
                        {
                            Tocki[0] = i;
                            min = i;
                        }
                    }
                }
                min = Int32.MaxValue;
                for (int i = index / 8 + 1; i < 8; i++)
                {
                    if (Zafateni.ZafateniMesta[i][index % 8])
                    {
                        if (min > i)
                        {
                            min = i;
                            Tocki[1] = i;
                        }
                    }
                }
                min = Int32.MinValue;
                for (int j = index % 8 - 1; j >= 0; j--)
                {
                    if (Zafateni.ZafateniMesta[index / 8][j])
                    {
                        if (min < j)
                        {
                            Tocki[2] = j;
                            min = j;
                        }

                        break;
                    }
                }
                min = Int32.MaxValue;
                for (int j = index % 8 + 1; j < 8; j++)
                {
                    if (Zafateni.ZafateniMesta[index / 8][j])
                    {
                        if (min > j)
                        {
                            min = j;
                            Tocki[3] = j;
                        }

                    }
                }
                if (Tocki[0] == -1)
                    Tocki[0] = 0;
                if (Tocki[1] == -1)
                    Tocki[1] = 7;
                if (Tocki[2] == -1)
                    Tocki[2] = 0;
                if (Tocki[3] == -1)
                    Tocki[3] = 7;


                if (White)
                {
                    for (int i = Tocki[0]; i <= Tocki[1]; i++)
                    {
                        if (!Zafateni.Whitte[i][index % 8] || !Zafateni.ZafateniMesta[i][index % 8])
                        {
                            Valid[i][index % 8] = true;

                        }
                    }
                    for (int j = Tocki[2]; j <= Tocki[3]; j++)
                    {
                        if (!Zafateni.Whitte[index / 8][j] || !Zafateni.ZafateniMesta[index / 8][j])
                        {
                            Valid[index / 8][j] = true;
                        }
                    }
                }
                else if (!White)
                {
                    for (int i = Tocki[0]; i <= Tocki[1]; i++)
                    {
                        if (Zafateni.Whitte[i][index % 8] || !Zafateni.ZafateniMesta[i][index % 8])
                        {
                            Valid[i][index % 8] = true;

                        }
                    }
                    for (int j = Tocki[2]; j <= Tocki[3]; j++)
                    {
                        if (Zafateni.Whitte[index / 8][j] || !Zafateni.ZafateniMesta[index / 8][j])
                        {
                            Valid[index / 8][j] = true;
                        }
                    }
                }
                Valid[index / 8][index % 8] = false;
            }
            else if (t.Equals(DifFigures.tip.Knight))
            {
                if (White)
                {
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) - 2][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) - 2][(index % 8) - 1])
                            Valid[(index / 8) - 2][(index % 8) - 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) - 2][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) - 2][(index % 8) + 1])
                            Valid[(index / 8) - 2][(index % 8) + 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) - 1][(index % 8) + 2] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) + 2])
                            Valid[(index / 8) - 1][(index % 8) + 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) - 1][(index % 8) - 2] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) - 2])
                            Valid[(index / 8) - 1][(index % 8) - 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) + 2][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) + 2][(index % 8) - 1])
                            Valid[(index / 8) + 2][(index % 8) - 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) + 2][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) + 2][(index % 8) + 1])
                            Valid[(index / 8) + 2][(index % 8) + 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) + 1][(index % 8) - 2] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) - 2])
                            Valid[(index / 8) + 1][(index % 8) - 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (!Zafateni.Whitte[(index / 8) + 1][(index % 8) + 2] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) + 2])
                            Valid[(index / 8) + 1][(index % 8) + 2] = true;
                    }
                    catch (Exception e)
                    {
                    }

                }
                else if (!White)
                {
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) - 2][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) - 2][(index % 8) - 1])
                            Valid[(index / 8) - 2][(index % 8) - 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) - 2][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) - 2][(index % 8) + 1])
                            Valid[(index / 8) - 2][(index % 8) + 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) - 1][(index % 8) + 2] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) + 2])
                            Valid[(index / 8) - 1][(index % 8) + 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) - 1][(index % 8) - 2] || !Zafateni.ZafateniMesta[(index / 8) - 1][(index % 8) - 2])
                            Valid[(index / 8) - 1][(index % 8) - 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) + 2][(index % 8) - 1] || !Zafateni.ZafateniMesta[(index / 8) + 2][(index % 8) - 1])
                            Valid[(index / 8) + 2][(index % 8) - 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) + 2][(index % 8) + 1] || !Zafateni.ZafateniMesta[(index / 8) + 2][(index % 8) + 1])
                            Valid[(index / 8) + 2][(index % 8) + 1] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) + 1][(index % 8) - 2] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) - 2])
                            Valid[(index / 8) + 1][(index % 8) - 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        if (Zafateni.Whitte[(index / 8) + 1][(index % 8) + 2] || !Zafateni.ZafateniMesta[(index / 8) + 1][(index % 8) + 2])
                            Valid[(index / 8) + 1][(index % 8) + 2] = true;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            else if (t.Equals(DifFigures.tip.Bishop))
            {
                int[][] Tocki = new int[4][];
                for (int i = 0; i < 4; i++)
                {
                    Tocki[i] = new int[2];
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Tocki[i][j] = -1;
                    }
                }
                int min = Int16.MinValue;
                bool barEdno = (index / 8 - 1 >= 0 && index % 8 - 1 >= 0);

                for (int a = index / 8 - 1, nesto = index % 8 - 1; a >= 0 && nesto >= 0; a--, nesto--)
                {
                    if (Zafateni.ZafateniMesta[a][nesto])
                    {

                        if (min < a)
                        {
                            Tocki[0][0] = a;
                            Tocki[0][1] = nesto;
                            min = a;
                        }
                        barEdno = false;
                    }
                    else if (barEdno)
                    {
                        Tocki[0][0] = a;
                        Tocki[0][1] = nesto;
                    }

                }
                barEdno = (index / 8 + 1 < 8 && index % 8 + 1 < 8);
                min = Int32.MaxValue;
                for (int a = index / 8 + 1, nesto = index % 8 + 1; a < 8 && nesto < 8; a++, nesto++)
                {
                    if (Zafateni.ZafateniMesta[a][nesto])
                    {
                        if (min > a)
                        {
                            Tocki[1][0] = a;
                            Tocki[1][1] = nesto;
                            min = a;
                        }
                        barEdno = false;
                    }
                    else if (barEdno)
                    {
                        Tocki[1][0] = a;
                        Tocki[1][1] = nesto;
                    }
                }
                barEdno = (index / 8 + 1 < 8 && index % 8 - 1 >= 0);
                min = Int32.MaxValue;
                for (int a = index / 8 + 1, nesto = index % 8 - 1; a < 8 && nesto >= 0; a++, nesto--)
                {
                    if (Zafateni.ZafateniMesta[a][nesto])
                    {
                        if (min > a)
                        {
                            Tocki[2][0] = a;
                            Tocki[2][1] = nesto;
                            min = a;
                        }
                        barEdno = false;
                    }
                    else if (barEdno)
                    {
                        Tocki[2][0] = a;
                        Tocki[2][1] = nesto;
                    }
                }
                min = Int32.MinValue;
                barEdno = (index / 8 - 1 >= 0 && index % 8 + 1 < 8);
                for (int a = index / 8 - 1, nesto = index % 8 + 1; a >= 0 && nesto < 8; a--, nesto++)
                {
                    if (Zafateni.ZafateniMesta[a][nesto])
                    {

                        if (min < a)
                        {
                            Tocki[3][0] = a;
                            Tocki[3][1] = nesto;
                            min = a;
                        }
                        barEdno = false;

                    }
                    else if (barEdno)
                    {
                        Tocki[3][0] = a;
                        Tocki[3][1] = nesto;
                    }
                }
                if (Tocki[0][0] == -1)
                {
                    Tocki[0][0] = index / 8;
                    Tocki[0][1] = index % 8;
                }
                if (Tocki[1][0] == -1)
                {
                    Tocki[1][0] = index / 8;
                    Tocki[1][1] = index % 8;
                }
                if (Tocki[2][0] == -1)
                {
                    Tocki[2][0] = index / 8;
                    Tocki[2][1] = index % 8;
                }
                if (Tocki[3][0] == -1)
                {
                    Tocki[3][0] = index / 8;
                    Tocki[3][1] = index % 8;
                }
                if (White)
                {
                    for (int i = Tocki[0][0], j = Tocki[0][1]; i <= Tocki[1][0] && j <= Tocki[1][1]; i++, j++)
                    {
                        if (!Zafateni.Whitte[i][j] || !Zafateni.ZafateniMesta[i][j])
                        {
                            Valid[i][j] = true;
                        }
                    }
                    for (int i = Tocki[2][0], j = Tocki[2][1]; i >= Tocki[3][0] && j <= Tocki[3][1]; i--, j++)
                    {
                        if (!Zafateni.Whitte[i][j] || !Zafateni.ZafateniMesta[i][j])
                        {
                            Valid[i][j] = true;
                        }
                    }

                }
                if (!(White))
                {
                    for (int i = Tocki[0][0], j = Tocki[0][1]; i <= Tocki[1][0] && j <= Tocki[1][1]; i++, j++)
                    {
                        if (Zafateni.Whitte[i][j] || !Zafateni.ZafateniMesta[i][j])
                        {
                            Valid[i][j] = true;
                        }
                    }
                    for (int i = Tocki[2][0], j = Tocki[2][1]; i >= Tocki[3][0] && j <= Tocki[3][1]; i--, j++)
                    {
                        if (Zafateni.Whitte[i][j] || !Zafateni.ZafateniMesta[i][j])
                        {
                            Valid[i][j] = true;
                        }
                    }
                }
                Valid[index / 8][index % 8] = false;
            }
            else if (t.Equals(DifFigures.tip.Queen))
            {
                DifFigures Top = new DifFigures();
                Top.t = DifFigures.tip.Rook;
                Top.White = this.figura.White;
                ValidMoves pom1 = new ValidMoves(Top, this.index, this.Zafateni, Enpassant);
                DifFigures Lamfer = new DifFigures();
                Lamfer.t = DifFigures.tip.Bishop;
                Lamfer.White = this.figura.White;
                ValidMoves pom2 = new ValidMoves(Lamfer, this.index, this.Zafateni, Enpassant);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Valid[i][j] = (pom1.Valid[i][j] || pom2.Valid[i][j]);
                    }
                }
                Valid[index / 8][index % 8] = false;
            }
        }
    }
}








