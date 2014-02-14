﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door : Iinteractable
    {
       static Texture2D sprite;
       bool isSolid { get; set; }
       bool isOpen { get; set; }

       public Door() 
       {
           isSolid = false;
           isOpen = false;
           //sprite = new Texture2D(
            
       }

       protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //sprite = Content.Load<Texture2D>("levelTileset");
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
    }

    class secretDoor : Iinteractable
    {
        static Texture2D sprite;
        bool isSolid { get; set; }
        bool isOpen { get; set; }

        public secretDoor()
        {
            isSolid = false;
            isOpen = false;
        }

        public void changeSolid()
        {
            if (isSolid)
            {
                isSolid = false;
            }
            else if (!isSolid)
            {
                isSolid = true;
            }
        }
    }
}
