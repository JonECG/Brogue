using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class WhirlwindSlash : AreaOfEffect
    {

        public WhirlwindSlash()
        {
            radius = 2;
            isActuallyFilled = false;
            castSquares = new IntVec[12];
            baseDamage = 3;
            abilityCooldown = 7;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
