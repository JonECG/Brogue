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
    public class Duelist : Hero
    {
        public Duelist()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Duelist");
            Hero.loadSprite();
            heroRole = Classes.Duelist;
            baseHealth = 275;
            healthPerLevel = 35;
            level = 10;
            requiredBranchLevel = 700;
            resetLevel();
            resetHealth();
            abilities[0] = new Mug();
            abilities[1] = new Invisibility();
            abilities[2] = new Parry();
            abilities[3] = new Eviscerate();
            Engine.Engine.Log(health.ToString());
        }
    }
}
