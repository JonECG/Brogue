using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using Brogue.Mapping;
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

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radius = 1;
            return AStar.getPossiblePositionsFrom(level, start, radius);
        }

        public override IntVec viewCastingSquares(Direction directionFacing)
        {

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
            return startingPoint;
        }

        public override int finishCastandDealDamage(int heroLevel, int heroDamage)
        {
            int baseSpellDamage = baseDamage * heroLevel;
            damage = baseSpellDamage + heroDamage;
            cooldown = 5;
            return damage;
        }
    }
}
