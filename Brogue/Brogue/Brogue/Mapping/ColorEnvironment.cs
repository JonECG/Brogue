﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    class ColorEnvironment : IEnvironmentObject
    {
        Color color;
        public ColorEnvironment( Color color )
        {
            this.color = color;
        }

        public bool IsSolid()
        {
            return true;
        }

        public Sprite GetSprite()
        {
            return new Sprite(Engine.Engine.placeHolder, color);
        }

        //public void render(SpriteBatch sb)
        //{
        //    //sb.Draw(Tile.tileset, new Rectangle(0, 0, 48, 48), new Rectangle(0, 0, 48, 48), Color.White);
        //    float tileWidth = 640.0f / 50;// Math.Max(tiles.GetLength(0), tiles.GetLength(1));

        //    sb.Draw(Tile.tileset, new Rectangle((int)(position.X * tileWidth), (int)(position.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), color);
        //}
    }
}
