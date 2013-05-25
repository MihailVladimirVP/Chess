using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Шах___Проект
{
    class SahMat
    {
        public int idx;
        public DifFigures[] OstanatiFiguri;
        public bool White;
        public TakenPlaces Zafateni;
        bool[][] Enpassant = new bool[2][];
        List<int> pom = new List<int>();


        public SahMat(int idx, DifFigures[] OstanatiFiguri, TakenPlaces Zafateni, bool White)
        {
            this.idx = idx;
            this.OstanatiFiguri = OstanatiFiguri;
            this.White = White;
            this.Zafateni = Zafateni;
            for (int i = 0; i < 2; i++)
            {
                Enpassant[i] = new bool[8];
                List<int> pom = new List<int>();
            }


        }
        public void Val()
        {
            bool ednas = false;
            bool SahMat = false;
            if (!White)
            {
                // За секоја фигура што не е од моите, провери дали му е валидна позицијата на мојот крал
                for (int i = 0; i < 64; i++)
                {
                    if (OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                    {

                        ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                        if (k.Valid[idx / 8][idx % 8])
                        {


                            if (this.Mat() == false && !(ednas) && !(SahMat))
                            {
                                ednas = true;
                                MessageBox.Show("Нема валидни потези за вашиот крал. Поместете други фигури во одбрана на кралот, за да не ви биде одзеден!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            if (FinalSahMat(i) && !(SahMat) && this.Mat() == false)
                            {
                                SahMat = true;
                                MessageBox.Show("Шах Мат!");
                            }
                            if (!SahMat)
                            {
                                MessageBox.Show("Црниот крал е во \"ШАХ\" позиција од " + OstanatiFiguri[i].t, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Zafateni.ZafateniMesta[idx / 8][idx % 8] = false;
                            }
                            Zafateni.ZafateniMesta[idx / 8][idx % 8] = true;

                        }
                    }
                }
            }
            else if (White)
            {

                for (int i = 0; i < 64; i++)
                {
                    if (!OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                    {

                        ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                        if (k.Valid[idx / 8][idx % 8] && !SahMat)
                        {
                            if (this.Mat() == false && !(ednas) && !SahMat)
                            {
                                ednas = true;
                                MessageBox.Show("Нема валидни потези за вашиот крал. Поместете други фигури во одбрана на кралот, за да не ви биде одзеден!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                            if (FinalSahMat(i) && !(SahMat) && this.Mat() == false)
                            {
                                SahMat = true;
                                MessageBox.Show("Шах Мат!");
                            }
                            if (!SahMat)
                            {
                                MessageBox.Show("Белиот крал е во \"ШАХ\" позиција од " + OstanatiFiguri[i].t, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Zafateni.ZafateniMesta[idx / 8][idx % 8] = false;
                            }

                            Zafateni.ZafateniMesta[idx / 8][idx % 8] = true;

                        }
                    }
                }
            }
        }

        public bool Value()
        {
            // Провери за секое валидно место на коешто можам да го движам кралот, дали некоја фигура од спротивниот тим има пристап
            if (!White)
            {

                for (int i = 0; i < 64; i++)
                {

                    if (OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                    {

                        ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                        if (k.Valid[idx / 8][idx % 8])
                        {
                            MessageBox.Show("Вашиот крал е оставен под директен напад од " + OstanatiFiguri[i].t + " на таа позиција");
                            return true;
                        }
                    }

                }
                return false;
            }
            else
            {

                for (int i = 0; i < 64; i++)
                {

                    if (!OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                    {

                        ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                        if (k.Valid[idx / 8][idx % 8])
                        {
                            MessageBox.Show("Вашиот крал е оставен под директен напад од " + OstanatiFiguri[i].t + " на таа позиција");
                            return true;

                        }
                    }

                }
                return false;

            }

        }
        public bool Mat()
        {
            // Стави ги во листа сите валидни потези на мојот крал
            if (!White)
            {
                ValidMoves t = new ValidMoves(new DifFigures(false, DifFigures.tip.King), idx, Zafateni, Enpassant);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (t.Valid[i][j])
                        {
                            pom.Add(i * 8 + j);
                        }
                    }
                }


                bool[] pomosna = new bool[8];

                bool Nevalidno = false;
                foreach (int sekojIdx in pom)
                {
                    Nevalidno = false;
                    for (int i = 0; i < 64; i++)
                    {
                        // Доколку најдеш дека фигура од спротивниот тим има пристап до тоа поле, оневозможи ја валидноста на тоа поле
                        if (OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                        {

                            ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                            if (k.Valid[sekojIdx / 8][sekojIdx % 8])
                            {
                                Nevalidno = true;

                            }

                        }


                    }
                    if (!Nevalidno) return true;

                }
                return false;

            }
            else
            {
                ValidMoves t = new ValidMoves(new DifFigures(true, DifFigures.tip.King), idx, Zafateni, Enpassant);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (t.Valid[i][j])
                        {
                            pom.Add(i * 8 + j);
                        }
                    }
                }


                bool[] pomosna = new bool[8];

                bool Nevalidno = false;
                foreach (int sekojIdx in pom)
                {
                    Nevalidno = false;
                    for (int i = 0; i < 64; i++)
                    {

                        if (!OstanatiFiguri[i].White && OstanatiFiguri[i].t != DifFigures.tip.None)
                        {

                            ValidMoves k = new ValidMoves(OstanatiFiguri[i], i, Zafateni, Enpassant);
                            if (k.Valid[sekojIdx / 8][sekojIdx % 8])
                            {
                                Nevalidno = true;

                            }
                        }

                    }
                    if (!Nevalidno) return true;

                }
                return false;
            }
        }


        public bool FinalSahMat(int a)
        {
            if (White)
            {

                if (OstanatiFiguri[a].t == DifFigures.tip.Knight || OstanatiFiguri[a].t == DifFigures.tip.Pawn)
                {

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (OstanatiFiguri[i * 8 + j].White && OstanatiFiguri[i * 8 + j].t != DifFigures.tip.King)
                            {
                                ValidMoves r = new ValidMoves(OstanatiFiguri[i * 8 + j], i * 8 + j, Zafateni, Enpassant);

                                if (r.Valid[a / 8][a % 8])
                                {

                                    return false;
                                }

                            }
                        }
                    }
                    return true;
                }


                else if (OstanatiFiguri[a].t == DifFigures.tip.Rook)
                {

                    if (a / 8 == idx / 8)
                    {
                        for (int r = Math.Min(a % 8, idx % 8); r <= Math.Max(a % 8, idx % 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[a / 8][r]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int r = Math.Min(a / 8, idx / 8); r <= Math.Max(a / 8, idx / 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[r][a % 8]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }

                }
                else if (OstanatiFiguri[a].t == DifFigures.tip.Bishop)
                {

                    for (int p = Math.Min(idx / 8, a / 8), tt = Math.Max(idx % 8, a % 8); p < Math.Max(idx / 8, a / 8) && tt > Math.Min(idx % 8, a % 8); p++, tt--)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                if (OstanatiFiguri[b * 8 + c].White && OstanatiFiguri[b * 8 + c].t != DifFigures.tip.King)
                                {
                                    ValidMoves k = new ValidMoves(OstanatiFiguri[b * 8 + c], b * 8 + c, Zafateni, Enpassant);
                                    if (k.Valid[p][tt])
                                    {
                                        return false;
                                    }
                                }
                            }

                        }
                    }
                    return true;


                }
                else
                {
                    if ((idx / 8 > a / 8 && idx % 8 < a % 8) || (idx / 8 < a / 8 && idx % 8 > a % 8))
                    {
                        for (int p = Math.Min(idx / 8, a / 8), tt = Math.Max(idx % 8, a % 8); p < Math.Max(idx / 8, a / 8) && tt > Math.Min(idx % 8, a % 8); p++, tt--)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int c = 0; c < 8; c++)
                                {
                                    if (OstanatiFiguri[b * 8 + c].White && OstanatiFiguri[b * 8 + c].t != DifFigures.tip.King)
                                    {
                                        ValidMoves k = new ValidMoves(OstanatiFiguri[b * 8 + c], b * 8 + c, Zafateni, Enpassant);
                                        if (k.Valid[p][tt])
                                        {

                                            return false;
                                        }
                                    }
                                }

                            }
                        }
                        return true;
                    }
                    else if (a / 8 == idx / 8)
                    {
                        for (int r = Math.Min(a % 8, idx % 8); r <= Math.Max(a % 8, idx % 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[a / 8][r]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int r = Math.Min(a / 8, idx / 8); r <= Math.Max(a / 8, idx / 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[r][a % 8]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }

                }
            }
            else
            {


                if (OstanatiFiguri[a].t == DifFigures.tip.Knight || OstanatiFiguri[a].t == DifFigures.tip.Pawn)
                {

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (!OstanatiFiguri[i * 8 + j].White && OstanatiFiguri[i * 8 + j].t != DifFigures.tip.King)
                            {
                                ValidMoves r = new ValidMoves(OstanatiFiguri[i * 8 + j], i * 8 + j, Zafateni, Enpassant);

                                if (r.Valid[a / 8][a % 8])
                                {
                                    return false;
                                }

                            }
                        }
                    }
                    return true;
                }


                else if (OstanatiFiguri[a].t == DifFigures.tip.Rook)
                {

                    if (a / 8 == idx / 8)
                    {
                        for (int r = Math.Min(a % 8, idx % 8); r <= Math.Max(a % 8, idx % 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (!OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[a / 8][r]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int r = Math.Min(a / 8, idx / 8); r <= Math.Max(a / 8, idx / 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (!OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[r][a % 8]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }

                }
                else if (OstanatiFiguri[a].t == DifFigures.tip.Bishop)
                {

                    for (int p = Math.Min(idx / 8, a / 8), tt = Math.Max(idx % 8, a % 8); p < Math.Max(idx / 8, a / 8) && tt > Math.Min(idx % 8, a % 8); p++, tt--)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                if (!OstanatiFiguri[b * 8 + c].White && OstanatiFiguri[b * 8 + c].t != DifFigures.tip.King)
                                {
                                    ValidMoves k = new ValidMoves(OstanatiFiguri[b * 8 + c], b * 8 + c, Zafateni, Enpassant);
                                    if (k.Valid[p][tt])
                                    {
                                        return false;
                                    }
                                }
                            }

                        }
                    }
                    return true;


                }
                else
                {
                    if ((idx / 8 > a / 8 && idx % 8 < a % 8) || (idx / 8 < a / 8 && idx % 8 > a % 8))
                    {
                        for (int p = Math.Min(idx / 8, a / 8), tt = Math.Max(idx % 8, a % 8); p < Math.Max(idx / 8, a / 8) && tt > Math.Min(idx % 8, a % 8); p++, tt--)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int c = 0; c < 8; c++)
                                {
                                    if (!OstanatiFiguri[b * 8 + c].White && OstanatiFiguri[b * 8 + c].t != DifFigures.tip.King)
                                    {
                                        ValidMoves k = new ValidMoves(OstanatiFiguri[b * 8 + c], b * 8 + c, Zafateni, Enpassant);
                                        if (k.Valid[p][tt])
                                        {
                                            return false;
                                        }
                                    }
                                }

                            }
                        }
                        return true;
                    }
                    else if (a / 8 == idx / 8)
                    {
                        for (int r = Math.Min(a % 8, idx % 8); r <= Math.Max(a % 8, idx % 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (!OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[a / 8][r]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int r = Math.Min(a / 8, idx / 8); r <= Math.Max(a / 8, idx / 8); r++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                for (int t = 0; t < 8; t++)
                                {
                                    if (!OstanatiFiguri[b * 8 + t].White && OstanatiFiguri[b * 8 + t].t != DifFigures.tip.King)
                                    {
                                        ValidMoves kkk = new ValidMoves(OstanatiFiguri[b * 8 + t], b * 8 + t, Zafateni, Enpassant);
                                        if (kkk.Valid[r][a % 8]) return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }

                }




            }
        }
    }
}