using Brogue.Abilities.AOE;
using Brogue.Enums;
using Brogue.Items.Equipment.Weapon.Melee;
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
            texture = Engine.Engine.GetTexture("Hero/RogueSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Rogue;
            inventory.addItem(new Dagger(1, 1));
            inventory.addItem(new Dagger(1, 1));
            baseHealth = 225;
            healthPerLevel = 40;
            resetLevel();
            resetHealth();
            abilities[0] = new Volley();
            Engine.Engine.Log(health.ToString());
        }
    }
}
