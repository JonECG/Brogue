using Brogue.Engine;
using Brogue.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class Level
    {
        Tile[,] tiles;
        public GridBoundList<IEnvironmentObject> Environment { get; private set; }
        //List<Tuple<IEnvironmentObject, IntVec>> environment;
        public GridBoundList<Item> DroppedItems { get; private set; }
        //List<Item> droppedItems;
        public GridBoundList<GameCharacter> CharacterEntities { get; private set; }
        //List<Tuple<GameCharacter, IntVec>> characterEntities;
        bool[,] cachedSolid;
        bool needToCache;

        static Random statRand = new Random();

        public Level(Tile[,] tiles, GridBoundList<IEnvironmentObject> environment, GridBoundList<GameCharacter> characterEntities)
        {
            this.tiles = tiles;
            this.Environment = environment;
            this.CharacterEntities = characterEntities;
            this.DroppedItems = new GridBoundList<Item>();
            needToCache = true;
            cachedSolid = new bool[tiles.GetLength(0), tiles.GetLength(1)];

            a = findRandomOpenPosition();
            b = findRandomOpenPosition();
            path = AStar.getPathBetween(this, a, b);
            moveset = AStar.getPossiblePositionsFrom(this, a, 15);

            for (int i = 0; i < 28; i++)
            {
                Item item = Item.randomItem(10, 10);

                Engine.Engine.Log(string.Format("Item Generated: {0}", item.Name));

                DroppedItems.Add(item, findRandomOpenPosition());
            }
        }

        public IEnumerable<GameCharacter> GetCharactersIsFriendly(bool isFriendly)
        {
            foreach (GameCharacter character in CharacterEntities.Entities())
            {
                if (character.isFriendly == isFriendly)
                    yield return character;
            }
        }

        public IntVec findRandomOpenPosition()
        {
            cache();

            IntVec result = null;
            do
            {
                result = new IntVec(statRand.Next(cachedSolid.GetLength(0)), statRand.Next(cachedSolid.GetLength(1)));
            }
            while(cachedSolid[result.X,result.Y]);

            return result;
        }

        public bool[,] getSolid()
        {
            cache();
            bool[,] result = new bool[cachedSolid.GetLength(0), cachedSolid.GetLength(1)];

            for (int x = 0; x < cachedSolid.GetLength(0); x++)
            {
                for (int y = 0; y < cachedSolid.GetLength(1); y++)
                {
                    result[x, y] = cachedSolid[x, y];
                }
            }

            return result;
        }

        public int[,] getIntSolid()
        {
            cache();

            int[,] result = new int[cachedSolid.GetLength(0), cachedSolid.GetLength(1)];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    result[x, y] = (cachedSolid[x, y]) ? int.MaxValue : int.MinValue;
                }
            }

            return result;
        }

        private void cache()
        {
            if (needToCache)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < tiles.GetLength(1); y++)
                    {
                        cachedSolid[x, y] = tiles[x, y].isSolid;
                    }
                }

                foreach (IntVec position in CharacterEntities.Positions() )
                {
                    cachedSolid[position.X, position.Y] = true;
                }

                foreach ( Tuple<IEnvironmentObject,IntVec> enviro in Environment.Tuples() )
                {
                    cachedSolid[enviro.Item2.X, enviro.Item2.Y] = enviro.Item1.IsSolid();
                }
            }
        }

        public bool isSolid(int x, int y)
        {
            cache();
            return cachedSolid[x, y];
        }

        public bool isSolid(IntVec position)
        {
            return isSolid(position.X, position.Y);
        }

        //public bool Move(GameCharacter character, IntVec position, bool absolute = false)
        //{
        //    bool moveWasMade = false;
        //    if( absolute ) 
        //        moveWasMade = CharacterEntities.SetPosition(character, position);
        //    else
        //        moveWasMade = CharacterEntities.AddPosition(character, position);
        //    return moveWasMade;
        //}


        IntVec a;
        IntVec b;
        Direction[] path;
        IntVec[] moveset;

        private bool checkTileSolid(int x, int y)
        {
            bool result = !(x >= 0 && x < tiles.GetLength(0) && y >= 0 && y < tiles.GetLength(1));

            if (!result)
                result = tiles[x, y].isSolid;

            return result;
        }

        public void render()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y].isSolid)
                    {
                        int right = checkTileSolid(x + 1, y) ? 1 : 0;
                        int left = checkTileSolid(x - 1, y) ? 1 : 0;
                        int up = checkTileSolid(x, y - 1) ? 1 : 0;
                        int down = checkTileSolid(x, y + 1) ? 1 : 0;

                        int index = right | (up << 1) | (left << 2) | (down << 3);

                        index = (index % 8) + ((index >= 8) ? 9 : 0);

                        Engine.Engine.Draw(Tile.tileset, new IntVec(x, y), new IntVec(index, 0));
                    }
                    else
                    {
                        Engine.Engine.Draw(Tile.tileset, new IntVec(x, y), new IntVec(8, 0), Color.Gray);
                    }

                }
            }

            //Environment.Draw();
            DroppedItems.Draw();
            CharacterEntities.Draw();

            //foreach (IntVec vec in moveset)
            //{
            //    sb.Draw(Tile.tileset, new Rectangle((int)(vec.X * tileWidth), (int)(vec.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Blue);
            //}

            IntVec current = new IntVec(a.X, a.Y);
            foreach (Direction dir in path)
            {
                current += dir;
                Engine.Engine.Draw(Engine.Engine.placeHolder, new IntVec(current.X, current.Y), Color.Orange); //Orange
            }

            Engine.Engine.Draw(Engine.Engine.placeHolder, new IntVec(a.X, a.Y), Color.Green); //Green
            Engine.Engine.Draw(Engine.Engine.placeHolder, new IntVec(b.X, b.Y), Color.Blue); //Red


            var nodes = AStar.getPathDrawnBetween(this, a, b, actionsToTake);
            foreach (var node in nodes)
            {
                Engine.Engine.Draw(Engine.Engine.placeHolder, new IntVec(node.position.X, node.position.Y), Color.BlanchedAlmond);
            }

            Engine.Engine.Draw(Engine.Engine.placeHolder, new IntVec(nodes.Min.position.X, nodes.Min.position.Y), Color.Yellow); 

        }

        public bool isComplete()
        {
            IntVec pos = findRandomOpenPosition();

            bool[,] solidCopy = getSolid();

            IntVec[] moveset = AStar.getPossiblePositionsFrom(this, pos, -1);

            foreach( IntVec move in moveset )
            {
                solidCopy[move.X, move.Y] = true;
            }

            bool result = true;

            for (int i = 0; i < solidCopy.GetLength(0); i++)
            {
                for (int j = 0; j < solidCopy.GetLength(1); j++)
                {
                    result = result && solidCopy[i, j];
                }
            }

            return result;
        }

        int actionsToTake = 10;
        int previousPathDistance = -1;

        internal void testUpdate()
        {
            IntVec aMove = new IntVec((KeyboardController.IsPressed('D') ? 1 : 0) - (KeyboardController.IsPressed('A') ? 1 : 0), (KeyboardController.IsPressed('S') ? 1 : 0) - (KeyboardController.IsPressed('W') ? 1 : 0));
            IntVec bMove = new IntVec((KeyboardController.IsPressed('L') ? 1 : 0) - (KeyboardController.IsPressed('J') ? 1 : 0), (KeyboardController.IsPressed('K') ? 1 : 0) - (KeyboardController.IsPressed('I') ? 1 : 0));
            actionsToTake += (KeyboardController.IsDown('2') ? 1 : 0) - (KeyboardController.IsDown('1') ? 1 : 0);

            if (aMove.X != 0 || aMove.Y != 0 || bMove.X != 0 || bMove.Y != 0)
            {
                if (!isSolid(a + aMove))
                    a += aMove;

                if (!isSolid(b + bMove))
                    b += bMove;

                path = AStar.getPathBetween(this, a, b);

                actionsToTake = 1;

                int movement = Math.Abs(aMove.X) + Math.Abs(aMove.Y) + Math.Abs(bMove.X) + Math.Abs(bMove.Y);

                Engine.Engine.Log(string.Format( "New A* path length: {0}", path.Length.ToString()));
                if (previousPathDistance != -1 && Math.Abs(previousPathDistance - path.Length) > movement)
                    Engine.Engine.Log(string.Format("<INCONSISTENT PATHFIND; MOVEMENTDELTA={0},PATHDELTA={1}>", movement, Math.Abs(previousPathDistance - path.Length)));

                moveset = AStar.getPossiblePositionsFrom(this, a, 15);

                previousPathDistance = path.Length;
            }
            
        }
    }
}