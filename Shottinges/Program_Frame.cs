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
using System.IO;
using Shooting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Controls;
using static OpenCvSharp.Cuda.GpuMat;

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

        GameScreen gameScreen;      // Canvas 객체를 상속한 화면 묘화 메인 클래스

        Thread worker;              // 스레드 객체

        Boolean loop = true;        // 스레드 루프 정보

        Random rnd = new Random();  // 랜덤 선언
        

        // 게임 제어를 위한 변수
        
        public int status = 0;  // 게임 상태 - 0 : 대기 / 1 : 1p / 2 : 2p / 3 : 게임오버
        public int cnt;         // 루프 제어용 컨트롤 변수
        public int delay;       // 루프 딜레이. 1/1000초 단위
        public long preTime;    // 루프 간격을 조절하기 위한 시간 체크값
        public int keybuff;     // 키 버퍼값
        
               

        // 게임용 변수
        
        public int score;           // 현재 점수
        public int mylife;          // 남은 life
        public int gameCnt;         // 게임 흐름 컨트롤
        public int scrspeed = 16;   // 프레임 단위시간, 스크롤 속도
        public int level = 1;       // 현재 레벨
         
        public int myX, myY ;       // 플레이어 좌표
        public int myspeed;         // 플레이어 이동 속도
        public int mydegree;        // 플레이어 이동 방향
        
        public int myWidth, myHeight;   // 캐릭터 크기, 플레이어 캐릭터의 너비 높이
        public int mymode;              // 플레이어 캐릭터 상태
        public int myimg;               // 플레이어 이미지
        public int mycnt;
        public Boolean myshoot = false; // 총알 발사가 눌리고 있는지 확인하는 변수
        public int myshield;            // 실드 남은 수비량

        public int gScreenWidth = 640;  // 게임 화면 너비
        public int gScreenHeight = 480; // 게임 화면 높이

        public ArrayList bullets = new ArrayList();     // 총알 관리, 총알의 갯수를 예상할 수 없기 때문에 가변적으로 관리한다.
        public ArrayList enemies = new ArrayList();     // 적 캐릭터 관리
        public ArrayList effects = new ArrayList();     // 이펙트 관리
        public ArrayList items = new ArrayList();       // 아이템 관리

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

        Random random;


        //private int elementCount;



        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Program_Frame
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Program_Frame";
            this.Text = "s";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Program_Frame_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Program_Frame_KeyUp);
            this.ResumeLayout(false);

        }

        /*
        Program_Frame()
        {
           
            
            gameScreen = new GameScreen(this);//화면 묘화를 위한 캔버스 객체
            gameScreen.setBounds(0, 0, gScreenWidth, gScreenHeight);
            add(gamescreen);//Canvas 객체를 프레임에 올린다

            systeminit();
            initialize();

            //기본적인 윈도우 정보 세팅. 게임과 직접적인 상관은 없이 게임 실행을 위한 창을 준비하는 과정.
            setIconImage(makeImage("./rsc/icon.png"));
            setBackground(new Color(0xffffff));//윈도우 기본 배경색 지정 (R=ff, G=ff, B=ff : 하얀색)
            setTitle("ストライクウィッチ-ズ Fan Game");//윈도우 이름 지정
            setLayout(null);//윈도우의 레이아웃을 프리로 설정
            setBounds(100, 100, 640, 480);//윈도우의 시작 위치와 너비 높이 지정
            setResizable(false);//윈도우의 크기를 변경할 수 없음
            setVisible(true);//윈도우 표시

            addKeyListener(this);//키 입력 이벤트 리스너 활성화
            addWindowListener(new MyWindowAdapter());//윈도우의 닫기 버튼 활성화


        }
        */


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

            gameScreen = new GameScreen(this);

            Init_Program();
            Init_Game();

        }


        public void Init_Program()// -> 자바 소스에서 SystemInit() 역할
        {
            status = 0;
            cnt = 0;
            delay = 17;
            keybuff = 0;

            //worker = new Thread(new ThreadStart(Program_Frame.StaticMethod));
            //worker.Start();
        }

        public void Init_Game()// -> 자바 소스에서 initialize() 역할
        {

        }

        // 스레드로 구동할 함수
        public void Run()
        {
            try
            {
                while (loop)
                {
                    preTime = Environment.TickCount;

                    //gs.Paint();
                    //process();
                    KeyProcess();

                    // 게임 루프 처리 시간을 delay값에서 차감 -> delay를 일정하게 유지
                    if (Environment.TickCount - preTime < delay)
                        Thread.Sleep((int)(delay - Environment.TickCount + preTime));

                    if (4 != status)
                        cnt++;
                }
            }
            catch (Exception e)
            {
                return;
            }

        }

        public static void StaticMethod()
        {
            Console.WriteLine(
                "ServerClass.StaticMethod is running on another thread.");

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(5000);
            Console.WriteLine(
                "The static method called by the worker thread has ended.");
        }



        // 키 이벤트 리스너 처리


        private void Program_Frame_KeyDown(object sender, KeyEventArgs e)
        {
            //if(status==2&&(mymode==2||mymode==0)){
            if (status == 2)
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
            }
            else if (status != 2) keybuff = (int)e.KeyCode;
        }

        private void Program_Frame_KeyUp(object sender, KeyEventArgs e)
        {
            //if(status==2&&(mymode==2||mymode==0)){
            //if(status==2){
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
                    break;
                case Keys.Down:
                    keybuff &= ~DOWN_PRESSED;
                    break;
            }
            //}
            //PC 환경에서는 기본적으로 키보드의 반복입력을 지원하지만,
            //그렇지 않은 플랫폼에서는 키 버퍼값에 떼고 눌렀을 때마다 값을 변경해 리피트 여부를 제어
        }


        private void KeyProcess()// -> 키 종합 처리
        {
            switch (status)
            {
                case 0:// 대기 상태
                    if (keybuff == (int)Keys.D1)// -> 1p전환
                    {
                        //Init_GAME();
                        //Init_MY();
                        //status = 1;
                    }
                    else if (keybuff == (int)Keys.D2)// -> 2p 전환
                    {
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
                                break;
                            case 0x001 | 0x010:// -> UP_PRESSED | FIRE_PRESSED
                                mydegree = 0;
                                myimg = 6;
                                break;
                            case 0x004:// -> LEFT_PRESSED
                                mydegree = 90;
                                myimg = 4;
                                break;
                            case 0x004 | 0x010:// -> LEFT_PRESSED | FIRE_PRESSED
                                mydegree = 90;
                                myimg = 6;
                                break;
                            case 0x008:// -> RIGHT_PRESSED
                                mydegree = 270;
                                myimg = 2;
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



        public void systemInit()// -> Init_Program 함수로 옮김
        {
            mymode = 1;
            status = 0;
            cnt = 0;
            delay = 17;         // 17/1000초 = 58 (프레임/초)
            keybuff = 0;

            //mainwork = new Thread(new ThreadStart(Program_Frame.StaticMethod));
            //mainwork.Start();

        }

        /*
         
        public void initialize()        //게임 초기화        -> Init_Game 함수로 옮김  
        {                       

            Init_TITLE();
            repaint();
        }


        public void repaint()
        {
            repaint(0, 0, 0, width, height);
        }

        public void repaint(long tm, int x, int y, int width, int height)
        {
            if (this.peer instanceof LightweightPeer) {
                // Needs to be translated to parent coordinates since
                // a parent native container provides the actual repaint
                // services.  Additionally, the request is restricted to
                // the bounds of the component.
                if (parent != null)
                {
                    if (x < 0)
                    {
                        width += x;
                        x = 0;
                    }
                    if (y < 0)
                    {
                        height += y;
                        y = 0;
                    }

                    int pwidth = (width > this.width) ? this.width : width;
                    int pheight = (height > this.height) ? this.height : height;

                    if (pwidth <= 0 || pheight <= 0)
                    {
                        return;
                    }

                    int px = this.x + x;
                    int py = this.y + y;
                    parent.repaint(tm, px, py, pwidth, pheight);
                }
            } else {
                if (isVisible() && (this.peer != null) &&
                    (width > 0) && (height > 0))
                {
                    PaintEvent e = new PaintEvent(this, PaintEvent.UPDATE,
                                                  new Rectangle(x, y, width, height));
                    SunToolkit.postEvent(SunToolkit.targetToAppContext(this), e);
                }
            }
        }
        */

        //서브루틴 일람
        /*
        public void Init_TITLE()
        {

            int i;
            /*gamescreen.bg=null;
            gamescreen.bg_f=null;
            for(i=0;i<4;i++) gamescreen.cloud[i]=null;
            for(i=0;i<4;i++) gamescreen.bullet[i]=null;
            gamescreen.enemy[0]=null;
            gamescreen.explo=null;
            gamescreen.item=null;
            gamescreen._start=null;
            gamescreen._over=null;
            System.gc();*/
            /*
            gameScreen.title = makeImage("./rsc/title.png");
            gameScreen.title_key = makeImage("./rsc/pushspace.png");
            
            //aclip[0]=myaudio.getClip("./snd/bgm_0.au");
            //aclip[0].loop();
        }


        public Image makeImage(String furl)
        {
            System.Drawing.Image img;
            System.Windows.Forms.ToolTip tk = ToolTip.getDefaultToolkit();
            img = tk.getImage(furl);

            try
            {
                //여기부터
                MediaTracker mt = new MediaTracker(this);
                mt.addImage(img, 0);
                mt.waitForID(0);
                //여기까지, getImage로 읽어들인 이미지가 로딩이 완료됐는지 확인하는 부분
            }
            catch (Exception ee)
            {
                ee.printStackTrace();
                return null;
            }
            return img;
        }
        */


        public int GetDistance(int x1, int y1, int x2, int y2)
        {
         
            return Math.Abs((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
        }

        /*
        protected int elementCount;

        public void elementAt(int index)
        {
            if (index >= elementCount)
            {
                index + " >= " + elementCount;
            }

            return elementData(index);
        }

        public void ArrayIndexOutOfBoundsException(String s)
        {
            super(s);
        }
        */

        private readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private long currentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }


        //// 스레드 파트 -> Run() 함수로
        //public void run()
        //{
        //    try
        //    {
        //        while (loof)
        //        {
        //            pretime = currentTimeMillis();


        //            //gameScreen.repaint();//화면 리페인트
        //            process();//각종 처리
        //            //keyprocess();//키 처리

        //            if (currentTimeMillis() - pretime < delay) Thread.Sleep(delay - (int)currentTimeMillis() + (int)pretime);
        //            //게임 루프를 처리하는데 걸린 시간을 체크해서 딜레이값에서 차감하여 딜레이를 일정하게 유지한다.
        //            //루프 실행 시간이 딜레이 시간보다 크다면 게임 속도가 느려지게 된다.

        //            if (status != 4) cnt++;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //e.printStackTrace();
        //    }
        //}

        // 각종 판단, 변수나 이벤트, CPU 관련 처리
        /*
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
        */

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

        public void process_ENEMY()
        {
            int i;
            Enemy buff;
            for (i = 0; i < enemies.Count; i++)
            {
                buff = (Enemy)(enemies.IndexOf(i));
                if (!buff.move()) enemies.Remove(i);
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

        /*
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
        */

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

        public static implicit operator Program_Frame(_1Stage v)
        {
            throw new NotImplementedException();
        }
    }


}
