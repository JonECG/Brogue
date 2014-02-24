﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue;
using Brogue.Items;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Items.Consumables;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest : Iinteractable, IEnvironmentObject, IRenderable
    {
        static Texture2D sprite { get; set; }

        int chestSize = 10;

        List<Item> contents;

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }

        public Chest(Item[] putIntoChest)
        {
            isSolid = false;
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
            throw new NotImplementedException();
        }

        public void changeSolid()
        {
           if(isSolid)
           {
               isSolid = false;
           }
           else if (!isSolid) 
           {
               isSolid =true;
           }
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Enviroment/Chest");
        }

        public bool getsolidity()
        {
            return isSolid;
        }

        public void actOn()
        {
            throw new NotImplementedException();
            //spewOutIteams();
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(sprite);
        }

    }
}
