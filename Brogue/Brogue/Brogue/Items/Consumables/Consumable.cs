using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Consumables
{
    abstract class Consumable : Item
    {
        public int RestoreAmount { get; protected set; }
        public int BaseAmount { get; protected set; }

        public static int findRestoreAmount(int dLevel, int bAmt)
        {
            return bAmt * dLevel;
        }
    }
}
