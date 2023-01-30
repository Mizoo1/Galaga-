// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Enemy.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Sprite
{
    public class Enemy : Sprite
    {
        public List<Enemy> Enemies;
        public static float enemyRadius = 30;
        public int directionX;
        public int directionY;
        public static bool isOk = true;
        public Enemy(IntPtr _texture, int x, int y, int WSize, int HSize) : base(_texture, x, y, WSize, HSize)
        {
            Enemies = new List<Enemy>();
        }
        public float getEnemyRadius()
        {
            return enemyRadius;
        }
    }
}
