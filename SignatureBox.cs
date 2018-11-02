using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DocumentManagement
{
    public class SignatureBox : PictureBox
    {
        private Bitmap drawArea;
        private Graphics graphics;
        private int x = -1;
        private int y = -1;
        private bool moving = false;
        private Pen pen;

        public SignatureBox()
        {
            Width = 150;
            Height = 90;
            pen = new Pen(Color.Black, 3);
            BackColor = Color.White;
            drawArea = new Bitmap(Width, Height);
            Image = drawArea;
            graphics = Graphics.FromImage(drawArea);
            MouseMove += new MouseEventHandler(MouseMoveEvent);
            MouseDown += new MouseEventHandler(MouseDownEvent);
            MouseUp += new MouseEventHandler(MouseUpEvent);
        }

        public void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (moving && x != -1 && y != -1)
            {
                graphics.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
                Image = drawArea;
            }
        }

        public void MouseDownEvent(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        public void MouseUpEvent(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

    }
}
