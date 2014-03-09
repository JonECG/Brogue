using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class Rage : ToggleAbility
    {
        const int increase = 5;

        public override void addCastingSquares(IntVec cursorPosition)
        {
            throw new NotImplementedException();
        }

        public override void removeCastingSquares(IntVec cursorPosition)
        {
            throw new NotImplementedException();
        }

        public override IntVec[] getCastingSquares()
        {
            throw new NotImplementedException();
        }

        public override IntVec[] viewCastRange(Mapping.Level level, IntVec start)
        {
            throw new NotImplementedException();
        }

        public override bool filledSquares()
        {
            throw new NotImplementedException();
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
                hero.damageBoost += increase + heroLevel;
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

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            throw new NotImplementedException();
        }
    }
}
