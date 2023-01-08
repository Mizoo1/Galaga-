using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Sprite
{
    public class Laser : Sprite
    {
        private Player player;
        private Player player2;
        private float laserSpeed = 10f;
        public List<Sprite> lasers = new List<Sprite>();
        public List<Sprite> lasersEnemy = new List<Sprite>();
        IntPtr window;
        IntPtr _renderer;
        public Laser(IntPtr _texture) : base(_texture, 0, 0, 10, 10)
        {

        }
        public Laser(IntPtr _texture, Player player, int x, int y) : base(_texture, x, y, 10, 10)
        {
            this.player = player;
        }
        public Laser(IntPtr _texture, Player player, Player player2, int x, int y) : base(_texture, x, y, 10, 10)
        {
            this.player = player;
            this.player2 = player2;
        }
        public void loadContent()
        {
            foreach (Laser v in lasersEnemy)
                v.LoadContent();
            foreach (Laser s in lasers)
                s.LoadContent();

            // TODO
            // Sounds laden
        }
        public void FireLaser(Player p)
        {

            lasers.Add(new Laser(_texture, p, p.X + p.WSize / 2 - 4, p.Y - p.HSize / 2 + 20));
            PlayLaserSound();
        }
        public void PlayLaserSound()
        {
            // TODO
        }
        public void DrawLaser(IntPtr surface, IntPtr randerer)
        {
            // TODO
            // Die Liste mit den Laser-Schüssen (laserShots) durchlaufen
            // und alle Schüsse (LaserTexture) zeichnen
            foreach (Laser v in lasers)
            {
                v.Draw(surface, randerer);
            }
            foreach (Laser v in lasersEnemy)
            {
                v.Draw(surface, randerer);
            }

        }
        public void UpdateLaserShots(Enemy enemy)
        {
            int laserIndex = 0;

            while (laserIndex < lasers.Count)
            {
                // hat der Schuss den Bildschirm verlassen?
                if (lasers[laserIndex].Y < 0)
                {
                    lasers.RemoveAt(laserIndex);
                }
                else
                {
                    lasers[laserIndex].Y -= (int)laserSpeed;

                    // Überprüfen ob ein Treffer vorliegt
                    int enemyIndex = 0;

                    while (enemyIndex < enemy.Enemies.Count)
                    {

                        // Abstand zwischen Feind-Position und Schuss-Position ermitteln
                        float distance = Vector2.Distance(new Vector2(enemy.Enemies[enemyIndex].X, enemy.Enemies[enemyIndex].Y)
                            , new Vector2(lasers[laserIndex].X, lasers[laserIndex].Y));
                        // Treffer?
                        if (distance < enemy.getEnemyRadius())
                        {
                            // Schuss entfernen
                            lasers.RemoveAt(laserIndex);
                            // Feind entfernen
                            enemy.Enemies.RemoveAt(enemyIndex);
                            // Punkte erhöhen

                            PlayExplosionSound();

                            // Schleife verlassen
                            break;
                        }
                        else
                        {
                            enemyIndex++;
                        }

                    }
                    laserIndex++;
                }

            }
        }
        public void UpdateLaserShotsFromEnemy(Sprite player, Sprite player2, List<Sprite> _sprite)
        {
            int laserIndex = 0;

            while (laserIndex < lasersEnemy.Count)
            {

                // hat der Schuss den Bildschirm verlassen?
                if (lasersEnemy[laserIndex].Y < 0)
                {
                    lasersEnemy.RemoveAt(laserIndex);
                }
                else
                {
                    lasersEnemy[laserIndex].Y += (int)laserSpeed;

                    // Überprüfen ob ein Treffer vorliegt
                    int enemyIndex = 0;

                    while (enemyIndex < 1)
                    {

                        // Abstand zwischen Feind-Position und Schuss-Position ermitteln
                        float distance = Vector2.Distance(new Vector2(player.X, player.Y)
                            , new Vector2(lasersEnemy[laserIndex].X, lasersEnemy[laserIndex].Y));
                        // Treffer?
                        if (distance < player.getEnemyRadius())
                        {
                            // Schuss entfernen
                            lasersEnemy.RemoveAt(laserIndex);
                            // Feind entfernen
                            

                            PlayExplosionSound();

                            // Schleife verlassen
                            break;
                        }
                        else
                        {
                            enemyIndex++;
                        }

                    }
                    laserIndex++;
                }
            }
            if (player2 != null)
            {
                int laserIndex2 = 0;

                while (laserIndex2 < lasersEnemy.Count)
                {

                    // hat der Schuss den Bildschirm verlassen?
                    if (lasersEnemy[laserIndex2].Y < 0)
                    {
                        lasersEnemy.RemoveAt(laserIndex2);
                    }
                    else
                    {
                        lasersEnemy[laserIndex2].Y += (int)laserSpeed;

                        // Überprüfen ob ein Treffer vorliegt
                        int enemyIndex2 = 0;

                        while (enemyIndex2 < 1)
                        {

                            // Abstand zwischen Feind-Position und Schuss-Position ermitteln
                            float distance = Vector2.Distance(new Vector2(player2.X, player2.Y)
                                , new Vector2(lasersEnemy[laserIndex2].X, lasersEnemy[laserIndex2].Y));
                            // Treffer?
                            if (distance < player2.getEnemyRadius())
                            {
                                // Schuss entfernen
                                lasersEnemy.RemoveAt(laserIndex2);
                                // Feind entfernen

                                // Punkte erhöhen

                                PlayExplosionSound();

                                // Schleife verlassen
                                break;
                            }
                            else
                            {
                                enemyIndex2++;
                            }

                        }
                        laserIndex2++;
                    }
                }
            }
        }
        public void FireLaserBoss(int x, int y, Player player, Player player2)
        {
            if (player2 == null)
                lasersEnemy.Add(new Laser(_texture, player, x, y));
            else
                lasersEnemy.Add(new Laser(_texture, player, player2, x, y));

        }
        public void PlayExplosionSound()
        {
            //TODO
        }
    }
}
