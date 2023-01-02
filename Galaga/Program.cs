using SDL2;
using static SDL2.SDL;
using System;
using Galaga.Menu;

namespace Pong
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SDL
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            // Create the window and renderer
            IntPtr window = SDL.SDL_CreateWindow
                (
                "Galaga",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                640,
                480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
                );
            IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            // Set the initial game state to the main menu
            GameState.State = new MainMenu(window,renderer);

            

            // Run the game loop
            while (GameState.State !=null)
            {
                // Handle input
                GameState.State.HandleInput();

                // Update the game state
                GameState.State.Update();

                // Display the game state
                GameState.State.Draw();

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