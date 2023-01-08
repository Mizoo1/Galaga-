using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Sprite
{
<<<<<<< HEAD
    public  class Enemy
    {
=======
    public class Enemy : Sprite
    {
        public List<Enemy> Enemies;
        public float enemyRadius;
        public int directionX;
        public int directionY;
        public Enemy(IntPtr _texture, int x, int y, int WSize, int HSize) : base(_texture, x, y, WSize, HSize)
        {
            Enemies = new List<Enemy>();
        }
        public float getEnemyRadius()
        {
            //TODO
            return enemyRadius;
        }
>>>>>>> Nour

    }
}
