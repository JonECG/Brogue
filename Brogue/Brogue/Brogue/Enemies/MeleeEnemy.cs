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
            if (aggro())
            {
                Direction[] path = AStar.getPathBetween(level, this.position, target.position);
            }
        }

        public override bool aggro()
        {
            bool targetFound = false;



            return targetFound;
        }

        public override void buildEnemy(int i)
        {
            range = 1;
        }

        protected override void die()
        {

        }
    }
}
