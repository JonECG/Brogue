using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    class LightningStorm : AreaOfEffect
    {
        public LightningStorm()
        {
            radius = 5;
            isActuallyFilled = false;
            castSquares = new IntVec[50];
            baseDamage = 10;
            abilityCooldown = 10;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0,0);
            }
        }
    }
}
