using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest
    {
        public bool isPassable { get; set; }
        public bool isOpen { get; set; }
        //public 

        public Chest() 
        {
            isPassable = false;
        }

        public ~Chest()
        {
            
        }
    }
}
