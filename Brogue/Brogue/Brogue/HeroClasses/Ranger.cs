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
    public class Ranger : Hero
    {
        public Ranger()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Marksman");
            Hero.loadSprite();
            heroRole = Classes.Ranger;
            baseHealth = 255;
            healthPerLevel = 35;
            level = 10;
            requiredBranchLevel = 700;
            resetLevel();
            resetHealth();
            abilities[0] = new Mug();
            abilities[1] = new Invisibility();
            abilities[2] = new Volley();
            abilities[3] = new SteadyShot();
            Engine.Engine.Log(health.ToString());
        }
    }
}
