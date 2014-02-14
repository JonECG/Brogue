using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    

    abstract class GameCharacter
    {
        //public GridLocation position;
        abstract public void TakeTurn();
        public abstract void takeDamage(int damage);
    }
}
