using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Sprite
{
    public class Sprite 
    {
        public SDL.SDL_Rect sprite;
        public IntPtr _texture;
        public Vector2D Velocity;
        public int X; public int Y;
        public int WSize; public int HSize;
        public int Left; public int Right; public int Top; public int Bottom;
        public float speed;
        public float Radius;


        public Sprite(IntPtr _texture, int x, int y, int WSize, int HSize)
        {
            this.WSize = WSize;
            this.HSize = HSize;
            this.X = x;
            this.Y = y;
            this._texture = _texture;
            this.Velocity = new Vector2D();

        }
        public void LoadContent()
        {
            sprite = new SDL.SDL_Rect()
            {
                x = X,
                y = Y,
                w = WSize,
                h = HSize
            };
            Left = X;
            Right = X + sprite.w;
            Top = Y;
            Bottom = Y + sprite.h;
        }
        public void Draw(IntPtr surface, IntPtr renderer)
        {
            // Erstellen Sie eine Texture aus der Surface
            SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref sprite); ;
        }
        public int getX() { return X; }
        public int getY() { return Y; }
        public void setX(int x) { X = x; }
        public void setY(int y) { Y = y; }
        public SDL.SDL_Rect getSprite() { return sprite; }
        #region Collisoin
        public bool IsTouchingLeft(Sprite sprite)
        {
            return Right + Velocity.X > sprite.Left &&
                Left < sprite.Left &&
                Bottom > sprite.Top &&
                Top < sprite.Bottom;
        }
        public bool IsTouchingRight(Sprite sprite)
        {
            return Left + Velocity.X < sprite.Right &&
              Right > sprite.Right &&
              Bottom > sprite.Top &&
              Top < sprite.Bottom;
        }
        public bool IsTouchingTop(Sprite sprite)
        {
            return Bottom + Velocity.Y > sprite.Top &&
              Top < sprite.Top &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }
        public bool IsTouchingBottom(Sprite sprite)
        {
            return Top + Velocity.Y < sprite.Bottom &&
              Bottom > sprite.Bottom &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }

        public float getEnemyRadius()
        {
            return Radius;
        }
        #endregion


    }

}
