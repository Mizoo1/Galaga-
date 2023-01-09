using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Sprite;

namespace Galaga.Menu
{
    class SinglePlay : IGameState
    {
        private Game01 Verwalten;
        public SinglePlay(IntPtr window, IntPtr renderer)
        {
            Verwalten = new Game01(window, renderer);
            Update();
        }

        // Method to display the single play screen
        public void Update()
        {
            Verwalten.Setup();
        }

        public void Draw()
        {

        }

        // Method to handle player input
        public void HandleInput()
        {

        }
    }
}
