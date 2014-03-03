using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{
    class Warrior : Hero
    {
        public Warrior()
        {
            numAbilities = 2;
            texture = Engine.Engine.GetTexture("Hero/WarriorSprite");
            Hero.sprite = new Sprite(texture);
        }
    }
}
