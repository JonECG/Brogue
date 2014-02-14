using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{

    public class Swiches
    {
        public bool active { get; set; }

        public Swiches() 
        {
            active = false;
        }

        public ~Swiches()
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
    }

    public class presserPlate
    {
        public bool active { get; set; }

        public presserPlate()
        {
            active = false;
        }

        public ~presserPlate()
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
    }

}
