using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using SDL2;

namespace Galaga.Utility
{
    public class Game03 : IGameState
    {
        // Fields to store the SDL _window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        MenuOption background;
        private List<MenuOption> options;

        public Game03(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;
            SDL.SDL_SetRenderDrawColor(_renderer, 0, 100, 170, 255);
            options = new List<MenuOption>();
            create();

        }

        public void create()
        {
            background = new MenuOption("background", _renderer, _window);
            options.Add(background);
            foreach (MenuOption menuOption in options)
                menuOption.createMenu();

        }

        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the button textures
            foreach (MenuOption menuOption in options)
                menuOption.Draw();
        }

        public void HandleInput()
        {
            foreach (MenuOption menuOption in options)
                menuOption.HandleInput();
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
