using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Sprite
{
    public class EnemSprite : Enemy
    {
        public EnemSprite(IntPtr _texture, int x, int y) : base(_texture, x, y, 20, 20)
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
            //TODO
        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            foreach (Enemy e in Enemies)
                e.Draw(surface, renderer);
        }
        public void createEnemy(IntPtr surface, IntPtr renderer)
        {
            //TODO
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
