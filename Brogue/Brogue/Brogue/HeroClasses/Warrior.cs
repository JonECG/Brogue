using Brogue.Abilities.AOE;
using Brogue.Abilities.Damaging.SingleTargets;
using Brogue.Enums;
using Brogue.Items.Equipment.Accessory;
using Brogue.Items.Equipment.Armor.Chest;
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
    [Serializable] class Warrior : Hero
    {
        public Warrior()
        {
            texture = Engine.Engine.GetTexture("Hero/WarriorSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Warrior;
            inventory.addItem(new Sword(1, 1));
            inventory.addItem(new Sword(1, 1));
            inventory.addItem(new GreatAxe(1, 1));
            inventory.addItem(new WarHammer(1, 1));
            baseHealth = 300;
            healthPerLevel = 50;
            resetLevel();
            resetHealth();
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
            Engine.Engine.Log(health.ToString());
        }
    }
}
