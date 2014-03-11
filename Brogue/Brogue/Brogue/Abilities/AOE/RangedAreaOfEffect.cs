using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities.AOE
{
    [Serializable]
    public abstract class RangedAreaOfEffect : Ability
    {
        protected IntVec[] radiusSquares;
        protected bool isActuallyFilled;
        protected int baseDamage, abilityCooldown, damageRadius;
        protected Level mapLevel;

        public RangedAreaOfEffect()
        {
            type = Enums.AbilityTypes.AOE;
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radiusSquares =  AStar.getPossiblePositionsFrom(level, start, radius, AStar.CharacterTargeting.PASS_THROUGH, true);
            mapLevel = level;
            return radiusSquares;
        }

        public override void addCastingSquares(IntVec cursorPosition)
        {
            IntVec mouse = new IntVec(cursorPosition.X, cursorPosition.Y);
            for (int i = 0; i < castSquares.Length && !isActuallyFilled; i++)
            {
                isActuallyFilled = !(castSquares[i].Equals(new IntVec(0,0)));
            }
            if (!isActuallyFilled)
            {
                for (int i = 0; i < radiusSquares.Length; i++)
                {
                    if (radiusSquares[i].Equals(mouse))
                    {
                        IntVec[] additions = AStar.getPossiblePositionsFrom(mapLevel, mouse, damageRadius, true);
                        castSquares[0] = mouse;
                        for (int j = 0; j < additions.Length; j++)
                        {

                            //drawVisualEffect();
                            castSquares[j + 1] = additions[j];
                        }
                    }
                }
            }
        }

        //public abstract void drawVisualEffect();

        public override bool filledSquares()
        {
            return isActuallyFilled;
        }

        public override void removeCastingSquares(IntVec cursorPosition)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                if (castSquares[i].Equals(cursorPosition))
                {
                    Engine.Engine.Log("Removing grid square");
                    for (int j = 0; j < castSquares.Length; j++)
                    {
                        castSquares[j] = new IntVec(0, 0);
                    }
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
            isActuallyFilled = false;
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
