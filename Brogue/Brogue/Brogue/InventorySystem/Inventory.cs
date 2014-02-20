using Brogue.InventorySystem;
using Brogue.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.InventorySystem
{
    public class Inventory
    {
        const int MAX_ITEM_COUNT = 16;
        public InventorySlot[] stored {get; protected set;}

        public Inventory()
        {
            stored = new InventorySlot[MAX_ITEM_COUNT];
        }

        public void addItem(Item item)
        {
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                stored[i].item = (!stored[i].isFilled) ? item : null;
            }
        }

        public void removeItem(int index)
        {
            stored[index].item = null;
        }

        public void swapItem(int indexOne, int indexTwo)
        {
            InventorySlot tempSlot = stored[indexOne];
            stored[indexOne] = stored[indexTwo];
            stored[indexTwo] = tempSlot;
        }
    }
}
