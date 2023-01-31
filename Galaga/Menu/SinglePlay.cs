// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation
/**========================================================================
 * @file      Game01.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @brief     The SinglePlay class is responsible for the implementation
 *            of the single player game mode. It initializes the game window
 *            and renderer, loads textures, creates and manages game objects
 *            such as the player, enemies and laser, and handles user input
 *            and game logic. It also keeps track of game time and updates
 *            the game state.
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using Galaga.Objects;
using Galaga.Utility;
using SDL2;
using static SDL2.SDL;

namespace Galaga.Sprite
{
    public class SinglePlay : IGameState
    {
        #region Variables
        private IntPtr _window;
        private IntPtr _randerer;
        private SDL.SDL_Rect hrect1;
        private SDL.SDL_Rect hrect2;
        private IntPtr surface;
        private IntPtr _hintergrund;
        private IntPtr _hintergrundTexture;
        private IntPtr texture;
        private Player player1;
        private EnemSprite enemSprite;
        private EnemyFighter enemyFighter;
        private Wave wave;
        private  static List<Sprite> sprites;
        private static Laser laser;
        private uint startTicks;
        private uint elapsedTicks;
        private Music music;
        public bool running;
        private int yPos1 = 0;
        private int yPos2 = -460;// y-Position des Hintergrundbildes
        private Font _font;
        private Stopwatch stopwatch = new Stopwatch();
        private TimeSpan elapsedTime;
        private int minutes ;
        private int seconds ;
        public  static bool isPaused = false;
        public static bool GameOver = false;
        private ItemRandomizer items;

        // Dictionary to store all textures and their corresponding file paths
        private Dictionary<string, string> textureFilePaths = new Dictionary<string, string>()
        {
            {"EnemyFighter", "./Assest/EnemyFighter.png"},
            {"Player", "./Assest/Flugzeug.bmp"},
            {"Enemy", "./Assest/enemy.png"},
            {"Laser", "./Assest/laser.png"},
        };
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor initializes the game window and renderer, initializes SDL_ttf, creates a font, calls the Draw_Bild and initialize methods and starts the stopwatch.
        /// </summary>
        public SinglePlay(IntPtr window, IntPtr randerer)
        {
            _window = window;
            _randerer = randerer;
            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
            }
            _font = new Font("./Assest/Lato-Italic.ttf", 20);
            Draw_Bild();
            initialize();
            stopwatch.Start();
        }
        #endregion

        #region Methods
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
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(_randerer, surface);
            if (texture == IntPtr.Zero)
            {
                // Throw an exception if the texture could not be created
                throw new Exception($"Error creating texture: {SDL.SDL_GetError()}");
            }

            // Free the surface
            SDL.SDL_FreeSurface(surface);

            return texture;
        }
        /// <summary>
        /// This method initializes the game by loading textures, creating objects for the background, player, enemies, laser, and wave.
        /// </summary>
        public void initialize()
        {

            _hintergrund = SDL_image.IMG_Load("./Assest/Sterne.jpg");
            IntPtr EnemyFighter = LoadTexture(textureFilePaths["EnemyFighter"]);
            IntPtr playerTexture = LoadTexture(textureFilePaths["Player"]);
            IntPtr Enemy = LoadTexture(textureFilePaths["Enemy"]);
            IntPtr Laser = LoadTexture(textureFilePaths["Laser"]);
            items = new ItemRandomizer(_randerer);
            _hintergrundTexture = SDL.SDL_CreateTextureFromSurface(_randerer, _hintergrund); ;
            player1 = new Player(playerTexture, new Input(SDL_Scancode.SDL_SCANCODE_W, SDL_Scancode.SDL_SCANCODE_S, SDL_Scancode.SDL_SCANCODE_A, SDL_Scancode.SDL_SCANCODE_D, SDL.SDL_Keycode.SDLK_SPACE));
            laser = new Laser(Laser);
            enemyFighter = new EnemyFighter(EnemyFighter, 0, 0, laser);
            enemSprite = new EnemSprite(Enemy, 200, 100, laser);
            wave = new Wave(enemSprite, enemyFighter);
            sprites = new List<Sprite>();
            items.CreateItem();

            

        }
        /// <summary>
        /// This method handles the input of the player by calling the HandleInput method of the player object.
        /// </summary>
        public void HandleInput()
        {

            player1.HandleInput(surface, _window, _randerer, laser);
        }
        /// <summary>
        /// This method updates the game state, handling input, checking for pause and game over conditions, updating the game elements, and drawing the game.
        /// </summary>
        public void Update()
        {

            yPos1 = 0;
             yPos2 = -460;// y-Position des Hintergrundbildes
            running = true;
            startTicks = SDL.SDL_GetTicks();
            while (running)
            {
                
                HandleInput();
                if (isPaused)
                {
                    _font.RenderText(_randerer, "PAUSED", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 300, 200);
                }
                if (GameOver)
                {
                    _font.RenderText(_randerer, "Game Over", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 300, 200);
                    _font.RenderText(_randerer, "Press ESC to new Game or Q to close", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 200, 250);
                }
                Draw();
                SDL.SDL_RenderPresent(_randerer);
                if (!GameOver)
                {
                    if (!isPaused)
                    {
                        elapsedTicks = SDL.SDL_GetTicks() - startTicks;
                        var gameTime = new GameTime(elapsedTicks);
                        LoadContent();
                        wave.Update(surface, _randerer, gameTime, player1, null);
                        player1.Update(sprites, surface, _window, _randerer, laser, items.item.items);
                        laser.UpdateLaserShotsFromEnemy(player1, null, sprites);
                        laser.UpdateLaserShots(enemSprite);
                        laser.UpdateLaserShots(enemyFighter);
                        elapsedTime = stopwatch.Elapsed;
                        minutes = elapsedTime.Minutes;
                        seconds = elapsedTime.Seconds;
                        Draw();
                        #region End
                        Draw_Bild();
                        SDL.SDL_RenderPresent(_randerer);
                        #endregion
                    }

                }
            }
        }
        /// <summary>
        /// This method loads the content for the game by calling the loadContent() method for player1, laser and wave.
        /// It also adds player1 and all enemies in the enemSprite.Enemies list to the sprites list.
        /// </summary>
        public void LoadContent()
        {
            player1.loadContent();
            items.LoadContent();
            laser.loadContent();
            wave.LoadContent();
            items.LoadContent();
            #region addInTheList
            sprites.Add(player1);
            foreach (Enemy enemy in enemSprite.Enemies)
                sprites.Add(enemy);
            #endregion
        }
        /// <summary>
        /// This method updates the animation of the game by getting the running status of player1
        /// and calls the draw() method on player1, laser and wave objects.
        /// </summary>
        public void Draw()
        {
            running = player1.getRunning();
            player1.draw(surface, _randerer);
            laser.DrawLaser(surface, _randerer);
            items.Draw();
            wave.Draw(surface, _randerer);
        }
        /// <summary>
        /// This method updates the animation of the background, displaying the score, the remaining lives, the time, and the highscore.
        /// </summary>
        public void Draw_Bild()
        {
            hrect1 = new SDL.SDL_Rect()
            {
                x = 0,
                y = yPos1,
                w = 640,
                h = 480
            };
            // Definieren Sie die Position und Größe des Rechtecks
            hrect2 = new SDL.SDL_Rect()
            {
                x = 0,
                y = yPos2,
                w = 640,
                h = 480
            };
            // Kopieren Sie die Texture auf den Renderer an der spezifizierten Position
            SDL.SDL_RenderCopy(_randerer, _hintergrundTexture, IntPtr.Zero, ref hrect1);
            SDL.SDL_RenderCopy(_randerer, _hintergrundTexture, IntPtr.Zero, ref hrect2);
            _font.RenderText(_randerer,"Score: "+Laser._score, new SDL.SDL_Color { r = 250, g = 0, b = 0 }, 20, 20);
            _font.RenderText(_randerer, "Lives: " + Player.Mana+  " %", new SDL.SDL_Color { r = 124, g = 255, b = 0 }, 520, 20);
            _font.RenderText(_randerer,  minutes + ":" + seconds.ToString("D2") , new SDL.SDL_Color { r = 180, g = 70, b = 150 }, 300, 20);
            _font.RenderText(_randerer, "Highscore: " + Laser.highScore, new SDL.SDL_Color { r = 180, g = 70, b = 150 }, 500, 420);
            yPos1++;
            if (yPos1 > 460)
                yPos1 = -460;
            yPos2++;
            if (yPos2 > 460)
                yPos2 = -460;
            SDL.SDL_Delay(16);
        }
        #endregion
    }
}
