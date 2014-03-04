using Brogue.Enums;
using Brogue.Items.Equipment.Armor.Helm;
using Brogue.Items.Equipment.Armor.Legs;
using Brogue.Items.Equipment.Weapon.Melee;
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
            heroRole = Class.Warrior;
            inventory.addItem(new Sword(1, 1));
            equipWeapon(0, 0);
            pickupItem(new MailHelm(1, 1));
            pickupItem(new MailLegs(1, 1));
        }
    }
}
