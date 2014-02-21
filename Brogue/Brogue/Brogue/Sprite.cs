using Brogue.Engine;
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
        private Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        private IntVec sourceTile;
        public IntVec SourceTile
        {
            get
            {
                return sourceTile;
            }
            set
            {
                sourceTile = value;
            }
        }

        private Color blend;
        public Color Blend
        {
            get
            {
                return blend;
            }
            set
            {
                blend = value;
            }
        }

        private Direction direction;
        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = Color.White;
            this.direction = Direction.RIGHT;
        }

        public Sprite(Texture2D texture, Color color)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = color;
            this.direction = Direction.RIGHT;
        }

        public Sprite(Texture2D texture, IntVec sourceTile )
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = Color.White;
            this.direction = Direction.RIGHT;
        }

        public Sprite(Texture2D texture, IntVec sourceTile, Color color)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = color;
            this.direction = Direction.RIGHT;
        }


        public Sprite(Texture2D texture, Direction direction)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = Color.White;
            this.direction = direction;
        }

        public Sprite(Texture2D texture, Color color, Direction direction)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = color;
            this.direction = direction;
        }

        public Sprite(Texture2D texture, IntVec sourceTile, Direction direction)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = Color.White;
            this.direction = direction;
        }

        public Sprite(Texture2D texture, IntVec sourceTile, Color color, Direction direction)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = color;
            this.direction = direction;
        }
    }
}
