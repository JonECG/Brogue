using Brogue.Enums;
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
            numAbilities = 2;
            texture = Engine.Engine.GetTexture("Hero/MageSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Mage;
            inventory.addItem(new Staff(1, 1));
            equipWeapon(0, 0);
        }
    }
}
