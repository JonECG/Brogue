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
        protected int numAbilities;
        protected int armorRating;
        protected float directionFacing;
        protected Ability[] abilities;
        static Texture2D tex;
        static Sprite sprite;
        protected Equipment currentlyEquippedItems;
        protected Inventory inventory;

        public void move(Direction dir, Mapping.Level level)
        {
            if (dir == Direction.RIGHT)
            {
                directionFacing = 0;
                sprite.direction = directionFacing;
                level.CharacterEntities.AddPosition(this, new IntVec(1, 0));
            }
            if (dir == Direction.DOWN)
            {
                directionFacing = (float)(3 * Math.PI / 2);
                sprite.direction = directionFacing;
                level.CharacterEntities.AddPosition(this, new IntVec(0, 1));
            }
            if (dir == Direction.LEFT)
            {
                directionFacing = (float)(Math.PI);
                sprite.direction = directionFacing;
                level.CharacterEntities.AddPosition(this, new IntVec(-1, 0));
            }
            if (dir == Direction.UP)
            {
                directionFacing = (float)(Math.PI / 2);
                sprite.direction = directionFacing;
                level.CharacterEntities.AddPosition(this, new IntVec(0, -1));
            }
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            int damagePostReduction = damage - armorRating;
            damagePostReduction = (damagePostReduction < 1) ? 1 : 0;
            health -= damagePostReduction;
        }

        public static void LoadContent(ContentManager content)
        {
            tex = content.Load<Texture2D>("Hero/Hero");
            sprite = new Sprite(tex);
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
                        move(Direction.LEFT, level);
                        turnOver = true;
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.W))
                {
                    if (canMove)
                    {
                        move(Direction.UP, level);
                        turnOver = true;
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.D))
                {
                    if (canMove)
                    {
                        move(Direction.RIGHT, level);
                        turnOver = true;
                    }
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.S))
                {
                    if (canMove)
                    {
                        move(Direction.DOWN, level);
                        turnOver = true;
                    }
                }
                // JUST FOR TESTING
                else if (Mapping.KeyboardController.IsDown(Keys.B))
                {
                    level += 1;
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

        public void equipArmor(int itemToEquip, int currentItemIndex = 0)
        {
            if(inventory.stored[itemToEquip].item.MyProperty == ITypes.Armor)
            {
                Item temp = currentlyEquippedItems.equippedArmor[currentItemIndex];
                currentlyEquippedItems.equippedArmor[currentItemIndex] = (Armor)(inventory.stored[itemToEquip].item);
                inventory.stored[itemToEquip].item = temp;
            }
        }

        public void equipWeapon(int itemToEquip, int currentItemIndex = 0)
        {
            if (inventory.stored[itemToEquip].item.MyProperty == ITypes.Weapon)
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
