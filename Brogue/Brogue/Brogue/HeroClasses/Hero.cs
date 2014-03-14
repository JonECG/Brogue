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

        public static int level = 5;

        public static List<ElementAttributes> Element { get; set; }

        public static int MaxJarBarAmount = 500;
        protected int numAbilities;
        public int damageBoost;
        protected static bool canDuelWield = false;

        public static Classes heroRole;
        public static bool visible = false;
        public static int parryCount;
        public int invisibilityTurnCount;
        public static Direction directionFacing;
        protected int baseHealth;
        protected bool turnOver;
        protected int armorRating;
        protected int armorBoost, currentBoost, rangeBoost, reflectedDamage;
        public int armorBoostTurnCount;
        protected int requiredBranchLevel;
        protected int experience = 0;
        protected int expRequired = 100;
        protected int healthPerLevel { get; set; }
        protected Ability[] abilities;
        public static int jarBarAmount;
        protected static Sprite sprite;
        public static DynamicTexture abilitySprite;
        public static DynamicTexture heroTexture;
        public static DynamicTexture castingSquareSprite;
        protected Equipment currentlyEquippedItems = new Equipment();
        protected Inventory inventory = new Inventory();
        //Variable for testing, delete
        private bool viewingCast = false;
        private int viewedAbility;
        private Keys abilityKey;

        public Hero()
        {
            Element = new List<ElementAttributes>();
            currentBoost = 0;
            rangeBoost = 0;
            baseHealth = 200;
            health = baseHealth;
            maxHealth = health;
            healthPerLevel = 50;
            requiredBranchLevel = 15;
            damageBoost = 1;
            experience = 0;
            expRequired = 500;
            jarBarAmount = (int)(MaxJarBarAmount/5);
            isFriendly = true;
            abilities = new Ability[6];
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i] = null;
            }
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
                int damageReduction = (int)(armorRating / 12.5);
                int finalDamage = (damage - damageReduction < 1)? 1: damage-damageReduction;
                health -= finalDamage;
                attacker.TakeDamage(reflectedDamage, this);
            }
            else
            {
                Audio.playSound("Parry");
                Engine.Engine.AddVisualAttack(this, "Items/MailChest", .25f, 1.5f, .1f);
                parryCount--;
            }
        }

        public int getTotalDamage()
        {
            return damageBoost + currentlyEquippedItems.getTotalDamageIncrease();
        }

        public void setParryCount(int count)
        {
            parryCount = count;
        }

        public void setRangeBoost(int boost)
        {
            rangeBoost = boost;
        }

        public void setReflectedDamage(int damage)
        {
            reflectedDamage = damage;
        }

        public static void loadSprite()
        {
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
            health = (health > maxHealth) ? maxHealth : health;
            MaxJarBarAmount = 500 + jarBarIncrease * level;
            if (experience >= expRequired)
            {
                int addedExp = experience - expRequired;
                level += 1;
                health += healthPerLevel;
                Engine.Engine.Log(MaxJarBarAmount.ToString());
                experience = 0 + addedExp;
                Engine.Engine.drawXP = experience;
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

            if (!visible)
            {
                Enemies.Enemy.DeAggroAll();
            }

            updateDOTabilities(mapLevel);

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
                    else if (Mapping.KeyboardController.IsTyped(Keys.M))
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
                        else if(abilities[viewedAbility].cooldown == 0)
                        {
                            abilities[viewedAbility].wasJustCast = false;
                        }
                        viewingCast = !viewingCast;
                    }
                }
                if (turnOver)
                {
                    cooldownAbilities(mapLevel);
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
                    List<IntVec> gridSquares = new List<IntVec>();
                    for (int i = 0; i < abilities[ability].getCastingSquares().Length; i++)
                    {
                        if(abilities[ability].getCastingSquares()[i] != new IntVec(0,0))
                        {
                            gridSquares.Add(abilities[ability].getCastingSquares()[i]);
                        }
                    }
                    Engine.Engine.AddGridSelections(gridSquares.ToArray(), castingSquareSprite);
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
                if (abilities[ability].cooldown == 0 && !abilities[ability].wasJustCast)
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
                    int weaponRange1 = currentlyEquippedItems.getPrimaryWeaponRange()+rangeBoost;
                    int weaponRange2 = currentlyEquippedItems.getAuxilaryWeaponRange()+rangeBoost;
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
                                    testEnemy.DealElementalDamage(Element[i], 5, level);
                                    break;
                                case ElementAttributes.Ice:
                                    testEnemy.DealElementalDamage(Element[i], 3);
                                    break;
                                case ElementAttributes.Lighting:
                                    testEnemy.DealElementalDamage(Element[i], 1, (level));
                                    break;
                                case ElementAttributes.Arcane:
                                    testEnemy.DealElementalDamage(Element[i], 7, level);
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
                            if (weapon.IsLegendary)
                            {
                                if (name[0] == "Blade" || name[0] == "Executioner" || name[0] == "Bloodthirster" || name[0] == "Pale" || name[0] == "Anarchy" || name[0] == "Judgement")
                                {
                                    Engine.Engine.AddVisualAttack(enemy, "Hero/sword-slash", .25f, 2.0f, .15f);
                                    Audio.playSound("swordAttack");
                                }
                                else if (name[0] == "Kris")
                                {
                                    Engine.Engine.AddVisualAttack(enemy, "Hero/DaggerSlash", .25f, 2.0f, .15f);
                                    Audio.playSound("DaggerStab");
                                }
                                else if (name[0] == "The")
                                {
                                    Engine.Engine.AddVisualAttack(enemy, "Hero/ClawSlash", .25f, 2.0f, .15f);
                                    Audio.playSound("DaggerStab");
                                }
                                else if (name[0] == "40k")
                                {
                                    Audio.playSound("HammerSmash");
                                    Engine.Engine.AddVisualAttack(enemy, "Hero/hammerSmash", .25f, 2.0f, .15f);
                                }
                                else if (name[0] == "Condemned" || name[0] == "Retribution")
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, "Enemies/Attacks/Arrow", .25f, 1.0f, .15f);
                                }
                                else if (name[0] == "Kunai" || name[0] == "Heart")
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, weapon.GetTexture());
                                }
                                else
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, "Hero/MageAttack", .5f, 1.0f, .03f);
                                }
                            }
                            else
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
                                    Audio.playSound("DaggerStab");
                                }
                                else if (name[1] == "War")
                                {
                                    Audio.playSound("HammerSmash");
                                    Engine.Engine.AddVisualAttack(enemy, "Hero/hammerSmash", .25f, 2.0f, .15f);
                                }
                                else if (name[1] == "Crossbow" || name[1] == "Bow")
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, "Enemies/Attacks/Arrow", .25f, 1.0f, .15f);
                                }
                                else if (name[1] == "Kunai" || name[1] == "Chakram")
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, weapon.GetTexture());
                                }
                                else
                                {
                                    Engine.Engine.AddVisualAttack(this, enemy, "Hero/MageAttack", .5f, 1.0f, .03f);
                                }
                            }
                        }
                        for (int j = 0; j < abilities.Length; j++)
                        {
                            if (abilities[j] != null && abilities[j].type == AbilityTypes.Toggle)
                            {
                                ToggleAbility toggle = (ToggleAbility)abilities[j];
                                if (toggle.isActive)
                                {
                                    toggle.toggledAttackEffects(this);
                                }
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

        public void cooldownAbilities(Level mapLevel)
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
                    if (abilities[i].type == AbilityTypes.DOTAOE)
                    {
                        DOTAreaOfEffect dot = (DOTAreaOfEffect)abilities[i];
                        dot.dotUsed = false;
                    }
                    if (!abilities[i].wasJustCast)
                    {
                        abilities[i].cooldown -= (abilities[i].cooldown > 0) ? 1 : 0;
                    }
                    abilities[i].wasJustCast = false;
                }
            }
        }

        private void updateDOTabilities(Level mapLevel)
        {
            for(int i=0; i<abilities.Length; i++)
            {
                if (abilities[i] != null && abilities[i].type == AbilityTypes.DOTAOE && !abilities[i].wasJustCast)
                {
                    DOTAreaOfEffect dot = (DOTAreaOfEffect)abilities[i];
                    if (!dot.dotUsed)
                    {
                        dot.updateAOEPosition(mapLevel, mapLevel.CharacterEntities.FindPosition(this), this);
                        dot.dotUsed = true;
                    }
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
            if (inventory.stored[itemToEquip].item != null)
            {
                string[] name = inventory.stored[itemToEquip].item.Name.ToString().Split();
                if (inventory.stored[itemToEquip].item.ItemType == ITypes.Armor && !(name.Length == 3 && name[2] == "Shield"))
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
        }

        public void equipWeapon(int inventoryIndex)
        {
            if (inventory.stored[inventoryIndex].item != null)
            {
                string[] name = inventory.stored[inventoryIndex].item.Name.ToString().Split();
                if ((inventory.stored[inventoryIndex].item.ItemType == ITypes.Weapon || inventory.stored[inventoryIndex].item.ItemType == ITypes.Offhand) || (name.Length == 3 && (name[2] == "Shield" || name[2] == "Avenger")))
                {
                    Item newlyEquippedItem = inventory.stored[inventoryIndex].item;
                    if (currentlyEquippedItems.isWeaponEquipable((Gear)newlyEquippedItem, heroRole, level))
                    {
                        inventory.removeItem(inventoryIndex);
                        Gear item = (Gear)newlyEquippedItem;
                        if (item.EquipableIn.Contains(Slots.Hand_Both))
                        {
                            inventory.addItem(currentlyEquippedItems.removeWeapon(null, true, 0));
                            inventory.addItem(currentlyEquippedItems.removeWeapon(null, true, 1));
                        }
                        else
                        {
                            inventory.addItem(currentlyEquippedItems.removeWeapon((Gear)newlyEquippedItem, canDuelWield));
                        }
                        currentlyEquippedItems.equipWeapon((Gear)newlyEquippedItem, this, canDuelWield);
                    }
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

        public int getHeroDamage()
        {
            return currentlyEquippedItems.getTotalDamageIncrease() + damageBoost;
        }

        public void pickupItem(Item item, Level mapLevel)
        {
            if (item != null && !inventory.inventoryMaxed())
            {
                inventory.addItem(item.PickUpEffect());
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
