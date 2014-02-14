using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door
    {
       bool isPassable { get; set; }
       bool isOpen { get; set; }

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
        bool isPassable { get; set; }
        bool isOpen { get; set; }

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
