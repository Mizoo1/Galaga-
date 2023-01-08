using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using static SDL2.SDL;

namespace Galaga.Sprite
{
    public class Game01
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
        private EnemSprite enemSprite;
        private EnemyFighter enemyFighter;
        private Wave wave;
        private List<Sprite> sprites;
        private Laser laser;
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
        public Game01(IntPtr window, IntPtr randerer)
        {
            this.window = window;
            this.randerer = randerer;
            initialize();
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
            _hintergrund = SDL_image.IMG_Load("D:\\One\\Desktop\\game\\PPong - Kopie\\PPong\\Assest\\Sterne.jpg");
            IntPtr EnemyFighter = LoadTexture(textureFilePaths["EnemyFighter"]);
            IntPtr playerTexture = LoadTexture(textureFilePaths["Player"]);
            IntPtr Enemy = LoadTexture(textureFilePaths["Enemy"]);
            IntPtr Laser = LoadTexture(textureFilePaths["Laser"]);
            player1 = new Player(playerTexture, new Input(SDL_Scancode.SDL_SCANCODE_W, SDL_Scancode.SDL_SCANCODE_S, SDL_Scancode.SDL_SCANCODE_A, SDL_Scancode.SDL_SCANCODE_D, SDL.SDL_Keycode.SDLK_SPACE));
            _hintergrundTexture = SDL.SDL_CreateTextureFromSurface(randerer, _hintergrund); ;
           

            sprites = new List<Sprite>();
        }
        public void Setup()
        {
            Update();
            cleanUp();
        }
        public void HandleInput()
        {
            player1.HandleInput(surface, window, randerer);
        }
        public void Update()
        {
            int yPos1 = 0;
            int yPos2 = -460;// y-Position des Hintergrundbildes
            bool running = true;
            startTicks = SDL.SDL_GetTicks();
            while (running)
            {
                HandleInput();
                player1.Update(sprites, surface, window, randerer);
                LoadContent();

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
                running = player1.getRunning();
                // Erstellen Sie eine Texture aus der Surface
                // Rendere Hintergrund
                SDL.SDL_SetRenderDrawColor(randerer, 0, 0, 0, 255);
                SDL.SDL_RenderClear(randerer);
                // Kopieren Sie die Texture auf den Renderer an der spezifizierten Position
                SDL.SDL_RenderCopy(randerer, _hintergrundTexture, IntPtr.Zero, ref hrect1);
                SDL.SDL_RenderCopy(randerer, _hintergrundTexture, IntPtr.Zero, ref hrect2);
                #endregion
                Draw();
                #region End
                SDL.SDL_RenderPresent(randerer);
                yPos1++;
                if (yPos1 > 460)
                    yPos1 = -460;
                yPos2++;
                if (yPos2 > 460)
                    yPos2 = -460;
                SDL.SDL_Delay(16);
                #endregion
            }
        }
        public void LoadContent()
        {


            player1.loadContent();
            sprites.Add(player1);
            #region addInTheList
            sprites.Add(player1);
            
            #endregion
        }
        public void Draw()
        {

            player1.draw(surface, randerer);


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
