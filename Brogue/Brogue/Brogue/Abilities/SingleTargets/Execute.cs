using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class Execute : SingleTarget
    {
        public Execute()
        {
            description = "The warrior strikes the enemy chosen. If the enemy \nis killed, this ability's cooldown is reset.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 8;
            radius = 1;
            abilityCooldown = 8;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return (enemy.health<= 0)? 0: abilityCooldown;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * heroLevel) + heroDamage;
        }
    }
}
