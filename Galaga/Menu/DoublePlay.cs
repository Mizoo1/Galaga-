// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      DoublePlay.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using Galaga.Menu;
using Galaga.Sprite;
using Microsoft.VisualBasic;
using Galaga.Menu;
using Galaga.Sprite;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SDL2.SDL;
using System.Diagnostics;
using Galaga.Utility;
using Galaga.Objects;

namespace Galaga.Sprite
{
    public class DoublePlay : IGameState
    {
        static int screenWidth = 640;
        static int screenHeight = 480;
        IntPtr window;
        IntPtr randerer;
        private IntPtr surface;
        private IntPtr _hintergrund;
        private IntPtr _hintergrundTexture;
        private IntPtr texture;
        private Player player1;
        private Player player2;
        private EnemSprite enemSprite;
        private EnemyFighter enemyFighter;
        private Wave wave;
        private List<Sprite> sprites;
        private Laser laser;
        private Input input;
        private Input input2;
        int yPos1 = 0;
        int yPos2 = -460;// y-Position des Hintergrundbildes
        private Font _font;
        private Stopwatch stopwatch = new Stopwatch();
        private TimeSpan elapsedTime;
        private int minutes;
        private int seconds;
        public static bool isPaused = false;
        bool running = true;
        private ItemRandomizer items;

        private uint startTicks;
        private uint elapsedTicks;
        // Dictionary to store all textures and their corresponding file paths
        private Dictionary<string, string> textureFilePaths = new Dictionary<string, string>()
        {
            {"EnemyFighter", "D:\\One\\Desktop\\game\\PPong - Kopie\\PPong\\Assest\\EnemyFighter.png"},
            {"Player", "D:\\One\\Desktop\\game\\PPong - Kopie\\PPong\\Assest\\Flugzeug.png"},
            {"Enemy", "D:\\One\\Desktop\\game\\PPong - Kopie\\PPong\\Assest\\enemy.png"},
            {"Laser", "D:\\One\\Desktop\\game\\PPong - Kopie\\PPong\\Assest\\laser.png"},
        };
        public DoublePlay(IntPtr window, IntPtr randerer)
        {
            this.window = window;
            this.randerer = randerer;
            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
            }
            _font = new Font("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Lato-Italic.ttf", 20);
            initialize();
            stopwatch.Start();
        }
        // Function to load a texture from a file
        public IntPtr LoadTexture(string filePath)
        {
            // Load the image from the file
            IntPtr surface = SDL_image.IMG_Load(filePath);
            if (surface == IntPtr.Zero)
            {
                // Throw an exception if the image could not be loaded
                throw new Exception($"Error loading image: {SDL.SDL_GetError()}");
            }

            // Create the texture from the surface
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(randerer, surface);
            if (texture == IntPtr.Zero)
            {
                // Throw an exception if the texture could not be created
                throw new Exception($"Error creating texture: {SDL.SDL_GetError()}");
            }

            // Free the surface
            SDL.SDL_FreeSurface(surface);

            return texture;
        }
        public void initialize()
        {
            _hintergrund = SDL_image.IMG_Load("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Sterne.jpg");
            IntPtr EnemyFighter = LoadTexture(textureFilePaths["EnemyFighter"]);
            IntPtr playerTexture = LoadTexture(textureFilePaths["Player"]);
            IntPtr Enemy = LoadTexture(textureFilePaths["Enemy"]);
            IntPtr Laser = LoadTexture(textureFilePaths["Laser"]);
            input = new Input(SDL_Scancode.SDL_SCANCODE_W,
                SDL_Scancode.SDL_SCANCODE_S,
                SDL_Scancode.SDL_SCANCODE_A,
                SDL_Scancode.SDL_SCANCODE_D,
                SDL.SDL_Keycode.SDLK_SPACE);
            input2 = new Input(SDL_Scancode.SDL_SCANCODE_UP,
                SDL_Scancode.SDL_SCANCODE_DOWN,
                SDL_Scancode.SDL_SCANCODE_LEFT,
                SDL_Scancode.SDL_SCANCODE_RIGHT,
                SDL.SDL_Keycode.SDLK_1);
            _hintergrundTexture = SDL.SDL_CreateTextureFromSurface(randerer, _hintergrund);
            player2 = new Player(playerTexture, input2,
                200, 420);
            player1 = new Player(playerTexture, input);

            laser = new Laser(Laser);
            enemyFighter = new EnemyFighter(EnemyFighter, 0, 0, laser);
            enemSprite = new EnemSprite(Enemy, 200, 100, laser);
            wave = new Wave(enemSprite, enemyFighter);
            sprites = new List<Sprite>
            {
                player1,
                player2
            };
        }
        public void HandleInput()
        {
            player2.HandleInput(surface, window, randerer, laser);
            player1.HandleInput(surface, window, randerer, laser);
        }
        public void Update()
        {

            
            startTicks = SDL.SDL_GetTicks();
            while (running)
            {
                HandleInput();
                if (SinglePlay.isPaused)
                {
                    _font.RenderText(randerer, "PAUSED", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 300, 200);
                }
                if (SinglePlay.GameOver)
                {
                    _font.RenderText(randerer, "Game Over", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 300, 200);
                    _font.RenderText(randerer, "Press ESC to new Game or Q to close", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 200, 250);
                }
                Draw();
                SDL.SDL_RenderPresent(randerer);
                if (!SinglePlay.GameOver)
                {
                    if (!SinglePlay.isPaused)
                    {
                        elapsedTicks = SDL.SDL_GetTicks() - startTicks;
                        var gameTime = new GameTime(elapsedTicks);
                        LoadContent();
                        wave.Update(surface, randerer, gameTime, player1, player2);

                        player1.Update(sprites, surface, window, randerer, laser,null);
                        player2.Update(sprites, surface, window, randerer, laser,null);
                        laser.UpdateLaserShotsFromEnemy(player1, player2, sprites);
                        laser.UpdateLaserShots(enemSprite);
                        laser.UpdateLaserShots(enemyFighter);
                        elapsedTime = stopwatch.Elapsed;
                        minutes = elapsedTime.Minutes;
                        seconds = elapsedTime.Seconds;
                        Draw();
                        Draw_Bild();
                        SDL.SDL_RenderPresent(randerer);
                    }
                }
            }
        }
        public void LoadContent()
        {

            player2.loadContent();
            player1.loadContent();

            laser.loadContent();
            wave.LoadContent();
            #region addInTheList
            sprites.Add(player2);
            sprites.Add(player1);
            foreach (Enemy enemy in enemSprite.Enemies)
                sprites.Add(enemy);
            #endregion
        }
        public void Draw()
        {
            player2.draw(surface, randerer);
            player1.draw(surface, randerer);
            laser.DrawLaser(surface, randerer);
            wave.Draw(surface, randerer);
        }

        public void Draw_Bild()
        {
            #region Beginn
            // Definieren Sie die Position und Größe des Rechtecks
            SDL.SDL_Rect hrect1 = new SDL.SDL_Rect()
            {
                x = 0,
                y = yPos1,
                w = 640,
                h = 480
            };
            // Definieren Sie die Position und Größe des Rechtecks
            SDL.SDL_Rect hrect2 = new SDL.SDL_Rect()
            {
                x = 0,
                y = yPos2,
                w = 640,
                h = 480
            };
            running = player2.getRunning();
            // Erstellen Sie eine Texture aus der Surface
            // Rendere Hintergrund
            SDL.SDL_SetRenderDrawColor(randerer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(randerer);
            // Kopieren Sie die Texture auf den Renderer an der spezifizierten Position
            SDL.SDL_RenderCopy(randerer, _hintergrundTexture, IntPtr.Zero, ref hrect1);
            SDL.SDL_RenderCopy(randerer, _hintergrundTexture, IntPtr.Zero, ref hrect2);
            _font.RenderText(randerer, "Score: " + Laser._score, new SDL.SDL_Color { r = 250, g = 0, b = 0 }, 20, 20);
            _font.RenderText(randerer, "Lives: " + Player.Mana + " %", new SDL.SDL_Color { r = 124, g = 255, b = 0 }, 520, 20);
            _font.RenderText(randerer, minutes + ":" + seconds.ToString("D2"), new SDL.SDL_Color { r = 180, g = 70, b = 150 }, 300, 20);
            yPos1++;
            if (yPos1 > 460)
                yPos1 = -460;
            yPos2++;
            if (yPos2 > 460)
                yPos2 = -460;
            SDL.SDL_Delay(16);
            #endregion
        }
        public void cleanUp()
        {
            // Räume auf
            SDL.SDL_DestroyRenderer(randerer);
            SDL.SDL_DestroyWindow(randerer);
            SDL.SDL_Quit();
        }
    }
}
