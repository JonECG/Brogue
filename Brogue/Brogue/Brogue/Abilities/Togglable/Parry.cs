using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable]
    public class Parry : ToggleAbility
    {
        private bool added = false;

        public Parry()
        {
            name = "Parry";
            description = "The duelist blocks the next set of attacks completely.";
            abilitySprite = new Sprite(abilityLine, new IntVec(22, 0));
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) {}

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            hero.setParryCount(1);
            cooldown = 8;
        }
    }
}
