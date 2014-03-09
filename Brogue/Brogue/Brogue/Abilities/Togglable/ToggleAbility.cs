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

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            throw new NotImplementedException();
        }

        public abstract void updateToggle(int heroLevel, HeroClasses.Hero hero);
    }
}
