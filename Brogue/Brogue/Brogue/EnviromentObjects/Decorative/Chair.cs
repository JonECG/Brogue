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
    class Chair : InonInteractable, IEnvironmentObject, IRenderable
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Chair");
        bool isSolid { get; set; }

        public Chair() 
       {
           isSolid = false;
       }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
        }


        //public bool getSolidity()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
