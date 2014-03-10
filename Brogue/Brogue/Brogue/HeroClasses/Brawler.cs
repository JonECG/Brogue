using Brogue.Abilities.AOE;
using Brogue.Abilities.Damaging.SingleTargets;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
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
    class Brawler : Hero
    {
        public Brawler()
        {
            texture = Engine.Engine.GetTexture("Hero/WarriorSprite");
            Hero.sprite = new Sprite(texture);
            heroRole = Classes.Brawler;
            baseHealth = 300;
            healthPerLevel = 40;
            level = 10;
            requiredBranchLevel = 700;
            resetLevel();
            resetHealth();
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
            abilities[2] = new Rage();
            abilities[3] = new DoubleSlash();
            Engine.Engine.Log(health.ToString());
        }
    }
}
