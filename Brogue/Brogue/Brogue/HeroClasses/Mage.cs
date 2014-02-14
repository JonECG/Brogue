﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{
    class Mage : Hero
    {
        public Mage()
        {
            numAbilities = 2;
            directionFacing = (float)(3 * Math.PI / 2);
            location = new GridLocation(0, 0);
            spacesPerTurn = 2;
        }
    }
}