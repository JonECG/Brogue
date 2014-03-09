using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class ShieldBash : SingleTarget
    {
        public ShieldBash()
        {
            description = "The sentinel strikes enemies with his shield.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 5;
            radius = 1;
            abilityCooldown = 3;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return abilityCooldown;
        }

        protected override void heroEffect(HeroClasses.Hero hero){}

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * (heroLevel/2)) + (heroDamage);
        }
    }
}
