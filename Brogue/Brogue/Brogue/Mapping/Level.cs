using Brogue.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class Level : IRenderable
    {
        Tile[,] tiles;
        //EnvironmentObject[] environment;
        //List<Item> droppedItems;
        List<GameCharacter> characterEntities;
        bool[,] cachedSolid;
        bool needToCache;

        static Random statRand = new Random();

        public Level(Tile[,] tiles)
        {
            this.tiles = tiles;
            needToCache = true;
            cachedSolid = new bool[tiles.GetLength(0), tiles.GetLength(1)];
            characterEntities = new List<GameCharacter>();

            a = findRandomOpenPosition();
            b = findRandomOpenPosition();
            path = AStar.getPathBetween(this, a, b);
            moveset = AStar.getPossiblePositionsFrom(this, a, 15);
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

                foreach (GameCharacter character in characterEntities)
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

        IntVec a;
        IntVec b;
        Direction[] path;
        IntVec[] moveset;

        public void render(SpriteBatch sb)
        {
            //sb.Draw(Tile.tileset, new Rectangle(0, 0, 48, 48), new Rectangle(0, 0, 48, 48), Color.White);
            float tileWidth = 640.0f / Math.Max( tiles.GetLength(0), tiles.GetLength(1) );

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if( !tiles[x,y].isSolid )
                        sb.Draw(Tile.tileset, new Rectangle((int)(x * tileWidth), (int)(y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.White);
                }
            }


            //IntVec current = new IntVec(a.X, a.Y);
            //foreach (Direction dir in path)
            //{
            //    current += dir;
            //    sb.Draw(Tile.tileset, new Rectangle((int)(current.X * tileWidth), (int)(current.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Orange);
            //}

            foreach (IntVec vec in moveset)
            {
                sb.Draw(Tile.tileset, new Rectangle((int)(vec.X * tileWidth), (int)(vec.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Blue);
            }

            sb.Draw(Tile.tileset, new Rectangle((int)(a.X * tileWidth), (int)(a.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Green);
            sb.Draw(Tile.tileset, new Rectangle((int)(b.X * tileWidth), (int)(b.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Red);
        }
    }
}
