using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TankGAME
{
    class NewTank 
    {
        bool Up = true, Down = false, right = false, left = false;
        public bool shooted { get; set; }
        Point point1 = new Point();
        public PictureBox[] gun = new PictureBox[5], walls = new PictureBox[2 * 5];
        public PictureBox PicNewTank = new PictureBox();
        Random random = new Random();
        Timer timer = new Timer(), timerAttact = new Timer(), Randomize = new Timer();
        int width, height;
        Form1 f1 = new Form1();
        int tw = 40, th = 40;                            // Tank Width and Tank Height
        int gc = 5;                                      // Guns Count  
        int ww = 700, wh = 25;                            // Wall Width and Height
        int[] locationsX = new int[2] { 700, 200 };
        int[] locationsY = new int[2] { 600, 450 };

        public NewTank(int w, int h, PictureBox[] walls)
        {
            width = w;
            height = h;
            this.walls = walls;
            point1 = PicNewTank.Location;
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer1_Tick);
            Randomize.Interval = 1000;
            Randomize.Tick += new EventHandler(Randomize_Tick);
            timerAttact.Interval = 1;
            for (int i = 0; i < gc; i++)
            {
                timerAttact.Tick += new EventHandler(timerAttact_Tick);
                gun[i] = new PictureBox();
                gun[i].SetBounds(10, 10, 10, 10);
                gun[i].BackColor = Color.Red;
                gun[i].Tag = 0;
                gun[i].Visible = false;
            }
            int locationX = locationsX[random.Next(2)];
            int locationY = locationsY[random.Next(2)];

            PicNewTank.SetBounds(locationX, locationY, tw, th);
            PicNewTank.BackgroundImage = Properties.Resources.RedTank;
            PicNewTank.BackgroundImageLayout = ImageLayout.Stretch;
            PicNewTank.BackColor = Color.Transparent;
            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            point1 = PicNewTank.Location;
        }

        ~NewTank()
        {
            //MessageBox.Show("Removed!");
        }

        public void Start()
        {
            Randomize.Start();
        }

        public void Remove()
        {
            timer.Stop();
            timerAttact.Stop();
            Randomize.Stop();
        }
        private void Randomize_Tick(object sender, EventArgs e)
        {
            Moving();
        }

        bool HaveWallUp = false, HaveWallRight = false, HaveWallDown = false, HaveWallLeft = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!HaveWallUp && Up)
            {
                if (point1.Y > 40)
                {
                    point1.Y--;
                }
                else
                {
                    point1.Y++;
                    HaveWallUp = true;
                    Moving();
                    return;
                }
            }
            else
            {
                HaveWallUp = false;
            }
            if (!HaveWallRight && right)
            {
                if (point1.X < width - 1.5 * PicNewTank.Width - 40)
                {
                    point1.X++;
                }
                else
                {
                    point1.X--;
                    HaveWallRight = true;
                    Moving();
                    return;
                }
            }
            else
            {
                HaveWallRight = false;
            }
            if (!HaveWallDown && Down)
            {
                if (point1.Y < height - 2 * PicNewTank.Height - 40 - 40)
                {
                    point1.Y++;
                }
                else
                {
                    point1.Y--;
                    HaveWallDown = true;
                    Moving();
                    return;
                }
            }
            else
            {
                HaveWallDown = false;
            }
            if (!HaveWallLeft && left)
            {
                if (point1.X > 40)
                {
                    point1.X--;
                }
                else
                {
                    point1.X++;
                    HaveWallLeft = true;
                    Moving();
                    return;
                }
            }
            else
            {
                HaveWallLeft = false;
            }


            for (int i = 0; i < walls.Length; i++)
            {
                if (i != 2 && i != 4 && i != 6)
                {
                    if (point1.X + tw / 2 >= walls[i].Location.X - tw / 2 &&
                                point1.Y + th / 2 >= walls[i].Location.Y - th / 2 &&
                                point1.X + tw / 2 <= walls[i].Location.X + ww + tw / 2 &&
                                point1.Y + th / 2 <= walls[i].Location.Y + wh + tw / 2)
                    {
                        if (Up)
                        {
                            point1.Y++;
                            HaveWallUp = true;
                        }
                        if (right)
                        {
                            point1.X--;
                            HaveWallRight = true;
                        }
                        if (Down)
                        {
                            point1.Y--;
                            HaveWallDown = true;
                        }
                        if (left)
                        {
                            point1.X++;
                            HaveWallLeft = true;
                        }
                    }
                }
                else
                {
                    if (point1.X + tw / 2 >= walls[i].Location.X - tw / 2 &&
                                point1.Y + th / 2 >= walls[i].Location.Y - th / 2 &&
                                point1.X + tw / 2 <= walls[i].Location.X + wh + tw / 2 &&
                                point1.Y + th / 2 <= walls[i].Location.Y + ww + tw / 2)
                    {
                        if (Up)
                        {
                            point1.Y++;
                            HaveWallUp = true;
                        }
                        if (right)
                        {
                            point1.X--;
                            HaveWallRight = true;
                        }
                        if (Down)
                        {
                            point1.Y--;
                            HaveWallDown = true;
                        }
                        if (left)
                        {
                            point1.X++;
                            HaveWallLeft = true;
                        }
                    }
                }
            }
            PicNewTank.Location = point1;
        }

        int last = 4;
        private void Moving()
        {
            if (Up && HaveWallUp)
            {
                int[] keys = new int[2] { 4, 2 };
                int key = keys[random.Next(2)];
                switch (key)
                {
                    case 2:
                        right = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        left = Up = Down = false;
                        break;
                    case 4:
                        left = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        right = Up = Down = false;
                        break;
                }
            }
            if (right && HaveWallRight)
            {
                int[] keys = new int[2] { 1, 3 };
                int key = keys[random.Next(2)];
                switch (key)
                {
                    case 1:
                        Up = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        Down = right = left = false;
                        break;
                    case 3:
                        Down = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Up = right = left = false;
                        break;
                }
            }
            if (Down && HaveWallDown)
            {
                int[] keys = new int[2] { 2, 4 };
                int key = keys[random.Next(2)];
                switch (key)
                {
                    case 2:
                        right = true; 
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        left = Up = Down = false;
                        break;
                    case 4:
                        left = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        right = Up = Down = false;
                        break;
                }
            }
            if (left && HaveWallLeft)
            {
                int[] keys = new int[2] { 3, 1 };
                int key = keys[random.Next(2)];
                switch (key)
                {
                    case 3:
                        Down = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        Up = right = left = false;
                        break;
                    case 1:
                        Up = true;
                        PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Down = right = left = false;
                        break;
                }
            }

            timer.Start();
            if (Math.Abs(Form1.point.X - PicNewTank.Location.X) < 20 ||
                Math.Abs(Form1.point.Y - PicNewTank.Location.Y) < 20)
            {
                if (Math.Abs(Form1.point.X - PicNewTank.Location.X) < 20)
                {
                    if (Form1.point.Y > PicNewTank.Location.Y)
                    {
                        if(Up)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (right)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (left)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        Down = true;
                        Up = right = left = false;
                    }
                    else
                    {
                        if (right)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (Down)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (left)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        Up = true;
                        Down = right = left = false;
                    }
                }
                if (Math.Abs(Form1.point.Y - PicNewTank.Location.Y) < 20)
                {
                    if (Form1.point.X > PicNewTank.Location.X)
                    {
                        if (Up)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (Down)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        if (left)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        right = true;
                        left = Up = Down = false;
                    }
                    else
                    {
                        if (right)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        if (Down)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        if (Up)
                            PicNewTank.BackgroundImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        left = true;
                        right = Up = Down = false;
                    }
                }

                for (int i = 0; i < gc; i++)
                {
                    if (!gun[i].Visible)
                    {
                        gun[i].Location = new Point(PicNewTank.Location.X + 15, PicNewTank.Location.Y + 15);
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
                        gun[last].Location = new Point(PicNewTank.Location.X + 15, PicNewTank.Location.Y + 15);
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
            }
            else
            {
                //timerAttact.Dispose();
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
                    if (gun[guncount].Location.X + 1 < width-40 && !HaveWall)
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
                    if (gun[guncount].Location.Y + 1 < height - 80-40 && !HaveWall)
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
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            timer.Stop();
        }
    }
}
