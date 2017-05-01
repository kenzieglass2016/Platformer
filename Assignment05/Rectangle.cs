using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment05
{
    public class Rectangle : Sprite
    {
        public int height;
        public int width;
        public Boolean visible;
        public int opacity;

        public Rectangle(int x, int y, int width, int height, int opacity)
        {
            X = x;
            Y = y;
            this.height = height;
            this.width = width;
            this.opacity = opacity;
            visible = false;
        }

        public void setDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void setOpacity(int opacity)
        {
            this.opacity = opacity;
        }

        public void setVisibility(Boolean visible)
        {
            this.visible = visible;
        }

        public override void paint(Graphics g)
        {
            base.paint(g);
            if (visible) g.FillRectangle(new SolidBrush(Color.FromArgb(opacity, Color.LawnGreen)), X, Y, width, height);
        }
    }
}
