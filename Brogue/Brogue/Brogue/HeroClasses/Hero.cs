using Brogue.InventorySystem;
using Brogue.Enums;
using Brogue.Abilities;
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
using Brogue.Items.Equipment.Armor;
using Brogue.Items.Equipment.Weapon;

namespace Brogue.HeroClasses
{

    public abstract class Hero : GameCharacter, IRenderable
    {
        public static int level {get;  set;}
        protected int numAbilities = 0;
        protected int armorRating;
        protected float directionFacing;
        protected Ability[] abilities;
        static Sprite sprite;
        protected Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();

        public IntVec move(Direction dir)
        {
            IntVec positionMovement = new IntVec(0,0);
            if (dir == Direction.RIGHT)
            {
                directionFacing = 0;
                positionMovement = new IntVec(1,0);
            }
            if (dir == Direction.DOWN)
            {
                directionFacing = (float)(Math.PI / 2);
                positionMovement = new IntVec(0, 1);
            }
            if (dir == Direction.LEFT)
            {
                directionFacing = (float)(Math.PI);
                positionMovement = new IntVec(-1, 0);
            }
            if (dir == Direction.UP)
            {
                directionFacing = (float)(3*Math.PI / 2);
                positionMovement = new IntVec(0, -1);
            }
            sprite.Direction = dir;
            return positionMovement;
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            int damagePostReduction = damage - armorRating;
            damagePostReduction = (damagePostReduction < 1) ? 1 : 0;
            health -= damagePostReduction;
        }

        public static void loadSprite()
        {
            sprite = new Sprite(texture);
        }

        private void resetArmor()
        {
            armorRating = currentlyEquippedItems.getTotalArmorRating();
        }

        private void resetHealth()
        {
            maxHealth = 20*level;
        }

        public override bool TakeTurn(Mapping.Level mapLevel)
        {
            bool canMove = true;
            bool turnOver = false;
            bool casting = false;
            //resetArmor();
            /*for (int i = 0; i < numAbilities && !casting; i++)
            {
                casting = abilities[i].isCasting;
            }*/

            if (!casting)
            {
                if (Mapping.KeyboardController.IsPressed(Keys.A))
                {
                    if (canMove)
                    {
                        turnOver = mapLevel.Move(this, move(Direction.LEFT));
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.W))
                {
                    if (canMove)
                    {
                        turnOver = mapLevel.Move(this, move(Direction.UP));
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.D))
                {
                    if (canMove)
                    {
                        turnOver = mapLevel.Move(this, move(Direction.RIGHT));
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.S))
                {
                    if (canMove)
                    {
                        turnOver = mapLevel.Move(this, move(Direction.DOWN));
                    }
                }
                // THESE ARE JUST FOR TESTING
                else if (Mapping.KeyboardController.IsPressed(Keys.B))
                {
                    level += 1;
                }
                else if (Mapping.KeyboardController.IsPressed(Keys.LeftShift))
                {
                    checkGround(mapLevel);
                }
                else if (Mapping.KeyboardController.IsPressed(Keys.RightShift))
                {
                    IntVec itemPosition = new IntVec(mapLevel.CharacterEntities.FindPosition(this).X, mapLevel.CharacterEntities.FindPosition(this).Y);
                    Item tempItem = dropItem(0);
                    if (tempItem != null)
                    {
                        mapLevel.DroppedItems.Add(tempItem, itemPosition);
                    }
                }

                else turnOver = (Mapping.KeyboardController.IsPressed(Keys.Space));
            }

            //cooldownAbilities();
            return turnOver;
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

        private void checkGround(Mapping.Level mapLevel)
        {
            Item temp = mapLevel.DroppedItems.FindEntity(mapLevel.CharacterEntities.FindPosition(this));
            pickupItem(temp);
            if (temp != null && !inventory.inventoryMaxed())
            {
                mapLevel.DroppedItems.RemoveEntity(temp);
                Engine.Engine.Log("item picked up");
            }
        }

        public void equipArmor(int itemToEquip, int currentItemIndex = 0)
        {
            if(inventory.stored[itemToEquip].item.ItemType == ITypes.Armor)
            {
                Item temp = currentlyEquippedItems.equippedArmor[currentItemIndex];
                currentlyEquippedItems.equippedArmor[currentItemIndex] = (Armor)(inventory.stored[itemToEquip].item);
                inventory.stored[itemToEquip].item = temp;
            }
        }

        public void equipWeapon(int itemToEquip, int currentItemIndex = 0)
        {
            if (inventory.stored[itemToEquip].item.ItemType == ITypes.Weapon)
            {
                Item temp = currentlyEquippedItems.equippedWeapons[currentItemIndex];
                currentlyEquippedItems.equippedWeapons[currentItemIndex] = (Weapon)(inventory.stored[itemToEquip].item);
                inventory.stored[itemToEquip].item = temp;
            }
        }

        public void swapItems(int itemOne, int itemTwo)
        {
            inventory.swapItem(itemOne, itemTwo);
        }

        public void pickupItem(Item item)
        {
            inventory.addItem(item);
        }

        public Item dropItem(int whichItem)
        {
            return inventory.removeItem(whichItem);
        }

        Sprite IRenderable.GetSprite()
        {
            return sprite;
        }
    }
}
