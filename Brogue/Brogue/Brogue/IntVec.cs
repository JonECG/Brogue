using Brogue.Engine;
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

        public double angle()
        {
            return Math.Atan2(Y, X);
        }

        public double dangle()
        {
            return 180 * angle() / Math.PI;
        }

        public IntVec(int x, int y)
        {
            ints[0] = x;
            ints[1] = y;
        }

        public static implicit operator IntVec(direction dir)
        {
            //This function was broken. direction is an enum, but the code was trying access members of it (X and Y). I made it work as intended. --Drew Hurdle
            IntVec result = new IntVec(0, 0);
            switch (dir)
            {
                case direction.DOWN:
                    result.Y = 1;
                    break;
                case direction.UP:
                    result.Y = -1;
                    break;
                case direction.LEFT:
                    result.X = -1;
                    break;
                case direction.RIGHT:
                    result.X = 1;
                    break;
            }
            return result;
        }

        public static IntVec operator +(IntVec vec1, IntVec vec2)
        {
            return new IntVec( vec1.X + vec2.X, vec1.Y + vec2.Y );
        }

        public static IntVec operator -(IntVec vec1, IntVec vec2)
        {
            return new IntVec(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }
    }
}
