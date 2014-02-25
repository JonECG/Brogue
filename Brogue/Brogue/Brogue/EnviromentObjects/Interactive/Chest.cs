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
    class Chest : IInteractable, IEnvironmentObject, IRenderable
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Chair");

        bool isVisable { get; set; }

        int chestSize = 10;

        List<Item> contents;

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }

        public Chest(Item[] putIntoChest)
        {
            isSolid = false;
            isVisable = true;
            contents = new List<Item>();

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

        public void spewOutIteams()
        {
            //Engine.Engine.currentLevel
            IntVec chestPosition = Engine.Engine.currentLevel.InteractableEnvironment.FindPosition(this);
            for (int currentSlot = 0; currentSlot< 10 && contents.Count > 0; currentSlot++)
            {
                if (Engine.Engine.currentLevel.isSolid(chestPosition.X - 1, chestPosition.Y - 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X - 1, chestPosition.Y - 1);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X, chestPosition.Y - 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X, chestPosition.Y - 1);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X - 1, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X - 1, chestPosition.Y);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X, chestPosition.Y);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X + 1, chestPosition.Y + 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X + 1, chestPosition.Y + 1);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X + 1, chestPosition.Y))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X + 1, chestPosition.Y);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
                else if (Engine.Engine.currentLevel.isSolid(chestPosition.X, chestPosition.Y + 1))
                {
                    IntVec dropPosition = new IntVec(chestPosition.X - 1, chestPosition.Y - 1);
                    Engine.Engine.currentLevel.DroppedItems.Add(contents[currentSlot], dropPosition);
                }
            }
            
        }

        public void changeSolid()
        {
           if(isSolid)
           {
               isSolid = false;
               isVisable = true;
           }
           else if (!isSolid) 
           {
               isSolid = true;
               isVisable = false;
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
            return new Sprite(texture);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            changeSolid();
            spewOutIteams();

        }
    }
}
