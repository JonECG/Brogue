using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Consumables
{
    abstract class Consumable
    {
        public string Name { get; set; }
        public int RestoreAmount { get; set; }
        public int BaseAmount { get; set; }

        public static int findRestoreAmount(int dLevel, int bAmt)
        {
            return bAmt * dLevel;
        }
    }
}
