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
using Brogue.Abilities.Damaging.SingleTargets;

namespace Brogue.HeroClasses
{

    public abstract class Hero : GameCharacter, IRenderable
    {
        public static int level { get; set; }
        protected int numAbilities = 0;
        protected int armorRating;
        protected int experience = 0;
        protected int expRequired = 300;
        protected float directionFacing;
        protected Ability[] abilities;
        static Sprite sprite;
        static Sprite castingSprite;
        public static Texture2D abilitySprite;
        protected Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();
        //Variable for testing, delete
        private static int testHealth;
        private static bool viewingCast = false;

        public IntVec move(Direction dir)
        {
            IntVec positionMovement = new IntVec(0, 0);
            if (dir == Direction.RIGHT)
            {
                directionFacing = 0;
                positionMovement = new IntVec(1, 0);
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
                directionFacing = (float)(3 * Math.PI / 2);
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
            castingSprite = new Sprite(abilitySprite);
        }

        public void AddExperience(int xp)
        {
            experience += xp;
        }

        public float GetXpPercent()
        {
            float percentage = (float)experience / (float)expRequired;
            return percentage;
        }

        private void resetArmor()
        {
            armorRating = currentlyEquippedItems.getTotalArmorRating();
        }

        private void resetHealth()
        {
            maxHealth = 20 * level;
            if (testHealth != maxHealth)
            {
                Engine.Engine.Log("Character's health increased to: " + maxHealth);
            }
            testHealth = maxHealth;
        }

        public override bool TakeTurn(Mapping.Level mapLevel)
        {
            bool turnOver = false;
            bool casting = false;
            //resetArmor();
            /*for (int i = 0; i < numAbilities && !casting; i++)
            {
                casting = abilities[i].isCasting;
            }*/
            resetArmor();
            resetHealth();
            if (!casting && !viewingCast)
            {
                if (Mapping.KeyboardController.IsTyped(Keys.A))
                {
                    turnOver = mapLevel.Move(this, move(Direction.LEFT));
                }

                else if (Mapping.KeyboardController.IsTyped(Keys.W))
                {
                    turnOver = mapLevel.Move(this, move(Direction.UP));

                }

                else if (Mapping.KeyboardController.IsTyped(Keys.D))
                {

                    turnOver = mapLevel.Move(this, move(Direction.RIGHT));

                }

                else if (Mapping.KeyboardController.IsTyped(Keys.S))
                {

                    turnOver = mapLevel.Move(this, move(Direction.DOWN));

                }
                // THESE ARE JUST FOR TESTING
                else if (Mapping.KeyboardController.IsTyped(Keys.B))
                {
                    level += 1;
                }
                else if (Mapping.KeyboardController.IsTyped(Keys.LeftShift))
                {
                    checkGround(mapLevel);
                }

                else if (Mapping.KeyboardController.IsTyped(Keys.RightShift))
                {
                    IntVec itemPosition = new IntVec(mapLevel.CharacterEntities.FindPosition(this).X, mapLevel.CharacterEntities.FindPosition(this).Y);
                    Item tempItem = dropItem(0);
                    if (tempItem != null)
                    {
                        mapLevel.DroppedItems.Add(tempItem, itemPosition);
                    }
                }
                else if (Mapping.KeyboardController.IsPressed(Keys.D1))
                {
                    abilities = new Ability[2];
                    viewingCast = true;
                    abilities[0] = new Cleave();
                }

                else turnOver = (Mapping.KeyboardController.IsTyped(Keys.Space));
            }

            if (viewingCast)
            {
                IntVec test = new IntVec(mapLevel.CharacterEntities.FindPosition(this).X+1, mapLevel.CharacterEntities.FindPosition(this).Y);
                IntVec[] castSquares = abilities[0].viewCastRange(mapLevel, test);
                Engine.Engine.ClearGridSelections();
                Engine.Engine.AddGridSelections(castSquares);
                
                if (Mapping.KeyboardController.IsPressed(Keys.D2))
                {
                    Engine.Engine.ClearGridSelections();
                    viewingCast = !viewingCast;
                }
            }

            //cooldownAbilities();
            return turnOver;
        }

        //public void castAbility(int ability)
        //public void attack();
        //public void useItem();
        //public void cooldownAbilities()
        //{
        //    for(int i=0; i<numAbilities; i++)
        //    {
        //        abilities[i].cooldown -= (abilities[i].isOnCooldown) ? 1 : 0;
        //    }
        //}

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
            if (inventory.stored[itemToEquip].item.ItemType == ITypes.Armor)
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
