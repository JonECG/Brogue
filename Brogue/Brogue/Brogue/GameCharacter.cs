using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    struct GridLocation
    {
        public int[] ints = new int[2];
        public GridLocation(int x, int y)
        {
            ints[0] = x;
            ints[1] = y;
        }
    }

    abstract class GameCharacter
    {
        abstract public GridLocation position;
        abstract public void TakeTurn();

    }
}
