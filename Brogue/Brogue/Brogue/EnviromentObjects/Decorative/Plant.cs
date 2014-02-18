using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Decorative
{
    class Plant : InonInteractable
    {
        static Texture2D sprite;
        bool isSolid { get; set; }

        public Plant() 
       {
           isSolid = false;
           //sprite = new Texture2D
       }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("levelTileset");
        }


        public bool getSolidity()
        {
            return isSolid;
        }
    }
}
