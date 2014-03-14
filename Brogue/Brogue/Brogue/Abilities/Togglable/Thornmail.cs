using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable]
    public class Thornmail : ToggleAbility
    {
        const int increase = 5;

        public Thornmail()
        {
            name = "Thornmail";
            description = "The juggernaut deals one fifth of \nhis armor as damage to enemies \nwhen he is struck.";
            abilityIndex = 8;
            isActive = true;
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero)
        {
            if (createdLevel < hero.GetArmorRating()/5)
            {
                hero.setReflectedDamage(hero.GetArmorRating() / 5);
                createdLevel = hero.GetArmorRating();
                Engine.Engine.Log("Armor to damage bonus: " + hero.GetArmorRating() / 5);
            }
        }

        public override void toggledAttackEffects(HeroClasses.Hero hero)
        {
            hero.health -= HeroClasses.Hero.level;
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            hero.setReflectedDamage(hero.GetArmorRating()/5);
            createdLevel = hero.GetArmorRating();
        }
    }
}
