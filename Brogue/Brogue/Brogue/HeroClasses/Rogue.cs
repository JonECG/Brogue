using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    class Rogue : Hero
    {
        public Rogue()
        {
            numAbilities = 2;
            texture = Engine.Engine.GetTexture("Hero/RogueSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Rogue;
        }
    }
}
