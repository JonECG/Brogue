using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Items.Consumables;

namespace Brogue.EnviromentObjects.Interactive
{
    class Chest : Iinteractable, IRenderable
    {
        static Texture2D sprite { get; set; }

        //List<> contents = new List<>;

        public bool isSolid { get; set; }
        public bool isOpen { get; set; }

        public Chest()
        {
            isSolid = false;
            //contents.add();
        }

        public void fillChest()
        {
            //place contents in to chest
        }

        public void spewOutIteams()
        {
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
            //spewOutIteams();
        }
    }
}
