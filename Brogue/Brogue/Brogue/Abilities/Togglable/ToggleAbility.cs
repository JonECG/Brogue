using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public abstract class ToggleAbility : Ability
    {
        protected bool isActive;
        protected int createdLevel;
        public ToggleAbility()
        {
            type = Enums.AbilityTypes.Toggle;
        }

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

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            throw new NotImplementedException();
        }

        public abstract void updateToggle(int heroLevel, HeroClasses.Hero hero);
    }
}
