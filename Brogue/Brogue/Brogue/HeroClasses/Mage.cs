using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{
    [Serializable]
    class Mage : Hero
    {
        public Mage()
        {
            numAbilities = 2;
            texture = Engine.Engine.GetTexture("Hero/RogueSprite");
            Hero.sprite = new Sprite(texture);
        }
    }
}
