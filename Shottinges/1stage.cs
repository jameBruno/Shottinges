using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shooting
{
    public partial class _1Stage : Form
    {
        public _1Stage()
        {
            InitializeComponent();
            worker = new Thread(new ThreadStart(Run));
            worker.Start();
        }

        public static int UP_PRESSED = 0x001;
        public static int DOWN_PRESSED = 0x002;
        public static int LEFT_PRESSED = 0x004;
        public static int RIGHT_PRESSED = 0x008;
        public static int FIRE_PRESSED = 0x010;

        public int status = 1;          // 게임 상태 - 0 : 대기 / 1 : 1p / 2 : 2p / 3 : 게임오버
        public int cnt;                 // 루프 제어용 컨트롤 변수
        public int delay;               // 루프 딜레이. 1/1000초 단위
        public long preTime;            // 루프 간격을 조절하기 위한 시간 체크값
        public int keybuff;             // 키 버퍼값
        //public int gameCnt;             // 게임 흐름 컨트롤

        //public int mymode;              // 플레이어 캐릭터 상태
        //public int myspeed;             // 플레이어 이동 속도
        //public int mydegree;            // 플레이어 이동 방향
        //public int myimg;               // 플레이어 이미지
        //public Boolean myshoot = false; // 총알 발사가 눌리고 있는지 확인하는 변수


        // 게임용 변수

        public int score;           // 현재 점수
        public int mylife;          // 남은 life
        public int gameCnt;         // 게임 흐름 컨트롤
        public int scrspeed = 16;   // 프레임 단위시간, 스크롤 속도
        public int level = 1;       // 현재 레벨

        public int myX, myY;       // 플레이어 좌표
        public int myspeed;         // 플레이어 이동 속도
        public int mydegree;        // 플레이어 이동 방향

        public int myWidth, myHeight;   // 캐릭터 크기, 플레이어 캐릭터의 너비 높이
        public int mymode;              // 플레이어 캐릭터 상태
        public int myimg;               // 플레이어 이미지
        public int mycnt;
        public Boolean myshoot = false; // 총알 발사가 눌리고 있는지 확인하는 변수
        //public int myshield;            

        Graphics g;

        public int myshield;            // 실드 남은 수비량

        public int gScreenWidth = 640;  // 게임 화면 너비
        public int gScreenHeight = 480; // 게임 화면 높이
        
        public ArrayList bullets = new ArrayList();     // 총알 관리, 총알의 갯수를 예상할 수 없기 때문에 가변적으로 관리한다.
        public ArrayList enemies = new ArrayList();     // 적 캐릭터 관리
        public ArrayList effects = new ArrayList();     // 이펙트 관리
        public ArrayList items = new ArrayList();       // 아이템 관리

        static int xNumberOfFighters = 3, yNumberOfFighters = 3;
        Enemy[,] fighter = new Enemy[xNumberOfFighters, yNumberOfFighters];

        //public Vector bullets = new Vector();

        int width, height;



        // 플레이어 변수

        public PictureBox myImg;

        private System.Drawing.Point Player;

        private System.Drawing.Point Players;

        private System.Drawing.Point Enemy;

        private System.Drawing.Point Zig_Enemy;

        // 점수 증가
        private System.Drawing.Point Support;

        private int Score = 0;

        //Random random;

        Random rnd = new Random();




        Boolean loop = true;        // 스레드 루프 정보

        Thread worker;              // 스레드 객체

        int play_x = 12;
        int play_y = 311;
        int playwidth = 144;
        int playheight = 113;

        /*
        private void GameForms_Paint(object sender, PaintEventArgs e)
        {
            Image image = Image.FromFile("D:\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\플레이어\\player.png");
            int x = 12;
            int y = 311;
            int width = 450;
            int height = 150;
            e.Graphics.DrawImage(image, x, y, width, height);

            Image images = Image.FromFile("D:\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\1stage\\1stage enemy2.png");
            int xPos = 687;
            int yPos = 12;
            int widths = 450;
            int heights = 150;
            e.Graphics.DrawImage(images, xPos, yPos, widths, heights);

            Image enemy = Image.FromFile("D:\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\1stage\\1stage enermy.png");
            int xPoes = 735;
            int yPoes = 487;
            int widthes = 450;
            int heightes = 150;
            e.Graphics.DrawImage(enemy, xPoes, yPoes, widthes, heightes);
                             


        }
        */

        Bitmap fighterOne = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\1stage\1stage enermy.png");
        Bitmap fighterTwo = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\1stage\1stage enemy2.png");
        Bitmap explotionOne = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\이펙트\explosion 1.png");
        Bitmap explotionTwo = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\이펙트\explosion 2.png");
        //Bitmap playerShipOne = new Bitmap(@"C:/Users/Administrator/Videos/팀 프로젝트/SpaceShooterGame-master/SpaceShooterGame-master/Resources/Player2.png");
        //Bitmap playerShipTwo = new Bitmap(@"C:/Users/Administrator/Videos/팀 프로젝트/SpaceShooterGame-master/SpaceShooterGame-master/Resources/Player2.png");
        Bitmap missile = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\총알,공격\attack1.png");
        Bitmap fireBall = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\총알,공격\bullet.png");
        //Bitmap starDistroyerPic = new Bitmap(@"C:/Users/Administrator/Videos/팀 프로젝트/SpaceShooterGame-master/SpaceShooterGame-master/Resources/1StageEnermy2.png");
        Bitmap starDistroyerBlank = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\아이템\powerup 1.png");
        Bitmap powerUpPic = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\아이템\powerup 2.png");
        Bitmap deathStarPic = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\1stage\1stage boss.png");
        Bitmap deathStarBlank = new Bitmap(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\아이템\100 scoreItem 1.png");
        
        


        // 이미지 폼 위에 그리기
        Image image = Image.FromFile(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\플레이어\player.png");
        Image images = Image.FromFile(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\1stage\1stage enemy2.png");
        Image enemy = Image.FromFile(@"D:\Shottinges-Bug_PlayerMove\Shottinges-Bug_PlayerMove\Shottinges\Images\1stage\1stage enermy.png");

        

        private void _1Stage_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, play_x, play_y, playwidth, playheight);          
            int xPos = 687;
            int yPos = 12;
            int widths = 149;
            int heights = 105;
            e.Graphics.DrawImage(images, xPos, yPos, widths, heights);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            int xPoes = 735;
            int yPoes = 487;
            int widthes = 122;
            int heightes = 98;
            e.Graphics.DrawImage(enemy, xPoes, yPoes, widthes, heightes);

        }

        // 스레드로 구동할 함수



        // 스레드로 구동할 함수
        public void Run()
        {
            try
            {
                while (loop)
                {
                    preTime = Environment.TickCount;

                    //gs.Paint();

                    KeyProcess();
                    buildFighters();
                    
                    process_ENEMY();
                    process();
                    
                    

                    Thread.Sleep(500);
                    Invalidate();

                    // 게임 루프 처리 시간을 delay값에서 차감 -> delay를 일정하게 유지
                    if (Environment.TickCount - preTime < delay)
                        //Thread.Sleep((int)(delay - Environment.TickCount + preTime));

                    if (4 != status)
                        cnt++;
                }
            }
            catch (Exception e)
            {
                return;
            }

        }



        /*
        public void Run()
        {
            try
            {
                while (loop)
                {
                    preTime = Environment.TickCount;

                    //gs.Paint();
                    KeyProcess();

                    Thread.Sleep(1000);
                    Invalidate();

                    // 게임 루프 처리 시간을 delay값에서 차감 -> delay를 일정하게 유지
                    if (Environment.TickCount - preTime < delay)
                    {

                    }

                   

                    if (4 != status)
                        cnt++;
                }
            }
            catch (Exception e)
            {
                return;
            }

        }
        */


        private void Init()
        {
            


        }
        

        // 키 이벤트 처리 구현 중

        
        private void _1Stage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    keybuff |= FIRE_PRESSED;
                    break;
                case Keys.Left:
                    keybuff |= LEFT_PRESSED;//멀티키의 누르기 처리
                    break;
                case Keys.Up:
                    keybuff |= UP_PRESSED;
                    break;
                case Keys.Right:
                    keybuff |= RIGHT_PRESSED;
                    break;
                case Keys.Down:
                    keybuff |= DOWN_PRESSED;
                    break;
                case Keys.D1:
                    if (myspeed > 1) myspeed--;
                    break;
                case Keys.D2:
                    if (myspeed < 9) myspeed++;
                    break;
                case Keys.D3:
                    if (status == 2) status = 4;
                    break;
                default:
                    break;
            }

            switch (e.KeyCode)
            {

                case Keys.Right:
                    //Point p = new Point(pictureBox4.Location.X + 20, pictureBox4.Location.Y);
                    //pictureBox4.Location = p;
                    //image.RotateFlip(RotateFlipType.RotateNoneFlipY + 20);


                    Point point = new Point(pictureBox5.Location.X + 20, pictureBox5.Location.Y);
                    pictureBox5.Location = point;

                    break;


            }


        }

        private void _1Stage_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)  //멀티키의 떼기 처리
            {
                case Keys.Space:
                    keybuff &= ~FIRE_PRESSED;
                    myshoot = true;
                    break;
                case Keys.Left:
                    keybuff &= ~LEFT_PRESSED;
                    break;
                case Keys.Up:
                    keybuff &= ~UP_PRESSED;
                    break;
                case Keys.Right:
                    keybuff &= ~RIGHT_PRESSED;
                    myspeed++;
                    break;
                case Keys.Down:
                    keybuff &= ~DOWN_PRESSED;
                    break;
            }
        }
        
        private void KeyProcess()// -> 키 종합 처리
        {
            switch (status)
            {
                case 0:// 대기 상태
                    if (keybuff == (int)Keys.D1)// -> 1p전환
                    {
                        myspeed--;
                        //Init_GAME();
                        //Init_MY();
                        //status = 1;
                    }
                    else if (keybuff == (int)Keys.D2)// -> 2p 전환
                    {
                        myspeed++;
                        //Init_GAME();
                        //Init_MY();
                        //status = 2;
                    }
                    break;
                case 1:

                case 2:
                    if (mymode == 2 || mymode == 0)
                    {
                        switch (keybuff)
                        {
                            case 0:
                                mydegree = -1;
                                myimg = 0;
                                break;
                            case 0x010:// -> FIRE_PRESSED
                                mydegree = -1;
                                myimg = 6;
                                break;
                            case 0x001:// -> UP_PRESSED
                                mydegree = 0;
                                myimg = 2;
                                myspeed++;
                                play_x += 30;
                                break;
                            case 0x001 | 0x010:// -> UP_PRESSED | FIRE_PRESSED
                                mydegree = 0;
                                myimg = 6;
                                break;
                            case 0x004:// -> LEFT_PRESSED
                                mydegree = 90;
                                myimg = 4;
                                myspeed--;
                                play_x -= 30;
                                break;
                            case 0x004 | 0x010:// -> LEFT_PRESSED | FIRE_PRESSED
                                mydegree = 90;
                                myimg = 6;
                                break;
                            case 0x008:// -> RIGHT_PRESSED
                                mydegree = 270;
                                myimg = 2;
                                myspeed++;
                                play_x += 30;
                                break;
                            case 0x008 | 0x010:// -> RIGHT_PRESSED | FIRE_PRESSED
                                mydegree = 270;
                                myimg = 6;
                                break;
                            case 0x001 | 0x004:// -> UP_PRESSED | LEFT_PRESSED
                                mydegree = 45;
                                myimg = 4;
                                break;
                            case 0x001 | 0x004 | 0x010:// -> UP_PRESSED | LEFT_PRESSED | FIRE_PRESSED
                                mydegree = 45;
                                myimg = 6;
                                break;
                            case 0x001 | 0x008:// -> UP_PRESSED | RIGHT_PRESSED
                                mydegree = 315;
                                myimg = 2;
                                break;
                            case 0x001 | 0x008 | 0x010:// -> UP_PRESSED | RIGHT_PRESSED | FIRE_PRESSED
                                mydegree = 315;
                                myimg = 6;
                                break;
                            case 0x002:// -> DOWN_PRESSED
                                mydegree = 180;
                                myimg = 2;
                                myspeed++;
                                play_y += 30;
                                break;
                            case 0x002 | 0x010:// -> DOWN_PRESSED | FIRE_PRESSED
                                mydegree = 180;
                                myimg = 6;
                                break;
                            case 0x002 | 0x004:// -> DOWN_PRESSED | LEFT_PRESSED
                                mydegree = 135;
                                myimg = 4;
                                break;
                            case 0x002 | 0x004 | 0x010:// -> DOWN_PRESSED | LEFT_PRESSED | FIRE_PRESSED
                                mydegree = 135;
                                myimg = 6;
                                break;
                            case 0x002 | 0x008:// -> DOWN_PRESSED | RIGHT_PRESSED
                                mydegree = 225;
                                myimg = 2;
                                break;
                            case 0x002 | 0x008 | 0x010:// -> DOWN_PRESSED | RIGHT_PRESSED | FIRE_PRESSED
                                mydegree = 225;
                                myimg = 6;
                                break;
                            default:
                                //System.out.println(""+keybuff);
                                keybuff = 0;
                                mydegree = -1;
                                myimg = 0;
                                break;
                        }
                    }
                    break;
                case 3:
                    if (gameCnt++ >= 200 && keybuff == (int)Keys.Space)
                    {
                        //Init_TITLE();
                        status = 0;
                        keybuff = 0;
                    }
                    break;
                case 4:
                    if (gameCnt++ >= 200 && keybuff == (int)Keys.D3) status = 2;
                    
                    break;
                default:
                    break;
            }
            if (4 != status)
                gameCnt++;
        }


        // 각종 판단, 변수나 이벤트, CPU 관련 처리
        private void process()
        {
            switch (status)
            {
                case 0://타이틀화면
                    break;
                case 1://스타트
                    process_MY();
                    if (mymode == 2) status = 2;
                    break;
                case 2://게임화면
                    process_MY();
                    process_ENEMY();
                    process_BULLET();
                    process_EFFECT();
                    process_GAMEFLOW();
                    process_ITEM();
                    break;
                case 3://게임오버
                    process_ENEMY();
                    process_BULLET();
                    process_GAMEFLOW();
                    break;
                case 4://일시정지
                    break;
                default:
                    break;
            }
            if (status != 4) gameCnt++;
        }

        public static double toRadians(double angdeg)
        {
            return angdeg / 180.0 * PI;
        }

        public const double PI = 3.14159265358979323846;

        public void Init_MYDATA()
        {
            score = 0;
            myX = 0;
            myY = 23000;
            myspeed = 4;
            mydegree = -1;
            //mywidth, myheight;//�÷��̾� ĳ������ �ʺ� ����
            mymode = 1;
            myimg = 2;
            mycnt = 0;
            mylife = 3;
            keybuff = 0;
        }


        public void process_MY()
        {
            Bullet shoot;
            switch (mymode)
            {
                case 1:
                    myX += 200;
                    if (myX > 20000) mymode = 2;
                    break;
                case 0:
                    if (mycnt-- == 0)
                    {
                        mymode = 2;
                        myimg = 0;
                    }
                    break;
                case 2:
                    if (mydegree > -1)
                    {
                        myX -= (myspeed * (int)Math.Sin(toRadians(mydegree)) * 100);
                        myY -= (myspeed * (int)Math.Cos(toRadians(mydegree)) * 100);
                    }
                    if (myimg == 6)
                    {
                        myX -= 20;
                        if (cnt % 4 == 0 || myshoot)
                        {
                            myshoot = false;
                            shoot = new Bullet(myX + 2500, myY + 1500, 0, 0, RAND(245, 265), 8);
                            bullets.Add(shoot);
                            shoot = new Bullet(myX + 2500, myY + 1500, 0, 0, RAND(268, 272), 9);
                            bullets.Add(shoot);
                            shoot = new Bullet(myX + 2500, myY + 1500, 0, 0, RAND(275, 295), 8);
                            bullets.Add(shoot);
                        }
                        //8myy+=70;
                    }
                    break;
                case 3:
                    //keybuff=0;
                    myimg = 8;
                    if (mycnt-- == 0)
                    {
                        mymode = 0;
                        mycnt = 50;
                    }
                    break;
            }
            if (myX < 2000) myX = 2000;
            if (myX > 62000) myX = 62000;
            if (myY < 3000) myY = 3000;
            if (myY > 45000) myY = 45000;
        }

        private void buildFighters()
        {
            float yLocation = 40;
            for (int x = 0; x < xNumberOfFighters; x++)
            {
                for (int y = 0; y < yNumberOfFighters; y++)
                {
                    fighter[x, y] = new Enemy(x, y, 900, yLocation, 100, 10);
                    yLocation = yLocation + 50;
                }
            }
        }



        public void process_ENEMY()
        {

            for (int x = 0; x < xNumberOfFighters; x++)
            {
                for (int y = 0; y < yNumberOfFighters; y++)
                {
                    //g.FillRectangle(black, fighter[x, y].xGraphicLocation, fighter[x, y].yGraphicLocation, 25, 25);
                    //g.DrawImage(fighterOne, 20, 20);
                    
                    fighter[x, y].xGraphicLocation = fighter[x, y].xGraphicLocation - rnd.Next(5, 15);
                    fighter[x, y].yGraphicLocation = fighter[x, y].yGraphicLocation - rnd.Next(-10, 10);
                    if (fighter[x, y].xGraphicLocation < 100)
                    {
                        fighter[x, y].xGraphicLocation = 1200;
                        fighter[x, y].yGraphicLocation = rnd.Next(100, 400);
                        fighter[x, y].shields = 100;
                    }

                }
            }



            
            int i;
            Enemy buff;
            for (i = 0; i < enemies.Count; i++)
            {
                buff = (Enemy)(enemies.IndexOf(i));

                g.DrawImage(fighterOne, 20, 20);

                rnd.Next(5, 15);

                buff.move();

                if (!buff.move())
                {
                    enemies.Remove(i);
                }
            }
            

        }


        public void process_BULLET()
        {

            Bullet buff;
            Enemy ebuff;
            Effect expl;

            int i, j, dist;

            for (i = 0; i < bullets.Count; i++)
            {
                buff = (Bullet)(bullets.IndexOf(i));

                //buff = (Bullet)(bullets.GetType(i));

                buff.move();

                if (buff.dis.X < 10 || buff.dis.X > gScreenWidth + 10 || buff.dis.Y < 10 || buff.dis.Y > gScreenHeight + 10)
                {
                    bullets.Remove(i);           //화면 밖으로 나가면 총알 제거
                    continue;
                }

                if (buff.from == 0)
                {//플레이어가 쏜 총알이 적에게 명중 판정
                    for (j = 0; j < enemies.Count; j++)
                    {

                        ebuff = (Enemy)(enemies.IndexOf(j));
                        //ebuff = (Enemy)(enemies.L );
                        //ebuff = (Enemy)(enemies.elementAt(j));
                        dist = GetDistance(buff.dis.X, buff.dis.Y, ebuff.dis.X, ebuff.dis.Y);


                        if (dist < 1500)
                        {//중간점 거리가 명중 판정이 가능한 범위에 왔을 때
                            if (ebuff.life-- <= 0)
                            {//적 라이프 감소
                                enemies.Remove(j);  //적 캐릭터 소거
                                expl = new Effect(0, ebuff.pos.X, buff.pos.Y, 0);
                                effects.Add(expl);  //폭발 이펙트 추가
                                Item tem = new Item(ebuff.pos.X, buff.pos.Y, RAND(1, (level + 1) * 20) / ((level + 1) * 20));//난수 결과가 최대값일 때만 생성되는 아이템이 1이 된다
                                items.Add(tem);
                            }

                            expl = new Effect(0, ebuff.pos.X, buff.pos.Y, 0);
                            effects.Add(expl);
                            score++;//점수 추가
                            bullets.Remove(i); //총알 소거
                        }
                    }







                }



            }

        }

        public void process_EFFECT()
        {
            int i;
            Effect buff;
            for (i = 0; i < effects.Count; i++)
            {
                buff = (Effect)(effects.IndexOf(i));
                if (cnt % 3 == 0) buff.cnt--;
                if (buff.cnt == 0) effects.Remove(i);
            }
        }

        public void process_GAMEFLOW()
        {
            int control = 0;
            int newy = 0, mode = 0;
            if (gameCnt < 500) control = 1;
            else if (gameCnt < 1000) control = 2;
            else if (gameCnt < 1300) control = 0;
            else if (gameCnt < 1700) control = 1;
            else if (gameCnt < 2000) control = 2;
            else if (gameCnt < 2400) control = 3;
            else
            {
                gameCnt = 0;
                level++;
            }
            if (control > 0)
            {
                newy = RAND(30, gScreenHeight - 30) * 100;
                if (newy < 24000) mode = 0; else mode = 1;
            }
            switch (control)
            {
                case 1:
                    if (gameCnt % 90 == 0)
                    {
                        Enemy en = new Enemy(this, 0, gScreenWidth * 100, newy, 0, mode);
                        enemies.Add(en);
                    }
                    break;
                case 2:
                    if (gameCnt % 50 == 0)
                    {
                        Enemy en = new Enemy(this, 0, gScreenWidth * 100, newy, 0, mode);
                        enemies.Add(en);
                    }
                    break;
                case 3:
                    if (gameCnt % 20 == 0)
                    {
                        Enemy en = new Enemy(this, 0, gScreenWidth * 100, newy, 0, mode);
                        enemies.Add(en);
                    }
                    break;
            }
        }

        public void process_ITEM()
        {
            int i, dist;
            Item buff;
            for (i = 0; i < items.Count; i++)
            {
                buff = (Item)(items.IndexOf(i));
                dist = GetDistance(myX / 100, myY / 100, buff.dis.X, buff.dis.Y);

                if (dist < 1000)
                {//아이템 획득
                    switch (buff.kind)
                    {
                        case 0://일반 득점
                            score += 100;
                            break;
                        case 1://실드
                            myshield = 5;
                            break;
                    }
                    items.Remove(i);
                }
                else
                    if (buff.move()) items.Remove(i);
            }
        }


        public int GetDistance(int x1, int y1, int x2, int y2)
        {

            return Math.Abs((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
        }




        public int RAND(int startnum, int endnum)
        {
            int a, b;
            if (startnum < endnum)
                b = endnum - startnum;
            else
                b = startnum - endnum;
            a = Math.Abs(rnd.Next() % (b + 1));
            return (a + startnum);
        }

        public int getAngle(int sx, int sy, int dx, int dy)
        {
            int vx = dx - sx;
            int vy = dy - sy;
            double rad = Math.Atan2(vx, vy);
            int degree = (int)((rad * 180) / Math.PI);
            return (degree + 180);
        }



        private void _1Stage_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            //Enemy enemy = new Enemy();
            buildFighters();
            process_ENEMY();

            process();

        }
    }
}
