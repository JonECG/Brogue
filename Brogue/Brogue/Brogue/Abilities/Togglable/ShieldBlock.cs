using Brogue.Abilities.Damaging;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class ShieldBlock : ToggleAbility
    {

        public ShieldBlock()
        {
            name = "Shield Block";
            description = "The sentinel braces himself for the \nnext attack, gaining armor for a \nsingle hit.";
            abilityIndex = 7;
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) {}

        public override void toggledAttackEffects(HeroClasses.Hero hero) { }

        protected void heroEffect(HeroClasses.Hero hero)
        {
            int boost = (HeroClasses.Hero.level / 3) * (Math.Abs(hero.GetArmorRating()-hero.getArmorBoost())+1);
            hero.ApplyArmorBoost(boost,1);
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            heroEffect(hero);
            cooldown = 6;
        }
    }
}
