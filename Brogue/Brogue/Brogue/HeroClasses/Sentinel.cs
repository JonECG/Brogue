using Brogue.Abilities.AOE;
using Brogue.Abilities.Damaging.SingleTargets;
using Brogue.Abilities.SingleTargets;
using Brogue.Enums;
using Brogue.Items.Equipment.Accessory;
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
    [Serializable]
    class Sentinel : Hero
    {
        public Sentinel()
        {
            warriorSprite = Engine.Engine.GetTexture("Hero/MageSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Sentinel;
            baseHealth = 400;
            healthPerLevel = 65;
            level = 10;
            resetLevel();
            resetHealth();
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
            abilities[2] = new ShieldBlock();
            abilities[3] = new ShieldBash();
            Engine.Engine.Log(health.ToString());
        }
    }
}
