﻿using Brogue.HeroClasses;
using Brogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brogue.Engine;

namespace Brogue.Abilities.Damaging
{
    public abstract class SingleTarget : Ability
    {
        protected int width, height;
        protected IntVec startingPoint;

        public abstract override IntVec cast(int heroDamage, int heroLevel, Direction directionFacing);
    }
}