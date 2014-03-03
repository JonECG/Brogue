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
    [Serializable]
    public class Cleave : SingleTarget
    {
        private int baseDamage = 5;

        public Cleave()
        {
            castSquares = new IntVec[2];
            for(int i = 0; i < castSquares.Length;i++)
            {
                castSquares[i] = new IntVec(0,0);
            }
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radius = 1;
            IntVec[] test = AStar.getPossiblePositionsFrom(level, start, radius, true);
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

        public override bool filledSquares()
        {
            bool filled = true;
            IntVec test = new IntVec(0, 0);
            for (int i = 0; i < castSquares.Length && filled; i++)
            {
                filled = !(castSquares[i].Equals(test));
            }
            return filled;
        }

        public override void removeCastingSquares(IntVec cursorPosition)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                if (castSquares[i].Equals(cursorPosition))
                {
                    Engine.Engine.Log("Removing grid square");
                    castSquares[i] = new IntVec(0, 0);
                }
            }
        }

        public override IntVec[] getCastingSquares()
        {
            return castSquares;
        }

        public override int finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, GameCharacter hero)
        {
            int baseSpellDamage = baseDamage * heroLevel;
            damage = baseSpellDamage + heroDamage;
            cooldown = 5;
            wasJustCast = true;
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0,0);
            }
            return damage;
        }
    }
}
