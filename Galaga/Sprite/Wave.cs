// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Wave.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;

namespace Galaga.Sprite
{
    /// <summary>
    /// The Wave class is responsible for managing the waves of enemies in the game.
    /// It holds the EnemSprite and EnemyFighter objects, and alternates between creating them to create a wave of enemies.
    /// It also handles updating and drawing the enemies, as well as loading their content.
    /// </summary>
    public class Wave
    {
        #region Variables

        private EnemSprite EnemSprite;
        private EnemyFighter EnemyFighter;
        private int counter;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Wave class.
        /// Initializes the EnemSprite and EnemyFighter properties and sets the counter to 6.
        /// </summary>
        /// <param name="EnemSprite">The EnemSprite object to be used in the wave.</param>
        /// <param name="EnemyFighter">The EnemyFighter object to be used in the wave.</param>
        public Wave(EnemSprite EnemSprite, EnemyFighter EnemyFighter)
        {
            this.EnemSprite = EnemSprite;
            this.EnemyFighter = EnemyFighter;
            this.counter = 6;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to create the first wave of enemies.
        /// Alternates between creating EnemSprite enemies and EnemyFighter enemies based on the value of the counter.
        /// The counter is decremented each time an enemy is created.
        ///
        /// <param name= "surface"> The surface to be used to create the enemies. </param> 
        /// <param name = "renderer" >  The renderer to be used to create the enemies.</param> 
        public void createWaveOne(IntPtr surface, IntPtr renderer)
        {
            if (counter > 3 || counter == 0)
            {
                EnemSprite.createEnemy(surface, renderer);
                counter--;
            }
            else
            {
                EnemyFighter.createEnemy(surface, renderer);
                counter--;
            }
        }
        /// <summary>
        /// Method to update the wave of enemies.
        /// If both EnemSprite enemies and EnemyFighter enemies are defeated, creates a new wave.
        /// Calls the update method for EnemSprite and EnemyFighter enemies.
        /// </summary>
        /// <param name="surface">The surface used to update the enemies.</param>
        /// <param name="renderer">The renderer used to update the enemies.</param>
        /// <param name="gameTime">The current game time.</param>
        /// <param name="player">The player object.</param>
        /// <param name="player2">The second player object.</param>
        public void Update(IntPtr surface, IntPtr renderer, GameTime gameTime, Player player, Player player2)
        {
            if (EnemSprite.Enemies.Count == 0 && EnemyFighter.Enemies.Count == 0)
                createWaveOne(surface, renderer);
            EnemSprite.update(surface, renderer);
            EnemyFighter.update(surface, renderer, gameTime, player, player);
        }
        /// <summary>
        /// Method to load the content for the wave, including the textures for the enemy sprites and enemy fighters.
        /// </summary>
        public void LoadContent()
        {
            EnemyFighter.loadContent();
            EnemSprite.loadContent();
        }
        /// <summary>
        /// Method to draw the wave, including the enemy sprites and enemy fighters, on the game screen.
        /// </summary>
        /// <param name="surface">The surface of the game _window</param>
        /// <param name="renderer">The renderer of the game _window</param>
        public void Draw(IntPtr surface, IntPtr renderer)
        {
            EnemyFighter.draw(surface, renderer);
            EnemSprite.draw(surface, renderer);
        }
        #endregion
    }
}
