using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Brogue.Items.Consumables
{
    public class Potion : Consumable
    {
        override Texture2D Texture { get; protected set; }

        public Potion(int dLevel, int cLevel)
        {
            Name = "Potion";
            BaseAmount = 50;
            RestoreAmount = findRestoreAmount(dLevel, BaseAmount);
        }
    }
}
