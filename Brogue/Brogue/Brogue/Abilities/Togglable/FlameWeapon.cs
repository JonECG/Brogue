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
            abilitySprite = new Sprite(abilityLine, new IntVec(16, 0));
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero){}

        public override void toggledAttackEffects(HeroClasses.Hero hero) { }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {
                if (hero.Element != null && !hero.Element.Contains(Enums.ElementAttributes.Fire))
                {
                    hero.Element.Add(Enums.ElementAttributes.Arcane);
                    added = true;
                }
                isActive = true;
            }
            else if (isActive && added)
            {
                hero.Element.Remove(Enums.ElementAttributes.Arcane);
                added = false;
                isActive = false;
            }
            cooldown = 0;
        }
    }
}
