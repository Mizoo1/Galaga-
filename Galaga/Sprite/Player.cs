using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using SDL2;

namespace Galaga.Sprite
{
    public class Player : Sprite
    {
        public static bool running = true;
        public SDL.SDL_Rect smallSprite;
        public Input input;
        public Player(IntPtr _texture, Input input) : base(_texture, 300, 420, 40, 40)
        {
            speed = 10;
            Radius = 35;
            this.input = input;
        }
        public Player(IntPtr _texture, Input input, int x, int y) : base(_texture, x, y, 40, 40)
        {
            speed = 10;
            Radius = 35;
            this.input = input;
        }
        public void loadContent()
        {
            LoadContent();

            // Definieren Sie die Position und Größe der beiden Rechteckobjekte
            smallSprite = new SDL.SDL_Rect()
            {
                x = 10,
                y = 450,
                w = 20,
                h = 20
            };
        }
        public void draw(IntPtr surface, IntPtr renderer)
        {
            // Erstellen Sie eine Texture aus der Surface
            Draw(surface, renderer);
            SDL.SDL_RenderCopy(renderer, _texture, IntPtr.Zero, ref smallSprite);
        }
        public bool getRunning() { return running; }
        public SDL.SDL_Rect getSmallSprite() { return smallSprite; }
        

    }
}
