using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable]
    public class DivineShield : ToggleAbility
    {
        private bool added = false;

        public DivineShield()
        {
            name = "Divine Shield";
            description = "The juggernaut ignores the \nnext few attacks completely.";
            abilityIndex = 9;
        }

        public override void toggledAttackEffects(HeroClasses.Hero hero)
        {
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) { }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            hero.setParryCount(heroLevel/5);
            cooldown = heroLevel/3;
        }
    }
}
