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
    [Serializable] public class Rogue : Hero
    {
        public Rogue()
        {
            heroRole = Classes.Rogue;
            inventory.addItem(new Dagger(1, 1));
            inventory.addItem(new Dagger(1, 1));
            baseHealth = 225;
            healthPerLevel = 40;
            resetLevel();
            resetHealth();
            abilities[0] = new Mug();
            abilities[1] = new Vault();
            Engine.Engine.Log(health.ToString());
        }
    }
}
