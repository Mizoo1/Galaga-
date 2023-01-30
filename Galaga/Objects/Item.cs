using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Sprite;
using Galaga.Objects;

namespace Galaga.Objects
{
    public class Items : Sprite.Sprite
    {
        public List<Items> items = new List<Items>();
        public IntPtr surface;
        public Items() : base(IntPtr.Zero, 0, 0, 0, 0)
        {

        }

        public Items(IntPtr _texture, int x, int y, int WSize, int HSize) : base(_texture, x, y, WSize, HSize)
        {

        }

        internal bool IsCollidingWith(Sprite.Sprite sprite)
        {
            bool ok = false;
            if (sprite == this)
                ok = false;
            if ((Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                (Velocity.X < 0 & this.IsTouchingRight(sprite)))
                ok = true;

            if ((Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                (Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                ok = true;
            return ok;
        }
    }
}
