using Brogue.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.InventorySystem
{
    public class ItemCount : IComparable<ItemCount>
    {
        public Item item;
        public int count;

        public ItemCount(Item item = null, int count = 0)
        {
            this.item = item;
            this.count = count;
        }

        public int CompareTo(ItemCount other)
        {
            return -(this.count - other.count);
        }
    }
}
