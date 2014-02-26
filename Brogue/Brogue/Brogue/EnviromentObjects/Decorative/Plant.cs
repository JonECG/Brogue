﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Decorative
{
    class Plant : InonInteractable, IEnvironmentObject, IRenderable
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Plant");
        static Texture2D sprite { get; set; }
        bool isSolid { get; set; }

        public Plant() 
        {
           isSolid = false;
        }

        public bool getSolidity()
        {
            return isSolid;
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
        }
    }
}
