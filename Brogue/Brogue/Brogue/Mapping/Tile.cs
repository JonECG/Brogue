﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Mapping
{
    public class Tile
    {
        public bool isSolid;
        public int solidNeighbors;
        public static Texture2D tileset;

        public Tile(bool isSolid = false, int solidNeighbors = 0)
        {
            this.isSolid = isSolid;
            this.solidNeighbors = solidNeighbors;
        }
    }
}
