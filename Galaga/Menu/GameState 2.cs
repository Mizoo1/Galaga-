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

        // Property to get and set the current game state
        public static IGameState? State
        {
            get { return _state; }
            set { _state = value; }
        }

        // Method to set the game state
        public static void SetState(IGameState? state)
        {
            _state = state;
        }
    }
}
