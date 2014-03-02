using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue.Items;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects
{
    [Serializable]
    abstract  class EnviromentObjects : IEnvironmentObject
    {
        public string Name { get; protected set; }
        public string Image { get; protected set; }
        //public Texture2D Texture { get; protected set; }

        public bool IsSolid()
        {
            throw new NotImplementedException();
        }

        public Sprite GetSprite()
        {
            throw new NotImplementedException();
        }
    }
}
