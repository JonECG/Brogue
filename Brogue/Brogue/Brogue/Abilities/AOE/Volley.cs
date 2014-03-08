using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class Volley : RangedAreaOfEffect
    {
        public Volley()
        {
            castSquares = new IntVec[5];
            damageRadius = 1;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 3;
            radius = 3;
            abilityCooldown = 4;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return baseDamage * heroDamage / 2;
        }
    }
}
