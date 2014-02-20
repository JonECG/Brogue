using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;


namespace Brogue.EnviromentObjects.Interactive
{
    class HiddenPassage : IEnvironmentObject
    {
        static Texture2D sprite { get; set; }
        IntVec exit { get; set; }
        bool solidity { get; set; }

        HiddenPassage(IntVec pointB)
        {
            solidity = true;
            exit = pointB;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("levelTileset");
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
