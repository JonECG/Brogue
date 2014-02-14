using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest : Iinteractable
    {
        public bool isSolid { get; set; }
        public bool isOpen { get; set; }
        //public 

        public Chest()
        {
            isSolid = false;
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
}
