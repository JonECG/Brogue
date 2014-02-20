﻿using Brogue;
using Brogue.Engine;
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
        public bool isOnCooldown { get; protected set; }
        public bool isCasting { get; protected set; }

        abstract public IntVec cast(int heroDamage, int heroLevel, Direction directionFacing);
        abstract public void viewCastRange();
    }
}