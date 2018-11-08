using System;
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

        public static int UP_PRESSED    = 0x001;
        public static int DOWN_PRESSED  = 0x002;
        public static int LEFT_PRESSED  = 0x004;
        public static int RIGHT_PRESSED = 0x008;
        public static int FIRE_PRESSED  = 0x010;

        // 스레드 객체
        Thread mainwork;

        Boolean loof = true;

        Random rnd = new Random();
        
        public int status = 0;  // 게임 상태 - 0 : 대기 / 1 : 1p / 2 : 2p / 3 : 게임오버
        public int cnt;
        public int delay;
        public long pretime;
        public int keybuff;

        public int statuse;

        public int key;
        

        // 게임용 변수
        
        public int score;   // 현재 점수
        public int mylife;  // 남은 life
        public int gamecnt;
        public int scrspeed = 16;   // 프레임 단위시간
        public int level = 1;   // 현재 레벨

        public int myX, myY ;// 캐릭터 좌표
        public int myspeed;
        public int mydegree;

        public int myWidth, myHeight;   // 캐릭터 크기
        public int mymode;  // 플레이 모드(1p / 2p)
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
        public PictureBox myImg;

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
            //Player.X = 150;
            //Player.Y = 350;

            //Enemy.X = 100;
            //Enemy.Y = 0;

            //Zig_Enemy.X = 200;
            //Zig_Enemy.Y = 0;

            //Support.X = 300;
            //Support.Y = 0;

            myX = 150;
            myY = 350;
            random = new Random();
            this.Controls.Clear();
            
            // 플레이어생성
            myImg = new PictureBox();
            myImg.Load(@"C:\Users\J\source\repos\Shottinges\Shottinges\Image\플레이어\player.png");
            myImg.Location = new System.Drawing.Point(myX, myY);
            myImg.Name = "player1";
            myImg.Size = new System.Drawing.Size(150, 120);
            myImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            myImg.TabIndex = 1;
            myImg.TabStop = false;
            this.Controls.Add(myImg);

        }

        public void systemInit()
        {
            mymode = 1;
        }



    

    }


}
