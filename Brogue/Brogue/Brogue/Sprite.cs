using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public struct Sprite
    {
        public Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            private set
            {
                texture = value;
            }
        }

        public IntVec sourceTile;
        public IntVec SourceTile
        {
            get
            {
                return sourceTile;
            }
            private set
            {
                sourceTile = value;
            }
        }

        public Color blend;
        public Color Blend
        {
            get
            {
                return blend;
            }
            private set
            {
                blend = value;
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = Color.White;
        }

        public Sprite(Texture2D texture, Color color)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = color;
        }

        public Sprite(Texture2D texture, IntVec sourceTile )
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = Color.White;
        }

        public Sprite(Texture2D texture, IntVec sourceTile, Color color)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = color;
        }
    }
}
