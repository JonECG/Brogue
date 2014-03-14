using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class EagleEye : ToggleAbility
    {
        private bool added = false;

        public EagleEye()
        {
            name = "Eagle Eye";
            description = "The marksman's accuracy \ngives each of his weapons \na +1 range bonus.";
            abilityIndex = 29;
        }

        public override void toggledAttackEffects(HeroClasses.Hero hero)
        {
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) {}

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {
                hero.setRangeBoost(1);
                isActive = true;
                wasJustCast = true;
            }
            else if (isActive)
            {
                hero.setRangeBoost(0);
                isActive = false;
                wasJustCast = true;
            }
        }
    }
}
