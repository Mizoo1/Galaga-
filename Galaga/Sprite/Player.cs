using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using SDL2;

namespace Galaga.Sprite
{
    public class Player : Sprite
    {
        public static bool running = true;
        public SDL.SDL_Rect smallSprite;
        public Input input;
        public Player(IntPtr _texture, Input input) : base(_texture, 300, 420, 40, 40)
        {
            speed = 3;
            Radius = 35;
            this.input = input;
        }
        public Player(IntPtr _texture, Input input, int x, int y) : base(_texture, x, y, 40, 40)
        {
            speed = 3;
            Radius = 35;
            this.input = input;
        }
        public void loadContent()
        {
            LoadContent();

            // Definieren Sie die Position und Größe der beiden Rechteckobjekte
            smallSprite = new SDL.SDL_Rect()
            {
                x = 10,
                y = 450,
                w = 20,
                h = 20
            };
        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            // Erstellen Sie eine Texture aus der Surface
            Draw(surface, renderer);
            SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite);
        }
        public bool getRunning() { return running; }
        public SDL.SDL_Rect getSmallSprite() { return smallSprite; }
        public void Update(List<Sprite> sprites, IntPtr surface, IntPtr _window, IntPtr _renderer)
        {
            HandleInput(surface, _window, _renderer);


            foreach (var sprite in sprites)
            {

                if (sprite == this)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))
                    Velocity.X = 0;

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                    Velocity.Y = 0;
            }
            Y += Velocity.Y;
            X += Velocity.X;
            Velocity.Y = 0;
            Velocity.X = 0;
        }

        public void MoveUp()
        {
            if (Y > 0) Velocity.Y -= (int)speed;
        }
        public void MoveDown()
        {
            if (Y < 480 - sprite.h) Velocity.Y += (int)speed;
        }
        public void MoveLeft()
        {
            if (X > 0) Velocity.X -= (int)speed;
        }
        public void MoveRight()
        {
            if (X < 640 - sprite.w) Velocity.X += (int)speed;
        }

        public IntPtr HandleInput(IntPtr surface, IntPtr _window, IntPtr _renderer)
        {
            // Verarbeite Ereignisse
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    running = false;
                }
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == input.fire)
                {
                    //TODO
                }
                // Check if the player pressed the f key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_f)
                {
                    // Toggle fullscreen mode
                    if ((SDL.SDL_GetWindowFlags(_window) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) == 0)
                    {
                        SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
                    }
                    else
                    {
                        SDL.SDL_SetWindowFullscreen(_window, 0);
                    }
                }
            }
            // An array to store the current state of the keyboard keys
            byte[] currentKeyStates = new byte[(int)SDL.SDL_Scancode.SDL_NUM_SCANCODES];
            int numKeys = 256;
            IntPtr keyStatePtr = SDL.SDL_GetKeyboardState(out numKeys);
            _ = new byte[numKeys];
            Marshal.Copy(keyStatePtr, currentKeyStates, 0, numKeys);

            // Bewegung des Rechtecks
            if (currentKeyStates[(int)SDL.SDL_Scancode.SDL_SCANCODE_W] != 0)
            {
                MoveUp(); // Erhöhe Y-Koordinate um speed

            }
            if (currentKeyStates[(int)SDL.SDL_Scancode.SDL_SCANCODE_S] != 0)
            {
                MoveDown(); // Verringere Y-Koordinate um speed

            }
            if (currentKeyStates[(int)SDL.SDL_Scancode.SDL_SCANCODE_A] != 0)
            {
                MoveLeft();// Verringere X-Koordinate um speed


            }
            if (currentKeyStates[(int)SDL.SDL_Scancode.SDL_SCANCODE_D] != 0)
            {
                MoveRight(); // Erhöhe X-Koordinate um speed

            }
            return surface;
        }

    }
}
