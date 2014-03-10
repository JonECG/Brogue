using Brogue.Abilities.AOE;
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
            heroRole = Classes.Ranger;
            baseHealth = 255;
            healthPerLevel = 35;
            level = 10;
            resetLevel();
            resetHealth();
            abilities[0] = new Volley();
            Engine.Engine.Log(health.ToString());
        }
    }
}
