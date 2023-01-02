using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Menu
{
    public class MenuOption : Constants
    {
        private IntPtr Texture;
        private IntPtr _renderer;
        private IntPtr _window;
        private SDL.SDL_Rect Rect;
        private String name;

        public MenuOption(String name, IntPtr _renderer, IntPtr _window)
        {
            this.name = name;
            this._renderer = _renderer;
            this._window = _window;
        }


        public void createMenu()
        {
            Texture = SDL_image.IMG_LoadTexture(_renderer, imagePaths[name]);
            switch (name)
            {
                case "background":
                    
                    Rect = new SDL.SDL_Rect()
                    {
                        x = 0,
                        y = 0,
                        w = 640,
                        h = 480
                    };
                    break;
                case "start":
                    
                    Rect = new SDL.SDL_Rect()
                    {
                        x = 400,
                        y = 100,
                        w = 150,
                        h = 50
                    };
                    break;
            }
        }

        public void draw()
        {
            SDL.SDL_RenderCopy(_renderer, Texture, IntPtr.Zero, ref Rect);
        }

        public void handleInput()
        {
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {

                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    // Quit the game
                    SDL.SDL_Quit();
                    Environment.Exit(0);
                }
            }
        }

        public void update()
        {

        }
    }
}
