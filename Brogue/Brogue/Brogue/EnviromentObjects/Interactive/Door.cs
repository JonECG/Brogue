using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Door : Iinteractable
    {
       bool isSolid { get; set; }
       bool isOpen { get; set; }

       public Door() 
       {
           isSolid = false;
           isOpen = false;
       }

       public void changeSolid()
       {
           if(isSolid)
           {
               isSolid = false;
           }
           else if (!isSolid) 
           {
               isSolid =true;
           }
       }
    }

    class secretDoor : Iinteractable
    {
        bool isSolid { get; set; }
        bool isOpen { get; set; }

        public secretDoor()
        {
            isSolid = false;
            isOpen = false;
        }

        public void changeSolid()
        {
            if (isSolid)
            {
                isSolid = false;
            }
            else if (!isSolid)
            {
                isSolid = true;
            }
        }
    }
}
