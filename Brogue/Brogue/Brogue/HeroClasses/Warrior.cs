using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{
    class Warrior : Hero
    {
        public Warrior()
        {
            numAbilities = 2;
            directionFacing = (float)(3*Math.PI/2);
            position = new IntVec(0, 0);
        }
    }
}
