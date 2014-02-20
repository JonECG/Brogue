using Brogue.InventorySystem;
using Brogue.Abilities;
using Brogue.Inventories;
using Brogue.Items;
using Brogue.Items.Equipment;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Brogue.Engine;

namespace Brogue.HeroClasses
{

    public abstract class Hero : GameCharacter, IRenderable
    {
        public static int level {get; protected set;}
        protected int numAbilities;
        protected int spacesPerTurn;
        protected float directionFacing;
        //protected Ability[] abilities;
        static Texture2D tex;
        static Sprite sprite;
        protected Item[] currentlyEquippedItems;
        protected Inventory inventory;

        public void move(Direction dir)
        {
            if (dir == Direction.RIGHT)
            {
                directionFacing = 0;
                position.ints[0] = 1;
            }
            if (dir == Direction.DOWN)
            {
                directionFacing = (float)(Math.PI / 2);
                position.ints[1] = 1;
            }
            if (dir == Direction.RIGHT)
            {
                directionFacing = (float)(Math.PI);
                position.ints[0] = -1;
            }
            if (dir == Direction.UP)
            {
                directionFacing = (float)(3 * Math.PI / 2);
                position.ints[1] = -1 ;
            }
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {
           
        }
        public static void LoadContent(ContentManager content)
        {
            tex = content.Load<Texture2D>("Hero/Hero");
            sprite = new Sprite(tex);
        }
        public override void TakeTurn(Mapping.Level level)
        {
            bool canMove = true;
            bool turnOver = false;
            bool casting = false;
            for (int i = 0; i < numAbilities && !casting; i++)
            {
                casting = abilities[i].isCasting;
            }

            if (!casting)
            {

            }

            else if(Mapping.KeyboardController.IsDown(Keys.A))
            {
                if (canMove){
                    move(Direction.LEFT);
                    turnOver = true;
                }
            }

            else if(Mapping.KeyboardController.IsDown(Keys.W))
            {
                if (canMove){
                    move(Direction.UP);
                    turnOver = true;
                }
            }

            else if(Mapping.KeyboardController.IsDown(Keys.D))
            {
                if (canMove){
                    move(Direction.RIGHT);
                    turnOver = true;
                }
            }

            else if (Mapping.KeyboardController.IsDown(Keys.S))
            {
                if (canMove)
                {
                    move(Direction.DOWN);
                    turnOver = true;
                }

            }

            cooldownAbilities();
        }

        //public void castAbility(int ability)
        //public void attack();
        //public void useItem();

        public void cooldownAbilities()
        {
            for(int i=0; i<numAbilities; i++)
            {
                abilities[i].cooldown -= (abilities[i].isOnCooldown) ? 1 : 0;
            }
        }

        public void equipItem(int itemToEquip, int currentItemIndex = 0)
        {
            //Item temp = currentlyEquippedItems[currentItemIndex];
            //currentlyEquippedItems[currentItemIndex] = inventory.stored[itemToEquip].item;
            inventory.stored[itemToEquip].item = temp;
        }

        public void swapItems(int itemOne, int itemTwo)
        {
            inventory.swapItem(itemOne, itemTwo);
        }

        public void pickupItem(Item item)
        {
            inventory.addItem(item);
        }

        public void dropItem(int whichItem)
        {
            inventory.removeItem(whichItem);
        }

        Sprite IRenderable.GetSprite()
        {
            return sprite;
        }
    }
}
