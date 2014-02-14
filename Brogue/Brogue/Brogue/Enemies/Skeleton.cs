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

        public override void aggro()
        {
            
        }

        public override void buildEnemy(int i)
        {
            
        }

        public override void TakeTurn()
        {
            aggro();
        }
    }
}
