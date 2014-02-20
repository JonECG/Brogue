using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Consumables
{
    public class Potion : Consumable
    {
        public Potion(int dLevel, int cLevel)
        {
            Name = "Potion";
            BaseAmount = 50;
            RestoreAmount = findRestoreAmount(dLevel, BaseAmount);
        }
    }
}
