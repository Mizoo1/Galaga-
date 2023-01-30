// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
@file       Input.cs
@author     Muaaz Bdear, Nour Ahmad
@email      mbdear@stud.hs-bremen.de, Muaaz
@email      nahmed@stud.hs-bremen.de, Nour
@createdOn  18.01.2023
@version    1.0.0
@brief      This file contains the Input class which is used for handling keyboard inputs in the game.
@summary
            The Input class is responsible for handling the key inputs in the game.
            It contains fields to store the keycodes for the up, down, left, right and fire actions.
            The constructor of the class accepts these keycodes as parameters and assigns them to the corresponding fields.
            The class provides a simple way to store and access the keycodes for different actions in the game.
@contract
            The class guarantees that the keycodes for the different actions are stored and can be accessed by other classes in the game.
            However, it does not handle the actual input events from the user, it only stores the keycodes.
            It is the responsibility of other classes to handle the input events and check if the pressed 
            key matches the keycode stored in this class.
========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Utility
{
    /**
    @class Input
    @brief The Input class is responsible for handling user inputs in the game.
    It contains fields to store the scancodes for the movement keys (up, down, left, right)
    and the keycode for the fire button.
    The constructor of the class takes in the scancodes and keycode as parameters
    and assigns them to the corresponding fields.
    */
    public class Input
    {
        public SDL.SDL_Scancode keyUp;
        public SDL.SDL_Scancode keyDown;
        public SDL.SDL_Scancode keyLeft;
        public SDL.SDL_Scancode keyRight;
        public SDL.SDL_Keycode fire;
        /// <summary>
        /// This constructor of the Input class assigns the passed in parameters to the corresponding fields.
        /// The fields store the scancodes for the movement keys (up, down, left, right) and the keycode for the fire button.
        /// These fields are used to check user input in the game loop.
        /// </summary>
        /// <param name="keyUp"> SDL_Scancode for the up movement key </param>
        /// <param name="keyDown">  SDL_Scancode for the down movement key </param>
        /// <param name="keyLeft"> SDL_Scancode for the left movement key </param>
        /// <param name="keyRight"> SDL_Scancode for the right movement key </param>
        /// <param name="fire"> SDL_Keycode for the fire button </param>
        /**
         @precondition
         The passed in parameters must be valid scancodes and keycode for the movement and fire buttons respectively.
         @postcondition
         The fields for the movement and fire buttons will be initialized with the passed in scancodes and keycode.
         */
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
