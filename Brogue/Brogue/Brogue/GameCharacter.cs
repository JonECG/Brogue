using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    

    public abstract class GameCharacter
    {
		public IntVec position;
        abstract public void TakeTurn();
        public abstract void takeDamage(int damage);
    }
}
