﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    class Rogue : Hero
    {
        public Rogue()
        {
            numAbilities = 2;
            position = new IntVec(0, 0);
        }
    }
}
