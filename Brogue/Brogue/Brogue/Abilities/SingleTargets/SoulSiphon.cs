using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class SoulSiphon : SingleTarget
    {
        public SoulSiphon()
        {
            name = "Soul Siphon";
            description = "The mage deals damage to the enemy \nand heals for 100% of the damage \ndealt.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 6;
            radius = 3;
            abilityCooldown = 10;
            abilitySprite = new Sprite(abilityLine, new IntVec(17, 0));
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * heroDamage/3) + heroLevel;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    test.TakeDamage(damage, hero);
                    hero.Heal(damage);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }
    }
}
