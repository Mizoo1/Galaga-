// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Program.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using static SDL2.SDL;
using Galaga.Menu;
using System;
using Galaga;
using System.Xml.Linq;
namespace Galaga;
internal class Program 
{
    private static void Main(string[] args)
    {
        #region Initialize SDL
        // Initialize SDL
        SDL_Init(SDL_INIT_EVERYTHING);

        // Create the _window and renderer
        var window = SDL_CreateWindow
        (
            "Galaga",
            SDL_WINDOWPOS_CENTERED,
            SDL_WINDOWPOS_CENTERED,
            640,
            480,
            SDL_WindowFlags.SDL_WINDOW_SHOWN
        );
        // Load the icon image
        string LOGO = "./Assest/Flugzeug.bmp";
        var icon = SDL_LoadBMP(LOGO);

        // Set the _window icon
        SDL_SetWindowIcon(window, icon);

        var renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        // Set the initial game state to the main menu
        GameState.State = new MainMenu(window, renderer);
        #endregion
        #region GameLoop
        // Run the game loop
        while (GameState.State != null)
        {
            
            // Handle input
            GameState.State.HandleInput();

            // Update the game state
            GameState.State.Update();

            // Display the game state
            GameState.State.Draw();

            // Update the _window
            SDL_RenderPresent(renderer);
        }
        #endregion
        #region Clean up
        // Clean up
        SDL_DestroyRenderer(renderer);
        SDL_DestroyWindow(window);
        SDL_Quit();
        #endregion
    }
}