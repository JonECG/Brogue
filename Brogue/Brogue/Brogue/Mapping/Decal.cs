using Brogue.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    [Serializable]
    class Decoration : IEnvironmentObject
    {
        Sprite sprite;
        bool solid;

        public Decoration( Sprite sprite, bool solid = false)
        {
            this.sprite = sprite;
            this.solid = solid;
        }

        public bool IsSolid()
        {
            return solid;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }
    }
}
