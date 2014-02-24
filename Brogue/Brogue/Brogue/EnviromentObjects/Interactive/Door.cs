using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door : Iinteractable, IEnvironmentObject, IRenderable
    {
       static Texture2D sprite { get; set; }
       bool isSolid { get; set; }
       bool isOpen { get; set; }

       public Door() 
       {
           isSolid = true;
           isOpen = false;
       }

       public void LoadContent(ContentManager content)
       {
           sprite = content.Load<Texture2D>("Enviroment/Door");
       }

       public void changeSolid()
       {
           if (isSolid)
           {
               isSolid = false;
               isOpen = true;
           }
           else if (!isSolid)
           {
               isSolid = true;
               isOpen = false;
           }
       }

       public void actOn()
       {
           changeSolid();
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

    class secretDoor : Iinteractable,IEnvironmentObject
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
                isOpen = true;
            }
            else if (!isSolid)
            {
                isSolid = true;
                isOpen = false;
            }
        }

        public bool getsolidity()
        {
            return isSolid;
        }

        public void actOn()
        {
            changeSolid();
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enviroment/Door");
        }

    }
}
