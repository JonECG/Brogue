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
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                stored[i] = new InventorySlot();
            }
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

        public Item GetItemAt(int index)
        {
            Item returnitem = null;
            if (index >= 0 && index < MAX_ITEM_COUNT)
            {
                if (stored[index].isFilled)
                {
                    returnitem = stored[index].item;
                }
            }
            return returnitem;
        }

        public bool inventoryMaxed()
        {
            int numItems = 0;
            for (int i = 0; i < MAX_ITEM_COUNT; i++)
            {
                numItems += (stored[i].isFilled) ? 1 : 0;
            }
            return numItems == MAX_ITEM_COUNT;
        }

        public Item removeItem(int index)
        {
            Item temp = null;
            if (stored[index].isFilled)
            {
                temp = stored[index].item;
                stored[index].item = null;
                stored[index].isFilled = false;
            }
            return temp;
        }

        public void swapItem(int indexOne, int indexTwo)
        {
            InventorySlot tempSlot = stored[indexOne];
            stored[indexOne] = stored[indexTwo];
            stored[indexTwo] = tempSlot;
        }
    }
}
