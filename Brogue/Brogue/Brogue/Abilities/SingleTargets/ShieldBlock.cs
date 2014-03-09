﻿using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class ShieldBlock : SingleTarget
    {
        public ShieldBlock()
        {
            description = "The sentinel braces himself for the next attack, gaining armor for a single hit.";
            castSquares = new IntVec[0];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 0;
            radius = 1;
            abilityCooldown = 5;
        }

        protected override int getCooldown(GameCharacter enemy)
        {
            return abilityCooldown;
        }

        protected override void heroEffect(HeroClasses.Hero hero)
        {
            int test = (hero.level / 3) * (hero.GetArmorRating() + 1);
            hero.ApplyArmorBoost((hero.level / 3) * (hero.GetArmorRating()+1), 1);
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return 0;
        }
    }
}
