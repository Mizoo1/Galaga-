using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Objects
{
    public class Coordinaten
    {
        private int X;
        private int Y;
        public List<Coordinaten> Coor;
        public Coordinaten(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int getX() { return this.X; }
        public int getY() { return this.Y; }
        public List<Coordinaten> GetCoordinatens()
        {
            return Coor;
        }
    }
}
