using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.IO;
using SDL2;
using Galaga.Menu;

namespace Galaga.Sprite
{
    public class EnemyFighter : Enemy
    {
        private Laser laser;
        private double timer = 0;
        private double interval = 1500;
        public EnemyFighter(IntPtr _texture, int x, int y, Laser laser) : base(_texture, x, y, 30, 30)
        {
            speed = 0;
            enemyRadius = 20;
            this.laser = laser;
        }
        public EnemyFighter(IntPtr _texture, int x, int y, int directionX, int directionY, Laser laser) : base(_texture, x, y, 30, 30)
        {
            this.directionX = directionX;
            this.directionY = directionY;
            speed = 0;
            enemyRadius = 20;
        }

        public void update(IntPtr surface, IntPtr renderer, GameTime gameTime, Player player, Player player2)
        {
            Move(gameTime, player, player2);

        }
        public void Move(GameTime gameTime, Player player, Player player2)
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
                if (timer >= interval)
                {
                    // Reset the timer
                    timer -= interval;

                    // Perform the desired action
                    laser.FireLaserBoss(e.sprite.x, e.sprite.y, player, player2);
                }
                interval = 1500;
                // Update the timer
                timer += gameTime.ElapsedSeconds;
            }


        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            foreach (Enemy e in Enemies)
                e.Draw(surface, renderer);
        }
        public void createEnemy(IntPtr surface, IntPtr renderer)
        {

            for (int i = 0; i < 5; i++)
            {
                EnemyFighter e = new EnemyFighter(_texture, 20 + i * 40, 50, 2, 2, laser);
                Enemies.Add(e);
            }
            for (int i = 0; i < 5; i++)
            {
                EnemyFighter e = new EnemyFighter(_texture, 600 - i * 40, 50, -2, -2, laser);
                Enemies.Add(e);
            }
        }
        public void EnemySound()
        {

        }

        public void loadContent()
        {
            foreach (Enemy e in Enemies)
                e.LoadContent();
        }
    }
}
