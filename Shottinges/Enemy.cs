using Shooting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shotting
{
    class Enemy
    {
        //게임에 등장하는 적 캐릭터 관리 클래스
        public Program_Frame main;
        public Point pos;
        public Point _pos;
        public Point dis;
        public int img;
        public int kind;
        public int life;
        public int mode;
        public int cnt;
        public int shoottype;
        public Bullet bul;
        public Enemy(Program_Frame main, int img, int x, int y, int kind, int mode)
        {
            this.main = main;
            pos = new Point(x, y);
            _pos = new Point(x, y);
            dis = new Point(x / 100, y / 100);
            this.kind = kind;
            this.img = img;
            this.mode = mode;
            life = 6 + main.RAND(0, 5) * main.level; //게임 레벨에 따라 라이프와 탄을 쏘는 시간이 짧아진다
            cnt = main.RAND(main.level * 5, 80);
            shoottype = main.RAND(0, 4);
        }

        public Boolean move()
        {
            Boolean ret = true;
            
            switch (shoottype)
            {
                case 0:
                    if (cnt % 100 == 0 || cnt % 103 == 0 || cnt % 106 == 0)
                    {
                        bul = new Bullet(pos.X, pos.Y, 2, 1, main.getAngle(pos.X, pos.Y, main.myX, main.myY), 6);
                        main.bullets.Add(bul);
                    }
                    break;
                case 1:
                    if (cnt % 90 == 0 || cnt % 100 == 0 || cnt % 110 == 0)
                    {
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (0 + (cnt % 36) * 10) % 360, 6);
                        main.bullets.Add(bul);
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (30 + (cnt % 36) * 10) % 360, 6);
                        main.bullets.Add(bul);
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (60 + (cnt % 36) * 10) % 360, 6);
                        main.bullets.Add(bul);
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (90 + (cnt % 36) * 10) % 360, 6);
                        main.bullets.Add(bul);
                    }
                    break;
                case 2:
                    if (cnt % 30 == 0 || cnt % 60 == 0 || cnt % 90 == 0 || cnt % 120 == 0 || cnt % 150 == 0 || cnt % 180 == 0)
                    {
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (main.getAngle(pos.X, pos.Y, main.myX, main.myY) + main.RAND(-20, 20)) % 360, 6);
                        main.bullets.Add(bul);
                    }
                    break;
                case 3:
                    if (cnt % 90 == 0 || cnt % 110 == 0 || cnt % 130 == 0)
                    {
                        bul = new Bullet(pos.X, pos.Y, 2, 1, main.getAngle(pos.X, pos.Y, main.myX, main.myY), 6);
                        main.bullets.Add(bul);
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (main.getAngle(pos.X, pos.Y, main.myX, main.myY) - 20) % 360, 6);
                        main.bullets.Add(bul);
                        bul = new Bullet(pos.X, pos.Y, 2, 1, (main.getAngle(pos.X, pos.Y, main.myX, main.myY) + 20) % 360, 6);
                        main.bullets.Add(bul);
                    }
                    break;
                case 4:
                    break;
            }

            switch (kind)
            {
                case 0:
                    switch (mode)
                    {
                        case 0:
                            pos.X -= 500;
                            pos.Y += 80;
                            if (pos.X < main.myX) mode = 2;
                            break;
                        case 1:
                            pos.X -= 500;
                            pos.Y -= 80;
                            if (pos.X < main.myX) mode = 3;
                            break;
                        case 2:
                            pos.X += 600;
                            pos.Y += 240;
                            break;
                        case 3:
                            pos.X += 600;
                            pos.Y -= 240;
                            break;
                    }
                    break;
                case 1:
                    break;
            }
            dis.X = pos.X / 100;
            dis.Y = pos.Y / 100;
            if (dis.X < 0 || dis.X > 640 || dis.Y < 0 || dis.Y > 480) ret = false;

            cnt++;
            return ret;
        }


        public static explicit operator Enemy(int v)
        {
            throw new NotImplementedException();
        }
    }

}
