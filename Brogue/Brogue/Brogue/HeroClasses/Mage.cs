using Brogue.Abilities.AOE;
using Brogue.Abilities.SingleTargets;
using Brogue.Enums;
using Brogue.Items.Consumables;
using Brogue.Items.Equipment.Offhand;
using Brogue.Items.Equipment.Weapon.Ranged;
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
            texture = Engine.Engine.GetTexture("Hero/MageSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Mage;
            inventory.addItem(new Staff(1, 1));
            inventory.addItem(new Staff(1, 1));
            inventory.addItem(new SpellBook(1, 1));
            baseHealth = 200;
            healthPerLevel = 30;
            resetLevel();
            resetHealth();
            abilities[0] = new Fireball();
            abilities[1] = new Blink();
            Engine.Engine.Log(health.ToString());
        }
    }
}
