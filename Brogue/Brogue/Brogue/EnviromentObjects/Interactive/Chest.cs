using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest : Iinteractable, IRenderable
    {
        static Texture2D sprite;

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }
        //public 

        public Chest()
        {
            isSolid = false;
        }

        public void changeSolid()
        {
           if(isSolid)
           {
               isSolid = false;
           }
           else if (!isSolid) 
           {
               isSolid =true;
           }
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("levelTileset");
        }
    }
}
