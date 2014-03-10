using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable]
    public class Invisibility : ToggleAbility
    {
        const int baseInvis = 10;
        private bool added = false;

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) {}

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            hero.setInvisibility(10 + heroLevel / 5);
            cooldown = 15 + heroLevel/5;
        }
    }
}
