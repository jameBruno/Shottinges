using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shooting
{
    public partial class _1Stage : Form
    {
        public _1Stage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }
        
        public static int UP_PRESSED = 0x001;
        public static int DOWN_PRESSED = 0x002;
        public static int LEFT_PRESSED = 0x004;
        public static int RIGHT_PRESSED = 0x008;
        public static int FIRE_PRESSED = 0x010;

        public int status = 0;  // 게임 상태 - 0 : 대기 / 1 : 1p / 2 : 2p / 3 : 게임오버
        public int cnt;         // 루프 제어용 컨트롤 변수
        public int delay;       // 루프 딜레이. 1/1000초 단위
        public long preTime;    // 루프 간격을 조절하기 위한 시간 체크값
        public int keybuff;     // 키 버퍼값


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

        Bitmap bit;

        // 이미지 폼 위에 그리기
        
        private void _1Stage_Paint(object sender, PaintEventArgs e)
        {

            Image image = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Images\\플레이어\\player.png");
            int x = 12;
            int y = 311;
            int width = 144;
            int height = 113;
            e.Graphics.DrawImage(image, x, y, width, height);
            
            Image images = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Images\\1stage\\1stage enemy2.png");
            int xPos = 687;
            int yPos = 12;
            int widths = 149;
            int heights = 105;
            e.Graphics.DrawImage(images, xPos, yPos, widths, heights);

            Image enemy = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Images\\1stage\\1stage enermy.png");
            int xPoes = 735;
            int yPoes = 487;
            int widthes = 122;
            int heightes = 98;
            e.Graphics.DrawImage(enemy, xPoes, yPoes, widthes, heightes);

            Image Item = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\아이템\\1up item 1.png");
            int xImage = 51;
            int yImage = 31;
            int widthImage = 62;
            int heightImage = 63;
            e.Graphics.DrawImage(Item, xImage, yImage, widthImage, heightImage);

            Image Item2 = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\아이템\\1up item 2.png");
            int xImage2 = 51;
            int yImage2 = 31;
            int widthImage2 = 62;
            int heightImage2 = 63;
            e.Graphics.DrawImage(Item2, xImage2, yImage2, widthImage2, heightImage2);

            Image Item3 = Image.FromFile("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\아이템\\1up item 3.png");
            int xImage3 = 51;
            int yImage3 = 31;
            int widthImage3 = 62;
            int heightImage3 = 63;
            e.Graphics.DrawImage(Item3, xImage3, yImage3, widthImage3, heightImage3);


            /*
            Graphics g = e.Graphics;

            foreach (Ball ball in balls)
            {
                ball.Update();
                g.FillEllipse(ball.Brush, ball.Pos.X - ball.Radius, ball.Pos.Y - ball.Radius, ball.Radius * 2, ball.Radius * 2);

            }
            */


                /*
                bit = new Bitmap("1sage-background2.gif");

                ImageAnimator.Animate(bit, new EventHandler(this.OnFrameChanged));

                base.OnLoad(e);

                */

                /*
                ImageAnimator.UpdateFrames();

                Graphics g = pictureBox1.CreateGraphics();
                */


                //g.DrawImage(this.bit, new Point(0, 0));

                //base.OnPaint(e);





            }

        protected override void OnLoad(EventArgs e)
        {

            bit = new Bitmap("D:\\Shooting 수정 코드\\1110_Shotting\\Shotting 수정\\Shottinges\\Shottinges\\Images\\1stage\\1stage-cloud2.gif");
            
            ImageAnimator.Animate(bit, new EventHandler(this.OnFrameChanged));
            
            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            ImageAnimator.UpdateFrames();
            
            Graphics g = this.CreateGraphics();
               
            
            g.DrawImage(this.bit, new Point(0, 0));


            base.OnPaint(e);
        }


        // 키 이벤트 처리 구현 중

        private void _1Stage_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(status == 1)
            {
                


            }


            /*
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                MessageBox.Show("Form.KeyPress: '" +
                    e.KeyChar.ToString() + "' pressed.");

                switch (e.KeyChar)
                {
                    case (char)49:
                    case (char)52:
                    case (char)55:
                        MessageBox.Show("Form.KeyPress: '" +
                            e.KeyChar.ToString() + "' consumed.");
                        e.Handled = true;
                        break;
                }
            }
            */

            /*
            if (status == 2)
            {
                switch (e.KeyChar)
                {
                    case e.KeyChar == (char)13:    //Keys.Space:
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
            */
            

        }

        private void _1Stage_KeyDown(object sender, KeyEventArgs e)
        {

            if(status == 1)
            {
                switch (e.KeyCode)
                {
                    


                }



            }




        }

        private void _1Stage_Load(object sender, EventArgs e)
        {

            
            //bit = new Bitmap("1sage-background2.gif");
              
            //ImageAnimator.Animate(bit, new EventHandler(this.OnFrameChanged));
            
            //base.OnLoad(e);
                        
            

        }
        

        private void OnFrameChanged(object sender, EventArgs e)

        {

            //Image image = new Image()
            

            this.Invalidate();

        }
        



    }
}
