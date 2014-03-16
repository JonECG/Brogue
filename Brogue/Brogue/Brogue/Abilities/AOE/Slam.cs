using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class Slam : AreaOfEffect
    {
        public Slam()
        {
            name = "Slam";
            description = "The brawler slams the ground \nhitting enemies within a line.";
            radius = 4;
            isActuallyFilled = false;
            castSquares = new IntVec[16];
            baseDamage = 5;
            abilityCooldown = 10;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            abilityIndex = 2;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage + heroLevel/3) + heroDamage;
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            
            Engine.Engine.AddVisualAttack(enemy, "Hero/hammerSmash", .5f, 1.0f, .03f);
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            int damage = calculateDamage(heroLevel, heroDamage);
            cooldown = abilityCooldown;
            wasJustCast = true;
            for (int i = 0; i < castSquares.Length; i++)
            {
                Audio.playSound("HammerSmash");
                Engine.Engine.AddVisualAttack(castSquares[i], "Hero/hammerSmash", .5f, 1.0f, .05f);
                GameCharacter enemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            isActuallyFilled = false;
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            List<IntVec> lines = new List<IntVec>();
            lines.AddRange(AStar.getTargetLine(level, start, new IntVec(start.X-radius,start.Y), false));
            lines.AddRange(AStar.getTargetLine(level, start, new IntVec(start.X+radius,start.Y), false));
            lines.AddRange(AStar.getTargetLine(level, start, new IntVec(start.X,start.Y-radius), false));
            lines.AddRange(AStar.getTargetLine(level, start, new IntVec(start.X,start.Y+radius), false));
            radiusSquares = lines.ToArray();
            return radiusSquares;
        }
    }
}
