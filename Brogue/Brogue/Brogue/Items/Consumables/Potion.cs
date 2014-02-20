using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Brogue.Items.Consumables
{
    public class Potion : Consumable
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
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
