using Brogue;
using Brogue.Engine;
using Brogue.Enums;
using Brogue.HeroClasses;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities
{
    [Serializable] public abstract class Ability
    {
        public int damage {get; protected set;}
        public int cooldown {get; set;}
        public int radius {get; protected set;}
        public bool isCasting { get; protected set; }
        public bool wasJustCast { get; set; }
        protected IntVec[] castSquares;
        public string description { get; protected set; }
        public AbilityTypes type;

        abstract public void addCastingSquares(IntVec cursorPosition);
        abstract public void removeCastingSquares(IntVec cursorPosition);
        abstract public IntVec[] getCastingSquares();
        abstract public IntVec[] viewCastRange(Level level, IntVec start);
        abstract public bool filledSquares();
        abstract public void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, Hero hero);
        abstract public int calculateDamage(int heroLevel, int heroDamage);

        public void resetSquares()
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
