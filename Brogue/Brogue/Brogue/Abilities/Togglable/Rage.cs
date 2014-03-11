using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class Rage : ToggleAbility
    {
        const int increase = 5;

        public Rage()
        {
            name = "Rage";
            description = "The berserker becomes enraged. Dealing \nincreased damage while losing health \nwith every attack.";
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero)
        {
            if (createdLevel < heroLevel && isActive)
            {
                hero.damageBoost -= increase + createdLevel;
                hero.damageBoost += increase + heroLevel;
                createdLevel = heroLevel;
            }
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {

                Engine.Engine.Log(hero.damageBoost.ToString());
                hero.damageBoost += increase + heroLevel;
                Engine.Engine.Log(hero.damageBoost.ToString());
                createdLevel = heroLevel;
                isActive = true;
            }
            else if (isActive)
            {
                hero.damageBoost -= increase + heroLevel;
                isActive = false;
            }
            cooldown = 0;
        }
    }
}
