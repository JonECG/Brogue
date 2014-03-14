using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class ArcaneWeapon : ToggleAbility
    {
        private bool added = false;

        public ArcaneWeapon()
        {
            name = "Arcane Weapon";
            description = "The mage conjurs arcane energy to \namplify his weapon damage.";
            abilityIndex = 16;
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero){}

        public override void toggledAttackEffects(HeroClasses.Hero hero) { }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {
                if (HeroClasses.Hero.Element != null && !HeroClasses.Hero.Element.Contains(Enums.ElementAttributes.Fire))
                {
                    HeroClasses.Hero.Element.Add(Enums.ElementAttributes.Arcane);
                    added = true;
                }
                isActive = true;
                wasJustCast = true;
            }
            else if (isActive)
            {
                if (added)
                {
                    HeroClasses.Hero.Element.Remove(Enums.ElementAttributes.Arcane);
                    added = false;
                }
                wasJustCast = true;
                isActive = false;
            }
            cooldown = 0;
        }
    }
}
