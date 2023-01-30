using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Galaga.Sprite;
using Galaga.Objects;
using SDL2;

namespace Galaga.Objects
{
    public class ItemRandomizer
    {
        #region initializing and constructor 
        Random random = new Random();
        public Items item;
        private Coordinaten coor;
        private IntPtr _randerer;
        public ItemRandomizer(IntPtr _randerer)
        {
            item = new Items();
            this._randerer = _randerer;
        }
        #endregion initializing and constructor 

        #region creating Items	
        public void CreateItem()
        {
            item.surface = SDL_image.IMG_Load("D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\heal.png");
            IntPtr holeTexture = SDL.SDL_CreateTextureFromSurface(_randerer, item.surface);
            Heal heal = new Heal(holeTexture, 100, 60, 50, 50);
            item.items.Add(heal);
        }

        public void Draw()
        {
            foreach (Items items in item.items)
                items.Draw(item.surface, _randerer);
        }
        public void LoadContent()
        {
            foreach (Items items in item.items)
                items.LoadContent();

        }
        #endregion creating Items
    }
}
