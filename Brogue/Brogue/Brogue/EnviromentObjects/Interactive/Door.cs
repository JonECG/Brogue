using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door
    {
       bool isPassable;
       bool isOpen;

       public Door() 
       {
           isPassable = false;
           isOpen = false;
       }

       public ~Door() 
       {
       
       }
    }

    class secretDoor
    {
        bool isPassable;
        bool isOpen;

        public secretDoor()
        {
            isPassable = false;
            isOpen = false;
        }

        public ~secretDoor()
        {

        }
    }
}
