using Brogue.HeroClasses;
using Brogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brogue.Engine;

namespace Brogue.Abilities.Damaging
{
    [Serializable] public abstract class SingleTarget : Ability
    {
        protected int width, height;
        protected IntVec[] castSquares;
    }
}
