using System;
using System.Collections.Generic;
using System.Linq;
using Brogue.Engine;
using System.Text;

namespace Brogue.Items.Consumables
{
    [Serializable] public class Potion : Consumable
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Potion(int dLevel, int cLevel)
        {
            Name = "Potion";
            BaseAmount = 50;
            RestoreAmount = findRestoreAmount(dLevel, BaseAmount);
        }
    }
}
