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
        public SDL.SDL_Scancode keyUp;
        public SDL.SDL_Scancode keyDown;
        public SDL.SDL_Scancode keyLeft;
        public SDL.SDL_Scancode keyRight;
        public SDL.SDL_Keycode fire;
        public Input(SDL.SDL_Scancode keyUp, SDL.SDL_Scancode keyDown, SDL.SDL_Scancode keyLeft, SDL.SDL_Scancode keyRight, SDL.SDL_Keycode fire)
        {
            this.keyUp = keyUp;
            this.keyDown = keyDown;
            this.keyLeft = keyLeft;
            this.keyRight = keyRight;
            this.fire = fire;
        }
    }
}
