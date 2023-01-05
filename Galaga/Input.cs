using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga
{
    public  class Input
    {
        public SDL.SDL_Keycode keyUp;
        public SDL.SDL_Keycode keyDown;
        public SDL.SDL_Keycode keyLeft;
        public SDL.SDL_Keycode keyRight;
        public SDL.SDL_Keycode fire;
        public Input(SDL.SDL_Keycode keyUp, SDL.SDL_Keycode keyDown, SDL.SDL_Keycode keyLeft, SDL.SDL_Keycode keyRight, SDL.SDL_Keycode fire)
        {
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            this.keyLeft = keyLeft;
            this.keyRight = keyRight;
            this.fire = fire;
        }
    }
}
