using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Menu
{
    public class GameTime
    {
        public uint ElapsedTicks { get; }
        public double ElapsedSeconds => ElapsedTicks / 1000.0;

        public GameTime(uint elapsedTicks)
        {
            ElapsedTicks = elapsedTicks;
        }
    }
}
