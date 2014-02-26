﻿using Brogue;
using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities
{
    public abstract class Ability
    {
        public int damage {get; protected set;}
        public int cooldown {get; set;}
        public int radius {get; protected set;}
        public bool isCasting { get; protected set; }
        public bool wasJustCast { get; set; }

        abstract public void addCastingSquares(IntVec cursorPosition);
        abstract public void removeCastingSquares(IntVec cursorPosition);
        abstract public IntVec[] getCastingSquares();
        abstract public IntVec[] viewCastRange(Level level, IntVec start);
        abstract public int finishCastandDealDamage(int heroLevel, int heroDamage);
    }
}
