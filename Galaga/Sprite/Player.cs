// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Player.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using Galaga.Objects;
using Galaga.Utility;
using SDL2;

namespace Galaga.Sprite
{
    public class Player : Sprite
    {
        public static bool running = true;
        private SDL.SDL_Rect smallSprite1;
        private SDL.SDL_Rect smallSprite2;
        private SDL.SDL_Rect smallSprite3;
        private Input input;
        public static int Mana ;
        public static int counter;
        public static  int index = 3;
        private Music music_heal;
        private Music music_shot;
        private Music music_explosion;
        public Player(IntPtr _texture, Input input) : base(_texture, 300, 420, 40, 40)
        {
            speed = 3;
            Radius = 35;
            this.input = input;
            Mana = 100;
            music_heal = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\heal.mp3");
            music_shot = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\collision.mp3");
            music_explosion  = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\explosion.mp3");
        }
        public Player(IntPtr _texture, Input input, int x, int y) : base(_texture, x, y, 40, 40)
        {
            speed = 3;
            Radius = 35;
            this.input = input;
            Mana = 100;
        }
        public void loadContent()
        {
            LoadContent();
            smallPlayer();


        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            // Erstellen Sie eine Texture aus der Surface
            Draw(surface, renderer);
            if (index == 3)
            {
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite3);
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite2);
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite1);
            }
            else if (index == 2)
            {
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite2);
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite1);
            }
            else if (index == 1)
            {
                SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite1);
            }
        }

        public bool getRunning() { return running; }

        //public SDL.SDL_Rect getSmallSprite() { return smallSprite; }
        public void Update(List<Sprite> sprites, IntPtr surface, IntPtr _window, IntPtr _renderer, Laser laser, List<Items> item)
        {
            HandleInput(surface, _window, _renderer, laser);

            bool manaReduced = false;
            bool musicPlayed = false;
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
                if(this.IsTouchingTop(sprite)|| this.IsTouchingBottom(sprite)|| this.IsTouchingLeft(sprite)|| this.IsTouchingRight(sprite))
                {
                    if (!manaReduced)
                    {
                        Player.Mana -= 1;
                        manaReduced = true;
                        if (!musicPlayed)
                        {
                            music_shot.Play();
                            musicPlayed = true;
                        }
                    }
                    else if (Player.Mana <= 0)
                    {
                        music_explosion.Play();
                        --index;
                        SDL.SDL_Delay(1000);
                        Player.Mana = 100;
                        if (index == 0)
                        {
                            SinglePlay.GameOver = true;
                            GameState.SetState(new HelpMenu(_window, _renderer));
                        }
                    }
                }
            }
            if(!StartMenu.check)
            {
                counter = 0;
                while (counter < item.Count)
                {
                    if ((this.Velocity.X > 0 && this.IsTouchingLeft(item[counter])) ||
                        (this.Velocity.X < 0 & this.IsTouchingRight(item[counter])) ||
                        (this.Velocity.Y > 0 && this.IsTouchingTop(item[counter])) ||
                        (this.Velocity.Y < 0 & this.IsTouchingBottom(item[counter])))
                    {
                        if (item[counter].GetType() == typeof(Heal))
                        {
                            Mana += 50;
                            music_heal.Play();
                            item.Remove(item[counter]);
                        }

                    }
                    counter++;
                }
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

        public IntPtr HandleInput(IntPtr surface, IntPtr _window, IntPtr _renderer, Laser laser)
        {
            // Verarbeite Ereignisse
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the "X" symbol with the Mouse
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_p)
                {
                    SinglePlay.isPaused = !SinglePlay.isPaused;

                }
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    _running(_window, _renderer);
                }
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == input.fire)
                {
                        laser.FireLaser(this);
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

        public  void _running (IntPtr _window, IntPtr _renderer)
        {
            running = false;
            // Transition to the main menu state
            GameState.SetState(new MainMenu(_window, _renderer));
        }

        public void smallPlayer()
        {
            // Definieren Sie die Position und Größe der beiden Rechteckobjekte
            smallSprite1 = new SDL.SDL_Rect()
            {
                x = 10,
                y = 450,
                w = 20,
                h = 20
            };
            smallSprite2 = new SDL.SDL_Rect()
            {
                x = 40,
                y = 450,
                w = 20,
                h = 20
            };
            smallSprite3 = new SDL.SDL_Rect()
            {
                x = 70,
                y = 450,
                w = 20,
                h = 20
            };
        }
    }
}
