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

        public Level(Tile[,] tiles)
        {
            this.tiles = tiles;
            needToCache = true;
            cachedSolid = new bool[tiles.GetLength(0), tiles.GetLength(1)];
            characterEntities = new List<GameCharacter>();
        }

        private void cache()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    cachedSolid[x, y] = tiles[x, y].isSolid;
                }
            }

            foreach( GameCharacter character in characterEntities)
            {
                //cachedSolid[character.x, character.y] = true;
            }

            //foreach (EnvironmentObject enviro in environment)
            //{
            //    cachedSolid[enviro.x, enviro.y] = enviro.isSolid;
            //}
        }

        public bool isSolid(int x, int y)
        {
            cache();
            return cachedSolid[x, y];
        }

        public void render(SpriteBatch sb)
        {
            //sb.Draw(Tile.tileset, new Rectangle(0, 0, 48, 48), new Rectangle(0, 0, 48, 48), Color.White);
            float tileWidth = 640.0f / Math.Max( tiles.GetLength(0), tiles.GetLength(1) );

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if( tiles[x,y].isSolid )
                        sb.Draw(Tile.tileset, new Rectangle((int)(x * tileWidth), (int)(y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.White);
                }
            }
        }
    }
}
