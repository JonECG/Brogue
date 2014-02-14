using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public class IntVec
    {
        public int[] ints = new int[2];

        public int X
        {
            get
            {
                return ints[0];
            }
            set
            {
                ints[0] = value;
            }
        }
        public int Y
        {
            get
            {
                return ints[1];
            }
            set
            {
                ints[1] = value;
            }
        }

        public GridLocation(int x, int y)
        {
            ints[0] = x;
            ints[1] = y;
        }
    }
}
