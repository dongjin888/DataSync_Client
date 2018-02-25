using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSyncSystem.Utils
{
    public class ShowPage
    {
        public static void showPg(int pgNow, int pgAll, int showNum, Panel panel1, int x, int y, EventHandler clickEvent)
        {
            int lx = x;
            int ly = y;
            int showed = 0;

            if (pgNow > 2)
            {
                // <<
                Button btL = new Button();
                btL.Text = "<<";
                btL.Location = new Point(x, y);
                btL.Click += clickEvent;
                panel1.Controls.Add(btL);

                // pgNow-2 
                Button bt2 = new Button();
                bt2.Text = pgNow - 2 + "";
                bt2.Location = new Point(btL.Location.X + btL.Width + 10, btL.Location.Y);
                bt2.Click += clickEvent;
                panel1.Controls.Add(bt2);

                // pgNow-1 
                Button bt1 = new Button();
                bt1.Text = pgNow - 1 + "";
                bt1.Location = new Point(bt2.Location.X + bt2.Width + 10, bt2.Location.Y);
                bt1.Click += clickEvent;
                panel1.Controls.Add(bt1);

                lx = bt1.Location.X + bt1.Width + 10;
                ly = bt1.Location.Y;

                int cur = pgNow;
                showed = 2;
                while (showed <= showNum && cur <= pgAll)
                {
                    Button bt = new Button();
                    bt.Location = new Point(lx, ly);
                    bt.Text = cur + "";
                    bt.Click += clickEvent;
                    lx = bt.Location.X + bt.Width + 10;
                    panel1.Controls.Add(bt);

                    if (cur == pgNow)
                    {
                        bt.BackColor = Color.Red;
                    }

                    cur++;
                    showed++;
                }

                if (pgNow < cur - 1)
                {
                    Button btR = new Button();
                    btR.Text = ">>";
                    btR.Location = new Point(lx, ly);
                    btR.Click += clickEvent;
                    panel1.Controls.Add(btR);
                }
            }
            else //pgNow <= 2
            {
                if (pgNow == 2)
                {
                    Button btL = new Button();
                    btL.Text = "<<";
                    btL.Location = new Point(x, y);
                    btL.Click += clickEvent;
                    lx = btL.Location.X + btL.Width + 10;
                    panel1.Controls.Add(btL);
                }

                int cur = 1;
                while (showed <= showNum && cur <= pgAll)
                {
                    Button bt = new Button();
                    bt.Location = new Point(lx, ly);
                    bt.Text = cur + "";
                    bt.Click += clickEvent;
                    lx = bt.Location.X + bt.Width + 10;
                    panel1.Controls.Add(bt);

                    if (cur == pgNow)
                    {
                        bt.BackColor = Color.Red;
                    }

                    cur++;
                    showed++;
                }
                if (pgNow < cur - 1)
                {
                    Button btR = new Button();
                    btR.Text = ">>";
                    btR.Location = new Point(lx, ly);
                    btR.Click += clickEvent;
                    panel1.Controls.Add(btR);
                }
            }
        }
    }
}
