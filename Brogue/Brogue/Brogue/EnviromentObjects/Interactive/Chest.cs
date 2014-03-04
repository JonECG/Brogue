using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue;
using Brogue.Items;
using Brogue.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Items.Consumables;

namespace Brogue.EnviromentObjects.Interactive
{
    [Serializable] class Chest : IInteractable, IEnvironmentObject, IRenderable
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Chest");

        bool isVisable { get; set; }
        bool empty { get; set; }

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }

        int chestSize = 10;

        List<Item> contents;

        public Chest(Item[] putIntoChest)
        {
            isSolid = true;
            isVisable = true;
            contents = new List<Item>();
            empty = false;

            foreach (Item i in putIntoChest)
            {
                if (contents.Count <= chestSize)
                {
                    contents.Add(i);
                }
            }

        }

        public bool putInChest(Item newIteam)
        {
            bool PutinChest = true;
            if (contents.Count < chestSize)
            {
                contents.Add(newIteam);
            }
            else
            {
                PutinChest = false;
            }
            return PutinChest;
        }

        private void setLocationsToCheck(IntVec position)
        {
            
        }

        [Serializable]
        private class PositionCheck
        {
            public IntVec position;
            public bool isChecked;
        };

        int size = 14;
        PositionCheck[] positonsToChecked;

        private PositionCheck CreatePosition(IntVec position)
        {
            PositionCheck temp = new PositionCheck();
            temp.position = position;
            temp.isChecked = false;
            return temp;
        }

        private PositionCheck CreatePosition(int X, int Y)
        {
            PositionCheck temp = new PositionCheck();
            temp.position = new IntVec(X,Y);
            temp.isChecked = false;
            return temp;
        }

        private void createLocations(IntVec position)
        {
            positonsToChecked = new PositionCheck[size];
            int layer = 1;
            positonsToChecked[0]  = CreatePosition(new IntVec(position.X - layer, position.Y - layer));
            positonsToChecked[1]  = CreatePosition(new IntVec(position.X, position.Y - layer));
            positonsToChecked[2]  = CreatePosition(new IntVec(position.X - layer, position.Y));
            positonsToChecked[3]  = CreatePosition(new IntVec(position.X, position.Y));
            positonsToChecked[4]  = CreatePosition(new IntVec(position.X + layer, position.Y + layer));
            positonsToChecked[5]  = CreatePosition(new IntVec(position.X + layer, position.Y));
            positonsToChecked[6]  = CreatePosition(new IntVec(position.X, position.Y + layer));
                                  
            layer = 2;            
            positonsToChecked[7]  = CreatePosition(new IntVec(position.X - layer, position.Y - layer));
            positonsToChecked[8]  = CreatePosition(new IntVec(position.X, position.Y - layer));
            positonsToChecked[9]  = CreatePosition(new IntVec(position.X - layer, position.Y));
            positonsToChecked[10] = CreatePosition(new IntVec(position.X, position.Y));
            positonsToChecked[11] = CreatePosition(new IntVec(position.X + layer, position.Y + layer));
            positonsToChecked[12] = CreatePosition(new IntVec(position.X + layer, position.Y));
            positonsToChecked[13] = CreatePosition(new IntVec(position.X, position.Y + layer));
        }


        private bool isPositionClear(IntVec position)
        {
            bool isClear = false;
            if (Engine.Engine.currentLevel.isSolid(position) && Engine.Engine.currentLevel.DroppedItems.FindEntity(position) == null)
            {
                isClear = true;
            }
            return isClear;
        }

        private bool isPositionClear(int X, int Y)
        {
            IntVec position = new IntVec(X, Y);
            bool isClear = false;
            if (!Engine.Engine.currentLevel.isSolid(position))
            {
                isClear = true;
            }
            return isClear;
        }

        private void dropItem(IntVec dropPosition, int slot)
        {
            if (slot < contents.Count - 1)
            {
                Engine.Engine.currentLevel.DroppedItems.Add(contents[slot], dropPosition);
            }
        }

        public void spewOutIteams()
        {
            IntVec chestPosition = Engine.Engine.currentLevel.InteractableEnvironment.FindPosition(this);

            for (int currentSlot = 0; currentSlot < contents.Count; currentSlot++)
            {
                if (isPositionClear(chestPosition.X - 1, chestPosition.Y - 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X - 1, chestPosition.Y - 1);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X, chestPosition.Y - 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X, chestPosition.Y - 1);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X - 1, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X - 1, chestPosition.Y);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X, chestPosition.Y);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X + 1, chestPosition.Y + 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X + 1, chestPosition.Y + 1);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X + 1, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X + 1, chestPosition.Y);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
                if (isPositionClear(chestPosition.X, chestPosition.Y + 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X, chestPosition.Y + 1);
                    dropItem(dropPosition, currentSlot);
                    currentSlot++;
                }
            }
            
        }

        public void changeSolid()
        {
           if(isSolid)
           {
               isSolid = false;
               isVisable = false;
           }
           else if (!isSolid) 
           {
               isSolid = true;
               isVisable = true;
           }
        }

        public bool getsolidity()
        {
            return isSolid;
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture, isVisable);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            if (!empty)
            {
                spewOutIteams();
                empty = true;
            }
            Engine.Engine.currentLevel.InteractableEnvironment.Remove(this);
        }
    }
}
