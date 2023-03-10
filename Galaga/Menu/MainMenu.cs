using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Menu
{
    public class MainMenu : IGameState
    {
        // Fields to store the SDL window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the button textures and rectangles
        MenuOption background;
        MenuOption start;
        MenuOption setting;
        MenuOption help;
        MenuOption exit;
        private List<MenuOption> options;
        public MainMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            options = new List<MenuOption>();
            SDL.SDL_SetRenderDrawColor(_renderer, 0, 100, 170, 255);
            create();

        }

        public void create()
        {
            background = new MenuOption("background", _renderer, _window);
            start = new MenuOption ("start", _renderer, _window);
            help = new MenuOption("help", _renderer, _window);
            setting = new MenuOption("setting", _renderer, _window);
            exit = new MenuOption("exit", _renderer, _window);
            options.Add(background);
            options.Add(start);
            options.Add(setting);
            options.Add(help);
            options.Add(exit);

            foreach (MenuOption menuOption in options)
                menuOption.createMenu();
        }

        public void Update()
        {
            start.UpdateButtonPositions();
            help.UpdateButtonPositions();
            setting.UpdateButtonPositions();
            exit.UpdateButtonPositions();
            
        }

        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the button textures
            foreach (MenuOption menuOption in options)
                menuOption.Draw();
        }

        // Method to handle player input
        public void HandleInput()
        {
            
            foreach (MenuOption menuOption in options)
                menuOption.HandleInput();
            
        }
    }
}
