// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Vector2D.cs
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
    /// The Vector2D class represents a 2D vector with an X and Y coordinate.
    /// It contains methods for setting the X and Y coordinates.
    /// </summary>
    public class Vector2D
    {
        #region Variables
        public int X;
        public int Y;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for the Vector2D class.
        /// Initializes the X and Y properties to 0.
        /// </summary>
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }
        /// <summary>
        /// Parameterized constructor for the Vector2D class.
        /// Initializes the X and Y properties to the given values.
        /// </summary>
        /// <param name="X">The value to initialize the X property to.</param>
        /// <param name="Y">The value to initialize the Y property to.</param>
        public Vector2D(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to set the value of the X property.
        /// </summary>
        /// <param name="x">The value to set the X property to.</param>
        public void setX(int x)
        {
            X += x;
        }
        /// <summary>
        /// Method to set the value of the Y property.
        /// </summary>
        /// <param name="y">The value to set the Y property to.</param>
        public void setY(int y)
        {
            Y += y;
        }
        #endregion
    }
}
