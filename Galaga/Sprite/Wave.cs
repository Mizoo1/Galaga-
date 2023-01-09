using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;

namespace Galaga.Sprite
{
    public class Wave
    {
        private EnemSprite EnemSprite;
        private EnemyFighter EnemyFighter;
        private int counter;

        public Wave(EnemSprite EnemSprite, EnemyFighter EnemyFighter)
        {
            this.EnemSprite = EnemSprite;
            this.EnemyFighter = EnemyFighter;
            this.counter = 6;
        }
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
        public void Update(IntPtr surface, IntPtr renderer, GameTime gameTime, Player player, Player player2)
        {
            if (EnemSprite.Enemies.Count == 0 && EnemyFighter.Enemies.Count == 0)
                createWaveOne(surface, renderer);
            EnemSprite.update(surface, renderer);
            EnemyFighter.update(surface, renderer, gameTime, player, player2);
        }
        public void LoadContent()
        {
            EnemyFighter.loadContent();
            EnemSprite.loadContent();
        }
        public void Draw(IntPtr surface, IntPtr renderer)
        {
            EnemyFighter.draw(surface, renderer);
            EnemSprite.draw(surface, renderer);
        }
    }
}
