using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;

namespace Brogue.Enemies
{
    class MageEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Attack();
            }
            else
            {
                //unimplemented
            }
            return true;
        }

        public override bool Aggro(Level level)
        {
            bool targetFound = false;

            IEnumerable<GameCharacter> chars = level.GetCharactersIsFriendly(true);

            if (target != null && (AStar.getCost(AStar.getPathBetween(level, this.position, target.position)) <= range))
            {
                targetFound = true;
            }
            else
            {
                foreach (GameCharacter g in chars)
                {
                    int gRange = AStar.getCost(AStar.getPathBetween(level, this.position, g.position));

                    if (gRange <= range && gRange < AStar.getCost(AStar.getPathBetween(level, this.position, target.position)))
                    {
                        target = level.CharacterEntities.FindEntity(g.position);
                        targetFound = true;
                    }
                }
            }

            return targetFound;
        }

        public override void BuildEnemy(int i)
        {
            if (i > 10)
            {
                i = 10;
            }
            if (i < 0)
            {
                i = 0;
            }

            range = 6+i;
            aggroRange = 3 + i / 2;
            defense = 2 + (2 * i);
            attack = 8 + (8 * i);
            health = 5 + (5 * i);
            moveSpeed = 0;
            exp = 10 + 3 * i;
        }

        protected override void Die()
        {
            //ItemDropper once created
        }

        public override DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("Enemies/MageEnemy");
        }
    }
}
