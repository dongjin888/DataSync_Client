using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSyncSystem.SelfView
{
    public class MyLable : Label
    {
        private SolidBrush SegBrush;

        protected override void OnPaint(PaintEventArgs e)
        {
            SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
            SegBrush = new SolidBrush(this.ForeColor);
            int x = Width / 2 - (int)size.Width / 2;
            int y = Height / 2 - (int)size.Height / 2;
            Rectangle rec = new Rectangle(x, y, (int)size.Width + 20, (int)size.Height);
            e.Graphics.DrawString(this.Text, this.Font, SegBrush, rec);

            DrawBorder(e.Graphics, this.BackColor, Color.White, this.Width, this.Height);
        }
        private void DrawBorder(Graphics g, Color bkColor, Color bordercolor, int x, int y)
        {
            SegBrush = new SolidBrush(bkColor);
            Pen pen = new Pen(SegBrush, 2);

            this.BorderStyle = BorderStyle.None;
            this.BackColor = bkColor;

            pen.Color = Color.White;

            Rectangle myRectangle = new Rectangle(0, 0, x, y);
            ControlPaint.DrawBorder(g, myRectangle, bordercolor, ButtonBorderStyle.Solid);//画个边框
                                                                                          // g.DrawRectangle(pen, myRectangle);
                                                                                          //g.DrawEllipse(pen, myRectangle);
            pen.Dispose();
            SegBrush.Dispose();
        }
    }
}
