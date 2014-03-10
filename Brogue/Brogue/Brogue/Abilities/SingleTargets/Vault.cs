using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable]
    public class Vault : SingleTarget
    {
        public Vault()
        {
            description = "The marksman leaps to the selected position.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 0;
            radius = 4;
            abilityCooldown = 5;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return 0;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return abilityCooldown;
        }

        protected override void heroEffect(HeroClasses.Hero hero)
        {
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {

            for (int i = 0; i < castSquares.Length; i++)
            {
                if (mapLevel.Move(hero, castSquares[0], true))
                {
                    cooldown = abilityCooldown;
                    wasJustCast = true;
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
