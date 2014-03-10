using Brogue.HeroClasses;
using Brogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brogue.Engine;
using Brogue.Mapping;

namespace Brogue.Abilities.Damaging
{
    [Serializable] public abstract class SingleTarget : Ability
    {
        protected int abilityCooldown, baseDamage;

        public SingleTarget()
        {
            type = Enums.AbilityTypes.Single;
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            IntVec[] test = AStar.getPossiblePositionsFrom(level, start, radius, AStar.CharacterTargeting.TARGET_FIRST, true);
            return test;
        }

        public override void addCastingSquares(IntVec cursorPosition)
        {
            IntVec mouse = new IntVec(cursorPosition.X, cursorPosition.Y);
            for (int i = 0; i < castSquares.Length; i++)
            {
                if ((castSquares[i].X == 0 && castSquares[i].Y == 0) && !castSquares.Contains(mouse))
                {
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

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            damage = calculateDamage(heroLevel, heroDamage);
            cooldown = abilityCooldown;
            wasJustCast = true;
            finishCast(damage, mapLevel, hero);
        }

        abstract protected void finishCast(int damage, Level mapLevel, HeroClasses.Hero hero);
    }
}
