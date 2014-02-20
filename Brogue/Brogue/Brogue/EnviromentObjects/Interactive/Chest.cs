using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue.Items;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Items.Consumables;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest : Iinteractable, IEnvironmentObject
    {
        static Texture2D sprite { get; set; }

        int chestSize = 10;

        public Item[] contents;

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }

        public Chest()
        {
            isSolid = false;
            contents = new Item[chestSize];
        }

        public void fillChest()
        {
            throw new NotImplementedException();
            //Item randomIteam;
            //for (int i = 0; i < chestSize; i++)
            //{
                
            //}
        }

        public void putInChest(Item newIteam)
        {
            throw new NotImplementedException();
            //place contents in to chest
        }

        public void spewOutIteams()
        {
            throw new NotImplementedException();
            //send array of iteams
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
            sprite = content.Load<Texture2D>("levelTileset");
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
