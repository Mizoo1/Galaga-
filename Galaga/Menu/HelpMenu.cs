
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
namespace Galaga.Menu
{
    class HelpMenu : IGameState
    {
        // Fields to store the SDL window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the help textures and rectangles
        private IntPtr _helpTexture;
        private SDL.SDL_Rect _helpRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;

        // Constructor
        public HelpMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;

            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Background.png");

            _hintergrungRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = 640,
                h = 480
            };

            // Load the help texture and set the help rectangle
            _backTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Back.png");
            _backButtonRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 340,
                w = 150,
                h = 50
            };
        }

        // Method to display the help screen
        public void Update()
        {
            UpdateButtonPositions();
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player clicked the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _backButtonRect.x && x <= _backButtonRect.x + _backButtonRect.w &&
                        y >= _backButtonRect.y && y <= _backButtonRect.y + _backButtonRect.h)
                    {
                        // Transition back to the main menu
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }
            }
        }


        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);

            // Render the help texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        // Method to handle player input
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }

                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }

                // Check if the player pressed the "X" symbol with the Mouse
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
                // Check if the player clicked on the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _backButtonRect.x && x <= _backButtonRect.x + _backButtonRect.w &&
                        y >= _backButtonRect.y && y <= _backButtonRect.y + _backButtonRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }
            }
        }
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
                mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 370)
            {
                // Mouse is hovering over the sound button
                _backButtonRect.x -= 1; // Move the button to the left
            }
            else if (_backButtonRect.x < 400)
            {
                _backButtonRect.x += 1; // Move the button back to its original position
            }

        }
    }
}

