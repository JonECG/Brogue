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
            bool itemAdded = false;
            for (int i = 0; i < MAX_ITEM_COUNT && !itemAdded; i++)
            {
                if (!stored[i].isFilled)
                {
                    itemAdded = true;
                    stored[i].item = (!stored[i].isFilled) ? item : null;
                    stored[i].isFilled = (stored[i].item != null);
                }
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
