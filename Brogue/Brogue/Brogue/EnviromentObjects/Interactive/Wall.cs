using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    class Wall
    {
        static Texture2D sprite;

        public bool isSolid;
        public bool isPassable { get; set; }

        public Wall()
        {
            isSolid = true;
            isPassable = false;
        }
    }

    class SecretWall : Iinteractable
    {
        static Texture2D sprite;
        public bool isSolid { get; set; }
        public bool isPassable { get; set; }

        public SecretWall()
        {
            isSolid = true;
            isPassable = false;
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
