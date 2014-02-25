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
        IntVec[] castSquares = new IntVec[3];

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            foreach (IntVec vec in castSquares)
                castSquares = null;
            radius = 2;
            IntVec[] test = AStar.getPossiblePositionsFrom(level, start, radius);
            return test;
        }

        //public override IntVec viewCastingSquares(Mouse cursorPosition)
        //{
        //    IntVec mouse = new IntVec(cursorPosition.X, cursorPosition.Y);
        //    for (int i = 0; i < castSquares.Length; i++)
        //    {
        //        if (castSquares[i] == null && !castSquares.Contains(mouse))
        //        {
        //            castSquares[i] = mouse;
        //        }
        //    }
        //    return startingPoint;
        //}

        public override int finishCastandDealDamage(int heroLevel, int heroDamage)
        {
            int baseSpellDamage = baseDamage * heroLevel;
            damage = baseSpellDamage + heroDamage;
            cooldown = 5;
            return damage;
        }
    }
}
