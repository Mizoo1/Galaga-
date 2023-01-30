// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      GameState.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @class GameState
 * @brief The GameState class is used to manage and maintain the current state of the game.
 *        The GameState class stores the current game state in a private static field and provides a public static property
 *        and method to get and set the current game state. The class implements the IGameState interface, which allows it to
 *        be used as the current game state in the game loop.
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Menu
{
    public class GameState
    {
        // Field to store the current game state
        private static IGameState? _state;

        /**
        @property State
        @brief Gets or sets the current game state.
        This property is used to get and set the current game state in the game loop.
        It is a static property and can be
        accessed directly from the GameState class.
        */
        public static IGameState? State
        {
            get { return _state; }
            set { _state = value; }
        }

        /**
        @method SetState
        @brief Sets the current game state.
        @param state The new game state to set.
        This method is used to set the current game state in the game loop.
        It is a static method and can be accessed directly
        from the GameState class. It sets the value of the State property to the given state.
        */
        public static void SetState(IGameState? state)
        {
            _state = state;
        }
    }
}
