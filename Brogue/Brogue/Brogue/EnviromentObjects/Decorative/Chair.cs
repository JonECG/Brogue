using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Brogue.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Decorative
{
    [Serializable] class Chair : InonInteractable, IEnvironmentObject, IRenderable
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Chair");
        bool isSolid { get; set; }

        Direction directionFacing { get; set; }

        public Chair() 
       {
           isSolid = false;
       }

        public Chair(Direction whereToFace)
        {
            isSolid = false;
            directionFacing = whereToFace;
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
            //return new Sprite(texture, directionFacing);

        }
    }
}
