using System;
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
        public int gameCnt;             // 게임 흐름 컨트롤

        public int mymode;              // 플레이어 캐릭터 상태
        public int myspeed;             // 플레이어 이동 속도
        public int mydegree;            // 플레이어 이동 방향
        public int myimg;               // 플레이어 이미지
        public Boolean myshoot = false; // 총알 발사가 눌리고 있는지 확인하는 변수

        Boolean loop = true;        // 스레드 루프 정보

        Thread worker;              // 스레드 객체



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

        // 이미지 폼 위에 그리기

        private void _1Stage_Paint(object sender, PaintEventArgs e)
        {

            Image image = Image.FromFile("C:\\Users\\J\\Desktop\\Shotting 수정\\Shottinges\\Shottinges\\Images\\플레이어\\player.png");
            int x = 12;
            int y = 311;
            int width = 144;
            int height = 113;
            e.Graphics.DrawImage(image, x, y, width, height);

            Image images = Image.FromFile("C:\\Users\\J\\Desktop\\Shotting 수정\\Shottinges\\Shottinges\\Images\\1stage\\1stage enemy2.png");
            int xPos = 687;
            int yPos = 12;
            int widths = 149;
            int heights = 105;
            e.Graphics.DrawImage(images, xPos, yPos, widths, heights);

            Image enemy = Image.FromFile("C:\\Users\\J\\Desktop\\Shotting 수정\\Shottinges\\Shottinges\\Images\\1stage\\1stage enermy.png");
            int xPoes = 735;
            int yPoes = 487;
            int widthes = 122;
            int heightes = 98;
            e.Graphics.DrawImage(enemy, xPoes, yPoes, widthes, heightes);

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
    }
}
