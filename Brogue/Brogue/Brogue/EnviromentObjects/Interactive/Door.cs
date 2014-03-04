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
    [Serializable] class Door : IInteractable, IEnvironmentObject, IRenderable
    {
       bool isSolid { get; set; }
       bool isOpen { get; set; }
       bool rotated { get; set; }

       public Direction directionFacing { get; set; }

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

       public void changeSolid()
       {
           if (isSolid)
           {
               isSolid = false;
               isOpen = true;
               directionFacing = directionFacing.Rotate(true);
           }
           else if (!isSolid)
           {
               isSolid = true;
               isOpen = false;
               directionFacing = directionFacing.Rotate(false);
           }
       }

       public bool IsSolid()
       {
           return isSolid;
       }

       public Sprite GetSprite()
       {
           //return new Sprite(texture);
           return new Sprite(texture, directionFacing);
       }

       public void actOn(GameCharacter actingCharacter)
       {
           changeSolid();
           //directionFacing = directionFacing.Rotate(true);
           Audio.playSound("door");
       }
    }

    [Serializable] class secretDoor : IInteractable,IEnvironmentObject
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
