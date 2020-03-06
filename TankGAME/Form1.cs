using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TankGAME
{
    public partial class Form1 : Form
    {
        bool Up = true, Down = false, right = false, left = false, ended = false;
        public static Point point = new Point();
        Point[][] WallLoc = new System.Drawing.Point[3][];
        PictureBox[] gun = new PictureBox[5], walls = new PictureBox[2 * 5]; // gun count = 5, wall count = gun count * 2
        List<NewTank> newTanks = new List<NewTank>();
        int tw = 40, th = 40;                                     // Tank Width and Tank Height
        int gc = 5, Point = 0;                                      // Guns Count and Point Player 
        int ww = 700, wh = 25;                                     // Wall Width and Height
        Random random = new Random();


        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1050, 750);
            timer1.Tick += new EventHandler(timer1_Tick);
            FillWallLoc();
            int walli = 1;
            for (int i = 0; i < gc; i++)
            {
                timerAttact.Tick += new EventHandler(timerAttact_Tick);
                gun[i] = new PictureBox();
                gun[i].SetBounds(10, 10, 10, 10);
                gun[i].BackColor = Color.Green;
                gun[i].Tag = 0;
                gun[i].Visible = false;
                this.Controls.Add(gun[i]);
                gun[i].BringToFront();
                walls[2 * i] = new PictureBox();
                walls[2 * i + 1] = new PictureBox();
                walls[2 * i].Location = WallLoc[walli][2 * i];
                walls[2 * i + 1].Location = WallLoc[walli][2 * i + 1];
                walls[2 * i].Size = new Size(ww, wh);
                walls[2 * i + 1].Size = new Size(ww, wh);
                walls[2 * i].BackgroundImage = Properties.Resources.wall;
                walls[2 * i + 1].BackgroundImage = Properties.Resources.wall;
                if (2 * i == 2||2*i==4||2*i==6)
                {
                    walls[2 * i].Size=new Size(wh,ww);
                }
            }
            this.Controls.AddRange(walls);
            PicTank.SetBounds(450, 600, tw, th);
            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            point = PicTank.Location;
            ShootCheck.Start();
        }

        private void FillWallLoc()
        {
            WallLoc[0] = new Point[10] { new Point(50, 200), new Point(400, 200), new Point(750, 200),
                new Point(50, 400), new Point(400, 400), new Point(200, 500), new Point(750, 400),
                new Point(600, 500), new Point(200, 100), new Point(600, 100) };

            WallLoc[1] = new Point[10] { new Point(100, 100), new Point(-250, 200), new Point(875, -300),
                new Point(100, 300), new Point(1000, 0), new Point(0, 650), new Point(500, 100),
                new Point(600, 675), new Point(600, 500), new Point(-250, 400) };

            WallLoc[2] = new Point[10] { new Point(50, 100), new Point(550, 100), new Point(300, 200),
                new Point(800, 200), new Point(50, 300), new Point(550, 300), new Point(300, 400),
                new Point(800, 400), new Point(50, 500), new Point(550, 500) };
        }

        bool HaveWall = false;
        string way = "Up";
        private void timer1_Tick(object sender, EventArgs e)
        {
            point = PicTank.Location;
            if (!HaveWall)
            {
                if (Up && point.Y > 40)
                {
                    point.Y--;
                }
                if (Down && point.Y < this.Height - 2 * PicTank.Height - panel1.Height-40)
                {
                    point.Y++;
                }
                if (right && point.X < this.Width - 1.5 * PicTank.Width-40)
                {
                    point.X++;
                }
                if (left && point.X > 40)
                {
                    point.X--;
                }
            }
            else
            {
                if (way == "Up")
                {
                    point.Y += 2;
                }
                if (way == "Down")
                {
                    point.Y -= 2;
                }
                if (way == "right")
                {
                    point.X -= 2;
                }
                if (way == "left")
                {
                    point.X += 2;
                }
                HaveWall = false;
            }

            for (int i = 0; i < walls.Length; i++)
            {
                if (i != 2&&i!=4&&i!=6)
                {
                    if (point.X + tw / 2 >= walls[i].Location.X - tw / 2 &&
                                point.Y + th / 2 >= walls[i].Location.Y - th / 2 &&
                                point.X + tw / 2 <= walls[i].Location.X + ww + tw / 2 &&
                                point.Y + th / 2 <= walls[i].Location.Y + wh + tw / 2)
                    {
                        HaveWall = true;
                        if (Up)
                        {
                            way = "Up";
                        }
                        if (Down)
                        {
                            way = "Down";
                        }
                        if (right)
                        {
                            way = "right";
                        }
                        if (left)
                        {
                            way = "left";
                        }
                    }
                }
                else
                {
                    if (point.X + tw / 2 >= walls[i].Location.X - tw / 2 &&
                                point.Y + th / 2 >= walls[i].Location.Y - th / 2 &&
                                point.X + tw / 2 <= walls[i].Location.X + wh + tw / 2 &&
                                point.Y + th / 2 <= walls[i].Location.Y + ww + tw / 2)
                    {
                        HaveWall = true;
                        if (Up)
                        {
                            way = "Up";
                        }
                        if (Down)
                        {
                            way = "Down";
                        }
                        if (right)
                        {
                            way = "right";
                        }
                        if (left)
                        {
                            way = "left";
                        }
                    }
                }
            }

            PicTank.Location = point;

            if (newTankcount < tankCount)
            {
                newTanks.Add(new NewTank(this.Width, this.Height, walls));
                this.Controls.Add(newTanks[newTankcount].PicNewTank);
                this.Controls.AddRange(newTanks[newTankcount].gun);
                for (int j = 0; j < gc; j++)
                {
                    newTanks[newTankcount].gun[j].BringToFront();
                }
                newTanks[newTankcount].PicNewTank.BringToFront();
                newTanks[newTankcount].Start();
                newTankcount++;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int last = 4;
        int newTankcount = 0, tankCount = 5;



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!ended)
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        Up = true;
                        if (Down)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (right)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (left)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Down = right = left = false;
                        timer1.Start();
                        break;
                    case Keys.Down:
                        Down = true;
                        if (Up)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (left)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (right)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Up = right = left = false;
                        timer1.Start();
                        break;
                    case Keys.Right:
                        right = true;
                        if (left)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (Down)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (Up)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        left = Up = Down = false;
                        timer1.Start();
                        break;
                    case Keys.Left:
                        left = true;
                        if (right)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (Up)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (Down)
                            PicTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        right = Up = Down = false;
                        timer1.Start();
                        break;
                    case Keys.Space:
                        for (int i = 0; i < gc; i++)
                        {
                            if (!gun[i].Visible)
                            {
                                gun[i].Location = new Point(PicTank.Location.X + 15, PicTank.Location.Y + 15);
                                if (Up)
                                    gun[i].Tag = 1;
                                if (right)
                                    gun[i].Tag = 2;
                                if (Down)
                                    gun[i].Tag = 3;
                                if (left)
                                    gun[i].Tag = 4;
                                gun[i].Visible = true;
                                break;
                            }
                            if (i == last)
                            {
                                if (last < gc - 1)
                                {
                                    last++;
                                }
                                else
                                {
                                    last = 0;
                                }
                                gun[last].Location = new Point(PicTank.Location.X + 15, PicTank.Location.Y + 15);
                                if (Up)
                                    gun[last].Tag = 1;
                                if (right)
                                    gun[last].Tag = 2;
                                if (Down)
                                    gun[last].Tag = 3;
                                if (left)
                                    gun[last].Tag = 4;
                                gun[last].Visible = true;
                                break;
                            }
                        }

                        timerAttact.Start();
                        break;
                }
        }

        int guncount = 0;
        private void timerAttact_Tick(object sender, EventArgs e)
        {
            bool HaveWall = false;

            if (guncount < gun.Length - 1)
            {
                guncount++;
            }
            else
            {
                guncount = 0;
            }

            for (int i = 0; i < walls.Length; i++)
            {
                if (i != 2 && i != 4 && i != 6)
                {
                    if (gun[guncount].Location.X + 5 >= walls[i].Location.X &&
                                gun[guncount].Location.Y + 5 >= walls[i].Location.Y &&
                                gun[guncount].Location.X + 5 <= walls[i].Location.X + ww &&
                                gun[guncount].Location.Y + 5 <= walls[i].Location.Y + wh)
                    {
                        HaveWall = true;
                    }
                }
                else
                {
                    if (gun[guncount].Location.X + 5 >= walls[i].Location.X &&
                                gun[guncount].Location.Y + 5 >= walls[i].Location.Y &&
                                gun[guncount].Location.X + 5 <= walls[i].Location.X + wh &&
                                gun[guncount].Location.Y + 5 <= walls[i].Location.Y + ww)
                    {
                        HaveWall = true;
                    }
                }
            }


            for (int i = 0; i < 2; i++)
            {
                if ((int)gun[guncount].Tag == 1)
                {
                    if (gun[guncount].Location.Y - 1 > 40 && !HaveWall)
                    {
                        gun[guncount].Location = new Point(gun[guncount].Location.X, gun[guncount].Location.Y - 1);
                    }
                    else
                    {
                        gun[guncount].Visible = false;
                    }
                }
                if ((int)gun[guncount].Tag == 2)
                {
                    if (gun[guncount].Location.X + 1 < this.Width-40 && !HaveWall)
                    {
                        gun[guncount].Location = new Point(gun[guncount].Location.X + 1, gun[guncount].Location.Y);
                    }
                    else
                    {
                        gun[guncount].Visible = false;
                    }
                }
                if ((int)gun[guncount].Tag == 3)
                {
                    if (gun[guncount].Location.Y + 1 < this.Height - 2 * panel1.Height-40 && !HaveWall)
                    {
                        gun[guncount].Location = new Point(gun[guncount].Location.X, gun[guncount].Location.Y + 1);
                    }
                    else
                    {
                        gun[guncount].Visible = false;
                    }
                }
                if ((int)gun[guncount].Tag == 4)
                {
                    if (gun[guncount].Location.X - 1 > 40 && !HaveWall)
                    {
                        gun[guncount].Location = new Point(gun[guncount].Location.X - 1, gun[guncount].Location.Y);
                    }
                    else
                    {
                        gun[guncount].Visible = false;
                    }
                }

                if (gun[guncount].Visible)
                {
                    for (int i1 = 0; i1 < newTankcount; i1++)
                    {
                        if (gun[guncount].Location.X + 5 >= newTanks[i1].PicNewTank.Location.X &&
                            gun[guncount].Location.Y + 5 >= newTanks[i1].PicNewTank.Location.Y &&
                            gun[guncount].Location.X + 5 <= newTanks[i1].PicNewTank.Location.X + tw &&
                            gun[guncount].Location.Y + 5 <= newTanks[i1].PicNewTank.Location.Y + th)
                        {
                            gun[guncount].Visible = false;
                            Bang(newTanks[i1]);
                            this.Controls.Remove(newTanks[i1].PicNewTank);
                            for (int j = 0; j < gc; j++)
                            {
                                this.Controls.Remove(newTanks[i1].gun[j]);
                            }
                            newTanks[i1].Remove();
                            newTanks.Remove(newTanks[i1]);
                            newTankcount--;
                            Point++;
                            lblPoint.Text = $"Point: {Point}";
                            break;
                        }
                    }
                }
            }


        }

        private void Bang(NewTank tank)
        {
            PictureBox bang = new PictureBox();
            bang.SetBounds(tank.PicNewTank.Location.X, tank.PicNewTank.Location.Y, tw, th);
            bang.BackgroundImageLayout = ImageLayout.Stretch;
            bang.BackgroundImage = Properties.Resources.EndRedTank;
            this.Controls.Add(bang);
            bang.SendToBack();
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Stop();
        }


        private void ShootCheck_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < newTankcount; i++)
            {
                for (int j = 0; j < gun.Length; j++)
                {
                    if (newTanks[i].gun[j].Visible)
                    {
                        if (newTanks[i].gun[j].Location.X + 5 >= PicTank.Location.X &&
                            newTanks[i].gun[j].Location.Y + 5 >= PicTank.Location.Y &&
                            newTanks[i].gun[j].Location.X + 5 <= PicTank.Location.X + tw &&
                            newTanks[i].gun[j].Location.Y + 5 <= PicTank.Location.Y + th)
                        {
                            newTanks[i].gun[i].Visible = false;
                            ShootCheck.Stop();
                            timer1.Stop();
                            timerAttact.Stop();
                            newTanks.Clear();
                            PicTank.BackgroundImage = Properties.Resources.EndTank;
                            lblOver.Visible = true;
                            lblRestart.Visible = true;
                            lblOver.BringToFront();
                            lblRestart.BringToFront();
                            ended = true;
                            goto end;
                        }
                    }
                }
            }
            end:;
        }
    }
}
