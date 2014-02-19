using Brogue.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class Level : IRenderable
    {
        Tile[,] tiles;
        List<IEnvironmentObject> environment;
        //List<Item> droppedItems;
        List<GameCharacter> characterEntities;
        bool[,] cachedSolid;
        bool needToCache;

        static Random statRand = new Random();

        public Level(Tile[,] tiles, List<IEnvironmentObject> environment, List<GameCharacter> characterEntities)
        {
            this.tiles = tiles;
            this.environment = environment;
            this.characterEntities = characterEntities;
            needToCache = true;
            cachedSolid = new bool[tiles.GetLength(0), tiles.GetLength(1)];
            characterEntities = new List<GameCharacter>();

            a = findRandomOpenPosition();
            b = findRandomOpenPosition();
            path = AStar.getPathBetween(this, a, b);
            moveset = AStar.getPossiblePositionsFrom(this, a, 15);
        }

        public GameCharacter getCharacterAtPosition(IntVec position)
        {
            GameCharacter result = null;

            foreach (GameCharacter character in characterEntities)
            {
                if (character.position.Equals(position))
                    result = character;
            }

            return result;
        }

        //public Iinteractable getInteractableAtPosition(IntVec position)
        //{
        //    GameCharacter result = null;

        //    foreach (GameCharacter character in characterEntities)
        //    {
        //        if (character.position.Equals(position))
        //            result = character;
        //    }

        //    return result;
        //}

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

                //foreach (GameCharacter character in characterEntities)
                {
                    //cachedSolid[character.x, character.y] = true;
                }

                //foreach (EnvironmentObject enviro in environment)
                //{
                //    cachedSolid[enviro.x, enviro.y] = enviro.isSolid;
                //}
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

        IntVec a;
        IntVec b;
        Direction[] path;
        IntVec[] moveset;

        public void render(SpriteBatch sb)
        {
            //sb.Draw(Tile.tileset, new Rectangle(0, 0, 48, 48), new Rectangle(0, 0, 48, 48), Color.White);
            float tileWidth = 640.0f / 50;// Math.Max(tiles.GetLength(0), tiles.GetLength(1));

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if( !tiles[x,y].isSolid )
                        sb.Draw(Tile.tileset, new Rectangle((int)(x * tileWidth), (int)(y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.White);
                }
            }

            foreach (IEnvironmentObject env in environment)
            {
                env.render(sb);
            }


            //foreach (IntVec vec in moveset)
            //{
            //    sb.Draw(Tile.tileset, new Rectangle((int)(vec.X * tileWidth), (int)(vec.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Blue);
            //}

            IntVec current = new IntVec(a.X, a.Y);
            foreach (Direction dir in path)
            {
                current += dir;
                sb.Draw(Tile.tileset, new Rectangle((int)(current.X * tileWidth), (int)(current.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Orange);
            }

            sb.Draw(Tile.tileset, new Rectangle((int)(a.X * tileWidth), (int)(a.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Green);
            sb.Draw(Tile.tileset, new Rectangle((int)(b.X * tileWidth), (int)(b.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Red);

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

        internal void testUpdate()
        {
            IntVec aMove = new IntVec((KeyboardController.IsPressed('D') ? 1 : 0) - (KeyboardController.IsPressed('A') ? 1 : 0), (KeyboardController.IsPressed('S') ? 1 : 0) - (KeyboardController.IsPressed('W') ? 1 : 0));
            IntVec bMove = new IntVec((KeyboardController.IsPressed('L') ? 1 : 0) - (KeyboardController.IsPressed('J') ? 1 : 0), (KeyboardController.IsPressed('K') ? 1 : 0) - (KeyboardController.IsPressed('I') ? 1 : 0));

            if (aMove.X != 0 || aMove.Y != 0 || bMove.X != 0 || bMove.Y != 0)
            {
                if (!isSolid(a + aMove))
                    a += aMove;

                if (!isSolid(b + bMove))
                    b += bMove;

                path = AStar.getPathBetween(this, a, b);
                moveset = AStar.getPossiblePositionsFrom(this, a, 15);
            }
            
        }
    }
}