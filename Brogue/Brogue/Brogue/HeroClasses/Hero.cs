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
using Brogue.EnviromentObjects.Interactive;
using Brogue.Mapping;
using Brogue.Items.Equipment.Weapon.Melee;
using Brogue.Items.Equipment.Weapon.Ranged;

namespace Brogue.HeroClasses
{

    public abstract class Hero : GameCharacter, IRenderable
    {
        public static int level { get; set; }
        public static int MaxJarBarAmount;
        protected int numAbilities = 0;
        protected int armorRating;
        protected int experience = 0;
        protected int expRequired = 100;
        protected Ability[] abilities;
        public int jarBarAmount;
        static Sprite sprite;
        static Sprite radiusSprite;
        static Sprite castingSprite;
        public static DynamicTexture abilitySprite;
        public static DynamicTexture castingSquareSprite;
        protected Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();
        //Variable for testing, delete
        private static int testHealth;
        private static bool viewingCast = false;

        public Hero()
        {
            level = 1;
            numAbilities = 0;
            experience = 0;
            expRequired = 100;
            MaxJarBarAmount = 50;
            jarBarAmount = 0;
            isFriendly = true;
            abilities = new Ability[2];
            abilities[0] = new Cleave();
            currentlyEquippedItems.equipWeapon(new Sword(1, 1));
        }

        public IntVec move(Direction dir)
        {
            IntVec positionMovement = new IntVec(0, 0);
            if (dir == Direction.RIGHT)
            {
                positionMovement = new IntVec(1, 0);
            }
            if (dir == Direction.DOWN)
            {
                positionMovement = new IntVec(0, 1);
            }
            if (dir == Direction.LEFT)
            {
                positionMovement = new IntVec(-1, 0);
            }
            if (dir == Direction.UP)
            {
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
            radiusSprite = new Sprite(abilitySprite);
            castingSprite = new Sprite(castingSquareSprite);
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

        private void resetLevel()
        {
            if (experience >= expRequired)
            {
                int addedExp = experience - expRequired;
                level += 1;
                experience = 0 + addedExp;
                expRequired = 1000 + 50 * (level-1);
            }
        }

        public override bool TakeTurn(Mapping.Level mapLevel)
        {
            bool turnOver = false;
            bool casting = false;
            /*for (int i = 0; i < numAbilities && !casting; i++)
            {
                casting = abilities[i].isCasting;
            }*/
            resetArmor();
            resetHealth();
            resetLevel();
            if (MouseController.LeftClicked())
            {
               
                IInteractable interactableObj = mapLevel.InteractableEnvironment.FindEntity(MouseController.MouseGridPosition());
                if (interactableObj != null)
                {
                    Engine.Engine.Log(interactableObj.ToString());
                    interactableObj.actOn(this);
                    turnOver = true;
                }
            }

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
                else if (MouseController.LeftClicked())
                {
                    attack(mapLevel);
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

                else if (Mapping.KeyboardController.IsPressed(Keys.D1))
                {
                    Engine.Engine.Log(abilities[0].cooldown.ToString());
                    if (abilities[0].cooldown == 0)
                    {
                        viewingCast = true;
                    }
                }

                else turnOver = (Mapping.KeyboardController.IsTyped(Keys.Space));
            }

            if (viewingCast)
            {
                IntVec[] castSquares = abilities[0].viewCastRange(mapLevel, mapLevel.CharacterEntities.FindPosition(this));
                Engine.Engine.ClearGridSelections();
                Engine.Engine.AddGridSelections(castSquares, abilitySprite);
                if (abilities[0].getCastingSquares() != null)
                {
                    Engine.Engine.AddGridSelections(abilities[0].getCastingSquares(), castingSquareSprite);
                }

                if (MouseController.LeftClicked())
                {
                    bool withinRange = false;
                    for (int i = 1; i < castSquares.Length && !withinRange; i++)
                    {
                        if (MouseController.MouseGridPosition().Equals(castSquares[i]))
                        {
                            withinRange = true;
                            abilities[0].addCastingSquares(MouseController.MouseGridPosition());
                        }
                    }
                }
                if (MouseController.RightClicked())
                {
                    abilities[0].removeCastingSquares(MouseController.MouseGridPosition());
                }
                
                if (Mapping.KeyboardController.IsReleased(Keys.D1))
                {
                    Engine.Engine.ClearGridSelections();
                    abilities[0].finishCastandDealDamage(level, currentlyEquippedItems.getTotalWeaponDamage());
                    turnOver = true;
                    viewingCast = !viewingCast;
                }
            }
            if (turnOver)
            {
                cooldownAbilities();
            }
            return turnOver;
        }

        //public void castAbility(int ability)
        public void attack(Level mapLevel)
        {
            Enemies.Enemy testEnemy = (Enemies.Enemy)mapLevel.CharacterEntities.FindEntity(MouseController.MouseGridPosition());
            if (testEnemy != null)
            {
                int weaponRange1 = currentlyEquippedItems.equippedWeapons[0].Range;
                int weaponRange2 = (currentlyEquippedItems.equippedWeapons[1] != null) ? currentlyEquippedItems.equippedWeapons[1].Range : -1;
                damageEnemyIfInRange(testEnemy, mapLevel.CharacterEntities.FindPosition(this), currentlyEquippedItems.equippedWeapons[0].Damage, weaponRange1);
                if (weaponRange2 != -1)
                {
                    damageEnemyIfInRange(testEnemy, mapLevel.CharacterEntities.FindPosition(this), currentlyEquippedItems.equippedWeapons[1].Damage, weaponRange2);
                }
            }
        }

        private void damageEnemyIfInRange(Enemies.Enemy testEnemy, IntVec heroPosition, int damage, int range)
        {
            Engine.Engine.Log(IsInRange(testEnemy.position, heroPosition, range).ToString());
            if (IsInRange(testEnemy.position, heroPosition, range))
            {
                Engine.Engine.Log("I'm called");
                testEnemy.TakeDamage(damage, this);
            }
        }

        private IntVec[] seeWeaponRange(Level mapLevel, int weaponIndex)
        {
            int totalWeaponRange = currentlyEquippedItems.equippedWeapons[weaponIndex].Range + 1;
            return AStar.getPossiblePositionsFrom(mapLevel, mapLevel.CharacterEntities.FindPosition(this), totalWeaponRange);
        }

        private bool IsInRange(IntVec firstPosition, IntVec secondPosition, int range)
        {
            int gridSquaresAway = Math.Abs(firstPosition.X - secondPosition.X) + Math.Abs(firstPosition.Y - secondPosition.Y);
            Engine.Engine.Log(gridSquaresAway.ToString());
            return (gridSquaresAway <= range);
        }
        //public void useItem();

        public void cooldownAbilities()
        {
            for(int i=0; i<abilities.Length; i++)
            {
                if (abilities[i] != null)
                {
                    if (!abilities[i].wasJustCast)
                    {
                        abilities[i].cooldown -= (abilities[i].cooldown > 0) ? 1 : 0;
                    }
                    abilities[i].wasJustCast = false;
                }
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
            if (inventory.stored[itemToEquip].item.ItemType == ITypes.Armor)
            {
                Item temp = currentlyEquippedItems.equippedArmor[currentItemIndex];
                currentlyEquippedItems.equippedArmor[currentItemIndex] = (Armor)(inventory.stored[itemToEquip].item);
                Engine.Engine.Log("Equipped " + inventory.stored[itemToEquip].item.Name);
                if (temp == null)
                {
                    inventory.stored[itemToEquip].isFilled = false;
                } 
                inventory.stored[itemToEquip].item = temp;
            }
        }

        public void equipWeapon(int itemToEquip, int currentItemIndex = 0)
        {
            if (inventory.stored[itemToEquip].isFilled && inventory.stored[itemToEquip].item.ItemType == ITypes.Weapon)
            {
                Item temp = currentlyEquippedItems.equippedWeapons[currentItemIndex];
                currentlyEquippedItems.equippedWeapons[currentItemIndex] = (Weapon)(inventory.stored[itemToEquip].item);
                Engine.Engine.Log("Equipped " + inventory.stored[itemToEquip].item.Name);
                if (temp == null)
                {
                    inventory.stored[itemToEquip].isFilled = false;
                }
                inventory.stored[itemToEquip].item = temp;
            }
        }

        public void swapItems(int itemOne, int itemTwo)
        {
            inventory.swapItem(itemOne, itemTwo);
        }

        public Inventory GetInventory()
        {
            return inventory;
        }

        public void pickupItem(Item item)
        {
            inventory.addItem(item);
        }

        public void dropItem(int whichItem, Level mapLevel)
        {
            IntVec itemPosition = new IntVec(mapLevel.CharacterEntities.FindPosition(this).X, mapLevel.CharacterEntities.FindPosition(this).Y);
            Item tempItem = inventory.removeItem(whichItem);
            if (tempItem != null)
            {
                mapLevel.DroppedItems.Add(tempItem, itemPosition);
            }
        }

        Sprite IRenderable.GetSprite()
        {
            return sprite;
        }
    }
}
