using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO;
using System.Reflection;


namespace Шах___Проект
{

    public partial class Form1 : Form
    {
        public int kliknato = 0;
        public int kliknato2 = 0;
        public bool ShowValid = true;
        public PictureBox[] figures = new PictureBox[64];
        public DifFigures[] Ostanati = new DifFigures[64];
        public bool[] PlayerTurn = new bool[4];
        public int[] Dva = new int[1];
        int Keep = 0;
        public bool FinishedPlayer = false;
        public TakenPlaces matrica = new TakenPlaces();
        public bool[] Castling = new bool[8];
        public List<DifFigures.tip> BlackTaken = new List<DifFigures.tip>();
        public List<DifFigures.tip> WhiteTaken = new List<DifFigures.tip>();
        public bool[][] Enpassant = new bool[2][];
        public bool[] voSah = new bool[2];
        public int tajmer1 = 0;
        public int tajmer2 = 0;
        public String prvIgrac;
        public String vtorIgrac;

        public Form1()
        {
            InitializeComponent();
          
          

            this.StartPosition = FormStartPosition.CenterScreen;
            startButton.BackColor = Color.FromArgb(255, 228, 225);
            textBox1.BackColor = Color.FromArgb(255, 250, 240);
            textBox2.BackColor = Color.FromArgb(255, 250, 240);
            BackColor = Color.FromArgb(255, 250, 240);
            menuStrip1.BackColor = Color.FromArgb(255, 250, 240);
            
            this.Icon = new System.Drawing.Icon(Properties.Resources.icon, new Size(20, 20));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Enpassant[0] = new bool[8];
 
            // 0 za Napraven Castling, 1 za Mrdnat Kral, 2  i 3 za Mrdnati Topovi soodvetno (lev i desen)
            // 5 za Napraven Castling od strana na Crnite, 6 za Mrdnat Kral, 7 i 8 za Mrdnati Topovi soodvetno (lev i desen)
            for (int i = 0; i < 8; i++)
            {
                Castling[i] = true;
            }
            for (int i = 0; i < 2; i++)
            {
                Enpassant[i] = new bool[8];
                for (int j = 0; j < 8; j++)
                {
                    Enpassant[i][j] = new bool();
                }

            }

            // Inicijaliziranje na Topovi
            Ostanati[0] = new DifFigures();
            Ostanati[7] = new DifFigures();
            Ostanati[56] = new DifFigures();
            Ostanati[63] = new DifFigures();
            Ostanati[56].t = DifFigures.tip.Rook;
            Ostanati[56].White = true;
            Ostanati[63].t = DifFigures.tip.Rook;
            Ostanati[63].White = true;
            Ostanati[0].t = DifFigures.tip.Rook;
            Ostanati[0].White = false;
            Ostanati[7].t = DifFigures.tip.Rook;
            Ostanati[7].White = false;

            // Inicijaliziranje na Konji
            Ostanati[1] = new DifFigures();
            Ostanati[6] = new DifFigures();
            Ostanati[57] = new DifFigures();
            Ostanati[62] = new DifFigures();
            Ostanati[1].t = DifFigures.tip.Knight;
            Ostanati[1].White = false;
            Ostanati[6].t = DifFigures.tip.Knight;
            Ostanati[6].White = false;
            Ostanati[57].t = DifFigures.tip.Knight;
            Ostanati[57].White = true;
            Ostanati[62].t = DifFigures.tip.Knight;
            Ostanati[62].White = true;
            //Inicijaliziranje na lamferi
            Ostanati[2] = new DifFigures();
            Ostanati[5] = new DifFigures();
            Ostanati[58] = new DifFigures();
            Ostanati[61] = new DifFigures();
            Ostanati[2].t = DifFigures.tip.Bishop;
            Ostanati[2].White = false;
            Ostanati[5].t = DifFigures.tip.Bishop;
            Ostanati[5].White = false;
            Ostanati[58].t = DifFigures.tip.Bishop;
            Ostanati[58].White = true;
            Ostanati[61].t = DifFigures.tip.Bishop;
            Ostanati[61].White = true;
            //Inicijaliziranje Kralici
            Ostanati[3] = new DifFigures();
            Ostanati[59] = new DifFigures();
            Ostanati[3].t = DifFigures.tip.Queen;
            Ostanati[3].White = false;
            Ostanati[59].t = DifFigures.tip.Queen;
            Ostanati[59].White = true;
            //Inicijaliziranje Kralovi
            Ostanati[4] = new DifFigures();
            Ostanati[60] = new DifFigures();
            Ostanati[4].t = DifFigures.tip.King;
            Ostanati[4].White = false;
            Ostanati[60].t = DifFigures.tip.King;
            Ostanati[60].White = true;


            for (int i = 16; i < 48; i++)
            {
                Ostanati[i] = new DifFigures();
                Ostanati[i].White = false;
                Ostanati[i].t = DifFigures.tip.None;
            }

            for (int i = 8; i < 16; i++)
            {
                Ostanati[i] = new DifFigures();
                Ostanati[i].t = DifFigures.tip.Pawn;
                Ostanati[i].White = false;
            }

            for (int i = 48; i < 56; i++)
            {
                Ostanati[i] = new DifFigures();
                Ostanati[i].t = DifFigures.tip.Pawn;
                Ostanati[i].White = true;
            }
            for (int i = 56; i < 64; i++)
            {
                Ostanati[i].White = true;
            }

            int temp2 = 0;
            int temp = 10;

            for (int i = 0; i < 64; i++)
            {
                figures[i] = new PictureBox();

                figures[i].SetBounds((i - temp2) * 50 + 10, temp, 30, 30);
                if (i % 8 == 7)
                {
                    temp += 50;
                    temp2 += 8;
                }
                figures[i].BackColor = Color.Transparent;
                this.Controls.Add(figures[i]);
            }


            for (int i = 0; i < 16; i++)
            {

                figures[i].Image = Properties.Resources.ResourceManager.GetObject("Piece" + i) as Image;
                figures[i].SizeMode = PictureBoxSizeMode.StretchImage;
                figures[i].BackColor = Color.Transparent;
            }

            for (int i = 48; i < 64; i++)
            {
                figures[i].Image = Properties.Resources.ResourceManager.GetObject("Piece" + i) as Image;
                figures[i].SizeMode = PictureBoxSizeMode.StretchImage;
                figures[i].BackColor = Color.Transparent;
                this.Controls.Add(figures[i]);
            }
            for (int i = 0; i < 32; i++)
            {
                Ostanati[i].White = false;
            }
            Ostanati[63].White = Ostanati[62].White = Ostanati[61].White = Ostanati[60].White = Ostanati[59].White = true;
            foreach (var p in figures)
            {

                p.MouseClick += this.figureHandlers;

            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics et = e.Graphics;
            Brush blackB = new SolidBrush(Color.Brown);
            int x = 0;
            int y = 0;
            Pen pen = new Pen(Color.Black, 50);
            et.DrawRectangle(pen, new Rectangle(0, 0, 400, 400));

            for (int i = 1; i < 33; i++)
            {

                et.FillRectangle(blackB, x, y, 50, 50);
                x += 100;

                if (i % 8 == 0)
                {
                    y += 50;
                    x = 0;
                }
                else if (i % 4 == 0)
                {
                    y += 50;
                    x = 50;
                }
            }

            Brush blackT = new SolidBrush(Color.FromArgb(238, 221, 187));
            x = 50;
            y = 0;
            for (int i = 1; i < 33; i++)
            {

                et.FillRectangle(blackT, x, y, 50, 50);
                x += 100;
                if (i % 8 == 0)
                {
                    x = 50;
                    y += 50;
                }
                else if (i % 4 == 0)
                {
                    x = 0;
                    y += 50;
                }
            }
            Font drawF = new Font("Arial", 18);
            int prom = 10;
            for (int i = 8; i > 0; i--)
            {
                et.DrawString(i.ToString(), drawF, new SolidBrush(Color.White), new PointF(400, prom));
                prom += 50;
            }
            prom = 10;
            for (int i = 97; i < 105; i++)
            {
                char p = (char)i;


                et.DrawString(p.ToString(), drawF, new SolidBrush(Color.White), new PointF(prom, 398));
                prom += 50;
            }

        }
        public void DrawIt()
        {
            Graphics et = this.CreateGraphics();
            ValidMoves k = new ValidMoves(Ostanati[kliknato], kliknato, matrica, Enpassant);
            Brush pinkt = new SolidBrush(Color.FromArgb(255, 182, 193));


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    {
                        if (k.Valid[i][j])
                        {
                            if (i % 2 == 0 && j % 2 == 0 || i % 2 == 1 && j % 2 == 1)
                                et.DrawRectangle(new Pen(new SolidBrush(Color.BlanchedAlmond), 3), j * 50 + 2, i * 50 + 2, 44, 44);
                            if (i % 2 == 1 && j % 2 == 0 || i % 2 == 0 && j % 2 == 1)
                                et.DrawRectangle(new Pen(new SolidBrush(Color.Brown), 3), j * 50 + 2, i * 50 + 2, 44, 44);


                        }
                        else
                        {
                            if (i % 2 == 0 && j % 2 == 0 || i % 2 == 1 && j % 2 == 1)
                            {
                                et.DrawRectangle(new Pen(new SolidBrush(Color.Brown), 3), j * 50 + 2, i * 50 + 2, 44, 44);
                            }
                            if (i % 2 == 1 && j % 2 == 0 || i % 2 == 0 && j % 2 == 1)
                            {
                                et.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(238, 221, 187)), 3), j * 50 + 2, i * 50 + 2, 44, 44);
                            }

                        }

                    }

                }
            }
            if (Ostanati[k.index].t != DifFigures.tip.None)
            {
                et.DrawRectangle(new Pen(new SolidBrush(Color.Black), 3), k.index % 8 * 50 + 2, k.index / 8 * 50 + 2, 44, 44);
            }
        }
        public void SpecialDraw()
        {
            Graphics et = this.CreateGraphics();
            for (int i = 0; i < 8; i++)
            {
                if (i == 0 || i == 1 || i == 6 || i == 7)
                {

                    for (int j = 0; j < 8; j++)
                    {
                        if (i % 2 == 0 && j % 2 == 0 || i % 2 == 1 && j % 2 == 1)
                        {
                            et.DrawRectangle(new Pen(new SolidBrush(Color.Brown), 3), j * 50 + 2, i * 50 + 2, 44, 44);
                        }
                        if (i % 2 == 1 && j % 2 == 0 || i % 2 == 0 && j % 2 == 1)
                        {
                            et.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(238, 221, 187)), 3), j * 50 + 2, i * 50 + 2, 44, 44);
                        }
                    }
                }
            }
        }
        void figureHandlers(object sender, EventArgs e)
        {
            PictureBox ptt = (PictureBox)sender;
            ValidMoves k = new ValidMoves(Ostanati[kliknato], kliknato, matrica, Enpassant);

            if (PlayerTurn[3])
            {
                kliknato2 = Array.IndexOf(figures, ptt);
                if (kliknato2 == -1)
                {
                    // Ако кликнеш надвор од шахот
                    kliknato2 = 0;
                    PlayerTurn[2] = true;
                    PlayerTurn[3] = false;

                }
                else if (k.Zafateni.Whitte[kliknato2 / 8][kliknato2 % 8] == false)
                {
                    // Ако се премислиш која фигура да ја поместиш
                    PlayerTurn[2] = true;
                }
                if ((kliknato2 / 8 == 3 && Dva[0] / 8 == 1 && matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] && Ostanati[Dva[0]].t == DifFigures.tip.Pawn))
                {
                    // Валидирање за enpassant move
                    Enpassant[1][Dva[0] % 8] = true;
                    for (int i = 0; i < 8; i++)
                    {
                        if (Enpassant[1][i] && i != Dva[0] % 8)
                        {
                            Enpassant[1][i] = false;
                            // Сите останати enpassant се false
                        }
                    }
                }

                // Рокада за црн лев топ && крал
                if (Castling[4] && Castling[5] && (Castling[6] || Castling[7]))
                {

                    if (((kliknato2 == 0 && Dva[0] == 4) || (kliknato2 == 4 && Dva[0] == 0)) && Castling[6])
                    {

                        Castling t = new Castling(matrica, false, true);
                        if (t.ValidirajCastle())
                        {


                            figures[Dva[0]].Image = null;
                            figures[2].Image = Properties.Resources.Piece4;
                            figures[2].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[3].Image = Properties.Resources.Piece0;
                            figures[3].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[kliknato2].Image = null;

                            for (int i = 5; i < 8; i++)
                            {
                                Castling[i] = false;
                            }
                            FinishedPlayer = false;
                            PlayerTurn[3] = false;
                            PlayerTurn[0] = true;
                            this.SpecialDraw();
                            DifFigures pomosna = Ostanati[Dva[0]];
                            DifFigures pomosna2 = Ostanati[kliknato2];

                            Ostanati[3].t = DifFigures.tip.Rook;
                            Ostanati[2].t = DifFigures.tip.King;
                            Ostanati[0].t = DifFigures.tip.None;
                            Ostanati[4].t = DifFigures.tip.None;
                            matrica.ZafateniMesta[0][2] = true;
                            matrica.ZafateniMesta[0][3] = true;
                            matrica.ZafateniMesta[0][0] = false;
                            matrica.ZafateniMesta[0][4] = false;
                            matrica.Whitte[0][2] = false;
                            matrica.Whitte[0][3] = false;
                            Ostanati[2].White = false;
                            Ostanati[3].White = false;

                            label1.Text = "White's Turn";

                        }
                    }
                    // Рокада за крал и десен црн топ
                    else if (((kliknato2 == 4 && Dva[0] == 7) || (kliknato2 == 7 && Dva[0] == 4)) && Castling[7])
                    {
                        Castling t = new Castling(matrica, false, false);

                        if (t.ValidirajCastle())
                        {
                            figures[Dva[0]].Image = null;
                            figures[5].Image = Properties.Resources.Piece0;
                            figures[5].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[6].Image = Properties.Resources.Piece4;
                            figures[6].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[kliknato2].Image = null;

                            for (int i = 5; i < 8; i++)
                            {
                                Castling[i] = false;
                            }

                            FinishedPlayer = false;
                            PlayerTurn[3] = false;
                            PlayerTurn[0] = true;
                            // Да ги отселектира валидните потези
                            this.SpecialDraw();
                            DifFigures pomosna = Ostanati[Dva[0]];
                            DifFigures pomosna2 = Ostanati[kliknato2];

                            Ostanati[5].t = DifFigures.tip.Rook;
                            Ostanati[6].t = DifFigures.tip.King;
                            Ostanati[7].t = DifFigures.tip.None;
                            Ostanati[4].t = DifFigures.tip.None;
                            matrica.ZafateniMesta[0][5] = true;
                            matrica.ZafateniMesta[0][6] = true;
                            matrica.ZafateniMesta[0][4] = false;
                            matrica.ZafateniMesta[0][7] = false;
                            matrica.Whitte[0][5] = false;
                            matrica.Whitte[0][6] = false;
                            Ostanati[5].White = false;
                            Ostanati[6].White = false;

                            label1.Text = "White's Turn";
                        }
                    }
                }
                // Проверка дали спротивниот крал е во Шах позиција
                if (k.figura.t == DifFigures.tip.King)
                {
                    matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                    matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                    matrica.Whitte[kliknato2 / 8][kliknato2 % 8] = false;
                    SahMat t = new SahMat(kliknato2, Ostanati, matrica, false);
                    if (t.Value())
                    {
                        k.Valid[kliknato2 / 8][kliknato2 % 8] = false;
                    }

                    matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = false;
                }

                // Менување pictureboxes и зачувани фигури во array листата
                if (k.Valid[kliknato2 / 8][kliknato2 % 8])
                {

                    figures[kliknato2].Image = Pom1.Image;
                    figures[kliknato2].SizeMode = PictureBoxSizeMode.StretchImage;
                    figures[Dva[0]].Image = null;
                    bool GoIma = false;
                    int indGoIma = 0;
                    TakenFigures pomosna = new TakenFigures();
                    // Додавање во листа, доколку е земена фигура
                    if (Ostanati[kliknato2].t != DifFigures.tip.None)
                    {
                        TakenFigures l = new TakenFigures();
                        foreach (TakenFigures p in listBox2.Items)
                        {
                            pomosna = (TakenFigures)p;
                            if (pomosna.v.Equals(Ostanati[kliknato2].t))
                            {
                                GoIma = true;
                                indGoIma = listBox2.Items.IndexOf(p);
                                l = p;
                            }
                        }


                        if (GoIma)
                        {
                            listBox2.Items.Remove(l);
                            l.taken++;
                            listBox2.Items.Add(l);
                        }

                        if (!GoIma)
                        {
                            WhiteTaken.Add(Ostanati[kliknato2].t);
                            if (Ostanati[kliknato2].t == DifFigures.tip.Pawn)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Pawn, 1));
                            }
                            else if (Ostanati[kliknato2].t == DifFigures.tip.Knight)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Knight, 1));
                            }
                            else if (Ostanati[kliknato2].t == DifFigures.tip.Bishop)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Bishop, 1));
                            }
                            else if (Ostanati[kliknato2].t == DifFigures.tip.Rook)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Rook, 1));
                            }
                            else if (Ostanati[kliknato2].t == DifFigures.tip.Queen)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Queen, 1));
                            }
                            else if (Ostanati[kliknato2].t == DifFigures.tip.King)
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.King, 1));
                            }
                        }

                    }

                    // Ако сум го поместил кралот, или некој од топовите, оневозможување на рокада
                    if (FinishedPlayer)
                    {
                        if (Dva[0] == 4)
                        {
                            Castling[5] = false;
                        }
                        if (Dva[0] == 0)
                        {
                            Castling[6] = false;
                        }
                        if (Dva[0] == 7)
                        {
                            Castling[7] = false;
                        }
                        bool enpa = false;

                        // За enpassante дали е валиден
                        if ((Dva[0] % 8 != kliknato2 % 8 && Dva[0] / 8 == 4 && kliknato2 / 8 == 5 && Ostanati[kliknato2].t == DifFigures.tip.None && Ostanati[Dva[0]].t == DifFigures.tip.Pawn && Ostanati[kliknato2 - 8].t == DifFigures.tip.Pawn))
                        {

                            Ostanati[kliknato2 - 8].t = DifFigures.tip.None;
                            Ostanati[kliknato2].t = DifFigures.tip.Pawn;
                            Ostanati[Dva[0]].t = DifFigures.tip.None;
                            if (ShowValid)
                            {
                                this.DrawIt();
                            }

                            matrica.ZafateniMesta[kliknato2 / 8 - 1][kliknato2 % 8] = false;
                            matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                            matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                            Ostanati[kliknato2].White = false;
                            figures[kliknato2 - 8].Image = null;
                            enpa = true;
                            PlayerTurn[3] = false;
                            PlayerTurn[0] = true;
                            FinishedPlayer = false;
                            bool goIma = false;

                            TakenFigures t = new TakenFigures();
                            foreach (TakenFigures p in listBox2.Items)
                            {
                                if (p.v.Equals(DifFigures.tip.Pawn))
                                {
                                    goIma = true;
                                    t = p;
                                }
                            }
                            if (goIma)
                            {
                                listBox2.Items.Remove(t);
                                t.taken++;
                                listBox2.Items.Add(t);
                            }
                            else
                            {
                                listBox2.Items.Add(new TakenFigures(DifFigures.tip.Pawn, 1));
                            }
                        }
                        // Ако enpa е true, тогаш е завршен потегот и не треба да влегува во овој иф
                        if (!enpa)
                        {
                            Ostanati[kliknato2].t = Ostanati[Dva[0]].t;
                            Ostanati[kliknato2].White = false;
                            Ostanati[Dva[0]].White = false;
                            Ostanati[Dva[0]].t = DifFigures.tip.None;
                            PlayerTurn[3] = false;
                            PlayerTurn[0] = true;
                            FinishedPlayer = false;
                            matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                            matrica.Whitte[kliknato2 / 8][kliknato2 % 8] = false;
                            matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                            if (ShowValid)
                            {
                                this.DrawIt();
                            }
                        }

                        label1.Text = "White's Turn";

                    }

                    bool Nekoja = false;
                    // Го барам белиот крал за да проверам дали е во Шах позиција, доколку не го најдам, завршена е играта
                    for (int i = 0; i < 64; i++)
                    {
                        if (Ostanati[i].t == DifFigures.tip.King && Ostanati[i].White)
                        {
                            indGoIma = i;
                            Nekoja = true;
                        }
                    }


                    if (Nekoja)
                    {

                        SahMat Krall = new SahMat(indGoIma, Ostanati, matrica, true);
                        Krall.Val();

                    }
                    else if (!Nekoja)
                    {
                        PlayerTurn[0] = PlayerTurn[1] = PlayerTurn[2] = PlayerTurn[3] = false;
                        label1.Text = "Victory!!!";
                        File.AppendAllText("HighScores.txt", Environment.NewLine + vtorIgrac + " " + textBox2.Text + " " + prvIgrac + " " + textBox1.Text);
                        MessageBox.Show("" + vtorIgrac + " победи! Честитки!", "Победа");


                    }


                    // Доколку пионот стигне на другиот крај од шаховската табла
                    if (kliknato2 / 8 == 7 && Ostanati[kliknato2].t == DifFigures.tip.Pawn)
                    {
                        MessageBox.Show("Pawn Promotion! Одберете друга фигура којашто сакате да ја замените наместо пионот");
                        PawnPromotion Promotion = new PawnPromotion(this, false, kliknato2, BlackTaken);
                        Promotion.Show();
                    }

                }

            }

            if (PlayerTurn[2] && FinishedPlayer)
            {
                Keep = 0;
                k = new ValidMoves(Ostanati[kliknato], kliknato, matrica, Enpassant);
                Pom1.Image = figures[kliknato].Image;
                kliknato = Array.IndexOf(figures, ptt);
                if (Keep == 1) Keep = 0;
                Dva[Keep] = kliknato;
                Keep++;

                // Ако сум одбрал црна фигура и белите фигури завршиле
                if (!Ostanati[kliknato].t.Equals(DifFigures.tip.None) && PlayerTurn[2] && Ostanati[kliknato].White == false)
                {

                    PictureBox pttt = (PictureBox)sender;
                    Pom1.Image = pttt.Image;

                    PlayerTurn[3] = true;
                    PlayerTurn[2] = false;
                    if (ShowValid)
                        DrawIt();

                }
                /*   else if (!PlayerTurn[3] && (Ostanati[kliknato].t.Equals(DifFigures.tip.None) || Ostanati[kliknato].White))
                      {
                          PlayerTurn[1] = false;
                      } */
            }
            else if (PlayerTurn[1])
            {
                kliknato2 = Array.IndexOf(figures, ptt);


                if (kliknato2 == -1)
                {
                    kliknato2 = 0;
                    PlayerTurn[1] = false;
                    PlayerTurn[0] = true;
                }
                // Aко кликнам бело, селектирај ја новата фигура
                else if (k.Zafateni.Whitte[kliknato2 / 8][kliknato2 % 8] == true)
                {
                    PlayerTurn[0] = true;
                }
                // Enpassante за бели фигури
                if ((kliknato2 / 8 == 4 && Dva[0] / 8 == 6 && matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] && Ostanati[Dva[0]].t == DifFigures.tip.Pawn))
                {
                    try
                    {

                        if (kliknato % 8 - 1 >= 0 && matrica.ZafateniMesta[4][kliknato2 % 8 - 1] && !matrica.Whitte[4][kliknato2 % 8 - 1] ||
                            (kliknato % 8 + 1 < 8) && matrica.ZafateniMesta[4][kliknato2 % 8 + 1] && !matrica.Whitte[4][kliknato2 % 8 + 1])
                        {

                            Enpassant[0][Dva[0] % 8] = true;
                            for (int i = 0; i < 8; i++)
                            {
                                if (Enpassant[0][i] && i != Dva[0] % 8)
                                {
                                    Enpassant[0][i] = false;
                                }
                            }
                        }
                    }
                    catch (Exception ent)
                    {
                    }


                }



                // Рокада за бел крал и десен бел топ
                if (Castling[0] && Castling[1] && (Castling[2] || Castling[3]))
                {
                    if (((kliknato2 == 63 && Dva[0] == 60) || (kliknato2 == 60 && Dva[0] == 63)) && Castling[3])
                    {

                        Castling t = new Castling(matrica, true, false);
                        if (t.ValidirajCastle())
                        {
                            this.SpecialDraw();
                            figures[Dva[0]].Image = null;
                            figures[61].Image = Properties.Resources.Piece63;
                            figures[61].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[62].Image = Properties.Resources.Piece60;
                            figures[62].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[kliknato2].Image = null;

                            for (int i = 0; i < 4; i++)
                            {
                                Castling[i] = false;
                            }
                            FinishedPlayer = true;
                            PlayerTurn[1] = false;
                            PlayerTurn[2] = true;

                            DifFigures pomosna = Ostanati[Dva[0]];
                            DifFigures pomosna2 = Ostanati[kliknato2];

                            Ostanati[61].t = DifFigures.tip.Rook;
                            Ostanati[62].t = DifFigures.tip.King;
                            Ostanati[60].t = DifFigures.tip.None;
                            Ostanati[63].t = DifFigures.tip.None;
                            matrica.ZafateniMesta[7][5] = true;
                            matrica.ZafateniMesta[7][6] = true;
                            matrica.ZafateniMesta[7][7] = false;
                            matrica.ZafateniMesta[7][4] = false;
                            matrica.Whitte[7][5] = true;
                            matrica.Whitte[7][6] = true;
                            Ostanati[61].White = true;
                            Ostanati[62].White = true;

                            label1.Text = "Black's Turn";
                        }
                    }
                    // Рокада за лев бел топ и бел крал
                    else if (((kliknato2 == 56 && Dva[0] == 60) || (kliknato2 == 60 && Dva[0] == 56)) && Castling[2])
                    {

                        Castling t = new Castling(matrica, true, true);

                        if (t.ValidirajCastle())
                        {
                            this.SpecialDraw();
                            figures[Dva[0]].Image = null;
                            figures[58].Image = Properties.Resources.Piece60;
                            figures[58].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[59].Image = Properties.Resources.Piece63;
                            figures[59].SizeMode = PictureBoxSizeMode.StretchImage;
                            figures[kliknato2].Image = null;

                            for (int i = 0; i < 4; i++)
                            {
                                Castling[i] = false;
                            }
                            FinishedPlayer = true;
                            PlayerTurn[1] = false;
                            PlayerTurn[2] = true;

                            DifFigures pomosna = Ostanati[Dva[0]];
                            DifFigures pomosna2 = Ostanati[kliknato2];

                            Ostanati[59].t = DifFigures.tip.Rook;
                            Ostanati[58].t = DifFigures.tip.King;
                            Ostanati[56].t = DifFigures.tip.None;
                            Ostanati[60].t = DifFigures.tip.None;
                            matrica.ZafateniMesta[7][2] = true;
                            matrica.ZafateniMesta[7][3] = true;
                            matrica.ZafateniMesta[7][0] = false;
                            matrica.ZafateniMesta[7][4] = false;
                            matrica.Whitte[7][2] = true;
                            matrica.Whitte[7][3] = true;
                            Ostanati[59].White = true;
                            Ostanati[58].White = true;

                            label1.Text = "Black's Turn";
                        }
                    }


                }
                // Проверка дали кралот е уште во игра
                if (k.figura.t == DifFigures.tip.King)
                {
                    matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                    matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                    matrica.Whitte[kliknato2 / 8][kliknato2 % 8] = true;
                    SahMat t = new SahMat(kliknato2, Ostanati, matrica, true);
                    if (t.Value())
                    {
                        k.Valid[kliknato2 / 8][kliknato2 % 8] = false;

                    }


                    matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = false;


                }


                if (k.Valid[kliknato2 / 8][kliknato2 % 8] && !FinishedPlayer)
                {

                    figures[kliknato2].Image = Pom1.Image;
                    figures[kliknato2].SizeMode = PictureBoxSizeMode.StretchImage;
                    figures[Dva[0]].Image = null;
                    TakenFigures pomosna = new TakenFigures();
                    bool GoIma = false;
                    int indGoIma = 0;

                    // Ако е земена фигура додај ја во листа
                    if (Ostanati[kliknato2].t != DifFigures.tip.None)
                    {
                        TakenFigures l = new TakenFigures();
                        foreach (TakenFigures p in listBox1.Items)
                        {
                            pomosna = (TakenFigures)p;

                            if (pomosna.v.Equals(Ostanati[kliknato2].t))
                            {

                                GoIma = true;
                                indGoIma = listBox1.Items.IndexOf(p);
                                l = p;
                            }
                        }
                        // Ако ја има фигурата зголеми го само бројот во листата
                        if (GoIma)
                        {
                            listBox1.Items.Remove(l);
                            pomosna.taken++;
                            listBox1.Items.Add(l);
                        }
                        // Ако не, додај 
                        if (!GoIma)
                        {
                            BlackTaken.Add(Ostanati[kliknato2].t);
                            if (Ostanati[kliknato2].t == DifFigures.tip.Pawn)
                            {
                                listBox1.Items.Add(new TakenFigures(DifFigures.tip.Pawn, 1));

                            }
                            else
                                if (Ostanati[kliknato2].t == DifFigures.tip.Knight)
                                {
                                    listBox1.Items.Add(new TakenFigures(DifFigures.tip.Knight, 1));
                                }
                                else if (Ostanati[kliknato2].t == DifFigures.tip.Bishop)
                                {
                                    listBox1.Items.Add(new TakenFigures(DifFigures.tip.Bishop, 1));
                                }
                                else if (Ostanati[kliknato2].t == DifFigures.tip.Rook)
                                {
                                    listBox1.Items.Add(new TakenFigures(DifFigures.tip.Rook, 1));
                                }
                                else if (Ostanati[kliknato2].t == DifFigures.tip.Queen)
                                {
                                    listBox1.Items.Add(new TakenFigures(DifFigures.tip.Queen, 1));
                                }
                                else if (Ostanati[kliknato2].t == DifFigures.tip.King)
                                {
                                    listBox1.Items.Add(new TakenFigures(DifFigures.tip.King, 1));
                                }
                        }
                    }
                    // Ако е поместен топ или крал, постави го соодветната булова за рокада false
                    if (!FinishedPlayer)
                    {
                        if (Dva[0] == 60)
                        {
                            Castling[1] = false;
                        }
                        if (Dva[0] == 56)
                        {
                            Castling[2] = false;
                        }
                        if (Dva[0] == 63)
                        {
                            Castling[3] = false;
                        }
                        bool enpa = false;
                        // Enpassante движење
                        if ((Dva[0] % 8 != kliknato2 % 8 && Dva[0] / 8 == 3 && kliknato2 / 8 == 2 && Ostanati[kliknato2].t == DifFigures.tip.None && Ostanati[Dva[0]].t == DifFigures.tip.Pawn && Ostanati[kliknato2 + 8].t == DifFigures.tip.Pawn))
                        {
                            Ostanati[kliknato2 + 8].t = DifFigures.tip.None;
                            Ostanati[kliknato2].t = DifFigures.tip.Pawn;
                            Ostanati[Dva[0]].t = DifFigures.tip.None;

                            matrica.ZafateniMesta[kliknato2 / 8 + 1][kliknato2 % 8] = false;
                            matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                            matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                            Ostanati[kliknato2].White = true;
                            figures[kliknato2 + 8].Image = null;
                            enpa = true;
                            PlayerTurn[1] = false;
                            PlayerTurn[2] = true;
                            FinishedPlayer = true;
                            if (ShowValid)
                            {
                                this.DrawIt();
                            }

                            bool goIma = false;

                            TakenFigures t = new TakenFigures();
                            foreach (TakenFigures p in listBox1.Items)
                            {
                                if (p.v.Equals(DifFigures.tip.Pawn))
                                {
                                    goIma = true;
                                    t = p;
                                }
                            }
                            // Ако ја има, зголеми бројач 
                            if (goIma)
                            {
                                listBox1.Items.Remove(t);
                                t.taken++;
                                listBox1.Items.Add(t);
                            }
                            else
                            {
                                listBox1.Items.Add(new TakenFigures(DifFigures.tip.Pawn, 1));
                            }

                        }
                        // Ако не се случило enpassante движење (односно не е завршен играчот)
                        if (!enpa & k.Valid[kliknato2 / 8][kliknato2 % 8])
                        {
                            Ostanati[kliknato2].t = Ostanati[Dva[0]].t;
                            Ostanati[kliknato2].White = true;
                            Ostanati[Dva[0]].White = false;
                            Ostanati[Dva[0]].t = DifFigures.tip.None;
                            PlayerTurn[1] = false;
                            PlayerTurn[2] = true;
                            FinishedPlayer = true;
                            matrica.ZafateniMesta[kliknato2 / 8][kliknato2 % 8] = true;
                            matrica.Whitte[kliknato2 / 8][kliknato2 % 8] = true;
                            matrica.ZafateniMesta[Dva[0] / 8][Dva[0] % 8] = false;
                            matrica.Whitte[Dva[0] / 8][Dva[0] % 8] = false;

                            if (ShowValid)
                            {
                                this.DrawIt();
                            }

                        }
                        bool Nekoja = false;



                        // Барај го кралот
                        for (int i = 0; i < 64; i++)
                        {
                            if (Ostanati[i].t == DifFigures.tip.King && !Ostanati[i].White)
                            {
                                indGoIma = i;
                                Nekoja = true;
                            }
                        }
                        // Провери дали е во шах
                        label1.Text = "Black's Turn";
                        if (Nekoja)
                        {

                            SahMat Krall = new SahMat(indGoIma, Ostanati, matrica, false);
                            Krall.Val();


                        }
                        // Кралот е погубен
                        else if (!Nekoja)
                        {
                            PlayerTurn[0] = PlayerTurn[1] = PlayerTurn[2] = PlayerTurn[3] = false;
                            label1.Text = "Victory!!!";
                            MessageBox.Show("" + prvIgrac + " победи! Честитки!", "Победа");
                            File.AppendAllText("HighScores.txt", Environment.NewLine + prvIgrac + " " + textBox1.Text + " " + vtorIgrac + " " + textBox2.Text);

                        }
                        // Пиончето стигнало на крајот од шаховската табла
                        if (kliknato2 / 8 == 0 && Ostanati[kliknato2].t == DifFigures.tip.Pawn)
                        {
                            MessageBox.Show("Pawn Promotion! Одберете друга фигура којашто сакате да ја замените наместо пионот");
                            PawnPromotion Promotion = new PawnPromotion(this, true, kliknato2, WhiteTaken);
                            Promotion.Show();
                        }


                    }

                }
            }

            if (PlayerTurn[0])
            {
                k = new ValidMoves(Ostanati[kliknato], kliknato, matrica, Enpassant);

                Pom1.SizeMode = PictureBoxSizeMode.StretchImage;
                Pom1.Image = figures[kliknato].Image;
                kliknato = Array.IndexOf(figures, ptt);

                if (Keep >= 1) Keep = 0;
                Dva[Keep] = kliknato;
                Keep++;
                if (Keep == 1) Keep = 0;
                if (kliknato == -1)
                {
                    kliknato = 0;
                    PlayerTurn[2] = false;
                    PlayerTurn[0] = true;
                }

                else if (!Ostanati[kliknato].t.Equals(DifFigures.tip.None) && PlayerTurn[0] && Ostanati[kliknato].White == true)
                {
                    if (ShowValid)
                    {
                        this.DrawIt();
                    }
                    PictureBox pttt = (PictureBox)sender;
                    Pom1.Image = pttt.Image;
                    PlayerTurn[1] = true;
                    PlayerTurn[0] = false;

                }
                // Ако е кликнато нешто што не е бела фигура
                else if (!PlayerTurn[1] && (Ostanati[kliknato].t.Equals(DifFigures.tip.None) || !Ostanati[kliknato].White))
                {
                    PlayerTurn[1] = false;
                }

            }

        }
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            f.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            f.Show();
        }
        public void removeItem1(DifFigures.tip r)
        {
            TakenFigures t = new TakenFigures();
            TakenFigures l = new TakenFigures();
            foreach (var p in listBox1.Items)
            {


                t = (TakenFigures)p;
                if (t.v.Equals(r))
                {
                    l = t;
                }
            }
            listBox1.Items.Remove(l);
            l.taken--;
            if (l.taken > 0)
            {
                listBox1.Items.Add(l);
            }


        }
        public void removeItem2(DifFigures.tip r)
        {
            TakenFigures t = new TakenFigures();
            TakenFigures l = new TakenFigures();
            foreach (var p in listBox2.Items)
            {

                t = (TakenFigures)p;
                if (t.v.Equals(r))
                {
                    l = t;
                }


            }
            listBox2.Items.Remove(l);
            l.taken--;

            if (l.taken > 0)
            {
                listBox2.Items.Add(l);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            AboutForm ab = new AboutForm();
            ab.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Text.Equals("White's Turn"))
            {
                timer1.Enabled = true;
                textBox1.Text = String.Format("{0:00}", tajmer1 / 60) + ":" + String.Format("{0:00}", tajmer1 % 60);
                tajmer1++;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label1.Text.Equals("Black's Turn"))
            {
                timer2.Enabled = true;

                textBox2.Text = String.Format("{0:00}", tajmer2 / 60) + ":" + String.Format("{0:00}", tajmer2 % 60);
                tajmer2++;
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            
            ImeiPrezime prviot = new ImeiPrezime("првиот");
            prviot.StartPosition = FormStartPosition.CenterParent;
            prviot.ShowDialog();
            prvIgrac = prviot.igrac();

            ImeiPrezime vtoriot = new ImeiPrezime("вториот");
            vtoriot.StartPosition = FormStartPosition.CenterParent;
            vtoriot.ShowDialog();
            vtorIgrac = vtoriot.igrac();
            timer1.Enabled = true;
            startButton.Enabled = false;
            PlayerTurn[0] = true;
        }
        private void покажиВалидниПотезиToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (покажиВалидниПотезиToolStripMenuItem.Checked)
            {
                покажиВалидниПотезиToolStripMenuItem.Checked = false;
                ShowValid = false;
            }
            else
            {
                покажиВалидниПотезиToolStripMenuItem.Checked = true;
                ShowValid = true;
            }
        }
        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighScores l = new HighScores();
            l.StartPosition = FormStartPosition.CenterParent;
            l.Show();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo u = new Undo();
            u.ShowDialog();
            опцииToolStripMenuItem.DropDownItems.Remove(undoToolStripMenuItem);
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog();
        }

  


    }
}