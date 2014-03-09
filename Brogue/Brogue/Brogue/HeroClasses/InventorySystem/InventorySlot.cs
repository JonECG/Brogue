using Brogue.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.InventorySystem
{
    [Serializable] public class InventorySlot
    {
        public Item item;
        public bool isFilled;

        public InventorySlot()
        {
            isFilled = false;
        }
    }
}
