using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Decorative
{
    class Chair : InonInteractable, IEnvironmentObject
    {
        static Texture2D sprite { get; set; }
        bool isSolid { get; set; }

        public Chair() 
       {
           isSolid = false;
       }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Chair.png");
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }


        //public bool getSolidity()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
