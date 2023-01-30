// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Laser.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using Galaga.Utility;
using SDL2;
using static System.Formats.Asn1.AsnWriter;
using static SDL2.SDL;

namespace Galaga.Sprite
{
    public class Laser : Sprite
    {
        private Game03 game03;
        private Player player;
        private Player player2;
        private float laserSpeed = 10f;
        public List<Sprite> lasers = new List<Sprite>();
        public List<Sprite> lasersEnemy = new List<Sprite>();
        IntPtr window;
        IntPtr _renderer;
        private Music music_1;
        private Music music_2;
        private Music music_3;
        public bool running = true;
        public static int highScore ;
        public static int _score = 0;
        private string filePath;
        private string highscoreString;
        public Laser(IntPtr _texture) : base(_texture, 0, 0, 10, 10)
        {
            music_3 = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\explosion.mp3");
            music_2 = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\shot_1.mp3");
            music_1 = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\AufschlagPadlle.wav");
            
            if (File.Exists("highscore.txt"))
            {
                highscoreString = File.ReadAllText("highscore.txt");
                highScore = int.Parse(highscoreString);
                Console.WriteLine(highScore);
            }
            else throw new Exception("t3rys");
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

        public bool getRunning() { return running; }
        public void setRunning(bool newRunning)
        {
            running = newRunning;
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
            music_2.Play();
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
                if (_score > highScore)
                {
                    highScore = _score;
                    filePath = "highscore.txt";
                    highscoreString = highScore.ToString();
                    File.WriteAllText(filePath, highscoreString);
                }

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
                            ++_score;
                            // Punkte erhöhen

                            PlayExplosionSound(1);

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
                            Player.Mana -=10;
                            if (Player.Mana <= 0)
                            {
                                music_3.Play();
                                --Player.index;
                                SDL.SDL_Delay(1000);
                                Player.Mana = 100;
                                if (Player.index == 0) 
                                {
                                    SinglePlay.GameOver = true;
                                    GameState.SetState(new HelpMenu(window, _renderer));
                                }
                            }


                                PlayExplosionSound(2);

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

                                PlayExplosionSound(2);

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
        public void PlayExplosionSound(int index)
        {
            switch(index)
            {
                case 1:
                    music_2.Play();
                    break;
                case 2:
                    music_1.Play();
                    break;
            }
        }
    }
}
