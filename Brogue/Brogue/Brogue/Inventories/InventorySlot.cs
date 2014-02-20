using Brogue.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Inventories
{
    public class InventorySlot
    {
        public Item item;
        public int count;
        public bool stackable;
        public bool isFilled;

        public InventorySlot()
        {
            isFilled = false;
        }
    }
}
