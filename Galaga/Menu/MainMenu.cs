// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      MainMenu.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @brief     Represents the main menu of the game.
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Sprite;
using Galaga.Utility;
using SDL2;

namespace Galaga.Menu
{
    public class MainMenu : IGameState
    {
        #region Variables
        private Font _font;
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the button textures and rectangles
        MenuOption background;
        MenuOption start;
        MenuOption setting;
        MenuOption help;
        int highScore;
        MenuOption exit;
        MenuOption galaga;
        private List<MenuOption> options;
        private Music music;

        #endregion

        #region Constructor

        /// <summary>
        /// The MainMenu constructor initializes the game window and renderer, initializes SDL_ttf,
        /// creates a font, reads the highscore from a text file, creates the menu options,
        /// sets the render draw color and calls the create method. It also starts the background music.
        /// </summary>
        /// <param name="window">The window object of the game</param>
        /// <param name="renderer">The renderer object of the game</param>
        /// <exception cref="Exception">Throws exception if the highscore file does not exist or if there is an error initializing SDL_ttf</exception>
        public MainMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            music = new Music("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\select.mp3");
            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new Exception("Failed to initialize SDL_ttf: " + SDL_ttf.TTF_GetError());
            }
            _font = new Font("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Lato-Italic.ttf", 40);
            if (File.Exists("highscore.txt"))
            {
                String highscoreString = File.ReadAllText("highscore.txt");
                highScore = int.Parse(highscoreString);
            }
            options = new List<MenuOption>();
            SDL.SDL_SetRenderDrawColor(_renderer, 0, 100, 170, 255);
            create();
            music.Play();

        }
        #endregion

        #region Methods
        /**
        * @brief This method creates all menu options,
        * adds them to the options list, and calls the createMenu method for each menu option.
        */
        public void create()
        {
            // Create all menu options
            background = new MenuOption("background", _renderer, _window);
            start = new MenuOption("start", _renderer, _window);
            help = new MenuOption("help", _renderer, _window);
            setting = new MenuOption("setting", _renderer, _window);
            galaga = new MenuOption("galaga", _renderer, _window);
            exit = new MenuOption("exit", _renderer, _window);

            // Add all menu options to the list
            options.Add(background);
            options.Add(galaga);
            options.Add(start);
            options.Add(setting);
            options.Add(help);
            options.Add(exit);

            // Call the createMenu method for each menu option
            foreach (MenuOption menuOption in options)
                menuOption.createMenu();
        }
        /// <summary>
        /// The Update method updates the position of each menu option.
        /// </summary>
        public void Update()
        {
            // Update the position of each menu option
            start.UpdateButtonPositions();
            help.UpdateButtonPositions();
            setting.UpdateButtonPositions();
            exit.UpdateButtonPositions();

        }
        /// <summary>
        /// The Draw method clears the renderer, renders the button textures,
        /// and renders the highscore on the screen.
        /// </summary>
        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the button textures
            foreach (MenuOption menuOption in options)
                menuOption.Draw();
            _font.RenderText(_renderer, "Hightscore: " + highScore, new SDL.SDL_Color { r = 255, g = 255, b = 255 }, 100, 420);
        }

        /// <summary>
        /// The HandleInput method handles user input for all menu options.
        /// </summary>
        public void HandleInput()
        {
            foreach (MenuOption menuOption in options)
                menuOption.HandleInput();
        }
        #endregion
    }
}
