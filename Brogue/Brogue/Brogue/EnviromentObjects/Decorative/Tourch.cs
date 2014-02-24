using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Decorative
{
    class Tourch : ILightsource, IEnvironmentObject, IRenderable
    {
        public static Texture2D sprite { get; set; }

        bool isSolid = false;

        public float intensity { get; set; }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enviroment/Tourch");
        }

        public float getIntensity()
        {
            return intensity;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }


        public bool IsSolid()
        {
            return isSolid;
        }
    }
}
