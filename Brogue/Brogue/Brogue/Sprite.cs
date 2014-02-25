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
        private static DynamicTexture nullTexture = Engine.Engine.GetTexture( "null" );

        private DynamicTexture texture;
        public DynamicTexture Texture
        {
            get
            {
                return texture ?? nullTexture;
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

        private bool isVisible;
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        public Sprite(DynamicTexture texture, bool isVisible = true )
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = Color.White;
            this.direction = Direction.RIGHT;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, Color color, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = color;
            this.direction = Direction.RIGHT;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, IntVec sourceTile, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = Color.White;
            this.direction = Direction.RIGHT;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, IntVec sourceTile, Color color, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = color;
            this.direction = Direction.RIGHT;
            this.isVisible = isVisible;
        }


        public Sprite(DynamicTexture texture, Direction direction, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = Color.White;
            this.direction = direction;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, Color color, Direction direction, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = new IntVec(0, 0);
            this.blend = color;
            this.direction = direction;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, IntVec sourceTile, Direction direction, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = Color.White;
            this.direction = direction;
            this.isVisible = isVisible;
        }

        public Sprite(DynamicTexture texture, IntVec sourceTile, Color color, Direction direction, bool isVisible = true)
        {
            this.texture = texture;
            this.sourceTile = sourceTile;
            this.blend = color;
            this.direction = direction;
            this.isVisible = isVisible;
        }
    }
}
