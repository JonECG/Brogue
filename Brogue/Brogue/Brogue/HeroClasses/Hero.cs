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
using Brogue.Abilities.Togglable;
using Brogue.Abilities.SingleTargets;
using Microsoft.Xna.Framework;

namespace Brogue.HeroClasses
{

    [Serializable]
    public abstract class Hero : GameCharacter, IRenderable
    {
        const int jarBarIncrease = 25;

        public int level { get; set; }

        public List<ElementAttributes> Element { get; set; }

        public int MaxJarBarAmount = 50;
        protected int numAbilities;
        public int damageBoost;
        protected bool canDuelWield;

        public static Classes heroRole;
        public static bool visible;
        public static int parryCount;
        public int invisibilityTurnCount;
        public static Direction directionFacing;
        protected int baseHealth;
        protected bool turnOver;
        protected int armorRating;
        protected int armorBoost, currentBoost;
        public int armorBoostTurnCount;
        protected int requiredBranchLevel;
        protected int experience = 0;
        protected int expRequired = 100;
        protected int healthPerLevel { get; set; }
        protected Ability[] abilities;
        public int jarBarAmount;
        protected static Sprite sprite;
        public static DynamicTexture abilitySprite;
        public static DynamicTexture warriorSprite;
        public static DynamicTexture mageSprite;
        public static DynamicTexture rogueSprite;
        public static DynamicTexture castingSquareSprite;
        protected Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();
        //Variable for testing, delete
        private bool viewingCast = false;
        private int viewedAbility;
        private Keys abilityKey;

        public Hero()
        {
            level = 5;
            currentBoost = 0;
            baseHealth = 200;
            health = baseHealth;
            maxHealth = health;
            healthPerLevel = 50;
            requiredBranchLevel = 10;
            damageBoost = 1;
            experience = 0;
            expRequired = 500;
            jarBarAmount = 0;
            isFriendly = true;
            visible = true;
            canDuelWield = false;
            abilities = new Ability[6];
        }

        public int getExperience()
        {
            return experience;
        }
        public int getExpReq()
        {
            return expRequired;
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
            directionFacing = dir;
            sprite.Direction = dir;
            return positionMovement;
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            if (parryCount == 0)
            {
                armorBoostTurnCount -= (armorBoostTurnCount > 0) ? 1 : 0;
                int percentHealth = 100 + armorRating * 2;
                int maxHealthPostArmorIncrease = (int)(((float)percentHealth / 100) * maxHealth);
                int damagePostReduction = maxHealthPostArmorIncrease - damage;
                int finalDamage = maxHealth - ((int)(((float)damagePostReduction / maxHealthPostArmorIncrease) * maxHealth));
                damagePostReduction = (damagePostReduction < 1) ? 1 : damagePostReduction;
                health -= finalDamage;
                Engine.Engine.Log(health.ToString());
                Engine.Engine.Log(attacker.ToString());
            }
            else
            {
                Engine.Engine.AddVisualAttack(this, "Items/MailChest", .25f, 1.5f, .1f);
                parryCount--;
            }
        }

        public void setParryCount(int count)
        {
            parryCount = count;
        }

        public static void loadSprite()
        {
            DynamicTexture heroTexture = null;
            switch (heroRole)
            {
                case Classes.Warrior:
                    heroTexture = warriorSprite;
                    break;
                case Classes.Mage:
                    heroTexture = mageSprite;
                    break;
                case Classes.Rogue:
                    heroTexture = rogueSprite;
                    break;
            }
            sprite = new Sprite(heroTexture);
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
            armorRating += (armorBoostTurnCount > 0) ? armorBoost : 0;
        }

        public void resetSprite()
        {
            sprite.Blend = (visible)?new Color(255, 255, 255, 255):new Color(255,255,255,122);
        }

        public void ApplyArmorBoost(int boost, int turnCount)
        {
            armorBoost -= currentBoost;
            currentBoost = boost;
            armorBoost += boost;
            armorBoostTurnCount = turnCount;
        }

        public void setArmorBoost(int boost)
        {
            armorBoost -= currentBoost;
            currentBoost = boost;
            armorBoost += currentBoost;
        }

        public int getArmorBoost()
        {
            return armorBoost;
        }

        public int GetArmorRating()
        {
            return armorRating;
        }

        public void setInvisibility(int turnCount)
        {
            invisibilityTurnCount = turnCount;
        }

        public bool hasReachedBranchLevel()
        {
            bool reached = requiredBranchLevel <= level;
            if (reached)
            {
                requiredBranchLevel = (reached) ? 700 : requiredBranchLevel;
            }
            return reached;
        }

        protected void resetLevel()
        {
            maxHealth = baseHealth + currentlyEquippedItems.getAccessoryHealthModifier() + healthPerLevel * level;
            if (experience >= expRequired)
            {
                int addedExp = experience - expRequired;
                level += 1;
                health += healthPerLevel;
                MaxJarBarAmount += jarBarIncrease;
                Engine.Engine.Log(MaxJarBarAmount.ToString());
                experience = 0 + addedExp;
                expRequired = 500 + 250 * (level - 1);
            }
        }

        protected void resetHealth()
        {
            health = maxHealth;
        }

        public void obtainStartingGear(Level mapLevel)
        {
            switch (heroRole)
            {
                case Classes.Ranger:
                    inventory.addItem(new CrossBow(mapLevel.DungeonLevel, 7));
                    break;
                case Classes.Magus:
                    inventory.addItem(new Sword(mapLevel.DungeonLevel, 7));
                    break;
                case Classes.Duelist:
                    inventory.addItem(new Rapier(mapLevel.DungeonLevel, 7));
                    break;
            }
        }

        private void drinkFromJarBar()
        {
            int amountToHeal = (jarBarAmount > maxHealth - health) ? maxHealth - health : jarBarAmount;
            jarBarAmount -= amountToHeal;

            Engine.Engine.Log(health.ToString());
            Heal(amountToHeal);
            Engine.Engine.Log(health.ToString());
        }

        public override bool TakeTurn(Mapping.Level mapLevel)
        {
            turnOver = false;
            bool casting = false;
            int test = health;
            resetSprite();

            if (!isFrozen)
            {
                if (MouseController.LeftClicked())
                {

                    IInteractable interactableObj = mapLevel.InteractableEnvironment.FindEntity(MouseController.MouseGridPosition());
                    if (interactableObj != null && Engine.Engine.IsMouseAdjacentToHero())
                    {
                        Engine.Engine.Log(interactableObj.ToString());
                        interactableObj.actOn(this);
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
                    else if (Mapping.KeyboardController.IsTyped(Keys.L))
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

                    else if (Mapping.KeyboardController.IsPressed(Keys.D1))
                    {
                        if (abilities[0] != null && abilities[0].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 0;
                            abilityKey = Keys.D1;
                        }
                    }
                    else if (Mapping.KeyboardController.IsPressed(Keys.D2))
                    {
                        if (abilities[1] != null && abilities[1].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 1;
                            abilityKey = Keys.D2;
                        }
                    }
                    else if (Mapping.KeyboardController.IsPressed(Keys.D3))
                    {
                        if (abilities[2] != null && abilities[2].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 2;
                            abilityKey = Keys.D3;
                        }
                    }
                    else if (Mapping.KeyboardController.IsPressed(Keys.D4))
                    {
                        if (abilities[3] != null && abilities[3].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 3;
                            abilityKey = Keys.D4;
                        }
                    }
                    else if (Mapping.KeyboardController.IsPressed(Keys.D5))
                    {
                        if (abilities[4] != null && abilities[4].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 4;
                            abilityKey = Keys.D5;
                        }
                    }
                    else if (Mapping.KeyboardController.IsPressed(Keys.D6))
                    {
                        if (abilities[5] != null && abilities[5].cooldown == 0)
                        {
                            viewingCast = true;
                            viewedAbility = 5;
                            abilityKey = Keys.D6;
                        }
                    }

                    else turnOver = (Mapping.KeyboardController.IsTyped(Keys.Space));
                }

                if (viewingCast)
                {
                    turnOver = castAbility(viewedAbility, mapLevel);

                    if (Mapping.KeyboardController.IsReleased(abilityKey))
                    {
                        if (abilities[viewedAbility].type != AbilityTypes.Toggle)
                        {
                            Engine.Engine.ClearGridSelections();
                            abilities[viewedAbility].resetSquares();
                        }
                        viewingCast = !viewingCast;
                    }
                }
                if (turnOver)
                {
                    cooldownAbilities();
                    invisibilityTurnCount -= (!visible)?1:0;
                    visible = invisibilityTurnCount <= 0;
                }
            }
            else
            {
                turnOver = true;
            }
            resetArmor();
            resetLevel();
            CheckElementDamage();
            return turnOver;
        }

        public bool castAbility(int ability, Level mapLevel)
        {
            bool turnOver = false;
            if (abilities[ability].type != AbilityTypes.Toggle)
            {
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
                                abilities[ability].finishCastandDealDamage(level, currentlyEquippedItems.getTotalDamageIncrease()+damageBoost, mapLevel, this);
                                Engine.Engine.ClearGridSelections();
                                viewingCast = false;
                                invisibilityTurnCount = 0;
                            }
                        }
                    }
                }

                if (MouseController.RightClicked())
                {
                    abilities[ability].removeCastingSquares(MouseController.MouseGridPosition());
                }
            }
            else
            {
                if (MouseController.LeftClicked() && abilities[ability].cooldown == 0)
                {
                    abilities[ability].finishCastandDealDamage(level, currentlyEquippedItems.getTotalDamageIncrease(), mapLevel, this);
                }
            }
            return turnOver;
        }

        public void attack(Level mapLevel)
        {
            if (mapLevel.CharacterEntities.FindEntity(MouseController.MouseGridPosition()) != this)
            {
                GameCharacter testEnemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(MouseController.MouseGridPosition());
                if (testEnemy != null)
                {
                    int weaponRange1 = currentlyEquippedItems.getPrimaryWeaponRange();
                    int weaponRange2 = currentlyEquippedItems.getAuxilaryWeaponRange();
                    IntVec[] weaponHitbox1 = AStar.getPossiblePositionsFrom(mapLevel, mapLevel.CharacterEntities.FindPosition(this), weaponRange1, true);
                    IntVec[] weaponHitbox2 = AStar.getPossiblePositionsFrom(mapLevel, mapLevel.CharacterEntities.FindPosition(this), weaponRange2, true);
                    damageEnemyIfInRange(weaponHitbox1, mapLevel, testEnemy, currentlyEquippedItems.getPrimaryWeapon(), true);
                    damageEnemyIfInRange(weaponHitbox2, mapLevel, testEnemy, currentlyEquippedItems.getAuxilaryWeapon(), false);
                    if (Element != null)
                    {
                        for (int i = 0; i < Element.Count; i++)
                        {
                            switch (Element[i])
                            {
                                case ElementAttributes.Fire:
                                    testEnemy.DealElementalDamage(Element[i], 5);
                                    break;
                                case ElementAttributes.Ice:
                                    testEnemy.DealElementalDamage(Element[i], 3);
                                    break;
                                case ElementAttributes.Lighting:
                                    testEnemy.DealElementalDamage(Element[i], 1);
                                    break;
                                case ElementAttributes.Arcane:
                                    testEnemy.DealElementalDamage(Element[i], 7);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void damageEnemyIfInRange(IntVec[] hitBox, Level mapLevel, GameCharacter enemy, Weapon weapon, bool playAttack)
        {
            bool found = false;
            if (weapon != null)
            {
                string[] name = weapon.Name.Split();
                for (int i = 0; i < hitBox.Length && !found; i++)
                {
                    IntVec test = mapLevel.CharacterEntities.FindPosition(enemy);
                    if (hitBox[i].Equals(mapLevel.CharacterEntities.FindPosition(enemy)))
                    {
                        int weaponDamage = weapon.Damage + damageBoost + currentlyEquippedItems.getAccessoryDamageIncrease();
                        found = true;
                        int damage = (!visible) ? (int)(1.5 * (weaponDamage)) : weaponDamage;
                        if (playAttack)
                        {
                            if (name[1] == "Sword" || name[1] == "Axe" || name[1] == "Great" || name[1] == "Bastard" || name[1] == "Rapier" || name[1] == "Scythe")
                            {
                                Engine.Engine.AddVisualAttack(enemy, "Hero/sword-slash", .25f, 2.0f, .15f);
                                Audio.playSound("swordAttack");
                            }
                            else if (name[1] == "Dagger")
                            {
                                Engine.Engine.AddVisualAttack(enemy, "Hero/DaggerSlash", .25f, 2.0f, .15f);
                                Audio.playSound("DaggerStab");
                            }
                            else if (name[1] == "Claw")
                            {
                                Engine.Engine.AddVisualAttack(enemy, "Hero/ClawSlash", .25f, 2.0f, .15f);
                            }
                            else if (name[1] == "War")
                            {
                                Audio.playSound("HammerSmash");
                                Engine.Engine.AddVisualAttack(enemy, "Hero/hammerSmash", .25f, 2.0f, .15f);
                            }
                            else
                            {
                                Engine.Engine.AddVisualAttack(this, enemy, "Hero/MageAttack", .5f, 1.0f, .03f);
                            }
                        }
                        enemy.TakeDamage(damage, this);
                        invisibilityTurnCount = 0;
                        turnOver = true;
                    }
                }
            }
        }

        private bool IsInRange(IntVec firstPosition, IntVec secondPosition, int range)
        {
            int gridSquaresAway = Math.Abs(firstPosition.X - secondPosition.X) + Math.Abs(firstPosition.Y - secondPosition.Y);
            Engine.Engine.Log(gridSquaresAway.ToString());
            return (gridSquaresAway <= range);
        }

        public void cooldownAbilities()
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                if (abilities[i] != null)
                {
                    if (abilities[i].type == AbilityTypes.Toggle)
                    {
                        ToggleAbility toggled = (ToggleAbility)abilities[i];
                        toggled.updateToggle(level, this);
                    }
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
            pickupItem(temp, mapLevel);
        }

        public void equipAccessory(int itemToEquip)
        {
            if (inventory.stored[itemToEquip].item != null && inventory.stored[itemToEquip].item.ItemType == ITypes.Accessory)
            {
                Item newlyEquippedItem = inventory.stored[itemToEquip].item;
                if (currentlyEquippedItems.isAccessoryEquipable((Accessory)newlyEquippedItem, heroRole, level))
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

        public void equipWeapon(int inventoryIndex)
        {
            if (inventory.stored[inventoryIndex].item != null && (inventory.stored[inventoryIndex].item.ItemType == ITypes.Weapon || inventory.stored[inventoryIndex].item.ItemType == ITypes.Offhand))
            {
                Item newlyEquippedItem = inventory.stored[inventoryIndex].item;
                if (currentlyEquippedItems.isWeaponEquipable((Gear)newlyEquippedItem, heroRole, level))
                {
                    inventory.removeItem(inventoryIndex);
                    Gear item = (Gear)newlyEquippedItem;
                    if (item.EquipableIn.Contains(Slots.Hand_Both))
                    {
                        inventory.addItem(currentlyEquippedItems.removeWeapon(null, true,  0));
                        inventory.addItem(currentlyEquippedItems.removeWeapon(null, true,  1));
                    }
                    else
                    {
                        inventory.addItem(currentlyEquippedItems.removeWeapon((Gear)newlyEquippedItem, canDuelWield));
                    }
                    currentlyEquippedItems.equipWeapon((Gear)newlyEquippedItem, this, canDuelWield);
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

        public Equipment GetEquipment()
        {
            return currentlyEquippedItems;
        }

        public void ObtainItems(Inventory oldInventory, Equipment oldEquipment)
        {
            inventory = oldInventory;
            currentlyEquippedItems = oldEquipment;
        }

        public void pickupItem(Item item, Level mapLevel)
        {
            if (item != null && !inventory.inventoryMaxed())
            {
                inventory.addItem(item.PickUpEffect(this));
                Engine.Engine.Log("" + jarBarAmount);
                mapLevel.DroppedItems.RemoveEntity(item);
                Engine.Engine.Log("item picked up");
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

        public Ability[] getAbilities()
        {
            return abilities;
        }

        public bool isAlive()
        {
            return health > 0;
        }

        public bool canDuelWieldWeapons()
        {
            return canDuelWield;
        }

        Sprite IRenderable.GetSprite()
        {
            return sprite;
        }
    }
}
