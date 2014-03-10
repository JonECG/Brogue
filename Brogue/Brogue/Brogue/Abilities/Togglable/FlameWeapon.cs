using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class FlameWeapon : ToggleAbility
    {
        private bool added = false;

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero){}

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {
                if (hero.Element != null && !hero.Element.Contains(Enums.ElementAttributes.Fire))
                {
                    hero.Element.Add(Enums.ElementAttributes.Fire);
                    added = true;
                }
            }
            else if (isActive && added)
            {
                hero.Element.Remove(Enums.ElementAttributes.Fire);
                added = false;
            }
            cooldown = 0;
        }
    }
}
