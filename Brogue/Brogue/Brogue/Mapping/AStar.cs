using Brogue.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        public static IntVec[] getPossiblePositionsFrom( Level level, IntVec start, int budget = -1, bool targetCharacters = false, bool straight = false )
        {
            List<IntVec> positions = new List<IntVec>();

            int[,] used = level.getIntSolid();
            used[start.X, start.Y] = int.MaxValue - 1;

            possiblePositionsFromStep(level, positions, used, start, start, budget, targetCharacters, straight, true);

            return positions.ToArray<IntVec>();
        }

        private static void possiblePositionsFromStep(Level level, List<IntVec> positions, int[,] used, IntVec start, IntVec position, int budget, bool targetCharacters, bool straight, bool expand)
        {
            if (budget != 0 && (!straight || lineIsFree(used, start, position))) 
            {
                used[position.X, position.Y] = budget;

                positions.Add(position);

                if (expand)
                foreach (Direction dir in Direction.Values)
                {
                    IntVec target = position + dir;
                    if (used[target.X, target.Y] < budget - 1 || (used[target.X, target.Y] == int.MaxValue && targetCharacters && level.CharacterEntities.FindEntity(target) != null))
                    {
                        possiblePositionsFromStep(level, positions, used, start, target, budget - 1, targetCharacters, straight, !(used[target.X, target.Y] == int.MaxValue) );
                    }
                }
            }
        }

        private static bool lineIsFree(int[,] used, IntVec start, IntVec end)
        {
            return true;
        }


        [Serializable] public class __AStarNode : IComparable<__AStarNode>
        {
            public IntVec position;
            public int actualCost;
            public int heuristic;
            public int pseudoMoveCost;
            public __AStarNode reachedFrom;
            public Direction directionTaken;
            public bool expanded;

            public __AStarNode(IntVec position, int actualCost = 0, int heuristic = 0, int pseudoMoveCost = 0, __AStarNode reachedFrom = null, Direction directionTaken = null)
            {
                this.position = position;
                this.actualCost = actualCost;
                this.heuristic = heuristic;
                this.pseudoMoveCost = pseudoMoveCost;
                this.reachedFrom = reachedFrom;
                this.directionTaken = directionTaken;
                expanded = false;
            }

            [Serializable] public class __AStarNodeComparer : IComparer<__AStarNode>
            {
                public int Compare(__AStarNode a, __AStarNode b)
                {
                    return a.CompareTo(b);
                }
            }

            public int CompareTo(__AStarNode other)
            {
                int difference = (actualCost + heuristic) - (other.actualCost + other.heuristic);
                if (difference == 0)
                    difference = heuristic.CompareTo(other.heuristic);
                if (difference == 0)
                    difference = pseudoMoveCost.CompareTo(other.pseudoMoveCost);
                if (difference == 0)
                    difference = position.X.CompareTo(other.position.X);
                if (difference == 0)
                    difference = position.Y.CompareTo(other.position.Y);
                return difference;
            }
            
        }

        public static Direction[] getPathBetween(Level level, IntVec from, IntVec to)
        {
            int[,] solid = level.getIntSolid();
            SortedSet<__AStarNode> nodes = new SortedSet<__AStarNode>( new __AStarNode.__AStarNodeComparer() );

            __AStarNode recentNode = new __AStarNode(from,0,10*calculateHeuristic(from,to));
            __AStarNode bestNode = recentNode;

            nodes.Add(recentNode);

            while ( recentNode != null && !recentNode.position.Equals( to ) && !recentNode.expanded )
            {
                recentNode = nodes.Min;

                if (recentNode != null)
                {
                    //recentNode.expanded = true;
                    Direction[] closest = closestDirections(to - recentNode.position);

                    if (recentNode.heuristic.CompareTo(bestNode.heuristic) <= 0)
                    {
                        //Console.Write("test");
                        bestNode = recentNode;
                    }

                    for( int i = 0; i < closest.Length; i++ )//foreach (Direction dir in Direction.Values)
                    {
                        Direction dir = closest[i];

                        IntVec newLocation = recentNode.position + dir;

                        if (solid[newLocation.X, newLocation.Y] < -(recentNode.actualCost + 10))
                        {
                            solid[newLocation.X, newLocation.Y] = -(recentNode.actualCost + 10);
                            nodes.Add(new __AStarNode(newLocation, recentNode.actualCost + 10, 10 * calculateHeuristic(newLocation, to), i, recentNode, dir));
                        }
                    }

                    

                    if (!nodes.Remove(recentNode))
                        throw new NotImplementedException();
                }
                
            }

            List<Direction> path = new List<Direction>();

            recentNode = recentNode ?? bestNode;

            while (recentNode != null && !recentNode.position.Equals(from))
            {
                path.Add(recentNode.directionTaken);
                recentNode = recentNode.reachedFrom;
            }

            path.Reverse();

            return path.ToArray();
        }

        public static SortedSet<__AStarNode> getPathDrawnBetween(Level level, IntVec from, IntVec to, int count)
        {
            int actions = 0;


            int[,] solid = level.getIntSolid();
            SortedSet<__AStarNode> nodes = new SortedSet<__AStarNode>(new __AStarNode.__AStarNodeComparer());

            __AStarNode recentNode = new __AStarNode(from, 0, 10 * calculateHeuristic(from, to));
            __AStarNode bestNode = recentNode;

            nodes.Add(recentNode);

            while (recentNode != null && !recentNode.position.Equals(to) && !recentNode.expanded && (actions < count))
            {
                recentNode = nodes.Min;

                if (recentNode != null)
                {
                    //recentNode.expanded = true;
                    Direction[] closest = closestDirections(to - recentNode.position);

                    if (recentNode.heuristic.CompareTo(bestNode.heuristic) <= 0)
                    {
                        //Console.Write("test");
                        bestNode = recentNode;
                    }

                    for (int i = 0; i < closest.Length; i++)//foreach (Direction dir in Direction.Values)
                    {
                        Direction dir = closest[i];

                        IntVec newLocation = recentNode.position + dir;

                        if (solid[newLocation.X, newLocation.Y] < -(recentNode.actualCost + 10) && (actions++ < count))
                        {
                            solid[newLocation.X, newLocation.Y] = -(recentNode.actualCost + 10);
                            nodes.Add(new __AStarNode(newLocation, recentNode.actualCost + 10, 10 * calculateHeuristic(newLocation, to), i, recentNode, dir));
                        }
                    }



                    if ((actions++ < count) && !nodes.Remove(recentNode))
                        throw new NotImplementedException();
                }

            }

            return nodes;
        }

        public static int calculateHeuristic( IntVec from, IntVec to )
        {
            //return (int) Math.Round( Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2) ));
            return Math.Abs(to.X - from.X) + Math.Abs(to.Y - from.Y);
        }

        /// <summary>
        /// Returns an ordered array of directions needed for moves between two positions
        /// </summary>
        /// <param name="level">Level layout to query</param>
        /// <param name="from">Starting position</param>
        /// <param name="to">Final position</param>
        /// <returns>The moves needed to make the journey, null if not possible</returns>
        public static Direction[] getPathBetweenOld( Level level, IntVec from, IntVec to)
        {
            bestPath = null;
            return pathBetweenStep(level.getIntSolid(), from, to, 1, new List<Direction>() );
        }

        private static Direction[] bestPath;

        private static Direction[] pathBetweenStep( int[,] used, IntVec position, IntVec to, int cost, List<Direction> currentMoves )
        {
            Direction[] currentPath = null;

            if (used[position.X, position.Y] < int.MaxValue - cost)
            {
                if (position.X == to.X && position.Y == to.Y)
                {
                    currentPath = currentMoves.ToArray<Direction>();
                    if (bestPath == null || bestPath.Length > currentPath.Length)
                        bestPath = currentPath;
                }
                else
                {
                    used[position.X, position.Y] = int.MaxValue - cost;

                    if (bestPath == null || bestPath.Length > cost)
                    {

                        Direction[] closest = closestDirections(to - position);
                        foreach (Direction dir in closest)
                        {
                            List<Direction> newPath = new List<Direction>(currentMoves);
                            newPath.Add(dir);

                            Direction[] path = pathBetweenStep(used, position + dir, to, cost + 1, newPath);

                            if (currentPath == null || (currentPath != null && path != null && path.Length < currentPath.Length))
                                currentPath = path;
                        }
                    }
                }
            }

            return currentPath;
        }

        private static int abs( int number )
        {
            return (number >= 0) ? number : -number;
        }

        public static Direction[] closestDirections(IntVec vec)
        {
            Direction[] result = new Direction[4];

            if( vec.Y >= 0 && vec.X >= 0 && abs(vec.X)>=abs(vec.Y) )
            {
                result = new Direction[] { Direction.RIGHT, Direction.DOWN, Direction.UP, Direction.LEFT };
            }

            if( vec.Y >= 0 && vec.X >= 0 && abs(vec.Y)>=abs(vec.X) )
            {
                result = new Direction[] { Direction.DOWN, Direction.RIGHT, Direction.LEFT, Direction.UP };
            }


            if( vec.Y >= 0 && vec.X <= 0 && abs(vec.X)>=abs(vec.Y) )
            {
                result = new Direction[] { Direction.LEFT, Direction.DOWN, Direction.UP, Direction.DOWN };
            }

            if( vec.Y >= 0 && vec.X <= 0 && abs(vec.Y)>=abs(vec.X) )
            {
                result = new Direction[] { Direction.DOWN, Direction.LEFT, Direction.RIGHT, Direction.UP };
            }



            if( vec.Y <= 0 && vec.X >= 0 && abs(vec.X)>=abs(vec.Y) )
            {
                result = new Direction[] { Direction.RIGHT, Direction.UP, Direction.DOWN, Direction.LEFT };
            }

            if( vec.Y <= 0 && vec.X >= 0 && abs(vec.Y)>=abs(vec.X) )
            {
                result = new Direction[] { Direction.UP, Direction.RIGHT, Direction.LEFT, Direction.DOWN };
            }


            if( vec.Y <= 0 && vec.X <= 0 && abs(vec.X)>=abs(vec.Y) )
            {
                result = new Direction[] { Direction.LEFT, Direction.UP, Direction.DOWN, Direction.RIGHT };
            }

            if( vec.Y <= 0 && vec.X <= 0 && abs(vec.Y)>=abs(vec.X) )
            {
                result = new Direction[] { Direction.UP, Direction.LEFT, Direction.RIGHT, Direction.DOWN };
            }

            return result;
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
