using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class Skeleton : Enemy
    {
        public Skeleton(int i)
        {
            buildEnemy(i);
        }

        public override bool aggro()
        {
            bool targetFound = false;



            return targetFound;
        }

        public override void buildEnemy(int i)
        {
            
        }

        public override void TakeTurn()
        {
            if (aggro())
            {

            }
            else
            {

            }
        }

    }
}
