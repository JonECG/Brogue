using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    [Serializable] public class DynamicTexture
    {
        private Texture2D _texture;
        public Texture2D texture {
            get 
            { 
                return _texture; 
            }
            set 
            {
                if (!concrete)
                {
                    _texture = value;
                }
                else
                {
                    Console.WriteLine( "You're a jerk" );
                }
            }
        }
        private bool concrete;

        public DynamicTexture(Texture2D tex = null, bool concrete = false)
        {
            this.texture = tex;
            this.concrete = concrete;
        }
    }
}
