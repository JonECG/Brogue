using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    public class Swiches : IEnvironmentObject, Iinteractable, IRenderable
    {
        static Texture2D spriteOne { get; set; }
        static Texture2D spriteTwo { get; set; }
        
        public bool active { get; set; }
        public bool isSolid { get; set; }
        public bool isPassable { get; set; }
        public Iinteractable target { get; set; }

        public Swiches() 
        {
            active = false;
            isSolid = true;
            isPassable = false;
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

        public void LoadContent(ContentManager content)
        {
            spriteOne = content.Load<Texture2D>("Enviroment/SwichUP");
            spriteTwo = content.Load<Texture2D>("Enviroment/SwichDown");
        }
        
        public void actOn()
        {
            throw new NotImplementedException();
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
                currentImage = new Sprite(spriteOne);
            }
            else if (!active)
            {
                currentImage = new Sprite(spriteTwo);
            }
            return currentImage;
        }
    }

    public class presserPlate : IEnvironmentObject, Iinteractable
    {
        public bool active { get; set; }
        public bool isPassable { get; set; }
        public bool isSolid { get; set; }

        static Texture2D sprite;

        public Iinteractable target { get; set; }

        public presserPlate()
        {
            active = false;
            isSolid = true;
            isPassable = true;
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

        public void stepOn() 
        {
            changeState();
            target.actOn();
        }

        public void stepOff()
        {
            changeState();
            target.actOn();
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enviroment/Presser Plate");
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }


        public void actOn()
        {
            throw new NotImplementedException();
        }
    }

}
