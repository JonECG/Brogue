﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    class FlameStrike : AreaOfEffect
    {
        public FlameStrike()
        {
            radius = 15;
            isActuallyFilled = false;
            castSquares = new IntVec[1200];
            baseDamage = 15;
            abilityCooldown = 30;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return ((baseDamage * baseDamage) + heroLevel) * heroDamage;
        }
    }
}
