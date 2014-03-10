using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Brogue.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    [Serializable] public class Switch : IEnvironmentObject, IInteractable, IRenderable
    {

        static DynamicTexture texture1 = Engine.Engine.GetTexture("Enviroment/SwichUP");
        static DynamicTexture texture2 = Engine.Engine.GetTexture("Enviroment/SwichDown");

        Direction directionFacing { get; set; }
        
        public bool active { get; set; }
        public bool isSolid { get; set; }
        public bool isPassable { get; set; }
        public IInteractable target { get; set; }

        public Switch(Direction directionToFace) 
        {
            active = false;
            isSolid = false;
            isPassable = false;
            directionFacing = directionToFace;
        }

        public Switch(IInteractable actingOn, Direction directionToFace)
        {
            active = false;
            isSolid = false;
            isPassable = false;
            target = actingOn;
            directionFacing = directionToFace;
        }

        public void changeState()
        {
            if (active)
            {
                active = false;
            }
            else if (!active)
            {
                active = true;
            }
        }

        public void click() 
        {
            changeState();
           
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            Sprite currentImage = new Sprite();
            if (active) 
            {
                currentImage = new Sprite(texture1, directionFacing);
            }
            else if (!active)
            {
                currentImage = new Sprite(texture2, directionFacing);
            }
            return currentImage;
        }

        public void actOn(GameCharacter actingCharacter)
        {
            changeState();
            target.actOn(null);
            Audio.playSound("switch");
            //actingCharacter.position = new IntVec()
        }
    }

    [Serializable] public class presserPlate : IEnvironmentObject, IInteractable
    {
        public bool active { get; set; }
        public bool isPassable { get; set; }
        public bool isSolid { get; set; }

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Presser Plate");

       // Direction directionFacing { get; set; }

        public IInteractable target { get; set; }

        public presserPlate()
        {
            active = false;
            isSolid = true;
            isPassable = true;
        }

        public presserPlate(IInteractable actingOn)
        {
            active = false;
            isSolid = true;
            isPassable = true;
            target = actingOn;
        }

        public void link(IInteractable actingOn)
        {
            target = actingOn;
        }

        public void changeState()
        {
            if (active)
            {
                active = false;
            }
            else if (!active)
            {
                active = true;
            }
        }

        public void stepOn(GameCharacter actingCharacter) 
        {
            changeState();
            target.actOn(actingCharacter);
        }

        public void stepOff(GameCharacter actingCharacter)
        {
            changeState();
            target.actOn(actingCharacter);
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
            //return new Sprite(texture, directionFacing);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            if (!active)
            {
                stepOn(actingCharacter);
            }
            if (active)
            {
                stepOff(actingCharacter);
            }  
        }
    }

}
