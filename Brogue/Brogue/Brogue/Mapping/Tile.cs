using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Brogue.Engine;

namespace Brogue.Mapping
{
    public class Tile : IRenderable
    {
        public bool isSolid;
        public int solidNeighbors;
        private const int BITMASK = 0xF;

        public static DynamicTexture tileset = Engine.Engine.GetTexture("levelTileset");

        public static DynamicTexture floorTileset = Engine.Engine.GetTexture("floorTileset");
        public static DynamicTexture wallTileset = Engine.Engine.GetTexture("wallTileset");

        public Tile(bool isSolid = false, int solidNeighbors = 0)
        {
            this.isSolid = isSolid;
            this.solidNeighbors = solidNeighbors;
        }

        public Sprite GetSprite()
        {
            int index = (isSolid) ? solidNeighbors : (~solidNeighbors)&BITMASK;
            index = (index % 8) + ((index >= 8) ? 9 : 0);

            Sprite result;
            if (isSolid)
            {
                result = new Sprite(wallTileset, new IntVec(index, 0), Color.White);
            }
            else
            {
                result = new Sprite(floorTileset, new IntVec(index, 0), Color.Gray);
            }
            return result;
        }
    }
}
