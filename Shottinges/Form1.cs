using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shooting;

namespace Shotting
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Program_Frame pf = new Program_Frame();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //if (0 == pf.status)// 대기 상태인 경우
                    // 게임 시작 함수 구동
                    break;
                case Keys.D1: 
                    //if (0 == pf.status)
                    //1p 선택
                    break;
                case Keys.D2:
                    //if (0 == pf.status)
                    // 2p 선택
                    break;
                case Keys.Left:
                    if (1 == pf.status || 2 == pf.status)   // 1p or 2p 플레이 상태인 경우
                        pf.myX -= 10;
                    break;
                case Keys.Up:
                    if (1 == pf.status || 2 == pf.status)
                        pf.myY += 10;
                    break;
                case Keys.Down:
                    if (1 == pf.status || 2 == pf.status)
                        pf.myY -= 10;
                    break;
                case Keys.Right:
                    if (1 == pf.status || 2 == pf.status)
                        pf.myX += 10;
                    break;
                case Keys.Space:
                    //if (1 == pf.status || 2 == pf.status)
                    // 공격 함수 구동
                    break;
            }
        }
    }
}
