using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;

namespace Brogue.Enemies
{
    class MeleeEnemy : Enemy
    {
        public override void TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Direction[] path = AStar.getPathBetween(level, this.position, target.position);
            }
        }

        public override void buildEnemy(int i)
        {
            range = 1;
            aggroRange = 7;
        }

        protected override void die()
        {

        }
    }
}
