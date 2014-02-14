using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    public class Swiches
    {
        static Texture2D sprite;
        
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
            target.changeSolid();
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

        ~presserPlate()
        {

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
            target.changeSolid();
        }

        public void stepOff()
        {
            changeState();
            target.changeSolid();
        }
        
    }

}
