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
            List<IntVec> positions = new List<IntVec>();
            
            possiblePositionsFromStep( positions, level.getIntSolid(), start, start, budget, straight );

            return positions.ToArray<IntVec>();
        }

        private static void possiblePositionsFromStep(List<IntVec> positions, int[,] used, IntVec start, IntVec position, int budget, bool straight)
        {
            if (budget != 0 && used[position.X, position.Y] < budget && (!straight||lineIsFree(used,start,position))
            {
                used[position.X, position.Y] = budget;

                foreach (Direction dir in Direction.Values)
                {
                    possiblePositionsFromStep(positions, used, start, position + dir, budget - 1, straight);
                }
            }
        }

        private static bool lineIsFree(int[,] used, IntVec start, IntVec end)
        {
            return true;
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
            return pathBetweenStep( level.getIntSolid(), from, to, 0, new List<Direction>() );
        }

        private static Direction[] pathBetweenStep( int[,] used, IntVec position, IntVec to, int cost, List<Direction> currentMoves)
        {
            Direction[] currentPath = null;

            if (used[position.X, position.Y] < int.MaxValue - cost)
            {
                used[position.X, position.Y] = int.MaxValue - cost;

                if (position.X == to.X && position.Y == to.Y)
                {
                    currentPath = currentMoves.ToArray<Direction>();
                }
                else
                {
                    foreach (Direction dir in Direction.Values)
                    {
                        List<Direction> newPath = new List<Direction>(currentMoves);
                        newPath.Add(dir);

                        Direction[] path = pathBetweenStep(used, position + dir, to, cost + 1, newPath);

                        if (currentPath != null && path != null && path.Length < currentPath.Length)
                            currentPath = path;
                    }
                }
            }

            return currentPath;
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
