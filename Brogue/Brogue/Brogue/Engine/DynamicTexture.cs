using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    public class DynamicTexture
    {
        public Texture2D texture;

        public DynamicTexture(Texture2D tex = null)
        {
            this.texture = tex;
        }
    }
}
