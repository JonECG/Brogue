using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities.Damaging.SingleTargets
{
    class Cleave : SingleTarget
    {
        private int baseDamage = 5;

        public override void viewCastRange()
        {
            radius = 1;
        }

        public override IntVec cast(int heroDamage, int heroLevel, Direction directionFacing)
        {
            int baseSpellDamage = baseDamage * heroLevel;
            damage = baseSpellDamage + heroDamage;

            if (directionFacing == Direction.RIGHT)
            {
                width = 1;
                height = 3;
                startingPoint = new IntVec(1,0);
            }
            if (directionFacing == Direction.DOWN)
            {
                width = 3;
                height = 1;
                startingPoint = new IntVec(0, -1);
            }
            if (directionFacing == Direction.LEFT)
            {
                width = 1;
                height = 3;
                startingPoint = new IntVec(-1, 0);
            }
            if (directionFacing == Direction.UP)
            {
                width = 3;
                height = 1;
                startingPoint = new IntVec(0, 1);
            }
            cooldown = 5;
            return startingPoint;
        } 
    }
}
