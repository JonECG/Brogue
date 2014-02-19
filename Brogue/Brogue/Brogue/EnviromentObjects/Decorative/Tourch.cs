using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Decorative
{
    class Tourch : ILightsource
    {
        public static Texture2D sprite { get; set; }

        public float intensity { get; set; }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("levelTileset");
        }

        public float getIntensity()
        {
            return intensity;
        }
    }
}
