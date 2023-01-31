// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @class     StartMenu
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, nahmed@stud.hs-bremen.de
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @summary
 * StartMenu is a class that implements the IGameState interface. It is responsible for displaying
 * the start menu of the game, which allows the player to choose between single player and
 * multiplayer modes, as well as navigate back to the main menu. It contains fields for storing the
 * SDL _window and renderer, as well as textures and rectangles for the menu elements. It also
 * includes methods for updating the menu, handling input, and drawing the menu on the screen.
 *========================================================================**/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Galaga.Sprite;
using SDL2;

namespace Galaga.Menu
{
    class StartMenu : IGameState
    {
        #region Variables
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the game textures and rectangles
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergroundRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _doublePlayTexture;
        private SDL.SDL_Rect _doublePlayRect;
        private IntPtr _singlePlayTexture;
        private SDL.SDL_Rect _singlePlayRect;
        public static bool check = false;
        Player player;

        #endregion

        #region Constructor
        // Constructor
        public StartMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            player = new Player(IntPtr.Zero,null);

            // Load the game texture and set the game rectangle
            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "./Assest/Background.png");
            _hintergroundRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = 640,
                h = 480
            };
            _singlePlayTexture = SDL_image.IMG_LoadTexture(_renderer,
                "./Assest/Play.png");
            _singlePlayRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 100,
                w = 150,
                h = 50
            };

            _doublePlayTexture = SDL_image.IMG_LoadTexture(_renderer,
                "./Assest/Multiplay.png");
            _doublePlayRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 180,
                w = 150,
                h = 50
            };
            _backTexture = SDL_image.IMG_LoadTexture(_renderer,
                "./Assest/Back.png");
            _backButtonRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 260,
                w = 150,
                h = 50
            };

        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to update the start menu screen.
        /// Polls for events and checks if the player pressed the back button.
        /// If so, transitions back to the main menu state.
        /// Also calls the UpdateButtonPositions method to update the positions of the buttons on the screen.
        /// </summary>
        public void Update()
        {
            UpdateButtonPositions();
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the back button
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition back to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
                }
            }
        }
        /// <summary>
        ///  Method to draw the start menu screen.
        ///  Clears the renderer and renders the game textures for the background, double play button, single play button, and back button.
        /// </summary>
        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);

            // Render the game texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergroundRect);
            SDL.SDL_RenderCopy(_renderer, _doublePlayTexture, IntPtr.Zero, ref _doublePlayRect);
            SDL.SDL_RenderCopy(_renderer, _singlePlayTexture, IntPtr.Zero, ref _singlePlayRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        /// <summary>
        /// Method to handle player input for the start menu state.
        /// Polls for events and checks for key presses and mouse clicks.
        /// Pressing the Q key or clicking the "X" symbol will quit the game.
        /// Pressing the Esc key or clicking the back button will transition back to the main menu state.
        /// Pressing the F key will toggle fullscreen mode.
        /// Clicking on the Play button will transition to the single play state.
        /// Clicking on the DoublePlay button will transition to the double play state.
        /// </summary>
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Q key
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

                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the main menu state
                    GameState.SetState(new MainMenu(_window, _renderer));
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
                // Check if the player clicked on the Play button

                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _singlePlayRect.x && x <= _singlePlayRect.x + _singlePlayRect.w &&
                        y >= _singlePlayRect.y && y <= _singlePlayRect.y + _singlePlayRect.h)
                    {
                        // Transition to the main menu state
                        Player.running = true;
                        SinglePlay.isPaused = false;
                        SinglePlay.GameOver = false;
                        check = false;
                        GameState.SetState(new SinglePlay(_window, _renderer));
                        
                    }
                }
                // Check if the player clicked on the DoublePlay button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _doublePlayRect.x && x <= _doublePlayRect.x + _doublePlayRect.w &&
                        y >= _doublePlayRect.y && y <= _doublePlayRect.y + _doublePlayRect.h)
                    {
                        // Transition to the main menu state
                        Player.running = true;
                        SinglePlay.isPaused = false;
                        SinglePlay.GameOver = false;
                        check = true;
                        GameState.SetState(new DoublePlay(_window, _renderer));
                    }
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
        /// <summary>
        /// UpdateButtonPositions method is used to update the positions of the buttons in the StartMenu class.
        /// It checks whether the mouse is hovering over the single play button, double play button or back button.
        /// If the mouse is hovering over the button, the button is moved to the left.
        /// If the mouse is not hovering over the button, the button is moved back to its original position.
        /// </summary>
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _singlePlayRect.x && mouseX <= _singlePlayRect.x + _singlePlayRect.w &&
                mouseY >= _singlePlayRect.y && mouseY <= _singlePlayRect.y + _singlePlayRect.h && _singlePlayRect.x > 370)
            {
                // Mouse is hovering over the sound button
                _singlePlayRect.x -= 1; // Move the button to the left
            }
            else if (_singlePlayRect.x < 400)
            {
                _singlePlayRect.x += 1; // Move the button back to its original position
            }
            if (mouseX >= _doublePlayRect.x && mouseX <= _doublePlayRect.x + _doublePlayRect.w &&
                mouseY >= _doublePlayRect.y && mouseY <= _doublePlayRect.y + _doublePlayRect.h && _doublePlayRect.x > 370)
            {
                // Mouse is hovering over the sound button
                _doublePlayRect.x -= 1; // Move the button to the left
            }
            else if (_doublePlayRect.x < 400)
            {
                _doublePlayRect.x += 1; // Move the button back to its original position
            }
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
        #endregion
    }
}