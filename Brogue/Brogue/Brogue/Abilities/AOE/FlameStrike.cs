using System;
using System.Collections.Generic;
using Brogue.Mapping;
using System.Linq;
using System.Text;
using Brogue.Engine;

namespace Brogue.Abilities.AOE
{
    class FlameStrike : AreaOfEffect
    {
        public FlameStrike()
        {
            name = "Flame Strike";
            description = "The sorcerer burns all enemies within \na four line radius.";
            radius = 7;
            isActuallyFilled = false;
            castSquares = new IntVec[28];
            baseDamage = 15;
            abilityCooldown = 10;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            abilitySprite = new Sprite(abilityLine, new IntVec(15, 0));
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return ((baseDamage * baseDamage) + heroLevel) * heroDamage;
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            Engine.Engine.AddVisualAttack(hero, enemy, "Hero/FireballSpell", .5f, 1.0f, .03f);
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
