using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.EnviromentObjects.Interactive
{
    class Wall
    {
        public bool isSolid;

        public Wall()
        {
            isSolid = false;
        }
    }

    class SecretWall : Iinteractable
    {
        public bool isSolid { get; set; }

        public SecretWall()
        {

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
