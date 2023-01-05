using SDL2;
using static SDL2.SDL;
using System;

namespace Pong
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SDL
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            // Create the window and renderer
            IntPtr window = SDL.SDL_CreateWindow("Game", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 640,
                480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            

            // Run the game loop
            while (true)
            {
                
                // Update the window
                SDL.SDL_RenderPresent(renderer);
            }

            // Clean up
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
    }
}