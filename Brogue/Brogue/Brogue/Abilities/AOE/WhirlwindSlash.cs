﻿using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class WhirlwindSlash : AreaOfEffect
    {

        public WhirlwindSlash()
        {
            radius = 2;
            isActuallyFilled = false;
            castSquares = new IntVec[12];
            baseDamage = 3;
            abilityCooldown = 7;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return baseDamage * (heroLevel + heroDamage / 2);
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            Engine.Engine.AddVisualAttack(hero, enemy, "Hero/Whirlwind", .5f, 1.0f, .03f);
        }
    }
}
