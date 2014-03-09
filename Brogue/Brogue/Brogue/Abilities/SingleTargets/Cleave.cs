﻿using Brogue;
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

        public Cleave()
        {
            description = "The warrior strikes two immideately adjacent squares.";
            castSquares = new IntVec[2];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 5;
            radius = 1;
            abilityCooldown = 5;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return abilityCooldown;
        }

        protected override void heroEffect(HeroClasses.Hero hero)
        {
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * heroLevel) + heroDamage;
        }
    }
}
