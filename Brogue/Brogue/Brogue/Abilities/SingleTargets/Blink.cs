﻿using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class Blink : SingleTarget
    {
        public Blink()
        {
            description = "The mage teleports to the selected position.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 0;
            radius = 2;
            abilityCooldown = 2;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return 0;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
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

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            
            for (int i = 0; i < castSquares.Length; i++)
            {
                if (mapLevel.Move(hero, castSquares[0], true))
                {
                    Audio.playSound("whoosh", 1.0f);
                    cooldown = abilityCooldown;
                    wasJustCast = true;
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
