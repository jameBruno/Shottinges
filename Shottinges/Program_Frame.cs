﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Forms;


namespace Shooting
{
    public partial class Program_Frame : Form
    {

        //public const ContextStaticAttribute int UP_PRESSED = 0x001;

        public static int UP_PRESSED = 0x001;
        public static int DOWN_PRESSED = 0x002;
        public static int LEFT_PRESSED = 0x004;
        public static int RIGHT_PRESSED = 0x008;
        public static int FIRE_PRESSED = 0x010;

        // 스레드 객체
        Thread mainwork;

        Boolean loof = true;

        Random rnd = new Random();
        
        public int status;
        public int cnt;
        public int delay;
        public long pretime;
        public int keybuff;

        public int statuse;

        public int key;
        
        public int keys;

        // 게임용 변수
        
        public int score;
        public int mylife;
        public int gamecnt;
        public int scrspeed = 16;
        public int level;

        public int myx, myy;
        public int myspeed;
        public int mydegree;

        public int mywidth, myheight;
        public int mymode = 1;
        public int myimg;
        public int mycnt;
        public Boolean myshoot = false;
        public int myshield;

        public int gScreenWidth = 640;
        public int gScreenHeight = 480;

        public ArrayList bullets = new ArrayList();
        public ArrayList enemies = new ArrayList();
        public ArrayList effects = new ArrayList();
        public ArrayList items = new ArrayList();





        /*

        Program_Frame()
        {









        }


        */






        // 플레이어
        private System.Drawing.Point Player;

        private System.Drawing.Point Players;

        private System.Drawing.Point Enemy;

        private System.Drawing.Point Zig_Enemy;

        // 점수 증가
        private System.Drawing.Point Support;

        private int Score = 0;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Program_Frame
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Program_Frame";
            this.Text = "s";
            this.ResumeLayout(false);

        }

        Random random;
        
        
        public Program_Frame()
        {
            // 플레이어 좌표값 초기화
            Player.X = 150;
            Player.Y = 350;
            
            Enemy.X = 100;
            Enemy.Y = 0;

            Zig_Enemy.X = 200;
            Zig_Enemy.Y = 0;

            Support.X = 300;
            Support.Y = 0;

            random = new Random();


        }





    

    }


}
