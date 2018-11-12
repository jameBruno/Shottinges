using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting
{
    class Item
    {
        public Point pos;
        public Point dis;
        public int speed;
        public int cnt;
        public int kind;
        public Item(int x, int y, int kind)
        {
            this.kind = kind;
            pos = new Point(x, y);
            dis = new Point(x / 100, y / 100);
            speed = -200;
            cnt = 0;
        }
        public Boolean move()
        {
            Boolean ret = false;
            pos.X -= speed;
            cnt++;
            if (cnt >= 50) speed = 200;
            else if (cnt >= 30) speed = 100;
            else if (cnt >= 20) speed = -100;
            dis.X = pos.X / 100;
            if (pos.X < 0) ret = true;
            return ret;
        }

        public static explicit operator Item(int v)
        {
            throw new NotImplementedException();
        }
    }
}
