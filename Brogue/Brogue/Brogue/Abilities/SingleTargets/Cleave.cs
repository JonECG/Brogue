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
        private IntVec[] castSquares = new IntVec[3];

        public Cleave()
        {
            for(int i = 0; i < castSquares.Length;i++)
            {
                castSquares[i] = new IntVec(0,0);
            }
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radius = 2;
            IntVec[] test = AStar.getPossiblePositionsFrom(level, start, radius);
            return test;
        }

        public override void addCastingSquares(IntVec cursorPosition)
        {
            IntVec mouse = new IntVec(cursorPosition.X, cursorPosition.Y);
            for (int i = 0; i < castSquares.Length; i++)
            {
                if ((castSquares[i].X == 0 && castSquares[i].Y == 0) && !castSquares.Contains(mouse))
                {
                    Engine.Engine.Log("Adding grid square");
                    castSquares[i] = mouse;
                }
            }
        }

        public override IntVec[] getCastingSquares()
        {
            return castSquares;
        }

        public override int finishCastandDealDamage(int heroLevel, int heroDamage)
        {
            int baseSpellDamage = baseDamage * heroLevel;
            damage = baseSpellDamage + heroDamage;
            cooldown = 5;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = null;
            }
            return damage;
        }
    }
}
