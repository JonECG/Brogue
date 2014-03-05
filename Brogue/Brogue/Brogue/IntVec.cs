using Brogue.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    [Serializable] public class IntVec
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

        public IntVec(IntVec other)
        {
            ints[0] = other.X;
            ints[1] = other.Y;
        }

        public override int GetHashCode()
        {
            return new { X, Y }.GetHashCode();
        }
        
        public override bool Equals(object other)
        {
            // If parameter is null return false.
            if (other == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            IntVec p = other as IntVec;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (X == p.X) && (Y == p.Y);
        }

        public static implicit operator IntVec(Direction dir)
        {
            return new IntVec(dir.X, dir.Y);
        }

        public static IntVec operator +(IntVec vec1, IntVec vec2)
        {
            return new IntVec( vec1.X + vec2.X, vec1.Y + vec2.Y );
        }

        public static IntVec operator *(IntVec vec1, IntVec vec2)
        {
            return new IntVec(vec1.X * vec2.X, vec1.Y * vec2.Y);
        }

        public static IntVec operator *(IntVec vec, int num)
        {
            return new IntVec(vec.X * num, vec.Y * num);
        }

        public static IntVec operator /(IntVec vec, int num)
        {
            return new IntVec(vec.X / num, vec.Y / num);
        }

        public static IntVec operator -(IntVec vec1, IntVec vec2)
        {
            return new IntVec(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public override string ToString()
        {
            return ("" + X + ", " + Y);
        }
    }
}
