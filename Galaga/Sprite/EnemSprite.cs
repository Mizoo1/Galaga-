using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Sprite
{
    public class EnemSprite : Enemy
    {
        int count = 3;
        public EnemSprite(IntPtr _texture, int x, int y, Laser laser) : base(_texture, x, y, 20, 20)
        {
            speed = 10;
            enemyRadius = 20;
        }
        public EnemSprite(IntPtr _texture, int x, int y, int directionX, int directionY) : base(_texture, x, y, 20, 20)
        {
            this.directionX = directionX;
            this.directionY = directionY;
            speed = 10;
            enemyRadius = 20;
        }
        public void update(IntPtr surface, IntPtr renderer)
        {
            Move();
        }

        public void Move()
        {
            foreach (Enemy e in Enemies)
            {
                e.Velocity.X += e.directionX;
                e.Velocity.Y += e.directionY;
                if (e.X < 640 - e.sprite.w)
                {
                    e.directionX *= -1;
                    e.Velocity.X += e.directionX;
                    e.Velocity.X += e.directionX;
                }
                if (e.X > 0)
                {
                    e.directionX *= -1;
                    e.Velocity.X += e.directionX;
                    e.Velocity.X += e.directionX;
                }
                if (e.Y < 0)
                {
                    e.directionY *= -1;
                    e.Velocity.Y += e.directionY;
                    e.Velocity.Y += e.directionY;
                }
                if (e.Y > 200)
                {
                    e.directionY *= -1;
                    e.Velocity.Y += e.directionY;
                    e.Velocity.Y += e.directionY;
                }

                e.Y += e.Velocity.Y;
                e.X += e.Velocity.X;
                e.Velocity.Y = 0;
                e.Velocity.X = 0;
            }
        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            foreach (Enemy e in Enemies)
                e.Draw(surface, renderer);
        }
        public void createEnemy(IntPtr surface, IntPtr renderer)
        {
            count--;
            for (int i = 0; i < 5; i++)
            {
                EnemSprite e = new EnemSprite(_texture, 20 + i * 40, 50, 2, 2);
                Enemies.Add(e);
            }
            for (int i = 0; i < 5; i++)
            {
                EnemSprite e = new EnemSprite(_texture, 600 - i * 40, 50, -2, -2);
                Enemies.Add(e);
            }
        }
        public void EnemySound()
        {
            //TODO
        }
        public void loadContent()
        {
            foreach (Enemy e in Enemies)
                e.LoadContent();
        }

    }
}
