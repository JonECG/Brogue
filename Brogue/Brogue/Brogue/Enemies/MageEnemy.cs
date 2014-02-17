using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class StillEnemy : Enemy
    {
        public override void TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Attack();
            }
            else
            {
                //unimplemented
            }
        }

        public override void buildEnemy(int i)
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
            aggroRange = 6 + i;
            defense = 2 + (2 * i);
            attack = 8 + (8 * i);
            health = 5 + (5 * i);
            moveSpeed = 0;
        }

        protected override void die()
        {
            //ItemDropper once created
        }
    }
}
