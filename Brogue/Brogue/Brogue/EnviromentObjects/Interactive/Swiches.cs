using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    public class Swiches
    {
        static Texture2D sprite { get; set; }
        
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
            sprite = content.Load<Texture2D>("levelTileset");
        }

    }

    public class presserPlate
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
            sprite = content.Load<Texture2D>("levelTileset");
        }
        
    }

}
