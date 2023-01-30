// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**================================================================================================
 * @class     sprite
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, nahmed@stud.hs-bremen.de
 * @createdOn 18.01.2023
 * @version   1.0.0
 * @brief     This class contains information and functionality for a sprite in a Galaga game.
 * It includes properties such as position (X, Y), size (WSize, HSize),
 * velocity (Velocity) and texture (_texture).
 * It also includes methods for loading content, drawing, getting and setting X and Y,
 * getting the sprite property and collision detection.
 *================================================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Utility;
using SDL2;

namespace Galaga.Sprite
{
    public class Sprite 
    {
        #region Variable
        public SDL.SDL_Rect sprite;
        public IntPtr _texture;
        public Vector2D Velocity;
        public int X; public int Y;
        public int WSize; public int HSize;
        public int Left; public int Right; public int Top; public int Bottom;
        public static float speed;
        public static float Radius;
        #endregion

        #region Constructor
        /// <summary>
        /// @brief Constructor for creating a new sprite object
        /// </summary>
        /// <param name="_texture"> The texture for the sprite </param>
        /// <param name="x"> The x-coordinate for the position of the sprite </param>
        /// <param name="y"> The y-coordinate for the position of the sprite </param>
        /// <param name="WSize"> The width of the sprite </param>
        /// <param name="HSize"> The height of the sprite</param>
        /// This constructor creates a new sprite object with the provided texture,
        /// position, width and height. It also initializes the velocity to a new Vector2D.
        /// </summary>
        public Sprite(IntPtr _texture, int x, int y, int WSize, int HSize)
        {
            this.WSize = WSize;
            this.HSize = HSize;
            this.X = x;
            this.Y = y;
            this._texture = _texture;
            this.Velocity = new Vector2D();

        }
        #endregion

        #region Mthodes
        /// <summary>
        /// @brief Loads the content for the sprite
        /// @summary This method is used to load the content for the sprite, including
        /// setting the SDL_Rect properties (x, y, w, h) based on the X, Y, WSize, and HSize 
        /// properties, and also sets the Left, Right, Top, and Bottom properties of the sprite.
        /// </summary>
        public void LoadContent()
        {
            sprite = new SDL.SDL_Rect()
            {
                x = X,
                y = Y,
                w = WSize,
                h = HSize
            };
            Left = X;
            Right = X + sprite.w;
            Top = Y;
            Bottom = Y + sprite.h;
        }
        /// <summary>
        /// @brief Draws the sprite on the provided surface
        /// @summary This method is used to draw the sprite on the provided surface
        /// using the provided renderer. It creates a texture from the surface and 
        /// then uses the SDL_RenderCopy function to render the texture to the screen
        /// at the sprite's position.
        /// </summary>
        /// <param name="surface"> The surface where the sprite will be drawn </param>
        /// <param name="renderer"> The renderer used to draw the sprite </param>
        public void Draw(IntPtr surface, IntPtr renderer)
        {
            // Erstellen Sie eine Texture aus der Surface
            SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref sprite); ;
        }

        #region Collisoin
        /// <summary>
        /// @brief Detects if the sprite is touching the left side of another sprite
        /// This method is used to detect if the current sprite is touching the left
        /// side of the provided sprite. It checks if the right side of the current
        /// sprite plus its velocity on the x-axis is greater than the left side of
        /// the other sprite, and if the left side of the current sprite is less than 
        /// the left side of the other sprite, and if the bottom side of the current 
        /// sprite is greater than the top side of the other sprite and if the top side
        /// of the current sprite is less than the bottom side of the other sprite.
        /// </summary>
        /// <param name="sprite"> The other sprite that is being checked for collision </param>
        /// <returns></returns>
        public bool IsTouchingLeft(Sprite sprite)
        {
            return Right + Velocity.X > sprite.Left &&
                Left < sprite.Left &&
                Bottom > sprite.Top &&
                Top < sprite.Bottom;
        }
        /// <summary>
        /// @brief Detects if the sprite is touching the right side of another sprite
        /// This method is used to detect if the current sprite is touching the right 
        /// side of the provided sprite. It checks if the left side of the current sprite
        /// plus its velocity on the x-axis is less than the right side of the other sprite,
        /// and if the right side of the current sprite is greater than the right side of
        /// the other sprite, and if the bottom side of the current sprite is greater than 
        /// the top side of the other sprite and if the top side of the current sprite is
        /// less than the bottom side of the other sprite.
        /// </summary>
        /// <param name="sprite"> The other sprite that is being checked for collision </param>
        /// <returns></returns>
        public bool IsTouchingRight(Sprite sprite)
        {
            return Left + Velocity.X < sprite.Right &&
              Right > sprite.Right &&
              Bottom > sprite.Top &&
              Top < sprite.Bottom;
        }
        /// <summary>
        /// @brief Detects if the sprite is touching the top side of another sprite
        /// This method is used to detect if the current sprite is touching the top 
        /// side of the provided sprite. It checks if the bottom side of the current
        /// sprite plus its velocity on the y-axis is greater than the top side of 
        /// the other sprite, and if the top side of the current sprite is less than
        /// the top side of the other sprite, and if the right side of the current 
        /// sprite is greater than the left side of the other sprite and if the left
        /// side of the current sprite is less than the right side of the other sprite.
        /// </summary>
        /// <param name="sprite"> The other sprite that is being checked for collision </param>
        /// <returns></returns>
        public bool IsTouchingTop(Sprite sprite)
        {
            return Bottom + Velocity.Y > sprite.Top &&
              Top < sprite.Top &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }
        /// <summary>
        /// @brief Detects if the sprite is touching the bottom side of another sprite
        /// This method is used to detect if the current sprite is touching the bottom
        /// side of the provided sprite. It checks if the top side of the current sprite
        /// plus its velocity on the y-axis is less than the bottom side of the other sprite,
        /// and if the bottom side of the current sprite is greater than the bottom side of
        /// the other sprite, and if the right side of the current sprite is greater than the
        /// left side of the other sprite and if the left side of the current sprite is less
        /// than the right side of the other sprite.
        /// </summary>
        /// <param name="sprite"> The other sprite that is being checked for collision </param>
        /// <returns></returns>
        public bool IsTouchingBottom(Sprite sprite)
        {
            return Top + Velocity.Y < sprite.Bottom &&
              Bottom > sprite.Bottom &&
              Right > sprite.Left &&
              Left < sprite.Right;
        }
        /// <summary>
        /// @brief Returns the radius of the sprite
        /// This method is used to return the radius of the current sprite.
        /// </summary>
        /// <returns></returns>
        public float getEnemyRadius()
        {
            return Radius;
        }
        #endregion
        #endregion
    }

}
