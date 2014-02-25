using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door : IInteractable, IEnvironmentObject, IRenderable
    {
       static Texture2D sprite { get; set; }
       bool isSolid { get; set; }
       bool isOpen { get; set; }

       Direction directionFacing { get; set; }

       static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Door");

       public Door() 
       {
           isSolid = true;
           isOpen = false;
       }

       public Door(Direction setDirection)
       {
           isSolid = true;
           isOpen = false;
           directionFacing = setDirection;
       }

       internal static void LoadContent(ContentManager content)
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

       public bool IsSolid()
       {
           return isSolid;
       }

       public Sprite GetSprite()
       {
           return new Sprite(texture);
           //return new Sprite(sprite, directionFacing);
       }

       public void actOn(GameCharacter actingCharacter)
       {
           changeSolid();
       }
    }

    class secretDoor : IInteractable,IEnvironmentObject
    {

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Door");
        
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

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            changeSolid();
        }
    }
}
