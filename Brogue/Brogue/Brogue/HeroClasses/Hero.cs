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
using Brogue.Abilities.AOE;
using Brogue.Items.Equipment.Accessory;

namespace Brogue.HeroClasses
{

    [Serializable] public abstract class Hero : GameCharacter, IRenderable
    {
        public int level { get; set; }
        public int MaxJarBarAmount = 50;
        protected int numAbilities;
        protected int damageBoost;
        public Classes heroRole;
        protected int baseHealth;
        protected int armorRating;
        protected int experience = 0;
        protected int expRequired = 100;
        protected int healthPerLevel;
        protected Ability[] abilities;
        public int jarBarAmount;
        protected static Sprite sprite;
        static Sprite radiusSprite;
        static Sprite castingSprite;
        public static DynamicTexture abilitySprite;
        public static DynamicTexture castingSquareSprite;
        public Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();
        //Variable for testing, delete
        private bool viewingCast = false;
        private int viewedAbility;
        private Keys abilityKey;

        public Hero()
        {
            level = 5;
            numAbilities = 0;
            baseHealth = 200;
            health = baseHealth;
            maxHealth = health;
            healthPerLevel = 50;
            damageBoost = 0;
            experience = 0;
            expRequired = 50;
            jarBarAmount = 0;
            isFriendly = true;
            numAbilities = 2;
            abilities = new Ability[6];
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
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
            damagePostReduction = (damagePostReduction < 1) ? 1 : damagePostReduction;
            health -= damagePostReduction;
            Engine.Engine.Log(health.ToString());
            Engine.Engine.Log(attacker.ToString());
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

        protected void resetLevel()
        {
            maxHealth = baseHealth + currentlyEquippedItems.getAccessoryHealthModifier() + healthPerLevel * level;
            if (experience >= expRequired)
            {
                int addedExp = experience - expRequired;
                level += 1;
                health += healthPerLevel;
                experience = 0 + addedExp;
                expRequired = 50 + 25 * (level-1);
            }
        }

        protected void resetHealth()
        {
            health = maxHealth;
        }

        private void drinkFromJarBar()
        {
            int amountToHeal = (jarBarAmount > maxHealth - health) ? maxHealth - health : jarBarAmount;
            jarBarAmount -= amountToHeal;
            Engine.Engine.Log(amountToHeal.ToString());
            
            Heal(amountToHeal);
            Engine.Engine.Log(health.ToString());
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
                    turnOver = true;
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
                    drinkFromJarBar();
                }
                else if (Mapping.KeyboardController.IsPressed(Keys.R))
                {
                    inventory.sortInventory();
                }

                else if (Mapping.KeyboardController.IsPressed(Keys.D1))
                {
                    if (abilities[0].cooldown == 0)
                    {
                        viewingCast = true;
                        viewedAbility = 0;
                        abilityKey = Keys.D1;
                    }
                }
                else if (Mapping.KeyboardController.IsPressed(Keys.D2))
                {
                    if (abilities[1].cooldown == 0)
                    {
                        viewingCast = true;
                        viewedAbility = 1;
                        abilityKey = Keys.D2;
                    }
                }

                else turnOver = (Mapping.KeyboardController.IsTyped(Keys.Space));
            }

            if (viewingCast)
            {
                turnOver = castAbility(viewedAbility, mapLevel);
               
                if (Mapping.KeyboardController.IsReleased(abilityKey))
                {
                    Engine.Engine.ClearGridSelections();
                    abilities[viewedAbility].resetSquares();
                    viewingCast = !viewingCast;
                }
            }
            if (turnOver)
            {
                cooldownAbilities();
            }
            return turnOver;
        }

        public bool castAbility(int ability, Level mapLevel)
        {
            bool turnOver = false;
            IntVec[] castSquares = abilities[ability].viewCastRange(mapLevel, mapLevel.CharacterEntities.FindPosition(this));
                Engine.Engine.ClearGridSelections();
                Engine.Engine.AddGridSelections(castSquares, abilitySprite);
                if (abilities[ability].getCastingSquares() != null)
                {
                    Engine.Engine.AddGridSelections(abilities[ability].getCastingSquares(), castingSquareSprite);
                }

                if (MouseController.LeftClicked())
                {
                    bool withinRange = false;
                    for (int i = 0; i < castSquares.Length && !withinRange; i++)
                    {
                        if (MouseController.MouseGridPosition().Equals(castSquares[i]))
                        {
                            withinRange = true;
                            abilities[ability].addCastingSquares(MouseController.MouseGridPosition());
                            if (abilities[ability].filledSquares())
                            {
                                turnOver = true;
                                abilities[ability].finishCastandDealDamage(level, currentlyEquippedItems.getTotalDamageIncrease(), mapLevel, this);
                                Engine.Engine.ClearGridSelections();
                                viewingCast = false;
                            }
                        }
                    }
                }
                if (MouseController.RightClicked())
                {
                    abilities[ability].removeCastingSquares(MouseController.MouseGridPosition());
                }
                
                return turnOver;
        }

        public void attack(Level mapLevel)
        {
            if (mapLevel.CharacterEntities.FindEntity(MouseController.MouseGridPosition()) != this)
            {
                Enemies.Enemy testEnemy = (Enemies.Enemy)mapLevel.CharacterEntities.FindEntity(MouseController.MouseGridPosition());
                if (testEnemy != null)
                {
                    int weaponRange1 = currentlyEquippedItems.getPrimaryWeapon().Range;
                    int weaponRange2 = (currentlyEquippedItems.getAuxilaryWeapon() != null) ? currentlyEquippedItems.getAuxilaryWeapon().Range : -1;
                    damageEnemyIfInRange(testEnemy, mapLevel, mapLevel.CharacterEntities.FindPosition(this), currentlyEquippedItems.getPrimaryWeapon().Damage, weaponRange1);
                    if (weaponRange2 != -1)
                    {
                        damageEnemyIfInRange(testEnemy, mapLevel,  mapLevel.CharacterEntities.FindPosition(this), currentlyEquippedItems.getAuxilaryWeapon().Damage, weaponRange2);
                    }
                }
            }
        }

        private void damageEnemyIfInRange(GameCharacter testEnemy, Level mapLevel, IntVec heroPosition, int damage, int range)
        {
            IntVec position = mapLevel.CharacterEntities.FindPosition(testEnemy);
            if (IsInRange(position, heroPosition, range))
            {
                testEnemy.TakeDamage(damage, this);
            }
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
            if (temp != null)
            {
                mapLevel.DroppedItems.RemoveEntity(temp);
                Engine.Engine.Log("item picked up");
            }
        }

        public void equipAccessory(int itemToEquip)
        {
            if (inventory.stored[itemToEquip].item != null && inventory.stored[itemToEquip].item.ItemType == ITypes.Accessory)
            {
                Item newlyEquippedItem = inventory.stored[itemToEquip].item;
                if(currentlyEquippedItems.isAccessoryEquipable((Accessory)newlyEquippedItem, heroRole, level))
                {
                    inventory.removeItem(itemToEquip);
                    inventory.addItem(currentlyEquippedItems.removeAccessory((Accessory)newlyEquippedItem));
                    currentlyEquippedItems.equipAccessory((Accessory)newlyEquippedItem);
                }
            }
        }

        public void equipArmor(int itemToEquip)
        {
            if (inventory.stored[itemToEquip].item != null && inventory.stored[itemToEquip].item.ItemType == ITypes.Armor)
            {
                Item newlyEquippedItem = inventory.stored[itemToEquip].item;
                if (currentlyEquippedItems.isArmorEquipable((Armor)newlyEquippedItem, heroRole, level))
                {
                    inventory.removeItem(itemToEquip);
                    inventory.addItem(currentlyEquippedItems.removeArmor((Armor)newlyEquippedItem));
                    currentlyEquippedItems.equipArmor((Armor)newlyEquippedItem);
                }
            }
        }

        public void equipWeapon(int inventoryIndex, int weaponIndex )
        {
            if (inventory.stored[inventoryIndex].item != null && inventory.stored[inventoryIndex].item.ItemType == ITypes.Weapon)
            {
                Item newlyEquippedItem = inventory.stored[inventoryIndex].item;
                if(currentlyEquippedItems.isWeaponEquipable((Weapon)newlyEquippedItem, heroRole, level))
                {
                    inventory.removeItem(inventoryIndex);
                    inventory.addItem(currentlyEquippedItems.removeWeapon(weaponIndex));
                    currentlyEquippedItems.equipWeapon((Weapon)newlyEquippedItem, weaponIndex);
                }
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
            if (item != null && !inventory.inventoryMaxed())
            {
                inventory.addItem(item.PickUpEffect(this));
                Engine.Engine.Log("" + jarBarAmount);
            }

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
