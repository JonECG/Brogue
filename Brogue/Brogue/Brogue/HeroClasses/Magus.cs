using Brogue.Abilities.AOE;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
using Brogue.Enums;
using Brogue.Items.Equipment.Weapon.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    [Serializable]
    public class Magus : Hero
    {
        public Magus()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Magus");
            heroRole = Classes.Magus;
            baseHealth = 250;
            healthPerLevel = 35;
            level = 10;
            requiredBranchLevel = 700;
            resetLevel();
            resetHealth();
            Hero.loadSprite();
            abilities[0] = new Fireball();
            abilities[1] = new Blink();
            abilities[2] = new ArcaneWeapon();
            abilities[3] = new SoulSiphon();
            Engine.Engine.Log(health.ToString());
        }
    }
}
