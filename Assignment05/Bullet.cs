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
    class Bullet:PhysicsSprite 
    {
        public Bullet(int x, int y) : base(Properties.Resources.bullet, x, y)
        {
            setMotionModel(1);
            Vx = 50f;
        }

        public void killCharacter()
        {
            X += Vx;
            List<CollisionSprite> list = getCollisions();
            X -= Vx;
            foreach (CollisionSprite s in list)
            {
                if (s.GetType() == typeof(Elephant))
                {
                    s.Kill();
                }
            }
            if(list.Count>0) this.Kill();
        }

        public override void act()
        {
            base.act();
            killCharacter();
        }

    }
}
