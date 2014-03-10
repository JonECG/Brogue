using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Offhand.Legendary
{
    [Serializable]
    public abstract class LegendaryOffhand : Offhand
    {
        public string FlavorText { get; protected set; }

         public LegendaryOffhand()
        {
            IsLegendary = true;
            ItemType = ITypes.Offhand;
        }
    }
}
