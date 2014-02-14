using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    public class Swiches
    {
        public bool active { get; set; }
        public bool isSolid { get; set; }
        //private 
        public Iinteractable target { get; set; }

        public Swiches() 
        {
            active = false;
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

        public Iinteractable target { get; set; }

        public presserPlate()
        {
            active = false;
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
