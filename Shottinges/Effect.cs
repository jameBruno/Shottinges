using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooting
{
    class Effect
    {
        public Point pos;
        public Point _pos;
        public Point dis;
        int img;
        int kind;

        public int cnt;

        public Effect(int img, int x, int y, int kind)
        {
            pos = new Point(x, y);
            _pos = new Point(x, y);
            dis = new Point(x / 100, y / 100);
            this.kind = kind;
            this.img = img;
            cnt = 16;
        }

        public static explicit operator Effect(int v)
        {
            throw new NotImplementedException();
        }
    }
}
