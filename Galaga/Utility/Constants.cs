// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Constants.cs
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

namespace Galaga.Utility
{
    /// <summary>
    /// This abstract class contains constant values and image paths used throughout the game.
    /// </summary>
    public abstract class Constants
    {
        #region Variables
        /// <summary>
        /// Dictionary containing the image paths for various game elements
        /// </summary>
        public Dictionary<string, string> imagePaths = new Dictionary<string, string>()
        {
            {"background", "./Assest/Background.png"},
            {"start", "./Assest/Start.png"},
            {"setting","./Assest/Setting.png" },
            {"help","./Assest/Help.png" },
            {"exit", "./Assest/Quit.png"},
            {"play","./Assest/Play.png"},
            {"multiPlay","./Assest/Multiplay.png"},
            {"Music_ON","./Assest/music_on.png"},
            {"Music_OFF","./Assest/music_off.png"},
            {"galaga","./Assest/galaga.png"},
            {"back","./Assest/Back.png"}

        };
        #endregion
    }


}
