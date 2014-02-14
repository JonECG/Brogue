using Brogue.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    /// <summary>
    /// Static class for A* pathfinding functionality
    /// </summary>
    static class AStar
    {
        /// <summary>
        /// Returns an array of positions that are able to be reached from a certain position given a move budget
        /// </summary>
        /// <param name="level">Level layout to query</param>
        /// <param name="start">Starting position</param>
        /// <param name="budget">How many movements it can make (-1 is infinite)</param>
        /// <param name="straight">Whether the positions should be in direct view</param>
        /// <returns>An unordered array of positions</returns>
        public static IntVec[] getPossiblePositionsFrom( Level level, IntVec start, int budget = -1, bool straight = false )
        {
            return null;
        }

        /// <summary>
        /// Returns an ordered array of directions needed for moves between two positions
        /// </summary>
        /// <param name="level">Level layout to query</param>
        /// <param name="from">Starting position</param>
        /// <param name="to">Final position</param>
        /// <returns>The moves needed to make the journey, null if not possible</returns>
        public static Direction[] getPathBetween( Level level, IntVec from, IntVec to)
        {
            return null;
        }

        /// <summary>
        /// Gets the cost of the moveset
        /// </summary>
        /// <param name="moves">Moveset</param>
        /// <returns>The cost to make all of the moves</returns>
        public static int getCost(Direction[] moves)
        {
            return moves.Length;
        }
    }
}
