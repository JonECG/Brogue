using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Interactive
{
    class HiddenPassage : IEnvironmentObject, IRenderable
    {
        static Texture2D sprite { get; set; }
        IntVec exit { get; set; }
        bool isSolid { get; set; }

        HiddenPassage(IntVec pointB)
        {
            isSolid = true;
            exit = pointB;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enviroment/Stairs");
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }

    }
}
