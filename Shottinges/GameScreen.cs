using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.ComponentModel;
using OpenCvSharp;
using OpenCvSharp.UserInterface;
using OpenCvSharp.Extensions;
using System.Threading;


namespace Shooting
{
    class GameScreen
    {

        Program_Frame main;

        int cnt, gamecnt;

        int x, y;

        Image dblBuff;

        Graphics gc;


        public Image bg;                            //배경화면
        public Image bg_f;
        public Image[] cloud = new Image[5];           //구름
        public Image title;                         //타이틀화면
        public Image title_key;
        
        public Image[] chr = new Image[9];
        public Image[] enemy = new Image[5];
        public Image[] bullet = new Image[5];
        public Image explo;
        public Image[] item = new Image[3];
        

        public Image _start;
        public Image _over;
        public Image shield;

        

        public Font font;

        public int[] v = { -2, -1, 0, 1, 2, 1, 0, -1 };
        public int[] v2 = { -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3, -4, -5, -6, -7 };
        public int step = 0;


        public GameScreen(Program_Frame main)
        {
            this.main = main;
            font = new Font("Default", 12, FontStyle.Bold);
        }

        public void Paint(Graphics g)
        {
            if (gc == null)
            {
                Graphics.FromImage();
                
            }

        }




















    }
}
