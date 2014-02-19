using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    class HiddenPassage
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
    }
}
