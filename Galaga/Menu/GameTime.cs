// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @Class      GameTime.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @brief Diese Klasse repräsentiert die verstrichene Zeit im Spiel.
 * Die Klasse enthält Informationen über die verstrichenen Ticks und Sekunden des Spiels.
 * @property uint ElapsedTicks Enthält die Anzahl der verstrichenen Ticks seit dem Start des Spiels.
 * @property double ElapsedSeconds Enthält die Anzahl der verstrichenen 
 * Sekunden seit dem Start des Spiels, berechnet aus ElapsedTicks.
 * @constructor GameTime(uint elapsedTicks)
 * Erstellt eine neue Instanz der GameTime Klasse mit der angegebenen Anzahl
 * an verstrichenen Ticks.
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaga.Menu
{
    public class GameTime
    {
        #region Variable
        public uint ElapsedTicks { get; }
        public double ElapsedSeconds => ElapsedTicks / 1000.0;
        #endregion

        #region Constructor
        /**
        @brief Constructor for the GameTime class.
        Initializes the elapsed ticks property with the value passed to the constructor.
        @param elapsedTicks uint value representing the elapsed time in ticks.
        */
        public GameTime(uint elapsedTicks)
        {
            ElapsedTicks = elapsedTicks;
        }

        #endregion
    }
}
