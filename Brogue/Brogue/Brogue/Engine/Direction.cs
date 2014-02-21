using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    public class Direction
    {
        public static readonly Direction UP = new Direction(0, -1);
        public static readonly Direction DOWN = new Direction(0, 1);
        public static readonly Direction LEFT = new Direction(-1, 0);
        public static readonly Direction RIGHT = new Direction(1, 0);

        public static IEnumerable<Direction> Values
        {
            get
            {
                yield return UP;
                yield return DOWN;
                yield return LEFT;
                yield return RIGHT;
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        private Direction(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator float(Direction dir)
        {
            return (float) Math.Atan2(dir.Y, dir.X);
        }
    }
}
