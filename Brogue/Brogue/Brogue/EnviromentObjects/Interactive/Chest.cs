using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest
    {
        bool isPassable;

        public Chest() 
        {
            isPassable = false;
        }

        public ~Chest()
        {
            
        }

        bool getPassable()
        {
            return isPassable;
        }
    }
}
