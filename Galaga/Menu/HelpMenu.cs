// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation
/**=====================================================================================================
@contract      HelpMenu
@author        Muaaz Bdear, Nour Ahmad
@email         mbdear@stud.hs-bremen.de, nahmed@stud.hs-bremen.de
@brief         The HelpMenu class is responsible for displaying the help screen in the game.
               It contains fields to store the SDL window and renderer, as well as textures
               and rectangles for the help screen and back button.
               It also includes the functionality for handling user input and updating
               the display of the help screen.
@invariant     The HelpMenu class must have valid SDL window and renderer IntPtrs.
               The textures and rectangles for the help screen, back button and background must be initialized.
               The class must have valid Font and Music objects.
@dependencies  The HelpMenu class depends on the SDL library for window and renderer creation,
               as well as handling input and rendering textures.
               It also depends on the SDL_image library for loading textures.
               It also depends on the SDL_ttf library for creating and rendering text.
               It also depends on the Music and Font classes for background music and font rendering.
@usage         The HelpMenu class is used to display the help screen in the game.
               It is called when the user selects the "Help" option from the main menu.
               It allows the user to view the game controls and instructions, 
               and also provides a way to return to the main menu.
*===================================================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Sprite;
using Galaga.Utility;
using SDL2;
namespace Galaga.Menu
{
    class HelpMenu : IGameState
    {
        #region Variable
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the help textures and rectangles
        private IntPtr _helpTexture;
        private SDL.SDL_Rect _helpRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;
        private Font _font;
        private Font _font_2;
        private Music music;

        #endregion

        #region Constructor
        /// <summary>
        /// The constructor of HelpMenu class initializes the SDL window and
        /// renderer by assigning the passed in parameters to the corresponding fields.
        /// It creates a new Music object and initializes the TTF library.
        /// Then it creates font objects and loads the textures for the
        /// background, back button, and help screen.
        /// It sets the size and positions of rectangles for these textures 
        /// using SDL_Rect struct. Additionally, it plays background music.
        /// </summary>
        /// <param name="window"> window IntPtr of the window </param>
        /// <param name="renderer"> renderer IntPtr of the renderer </param>
        /// <exception cref="Exception"> Failed to initialize SDL_ttf </exception>
        public HelpMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            music = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\select.mp3");
            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
            }
            _font = new Font("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Lato-Italic.ttf", 22);
            _font_2 = new Font("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Lato-Italic.ttf", 30);

            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\rr.jpg");

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
            music.Play();
        }
        #endregion

        #region Methods
        /// <summary>
        /// The Update method in the HelpMenu class is responsible for displaying
        /// the help screen and handling user input. It starts by calling the
        /// UpdateButtonPositions method to update the positions of the buttons
        /// on the screen. Then, it polls for events and checks if the player 
        /// clicked the back button. If so, it transitions back to the main
        /// menu by calling the GameState.SetState method with a new MainMenu object.
        /// </summary>
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
        /// <summary>
        /// The method Draw() is responsible for displaying the help menu of the game.
        /// It first clears the renderer and then renders the background texture,
        /// the back button texture, and the text explaining the game controls and
        /// the game's goal. The text is rendered using the SDL_TTF library and
        /// is rendered in different positions on the screen with different colors.
        /// It also renders the game title "GALAGA" using SDL_TTF library and renders
        /// text at different positions with different colors. The method also renders
        /// the text describing the game controls, game's goal and hope for fun playing the game.
        /// </summary>
        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);

            // Render the help texture
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
            _font_2.RenderText(_renderer, "GALAGA", new SDL.SDL_Color { r = 180, g = 0, b = 0 }, 260, 20);
            _font.RenderText(_renderer, "Galaga is a classic arcade game developed by Namco and released ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 80);
            _font.RenderText(_renderer, " in 1981. The game is developed by Muaaz Bdear and Nour Ahmad", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 120);
            _font.RenderText(_renderer, "and the goal is to destroy enemy spacecraft by shooting them", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 160);
            _font.RenderText(_renderer, "with your own spaceship.Press 'Q' to close the game,", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 200);
            _font.RenderText(_renderer, "'F' for full screen mode, 'W' to move up, 's' to move ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 240);
            _font.RenderText(_renderer, "down, 'D' to move right, 'A' to move left, ", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 280);
            _font.RenderText(_renderer, "and 'SPACE' to shoot. 'P' to pause", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 320);
            _font.RenderText(_renderer, "and 'ESC' to return to the menu..", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 360);
            _font.RenderText(_renderer, "We hope you have fun playing the game!", new SDL.SDL_Color { r = 240, g = 240, b = 230 }, 20, 400);
        }
        /// <summary>
        /// The method "HandleInput()" in the HelpMenu class is responsible for handling
        /// user input and responding to it. It polls for events and checks for specific
        /// key presses and mouse clicks, such as the Esc key, "X" symbol, or the back button.
        /// When the appropriate input is detected, the method changes the game state accordingly,
        /// such as transitioning back to the main menu or quitting the game.
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
        /// <summary>
        /// The UpdateButtonPositions() method updates the position of the back button
        /// on the help screen. It first retrieves the current position of the mouse and
        /// checks if it is hovering over the back button. If it is, the x-coordinate of
        /// the button's rectangle is decreased by 1, creating a "hover" effect.
        /// If the mouse is not hovering over the button and the button is not in its original position,
        /// the x-coordinate is increased by 1, moving the button back to its original position.
        /// </summary>
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
                mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 370)
            {
                // Mouse is hovering over the sound button
                _backButtonRect.x -= 1; 
            }
            else if (_backButtonRect.x < 400)
            {
                _backButtonRect.x += 1; // Move the button back to its original position
            }
        }
        #endregion
    }
}

