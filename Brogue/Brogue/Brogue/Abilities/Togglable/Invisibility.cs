using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable]
    public class Invisibility : ToggleAbility
    {
        const int baseInvis = 10;

        public Invisibility()
        {
            name = "Invisibility";
            description = "The rogue shrouds himself to become \ninvisible for a certain number of \nturns. (Being invisble means that \nyour next basic attack will deal \n1.5*damage.)";
            abilityIndex = 21;
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) {}

        public override void toggledAttackEffects(HeroClasses.Hero hero) { }



        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            Audio.playSound("Smoke", 1.0f);
            hero.setInvisibility(10 + heroLevel / 5);
            cooldown = 20 + heroLevel/5;
        }
    }
}
