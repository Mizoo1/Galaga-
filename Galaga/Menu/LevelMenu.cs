// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @class     LevelMenu.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 * This class represents the level selection menu in the game.
 *========================================================================**/
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Galaga.Sprite;
using SDL2;

namespace Galaga.Menu
{
    public class LevelMenu : IGameState
    {
        #region Variable 
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the level menu textures and rectangles
        private IntPtr _easyTexture;
        private SDL.SDL_Rect _easyRect;
        private IntPtr _mediumTexture;
        private SDL.SDL_Rect _mediumRect;
        private IntPtr _hardTexture;
        private SDL.SDL_Rect _hardRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;

        #endregion 

        #region Constructor 
        /// <summary>
        /// The LevelMenu constructor initializes the window, renderer, and textures for the level menu.
        /// </summary>
        /// <param name="window">The SDL window</param>
        /// <param name="renderer">The SDL renderer</param>
        public LevelMenu(IntPtr window, IntPtr renderer)
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
            // Load the level menu textures and set the level menu rectangles
            _easyTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Easy.png");
            _easyRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 100,
                w = 150,
                h = 50

            };
            _mediumTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Medium.png");
            _mediumRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 180,
                w = 150,
                h = 50
            };
            _hardTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Hard.png");
            _hardRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 260,
                w = 150,
                h = 50
            };
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
        #endregion

        #region Methods 
        /// <summary>
        /// The Update method handles user input and updates the level menu's display.
        /// It polls for events and checks if the player has clicked on the back button
        /// or selected a level difficulty.
        /// </summary>
        public void Update()
        {
            UpdateButtonPositions();
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player clicked on the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _hintergrungRect.x && x <= _hintergrungRect.x + _hintergrungRect.w &&
                        y >= _hintergrungRect.y && y <= _hintergrungRect.y + _hintergrungRect.h)
                    {
                        // Transition back to the main menu state
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }
                // Check if the player clicked on the back button
                //Button(e, _backButtonRect, new SettingsMenu(_window, _renderer), 4);
            }
        }
        /// <summary>
        /// Draw method is used to render the level menu of the game.
        /// It clears the renderer and then renders the textures for 
        /// the background, easy, medium, and hard level buttons, and the back button.
        /// </summary>
        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the level menu textures
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _easyTexture, IntPtr.Zero, ref _easyRect);
            SDL.SDL_RenderCopy(_renderer, _mediumTexture, IntPtr.Zero, ref _mediumRect);
            SDL.SDL_RenderCopy(_renderer, _hardTexture, IntPtr.Zero, ref _hardRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        /// <summary>
        /// This method is used to handle user input for the level menu.
        /// It polls for events using the SDL_PollEvent() method and 
        /// checks for different types of events.
        /// If the player presses the Esc key, 
        /// it transitions to the settings menu state.
        /// If the player clicks on the back button,
        /// it transitions to the settings menu state.
        /// Additionally, if the player clicks on the easy,
        /// medium, or hard level button, it triggers an 
        /// event with an associated level number (1, 2, 3 respectively).
        /// </summary>
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the settings menu state
                    GameState.SetState(new SettingsMenu(_window, _renderer));
                }
                // Check if the player clicked on the back button
                Button(e, _easyRect, null, 1);

                // Check if the player clicked on the back button
                Button(e, _mediumRect, null, 2);

                // Check if the player clicked on the back button
                Button(e, _hardRect, null,3);
                
                // Check if the player clicked on the back button
                Button(e, _backButtonRect, new SettingsMenu(_window, _renderer),4);
            }
        }
        /// <summary>
        /// This method updates the position of the easy level button based on the mouse position.
        ///If the mouse is within the boundaries of the button and the x-coordinate of the button 
        ///is greater than 370, the x-coordinate of the button is decremented by 1 to move the button to the left.
        ///If the x-coordinate of the button is less than 400, the x-coordinate of the button is 
        ///incremented by 1 to move the button to the right.
        ///This creates a hover effect where the button moves slightly when the mouse is over it.
        /// </summary>
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _easyRect.x && mouseX <= _easyRect.x + _easyRect.w &&
                mouseY >= _easyRect.y && mouseY <= _easyRect.y + _easyRect.h && _easyRect.x > 370)
            {
                _easyRect.x -= 1; 
            }
            else if (_easyRect.x < 400)
            {
                _easyRect.x += 1; 
            }

            if (mouseX >= _mediumRect.x && mouseX <= _mediumRect.x + _mediumRect.w &&
                mouseY >= _mediumRect.y && mouseY <= _mediumRect.y + _mediumRect.h && _mediumRect.x > 370)
            {
                _mediumRect.x -= 1;
            }
            else if (_mediumRect.x < 400)
            {
                _mediumRect.x += 1;
            }

            if (mouseX >= _hardRect.x && mouseX <= _hardRect.x + _hardRect.w &&
                mouseY >= _hardRect.y && mouseY <= _hardRect.y + _hardRect.h && _hardRect.x > 370)
            {
                _hardRect.x -= 1;
            }
            else if (_hardRect.x < 400)
            {
                _hardRect.x += 1;
            }

            if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
                mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 370)
            {
                _backButtonRect.x -= 1;
            }
            else if (_backButtonRect.x < 400)
            {
                _backButtonRect.x += 1;
            }
        }
        /// <summary>
        /// This method is used to check if the player has clicked
        /// on a button in the level menu and if so, it performs an
        /// action based on the button that was clicked.
        /// </summary>
        /// <param name="e">  The SDL_Event object that contains 
        /// information about the event </param>
        /// <param name="_button"> The SDL_Rect object representing 
        /// the position and size of the button on the screen </param>
        /// <param name="state"> The IGameState object representing
        /// the state to transition to </param>
        /// <param name="i"> An integer representing the button 
        /// that was clicked. </param>
        public void Button(SDL.SDL_Event e, SDL.SDL_Rect _button, IGameState? state, int i)
        {
            // Check if the player clicked on the back button
            if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
            {
                int x = e.button.x;
                int y = e.button.y;
                if (x >= _button.x && x <= _button.x + _button.w &&
                    y >= _button.y && y <= _button.y + _button.h)
                {
                    switch(i)
                    {
                        case 1:
                            Enemy.enemyRadius = 30;
                            Sprite.Sprite.Radius = 20;
                            break;
                        case 2:
                            Enemy.enemyRadius = 20;
                            Sprite.Sprite.Radius = 30;
                            break;
                        case 3:
                            Enemy.enemyRadius = 11;
                            Sprite.Sprite.Radius = 40;
                            break;
                        case 4:
                            // Transition to the main menu state
                            GameState.SetState(state);
                            break;
                        default: break;
                    }
                }
            }
        }
        #endregion
    }
}
