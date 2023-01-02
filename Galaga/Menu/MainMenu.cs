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
        MenuOption start;
        private List<MenuOption> options;
        public MainMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            SDL.SDL_SetRenderDrawColor(_renderer, 0, 100, 170, 255);
            create();

        }

        public void create()
        {
            start = new MenuOption ("start", _renderer, _window);
            options.Add(start);
        }

        public void Update()
        {

        }

        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the button textures
            foreach (MenuOption menuOption in options)
                menuOption.draw();
        }

        // Method to handle player input
        public void HandleInput()
        {
            foreach (MenuOption menuOption in options)
                menuOption.handleInput();
        }
    }
}
