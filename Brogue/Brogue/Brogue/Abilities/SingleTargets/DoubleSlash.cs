using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    class DoubleSlash : SingleTarget
    {

        public DoubleSlash()
        {
            description = "The warrior strikes a single target to deal double damage.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 0;
            radius = 1;
            abilityCooldown = 6;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return abilityCooldown;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return heroDamage * 2;
        }
    }
}
