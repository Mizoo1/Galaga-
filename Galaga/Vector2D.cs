using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Sprite
{
    public class Vector2D
    {
        public int X;
        public int Y;
        public Vector2D()
        {
            this.X = 0;
            this.Y = 0;
        }
        public Vector2D(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public void setX(int x)
        {
            X += x;
        }
        public void setY(int y)
        {
            Y += y;
        }
    }
}
