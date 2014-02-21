using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities.AOE
{
    public abstract class AreaOfEffect : Ability
    {
        public int width, height;

        public abstract override IntVec viewCastingSquares(Direction directionFacing);
    }
}
