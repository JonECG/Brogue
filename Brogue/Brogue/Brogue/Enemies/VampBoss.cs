using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;

namespace Brogue.Enemies
{
    class VampBoss : BossEnemy
    {
        int turnCounter = 0;

        public override void TakeTurn(Level level)
        {
            turnCounter++;

            Aggro(level);

            if (turnCounter % 9 == 0)
            {
                foreach(GameCharacter g in targets)
                {
                    if (g.maxHealth / g.health > 4)
                    {
                        g.TakeDamage(9001, this);
                    }
                    else
                    {
                        g.Heal(g.maxHealth / 5);
                    }
                }
            }
            else if (turnCounter % 3 == 0)
            {
                foreach (GameCharacter g in targets)
                {
                    g.TakeDamage(attacks[1], this);
                }
            }
            else
            {
                targets[0].TakeDamage(attacks[0], this);
                Heal(attacks[0] / 10);
            }
        }

        public override void Aggro(Level level)
        {
            targets[0] = level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level));
            foreach (GameCharacter g in level.GetCharactersIsFriendly(true))
            {
                if (g.position != targets[0].position)
                {
                    targets.Add(g);
                }
            }
        }

        public override void BuildBoss(int i)
        {
            throw new NotImplementedException();
        }

        protected override void Die()
        {
            throw new NotImplementedException();
        }

        private IntVec FindNearestTarget(IEnumerable<GameCharacter> chars, Level level)
        {
            IntVec target = null;

            foreach (GameCharacter g in chars)
            {
                int gRange = AStar.getCost(AStar.getPathBetween(level, this.position, g.position));
                int targetRange = AStar.getCost(AStar.getPathBetween(level, this.position, target));

                if (target != null && gRange < targetRange)
                {
                    target = g.position;
                }
                else
                {
                    target = g.position;
                }
            }

            return target;
        }
    }
}
