using Brogue;
using Brogue.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.EnviromentObjects.Interactive;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Brogue.Items.Equipment.Weapon;
using Brogue.Items.Equipment;


namespace Brogue.Engine
{
    [Serializable]
    class SaveGameData
    {
        public HeroClasses.Hero character;
        public int seed;
        public int saveSlot;
        public int levelComplexity;
        public int dungeonLevel;
    }

    class XPParticle
    {
        public Vector2 screenPosition;
        float speed;
        public float scale = 0;
        Vector2 direction;
        float distance;
        public XPParticle(Vector2 screenPosition, float speed)
        {
            this.screenPosition = screenPosition;
            this.speed = speed;
            this.direction = new Vector2(Engine.xpBarPosition.X + Engine.enginerand.Next(275), Engine.xpBarPosition.Y + 20) - screenPosition;
            distance = direction.Length();
            this.direction.Normalize();
        }
        public bool update()
        {
            screenPosition += direction * speed;
            bool finished = false;
            if (scale < .50f)
            {
                scale += 0.02f;
            }

            distance -= speed;
            if (distance < 15)
            {
                finished = true;
            }
            return finished;
        }
    }

    class VisualAttack
    {
        public Vector2 screenPosition;
        float speed;
        public float scale = 0;
        public float endScale;
        public float scaleRate;
        public float angle;
        public DynamicTexture tex;
        Vector2 direction;
        float distance;

        public VisualAttack(Vector2 screenPosition, Vector2 destination, float speed, string weaponSprite = "attackDefault")
        {
            this.screenPosition = screenPosition;
            this.speed = speed; 
            this.scaleRate = 0;
            this.endScale = 1;

            tex = Engine.GetTexture(weaponSprite);
            this.direction = new Vector2(destination.X, destination.Y) - screenPosition;
            distance = direction.Length();
            angle = (float)Math.Atan2(direction.X, -direction.Y);
            this.direction.Normalize();
        }

        public VisualAttack(Vector2 screenPosition, Vector2 destination, float speed, DynamicTexture weaponDynTex, float startScale, float endScale, float scaleAmount)
        {
            
            this.screenPosition = screenPosition;
            this.speed = speed;
            this.scale = startScale;
            this.endScale = endScale;
            this.scaleRate = scaleAmount;
            tex = weaponDynTex;
            this.direction = new Vector2(destination.X, destination.Y) - screenPosition;
            distance = direction.Length();
            angle = (float)Math.Atan2(direction.X, -direction.Y);
            this.direction.Normalize();
        }

        public VisualAttack(Vector2 screenPosition, Vector2 destination, float speed, string weaponDynTex, float startScale, float endScale, float scaleAmount)
        {
            this.screenPosition = screenPosition;
            this.speed = speed;
            this.scale = startScale;
            this.endScale = endScale;
            this.scaleRate = scaleAmount;
            tex = Engine.GetTexture(weaponDynTex);
            this.direction = new Vector2(destination.X, destination.Y) - screenPosition;
            distance = direction.Length();
            angle = (float)Math.Atan2(direction.X, -direction.Y);
            this.direction.Normalize();
        }

        public VisualAttack(Vector2 screenPosition, string weaponDynTex, float startScale, float endScale, float scaleAmount)
        {
            this.screenPosition = screenPosition;
            this.scale = startScale;
            this.endScale = endScale;
            this.scaleRate = scaleAmount;
            tex = Engine.GetTexture(weaponDynTex);
        }

        public bool update()
        {
            screenPosition += direction * speed;
            bool finished = false;
            if (scale < endScale)
            {
                scale += scaleRate;
            }
            else
            {
                scale = endScale;
                finished = (speed != 5);
            }

            distance -= speed;
            if (distance < -speed)
            {
                finished = true;
            }
            return finished;
        }
    }

    class GridSelection
    {
        public IntVec gridPos;
        public DynamicTexture tex;
        public GridSelection(IntVec iv, DynamicTexture t)
        {
            gridPos = iv;
            tex = t;
        }
    }

    class UIButton
    {
        static DynamicTexture standard = Engine.GetTexture("UI/InvSlot");
        static DynamicTexture highlighted = Engine.GetTexture("UI/InvSlotHighlighted");
        Texture2D currentBackTex;
        public string toolTip = "";
        public DynamicTexture drawOver;
        public bool doDrawOver;
        public bool doToolTip = false;
        string caption;
        Vector2 pos;
        public UIButton(Vector2 position, bool centered, string drawOverTexture, string caption)
        {
            pos = position;
            if (centered)
            {
                pos.X -= Engine.CELLWIDTH / 2;//standard.texture.Width / 2;
                pos.Y -= Engine.CELLWIDTH / 2;//standard.texture.Height / 2;
            }
            if (drawOverTexture != null)
            {
                drawOver = Engine.GetTexture(drawOverTexture);
                doDrawOver = true;
            }
            this.caption = caption;
            currentBackTex = standard.texture;
        }

        public bool isMouseOver()
        {
            bool isMouseOver = false;
            if (currentBackTex != null)
            {
                IntVec mpos = MouseController.MouseScreenPosition();
                isMouseOver = mpos.X > pos.X && mpos.X < pos.X + currentBackTex.Width &&
                    mpos.Y > pos.Y && mpos.Y < pos.Y + currentBackTex.Height;
                if (isMouseOver)
                {
                    currentBackTex = highlighted.texture;
                    doToolTip = true;
                }
                else
                {
                    currentBackTex = standard.texture;
                    doToolTip = false;
                }
            }

            return isMouseOver;
        }

        public bool isClicked()
        {
            return isMouseOver() && MouseController.LeftClicked();
        }

        public void Draw(SpriteBatch sb)
        {
            if (currentBackTex != null)
            {
                sb.Draw(currentBackTex, pos, Color.White);
                sb.Draw(drawOver.texture, pos, Color.White);
                sb.DrawString(Engine.font, caption,
                    new Vector2(pos.X + currentBackTex.Width / 2 - Engine.font.MeasureString(caption).X / 2, pos.Y - 20), Color.Red);
               
            }
        }
    }

    class AbilityButton
    {
        static DynamicTexture currentBackTex = Engine.GetTexture("UI/InvSlot");
        Abilities.Ability ability;
        public DynamicTexture drawOver;
        static DynamicTexture grayFade = Engine.GetTexture("UI/AbilityGray");
        int number;
        public bool doDrawOver;
        public bool doToolTip = false;
        string caption;
        Vector2 pos;
        public AbilityButton(Vector2 position, int number, bool centered, Abilities.Ability ab)
        {
            this.number = number;
            ability = ab;
            pos = position;
            if (centered)
            {
                pos.X -= Engine.CELLWIDTH / 2;//standard.texture.Width / 2;
                pos.Y -= Engine.CELLWIDTH / 2;//standard.texture.Height / 2;
            }
            this.caption = ab != null? ab.name : "";
        }

        public bool isMouseOver()
        {
            bool isMouseOver = false;
            if (currentBackTex != null)
            {
                IntVec mpos = MouseController.MouseScreenPosition();
                isMouseOver = mpos.X > pos.X && mpos.X < pos.X + currentBackTex.texture.Width &&
                    mpos.Y > pos.Y && mpos.Y < pos.Y + currentBackTex.texture.Height;
                doToolTip = isMouseOver;
            }

            return isMouseOver;
        }

        public void update()
        {
            if (currentBackTex != null)
            {
                IntVec mpos = MouseController.MouseScreenPosition();
                bool isMouseOver = mpos.X > pos.X && mpos.X < pos.X + currentBackTex.texture.Width &&
                    mpos.Y > pos.Y && mpos.Y < pos.Y + currentBackTex.texture.Height;
                doToolTip = isMouseOver;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(currentBackTex.texture, pos, Color.White);
            if (ability != null)
            {
                sb.Draw(ability.getSprite().Texture.texture, pos, new Rectangle(ability.getSprite().SourceTile.X * Engine.CELLWIDTH, 0, Engine.CELLWIDTH, Engine.CELLWIDTH), Color.White);
                sb.DrawString(Engine.font, "" + number, new Vector2(pos.X + currentBackTex.texture.Width / 2 - Engine.font.MeasureString("" + number).X / 2, pos.Y - 20), Color.Red);
                if (ability.getCooldown() > 0)
                {
                    sb.Draw(grayFade.texture, pos, Color.White);
                    sb.DrawString(Engine.font, "" + ability.getCooldown(), pos + new Vector2(-40, +(currentBackTex.texture.Width / 2 - Engine.font.MeasureString("" + ability.getCooldown()).X / 2)), Color.White);
                }
                if (doToolTip)
                {
                    AbilityToolTip.Draw(sb, pos+ new Vector2(-200, -200), ability.name, ability.description);
                }
            }
        }
    }

    class CharButton
    {
        public DynamicTexture standard;
        public DynamicTexture highlighted;
        Texture2D currentBackTex;
        Vector2 pos;
        public CharButton(Vector2 position, bool centered, string standardS, string highS)
        {
            standard = Engine.GetTexture(standardS);
            highlighted = Engine.GetTexture(highS);
            pos = position;
            if (centered)
            {
                pos.X -= standard.texture.Width / 2;//standard.texture.Width / 2;
                pos.Y -= standard.texture.Height / 2;//standard.texture.Height / 2;
            }
            
            currentBackTex = standard.texture;
        }
        public bool isMouseOver()
        {
            bool isMouseOver = false;
            if (currentBackTex != null)
            {
                IntVec mpos = MouseController.MouseScreenPosition();
                isMouseOver = mpos.X > pos.X && mpos.X < pos.X + currentBackTex.Width &&
                    mpos.Y > pos.Y && mpos.Y < pos.Y + currentBackTex.Height;
                if (isMouseOver)
                {
                    currentBackTex = highlighted.texture;
                   
                }
                else
                {
                    currentBackTex = standard.texture;
                    
                }
            }

            return isMouseOver;
        }

        public bool isClicked()
        {
            return isMouseOver() && MouseController.LeftClicked();
        }

        public void Draw(SpriteBatch sb)
        {
            if (currentBackTex != null)
            {
                sb.Draw(currentBackTex, pos, Color.White);
                
            }
        }
    }

    class InventoryButton
    {
        static DynamicTexture standard = Engine.GetTexture("UI/InvSlot");
        static DynamicTexture highlighted = Engine.GetTexture("UI/InvSlotHighlighted");
        Texture2D currentBackTex;
        public Items.Item currentItem;
        public bool doDrawOver;
        bool doToolTip = false;
        string caption;
        Vector2 pos;
        Vector2 toolTipPos;
        public InventoryButton(Vector2 position, bool centered, Vector2 toolTipPos)
        {
            pos = position;
            this.toolTipPos = toolTipPos;
            if (centered)
            {
                pos.X -= Engine.CELLWIDTH / 2;//standard.texture.Width / 2;
                pos.Y -= Engine.CELLWIDTH / 2;//standard.texture.Height / 2;
            }
            currentItem = null;
            doDrawOver = true;
            currentBackTex = standard.texture;
        }
        public bool isMouseOver()
        {
            bool isMouseOver = false;
            if (currentBackTex != null)
            {
                IntVec mpos = MouseController.MouseScreenPosition();
                isMouseOver = mpos.X > pos.X && mpos.X < pos.X + currentBackTex.Width &&
                    mpos.Y > pos.Y && mpos.Y < pos.Y + currentBackTex.Height;
                if (isMouseOver)
                {
                    currentBackTex = highlighted.texture;
                    doToolTip = true;
                }
                else
                {
                    currentBackTex = standard.texture;
                    doToolTip = false;
                }
            }

            return isMouseOver;
        }

        public bool isClicked()
        {
            return isMouseOver() && MouseController.LeftClicked();
        }
        public bool isRightClicked()
        {
            return isMouseOver() && MouseController.RightClicked();
        }

        public void Draw(SpriteBatch sb, HeroClasses.Hero hero)
        {
            if (currentBackTex != null)
            {
                sb.Draw(currentBackTex, pos, Color.White);

                
                if (currentItem != null)
                {
                    sb.Draw(currentItem.GetTexture().texture, pos, Color.White);
                }
                
                if (doToolTip && currentItem != null)
                {
                    /*Old tool tip drawing
                    Vector2 toolTipMeasure = Engine.font.MeasureString(currentItem.Name);
                    sb.DrawString(Engine.font, currentItem.Name, toolTipPos, Color.DarkRed);
                    if (currentItem.ItemType == Enums.ITypes.Legendary)
                    {
                        Items.Equipment.Weapon.Legendary.LegendaryWeapon lweap = (Items.Equipment.Weapon.Legendary.LegendaryWeapon)currentItem;
                        sb.DrawString(Engine.font, lweap.FlavorText, toolTipPos + new Vector2(10, 15), Color.DarkRed);
                    }
                    */
                    ToolTip.Draw(sb, (Items.Equipment.Gear)currentItem, toolTipPos, hero);
                }
            }
        }
    }

    class ToolTip
    {
        static Vector2 
            namePosition = new Vector2(10, 10),
            mainStatPosition = new Vector2(10, 30),
            requiredLevelPosition = new Vector2(10, 50),
            classesPosition = new Vector2(10, 70),
            flavPosition = new Vector2(10, 90)
            ;

        static DynamicTexture background = Engine.GetTexture("UI/ToolTipBack");
        public static void Draw(SpriteBatch sb, Items.Equipment.Gear gear, Vector2 position, HeroClasses.Hero hero)
        {
            sb.Draw(background.texture, position, Color.White);
            //sb.Draw(background.texture, position, new Rectangle(0, 0, background.texture.Width, background.texture.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            DrawOutlined(sb, position + namePosition, gear.Name, Color.Black, Color.White);
            
            string mstext = "";
            Color front = Color.White;
            switch (gear.ItemType){
                case Enums.ITypes.Armor:
                    int av  = ((Items.Equipment.Armor.Armor)gear).ArmorValue;
                    mstext = "Armor Value : " + av;
                    front = GetArmorColor((Items.Equipment.Armor.Armor)gear, hero);
                    break;
                case Enums.ITypes.Accessory:
                    int iv = ((Items.Equipment.Accessory.Accessory)gear).BaseIncrease;
                    string statI = ((Items.Equipment.Accessory.Accessory)gear).StatIncreased[0].ToString();
                    mstext = statI + " + " + iv;
                    front = GetAccessoryColor(((Items.Equipment.Accessory.Accessory)gear), hero);
                    break;
                case Enums.ITypes.Weapon:
                    mstext = "Damage : " + ((Items.Equipment.Weapon.Weapon)gear).Damage;
                    front = GetWeaponColor((Items.Equipment.Weapon.Weapon)gear, hero);
                    break;
                case Enums.ITypes.Offhand:
                    mstext = ((Items.Equipment.Offhand.Offhand)gear).Element[0].ToString();
                    for (int i = 1; i < ((Items.Equipment.Offhand.Offhand)gear).Element.Count; i++)
                    {
                        mstext += "," + ((Items.Equipment.Offhand.Offhand)gear).Element[i].ToString();
                    }
                    break;
            }
            DrawOutlined(sb, position + mainStatPosition, mstext, Color.Black, front);

            Color levelColor = hero.level >= gear.LevelReq ? Color.Green : Color.Red;
            DrawOutlined(sb, position + requiredLevelPosition, "Level required : " + gear.LevelReq, Color.Black, levelColor);

            Color classColor = gear.UsedBy.Contains(HeroClasses.Hero.heroRole) ? Color.Green : Color.Red;
            string classString = gear.UsedBy.Contains(HeroClasses.Hero.heroRole) ? "Your class can use this!" : "Your class can't use this.";
            DrawOutlined(sb, position + classesPosition, classString, Color.Black, classColor);

            if (gear.IsLegendary)
            {
                switch(gear.ItemType)
                {
                    case Enums.ITypes.Armor:
                        DrawOutlined(sb, position + flavPosition, "\"" + ((Items.Equipment.Armor.Legendary.LegendaryArmor)gear).FlavorText + "\"", Color.Black, Color.RosyBrown);
                        break;
                    case Enums.ITypes.Accessory:
                        DrawOutlined(sb, position + flavPosition, "\"" + ((Items.Equipment.Accessory.Legendary.LegendaryAccessory)gear).FlavorText + "\"", Color.Black, Color.RosyBrown);
                        break;
                    case Enums.ITypes.Weapon:
                        DrawOutlined(sb, position + flavPosition, "\"" + ((Items.Equipment.Weapon.Legendary.LegendaryWeapon)gear).FlavorText + "\"", Color.Black, Color.RosyBrown);
                        break;
                    case Enums.ITypes.Offhand:
                        DrawOutlined(sb, position + flavPosition, "\"" + ((Items.Equipment.Offhand.Legendary.LegendaryOffhand)gear).FlavorText + "\"", Color.Black, Color.RosyBrown);
                        break;
                
                }
                
            }
        }

        public static Color GetArmorColor(Items.Equipment.Armor.Armor armor, HeroClasses.Hero hero)
        {
            Color aColor = Color.White;

            switch (armor.EquipableIn[0])
            {
                case Enums.Slots.Head:
                    if (hero.GetEquipment().helmet != null)
                    {
                        aColor = hero.GetEquipment().helmet.ArmorValue < armor.ArmorValue ?
                            Color.Green :
                            hero.GetEquipment().helmet.ArmorValue == armor.ArmorValue ?
                            Color.Gray :
                            Color.Red;
                    }
                    break;
                case Enums.Slots.Legs:
                    if (hero.GetEquipment().grieves != null)
                    {
                        aColor = hero.GetEquipment().grieves.ArmorValue < armor.ArmorValue ?
                            Color.Green :
                            hero.GetEquipment().grieves.ArmorValue == armor.ArmorValue ?
                            Color.Gray :
                            Color.Red;
                    }
                    break;
                case Enums.Slots.Chest:
                    if (hero.GetEquipment().chestPlate != null)
                    {
                    aColor = hero.GetEquipment().chestPlate.ArmorValue < armor.ArmorValue ?
                        Color.Green :
                        hero.GetEquipment().chestPlate.ArmorValue == armor.ArmorValue ?
                        Color.Gray :
                        Color.Red;
                    }
                    break;
            }
            return aColor;
        }
        public static Color GetAccessoryColor(Items.Equipment.Accessory.Accessory acc, HeroClasses.Hero hero)
        {
            Color aColor = Color.White;

            switch (acc.EquipableIn[0])
            {
                case Enums.Slots.Neck:
                    if (hero.GetEquipment().necklace != null)
                    {
                        aColor = hero.GetEquipment().necklace.BaseIncrease < acc.BaseIncrease ?
                            Color.Green :
                            hero.GetEquipment().necklace.BaseIncrease == acc.BaseIncrease ?
                            Color.Gray :
                            Color.Red;
                    }
                    break;
                case Enums.Slots.Finger_One:
                    aColor = Color.Gray;
                    break;
            }
            return aColor;
        }

        public static Color GetWeaponColor(Items.Equipment.Weapon.Weapon wep, HeroClasses.Hero hero)
        {
            Color aColor = hero.GetEquipment().getPrimaryWeaponDamage() < wep.Damage ?
                        Color.Green :
                        hero.GetEquipment().getPrimaryWeaponDamage() == wep.Damage ?
                        Color.Gray :
                        Color.Red;
                   
            return aColor;
        }


        public static void DrawOutlined(SpriteBatch sb, Vector2 pos, string text, Color back, Color front)
        {
            foreach (Direction dir in Direction.Values)
            {
                sb.DrawString(Engine.font, text, pos + new Vector2(dir.X, dir.Y), back);
            }
            sb.DrawString(Engine.font, text, pos, front);
        }
    }

    class AbilityToolTip
    {
        static Vector2
            namePosition = new Vector2(10, 10),
            desPosition = new Vector2(10, 30)
            
            ;

        static DynamicTexture background = Engine.GetTexture("UI/ToolTipBack");
        public static void Draw(SpriteBatch sb, Vector2 position, string name, string description)
        {
            sb.Draw(background.texture, position, Color.White);
            //sb.Draw(background.texture, position, new Rectangle(0, 0, background.texture.Width, background.texture.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            DrawOutlined(sb, position + namePosition, name, Color.Black, Color.White);
            DrawOutlined(sb, position + desPosition, description, Color.Black, Color.White);
        }

        public static void DrawOutlined(SpriteBatch sb, Vector2 pos, string text, Color back, Color front)
        {
            foreach (Direction dir in Direction.Values)
            {
                sb.DrawString(Engine.font, text, pos + new Vector2(dir.X, dir.Y), back);
            }
            sb.DrawString(Engine.font, text, pos, front);
        }
    }

    partial class Engine
    {
        public const bool DOLIGHTING = true;
        public const bool DOAUDIO = false;
        public const bool DOSTARTMENU = true;
        public static bool DOLOG = false;
        public const float sightDistance = 1;
        public static bool inventoryOpen = false;
        public static bool mainMenuOpen = true;
        public static bool savePromptOpen = false;
        public static bool showDeathScreen = false;
        public static int CELLWIDTH = 48;
        private static int logSize = 20;
        private static Game1 game;
        private static bool gameStarted = false;
        public static IntVec cameraPosition = new IntVec(12, 8);
        private static Queue<String> log = new Queue<string>(10);
        private static Vector2 LogPosition, InvButtonPosition, InventoryPosition, InventorySize;
        private static HeroClasses.Hero hero;

        private static AbilityButton[] abilityBar = new AbilityButton[6];
        private static Vector2 abilityPosition;

        private static RenderTarget2D lightsTarget;
        private static RenderTarget2D mainTarget;
        private const int AIDist = 15;
        public static Random enginerand = new Random();
        private static List<XPParticle> xpList = new List<XPParticle>();
        private static List<VisualAttack> vattacks = new List<VisualAttack>();
        public static Matrix worldToView;
        public static Vector2 xpBarPosition;
        public static Vector2 healthBarPosition;
        public static Vector2 weaponEquipPosition;
        public static Vector2 armorEquipPosition;
        public static IntVec windowSizeInTiles;
        public static int lightMaskWidthInTilesDividedByTwo;
        private static IntVec modifiedCameraPosition = new IntVec(0, 0);
        private static GeneratedLevel nextLevel;

        private static DynamicTexture visualAttackTex = GetTexture("attackDefault");

        private static InventoryButton[] inventoryButtons = new InventoryButton[16];
        private static UIButton headSlot, chestSlot, ringSlot1, ringSlot2, legSlot, neckSlot;
        private static UIButton weaponSlot1, weaponSlot2;

        private static int levelSeed;
        private static int currentSaveSlot = -1;
        private static int levelComplexity;
        private static int currentDungeonLevel = 1;


        
        ///////////////////////////////////////////
        //public static AuxerTestingZone auxerTests;
        ///////////////////////////////////////////

        private static bool showSaveSlotSelection;


        private static DynamicTexture
            lightMask = GetTexture("lightmask")
        , sightMask = GetTexture("lightmask")
        , particleTex = GetTexture("UI/exp")
        , gridSelectionOverlay
        ;

        static DynamicTexture
            jar = GetTexture("UI/Jar"),
            bar = GetTexture("UI/Bar"), 
            healthcontainer = GetTexture("UI/HealthJar"),
            healthbar = GetTexture("UI/HealthBar"),
            healthBack = GetTexture("UI/HealthJarBack"),
            xpbar = GetTexture("UI/XPBar"),
            invSlot = GetTexture("UI/InvSlot"),
            invHighlightSlot = GetTexture("UI/InvSlotHighlighted"),
            invButton = GetTexture("UI/InventoryIcon"),
            deathNote = GetTexture("UI/DeathNote"),
            obscure = GetTexture("UI/Obscure"),
            newGame = GetTexture("UI/NewGame"),
            loadGame = GetTexture("UI/Load"),
            selectSave = GetTexture("UI/SelectSave");

        const int SAVE_SLOTS = 8;
        static CharButton mageButton, warriorButton, rogueButton;
        static UIButton saveButton, continueButton, quitButton;
        static UIButton restartButton, quitDeathButton;
        static UIButton[] saveSlots = new UIButton[SAVE_SLOTS];
        public static SpriteFont font;
        static List<GridSelection> gridSelection = new List<GridSelection>();

        public static DynamicTexture placeHolder = GetTexture("placeholder");

        public static Level currentLevel;

        public static void AddXP(int xp, IntVec gameGrid)
        {
            IntVec worldPosition = gameGrid * CELLWIDTH;
            Vector2 worldVector = Vector2.Transform(new Vector2(worldPosition.X, worldPosition.Y), worldToView);

            for (int i = 0; i < xp; i++)
            {
                XPParticle newxp = new XPParticle(
                    new Vector2(worldVector.X + enginerand.Next(CELLWIDTH) - CELLWIDTH/2, 
                        worldVector.Y + enginerand.Next(CELLWIDTH) - CELLWIDTH/2), enginerand.Next(15) + 10);
                xpList.Add(newxp);
            }
        }

        public static void AddVisualAttack(GameCharacter origin, GameCharacter target, string attackSprite)
        {
            IntVec gamePositionOrigin = currentLevel.CharacterEntities.FindPosition(origin) * CELLWIDTH;
            Vector2 originVector = Vector2.Transform(new Vector2(gamePositionOrigin.X, gamePositionOrigin.Y), worldToView);
            IntVec gamePositionDest = currentLevel.CharacterEntities.FindPosition(target) * CELLWIDTH;
            Vector2 destVector = Vector2.Transform(new Vector2(gamePositionDest.X, gamePositionDest.Y), worldToView);
            vattacks.Add(new VisualAttack(originVector, destVector, 5, attackSprite));
        }

        public static void AddVisualAttack(GameCharacter origin, GameCharacter target, DynamicTexture attackSprite, float startScale = 1, float endScale = 1, float scaleAmount = 0.05f)
        {
            IntVec gamePositionOrigin = currentLevel.CharacterEntities.FindPosition(origin) * CELLWIDTH;
            Vector2 originVector = Vector2.Transform(new Vector2(gamePositionOrigin.X, gamePositionOrigin.Y), worldToView);
            IntVec gamePositionDest = currentLevel.CharacterEntities.FindPosition(target) * CELLWIDTH;
            Vector2 destVector = Vector2.Transform(new Vector2(gamePositionDest.X, gamePositionDest.Y), worldToView);
            if (attackSprite != null)
            {
                vattacks.Add(new VisualAttack(originVector, destVector, 5, attackSprite, startScale, endScale, scaleAmount));
            }
            else
            {
                vattacks.Add(new VisualAttack(originVector, destVector, 5, "attackSprite"));
            }
        }

        public static void AddVisualAttack(GameCharacter origin, GameCharacter target, string attackSprite, float startScale = 1, float endScale = 1, float scaleAmount = 0.05f)
        {
            IntVec gamePositionOrigin = currentLevel.CharacterEntities.FindPosition(origin) * CELLWIDTH;
            Vector2 originVector = Vector2.Transform(new Vector2(gamePositionOrigin.X, gamePositionOrigin.Y), worldToView);
            IntVec gamePositionDest = currentLevel.CharacterEntities.FindPosition(target) * CELLWIDTH;
            Vector2 destVector = Vector2.Transform(new Vector2(gamePositionDest.X, gamePositionDest.Y), worldToView);
            if (attackSprite != null)
            {
                vattacks.Add(new VisualAttack(originVector, destVector, 5, attackSprite, startScale, endScale, scaleAmount));
            }
            else
            {
                vattacks.Add(new VisualAttack(originVector, destVector, 5, "attackSprite"));
            }
        }

        public static void AddVisualAttack(GameCharacter origin, string attackSprite, float startScale = 1, float endScale = 1, float scaleAmount = 0.05f)
        {
            IntVec gamePositionOrigin = currentLevel.CharacterEntities.FindPosition(origin) * CELLWIDTH;
            Vector2 originVector = Vector2.Transform(new Vector2(gamePositionOrigin.X, gamePositionOrigin.Y), worldToView);
            if (attackSprite != null)
            {
                vattacks.Add(new VisualAttack(originVector, attackSprite, startScale, endScale, scaleAmount));
            }
            else
            {
            }
        }

        public static void NextLevel()
        {
            GoToNextLevel();
        }

        public static void SaveGame(string filename)
        {
            SaveGameData sg = new SaveGameData();
            sg.character = hero;
            sg.seed = enginerand.Next();
            sg.levelComplexity = enginerand.Next(500) + 50;
            sg.saveSlot = currentSaveSlot;
            sg.dungeonLevel = currentDungeonLevel + 1;
            
            //Write to binary file...
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, sg);
            stream.Close();
        }

        public static void LoadGame(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            SaveGameData gd = (SaveGameData)formatter.Deserialize(stream);
            stream.Close();
            File.Delete(filename);
            currentSaveSlot = gd.saveSlot;
            hero = gd.character;
            GeneratedLevel nlevel = new GeneratedLevel(gd.seed, gd.levelComplexity, gd.dungeonLevel);
            currentLevel = nlevel.RetrieveLevel();
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
            showSaveSlotSelection = false;
            mainMenuOpen = false;
            gameStarted = true;
        }

        public static void ClearGridSelections()
        {
            gridSelection.Clear();
        }

        public static void AddGridSelections(IntVec[] gridSpaces, DynamicTexture tex)
        {
            foreach (IntVec iv in gridSpaces){
                gridSelection.Add(new GridSelection(iv, tex));
            }
        }

        public static bool IsMouseAdjacentToHero()
        {
            bool result = false;
            IntVec hpos = currentLevel.CharacterEntities.FindPosition(hero);
            IntVec mpos = MouseController.MouseGridPosition();

            result = (Math.Abs(hpos.X - mpos.X) == 1 && hpos.Y - mpos.Y == 0)
                || (Math.Abs(hpos.Y - mpos.Y) == 1 && hpos.X - mpos.X == 0);

            return result;

        }

        public static void Start(Game1 injectedGame)
        {
            game = injectedGame;
            hero = new HeroClasses.Warrior();
            GenerateLevel();

            windowSizeInTiles = new IntVec(game.Width / CELLWIDTH, game.Height / CELLWIDTH);
            cameraPosition = currentLevel.CharacterEntities.FindPosition(hero);
            modifiedCameraPosition.X = cameraPosition.X - (windowSizeInTiles.X / 2);
            modifiedCameraPosition.Y = cameraPosition.Y - (windowSizeInTiles.Y / 2);
            
            LogPosition = new Vector2(12, 12);
            // = new Vector2(game.Width - 48, game.Height - 48);
            InventoryPosition = new Vector2(game.Width - 5 * (CELLWIDTH), game.Height - 4 * (CELLWIDTH));
            InvButtonPosition = new Vector2(game.Width - CELLWIDTH, game.Height - CELLWIDTH);
            abilityPosition = new Vector2(game.Width / 2 - (3 * CELLWIDTH), game.Height - CELLWIDTH);
            InventorySize = new Vector2(4 * CELLWIDTH, 4 * CELLWIDTH);
            weaponEquipPosition = new Vector2(0, game.Height - 2 * CELLWIDTH - 20);
            armorEquipPosition = new Vector2(0, game.Height - CELLWIDTH);
            game.IsMouseVisible = true;

            LoadMainMenu();

            saveButton = new UIButton(new Vector2(game.Width / 2 - 150, game.Height / 2), true, "UI/Save", "Save and quit");
            continueButton = new UIButton(new Vector2(game.Width / 2 + 150, game.Height / 2), true, "UI/Continue", "Continue");

            headSlot = new UIButton(armorEquipPosition, false, "UI/HelmOverlay", "Helm");
            chestSlot = new UIButton(new Vector2(armorEquipPosition.X + CELLWIDTH, armorEquipPosition.Y),
                false, "UI/ChestOverlay", "Chest");
            legSlot = new UIButton(new Vector2(armorEquipPosition.X + 2 * (CELLWIDTH), armorEquipPosition.Y),
                false, "UI/LegsOverlay", "Legs");
            ringSlot1 = new UIButton(new Vector2(armorEquipPosition.X + 3 * (CELLWIDTH), armorEquipPosition.Y),
                false, "UI/RingOverlay", "Ring");
            ringSlot2 = new UIButton(new Vector2(armorEquipPosition.X + 4 * (CELLWIDTH), armorEquipPosition.Y),
                false, "UI/RingOverlay", "Ring");
            neckSlot = new UIButton(new Vector2(armorEquipPosition.X + 5 * (CELLWIDTH), armorEquipPosition.Y),
                false, "UI/NeckOverlay", "Neck");

            weaponSlot1 = new UIButton(new Vector2(weaponEquipPosition.X, weaponEquipPosition.Y),
                false, "UI/InvSlot", "Left");
            weaponSlot2 = new UIButton(new Vector2(weaponEquipPosition.X + CELLWIDTH, weaponEquipPosition.Y),
                false, "UI/InvSlot", "Right");

            


            //StartGame();
        }

        public static void End()
        {

        }

        public static void UpdateAbilities()
        {
            Abilities.Ability[] heroAbs = hero.getAbilities();
            for (int i = 0; i < abilityBar.Length; i++)
            {
                if (heroAbs[i] != null)
                {
                    abilityBar[i] = new AbilityButton(abilityPosition + new Vector2(CELLWIDTH * i, 0), i + 1, false, heroAbs[i]);
                }
            }
        }

        public static void Log(string input)
        {
            log.Enqueue(input);
            if (log.Count > logSize)
            {
                log.Dequeue();
            }
        }

        public static void LoadMainMenu()
        {
            LoadContent(game.Content);
            if (DOSTARTMENU)
            {
                warriorButton = new CharButton(new Vector2(game.Width / 3, 2 * game.Height / 3), true, "UI/WarriorCharCreation", "UI/WarriorCharCreationHigh");
                mageButton = new CharButton(new Vector2(game.Width / 3 - 220, game.Height / 4), true, "UI/MageCharCreation", "UI/MageCharCreationHigh");
                rogueButton = new CharButton(new Vector2(game.Width / 3 + 220,game.Height / 4), true, "UI/RogueCharCreation", "UI/RogueCharCreationHigh");
                quitButton = new UIButton(new Vector2(game.Width - CELLWIDTH, 0), false, "UI/QuitButton", "");

                restartButton = new UIButton(new Vector2(game.Width / 2 - 40, game.Height / 2), true, "UI/RestartButton", "");
                quitDeathButton = new UIButton(new Vector2(game.Width / 2 + 40, game.Height / 2), true, "UI/QuitButton", "");
                mainMenuOpen = true;

                Audio.playMusic("Stoneworld Battle", 1.0f);

                LoadSaveSlots();

                for (int i = 0; i < 4; i ++)
                {
                    for (int j = 0; j < 4; j ++)
                    {
                        inventoryButtons[i + j  * 4] = new InventoryButton(InventoryPosition + new Vector2(CELLWIDTH * i, CELLWIDTH * j), false, InventoryPosition + new Vector2(-300, -200));
                    }
                }
            }
            else
            {
                hero = new HeroClasses.Warrior();
                currentSaveSlot = 1;
                StartGame();
            }
        }

        public static void LoadSaveSlots()
        {
            Vector2 postemp = new Vector2(5 * game.Width / 6, 60 + game.Height/ 2 - (((CELLWIDTH + 20) * SAVE_SLOTS) / 2));
            for (int i = 0; i < SAVE_SLOTS; i++)
            {
                if (File.Exists("saveSlot" + (i + 1) + ".bro"))
                {
                    saveSlots[i] = new UIButton(new Vector2(postemp.X, postemp.Y + (CELLWIDTH + 20) * i), false, "UI/FilledSaveSlot", "Slot " + (i + 1));
                    saveSlots[i].toolTip = "Selecting this save slot\nwill overwrite the previous save!";
                }
                else
                {
                    saveSlots[i] = new UIButton(new Vector2(postemp.X, postemp.Y + (CELLWIDTH + 20) * i), false, "UI/FreeSaveSlot", "Slot " + (i + 1));
                    saveSlots[i].toolTip = "Select a save slot to start a game.";
                }
            }
        }

        public static void GenerateLevel()
        {
            levelSeed = enginerand.Next();
            levelComplexity = enginerand.Next(250) + 50;
            currentDungeonLevel = 1;
            if (nextLevel == null)
            {
                nextLevel = new GeneratedLevel(levelSeed, levelComplexity, currentDungeonLevel);
            }
            currentLevel = nextLevel.RetrieveLevel();
            currentDungeonLevel++;
            nextLevel = new GeneratedLevel(levelSeed, levelComplexity, currentDungeonLevel++);
            Log("Level generated.");
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
        }

        public static void StartGame()
        {
            Log("Game started");
            gameStarted = true;
            mainMenuOpen = false;
            showSaveSlotSelection = false;
            GenerateLevel();
            Audio.playMusic("Brogue II", 1.0f);
        }

        public static void ContentLoaded(ContentManager content)
        {
            lightsTarget = new RenderTarget2D(game.GraphicsDevice, game.Width, game.Height);
            mainTarget = new RenderTarget2D(game.GraphicsDevice, game.Width, game.Height);
            lightMaskWidthInTilesDividedByTwo = lightMask.texture.Width / (2 * CELLWIDTH);
            xpBarPosition = new Vector2(4, 35);
            healthBarPosition = new Vector2(4, 4);
            font = content.Load<SpriteFont>("UI/Font");

            Audio.LoadContent(content);

            /////////////////////////////////////
            //auxerTests = new AuxerTestingZone();
            //auxerTests.runTests();
            //auxerTests.Exit();
            /////////////////////////////////////
        }

        private static void AITurn()
        {
            //Iterate through each AI within maximum AI distance and call its TakeTurn method.
        }

        public static void Draw(Sprite sprite, IntVec destination)
        {
            if (sprite.Texture != null && IsTileInView(destination) && sprite.IsVisible && sprite.Texture.texture != null)
            {
                game.spriteBatch.Draw(sprite.Texture.texture, new Rectangle(destination.X * CELLWIDTH, destination.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), new Rectangle(sprite.SourceTile.X * CELLWIDTH, sprite.SourceTile.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), sprite.Blend, sprite.Direction, new Vector2(CELLWIDTH / 2, CELLWIDTH / 2), SpriteEffects.None, 0);
            }
        }
        
        static int charIndex = 0;
        static IntVec heroPos;

        public static void Update(GameTime gameTime)
        {
            MouseController.Update();
            if (gameStarted)
            {
                if (heroPos == null)
                {
                    heroPos = currentLevel.CharacterEntities.FindPosition(hero);
                }
                
                for (int i = 0; i < xpList.Count; i++)
                {
                    if (xpList[i].update())
                    {
                        xpList.RemoveAt(i);
                        hero.AddExperience(1);
                    }
                }
                for (int i = 0; i < vattacks.Count; i++)
                {
                    if (vattacks[i].update())
                    {
                        vattacks.RemoveAt(i);
                    }
                }

                if (!GameCommands())
                {
                    currentLevel.testUpdate();
                    //Game turns

                    //hero.TakeTurn(currentLevel);
                    if (charIndex >= currentLevel.CharacterEntities.Entities().Count<GameCharacter>())
                    {
                        charIndex = 0;
                    }
                    while (currentLevel.CharacterEntities.Entities().ElementAt<GameCharacter>(charIndex) != hero)
                    {
                        if (charIndex < currentLevel.CharacterEntities.Entities().Count<GameCharacter>())
                        {
                            IntVec enemyPosition = currentLevel.CharacterEntities.FindPosition(currentLevel.CharacterEntities.Entities().ElementAt<GameCharacter>(charIndex));
                            if (enemyPosition.X > heroPos.X - AIDist &&
                                enemyPosition.X < heroPos.X + AIDist &&
                                enemyPosition.Y > heroPos.Y - AIDist &&
                                enemyPosition.Y < heroPos.Y + AIDist)
                            {
                                if (currentLevel.CharacterEntities.Entities().ElementAt<GameCharacter>(charIndex).TakeTurn(currentLevel))
                                {
                                    currentLevel.InvalidateCache();
                                    charIndex++;
                                    heroPos = currentLevel.CharacterEntities.FindPosition(hero);
                                }
                            }
                            else
                            {
                                charIndex++;
                            }
                        }
                        /*
                        if (charIndex >= currentLevel.CharacterEntities.Entities().Count<GameCharacter>())
                        {
                            charIndex = 0;
                        }
                         * */
                    }
                    charIndex += hero.TakeTurn(currentLevel) ? 1 : 0;
                    
                    
                    if (hero.hasReachedBranchLevel())
                    {
                        IntVec position = currentLevel.CharacterEntities.FindPosition(hero);
                        HeroClasses.Equipment tempEquip = hero.GetEquipment();
                        InventorySystem.Inventory tempInventory = hero.GetInventory();
                        currentLevel.CharacterEntities.Remove(hero);
                        int rand = enginerand.Next(2);
                        if (rand == 1)
                        {
                            if (HeroClasses.Hero.heroRole == Enums.Classes.Warrior)
                            {
                                hero = new HeroClasses.Brawler();
                                UpdateAbilities();
                                Log("You are now a brawler.");
                            }
                            else if (HeroClasses.Hero.heroRole == Enums.Classes.Mage)
                            {
                                hero = new HeroClasses.Magus();
                                UpdateAbilities();
                                Log("You are now a Magus.");
                            }
                            else if (HeroClasses.Hero.heroRole == Enums.Classes.Rogue)
                            {
                                hero = new HeroClasses.Duelist();
                                UpdateAbilities();
                                Log("You are now a Duelist.");
                            }
                        }
                        else
                        {
                            if (HeroClasses.Hero.heroRole == Enums.Classes.Warrior)
                            {
                                hero = new HeroClasses.Sentinel();
                                UpdateAbilities();
                                Log("You are now a sentinel.");
                            }
                            else if (HeroClasses.Hero.heroRole == Enums.Classes.Mage)
                            {
                                hero = new HeroClasses.Sorcerer();
                                UpdateAbilities();
                                Log("You are now a Sorcerer.");
                            }
                            else if (HeroClasses.Hero.heroRole == Enums.Classes.Rogue)
                            {
                                hero = new HeroClasses.Ranger();
                                UpdateAbilities();
                                Log("You are now a Ranger.");
                            }
                        }
                        hero.ObtainItems(tempInventory, tempEquip);
                        hero.obtainStartingGear(currentLevel);
                        currentLevel.CharacterEntities.Add(hero, position);
                    }

                    cameraPosition = currentLevel.CharacterEntities.FindPosition(hero);
                    modifiedCameraPosition.X = cameraPosition.X - (windowSizeInTiles.X / 2);
                    modifiedCameraPosition.Y = cameraPosition.Y - (windowSizeInTiles.Y / 2);
                    currentLevel.InvalidateCache();
                }
            }
            else
            {
                if (mainMenuOpen)
                {
                    if (mageButton.isClicked())
                    {
                        hero = new HeroClasses.Mage();
                        UpdateAbilities();
                        mainMenuOpen = false;
                        showSaveSlotSelection = true;
                    }
                    if (warriorButton.isClicked())
                    {
                        hero = new HeroClasses.Warrior();
                        UpdateAbilities();
                        mainMenuOpen = false;
                        showSaveSlotSelection = true;
                    }
                    if (rogueButton.isClicked())
                    {
                        hero = new HeroClasses.Rogue();
                        UpdateAbilities();
                        mainMenuOpen = false;
                        showSaveSlotSelection = true;
                    }
                    int saveSlot = saveSlotClicked();
                    if (saveSlot != -1)
                    {
                        LoadFromSlot(saveSlot);
                    }
                    if (quitButton.isClicked())
                    {
                        System.Environment.Exit(0);
                    }
                }

                else if (savePromptOpen)
                {
                    if (saveButton.isClicked())
                    {
                        SaveToSlot(currentSaveSlot);
                        System.Environment.Exit(0);
                    }
                    if (continueButton.isClicked())
                    {
                        savePromptOpen = false;
                        gameStarted = true;
                    }
                }
                else if (showSaveSlotSelection)
                {
                    int saveClicked = saveSlotClicked();
                    if (saveClicked != -1)
                    {
                        currentSaveSlot = saveClicked;
                        showSaveSlotSelection = false;
                        StartGame();
                    }

                }
                else if (showDeathScreen)
                {
                    if (quitDeathButton.isClicked())
                    {
                        Environment.Exit(0);
                    }
                    if (restartButton.isClicked())
                    {
                        showDeathScreen = false;
                        xpList.Clear();
                        LoadSaveSlots();
                        vattacks.Clear();
                        mainMenuOpen = true;
                    }
                }

            }

            if (gameStarted)
            {
                Audio.update();
            }

            if (hero != null && !hero.isAlive())
            {
                gameStarted = false;
                showDeathScreen = true;
            }
        }

        private static void LoadFromSlot(int slot)
        {
            string filename = "saveSlot" + slot + ".bro";
            if (File.Exists(filename))
            {
                LoadGame(filename);
            }
        }

        private static void SaveToSlot(int slot)
        {
            string filename = "saveSlot" + slot + ".bro";
            SaveGame(filename);
        }

        private static int saveSlotClicked()
        {
            
            int clicked = -1;
            for (int i = 0; i < SAVE_SLOTS && clicked == -1; i++)
            {
                if (saveSlots[i].isClicked())
                {
                    clicked = i + 1;
                }
            }
            return clicked;
        }

        private static IntVec[] GetEnemyPositions()
        {
            int count = currentLevel.CharacterEntities.Entities().Count<GameCharacter>();
            IntVec[] poses = new IntVec[count];
            for (int i = 0; i < count; i++)
            {
                poses[i] = currentLevel.CharacterEntities.FindPosition(currentLevel.CharacterEntities.Entities().ElementAt<GameCharacter>(i));
            }
            return poses;
        }

        private static bool GameCommands()
        {
            bool didSomething = false;

            if (KeyboardController.IsPressed(Keys.L))
            {
                DOLOG = !DOLOG;
            }
            if (KeyboardController.IsPressed(Keys.O))
            {
                //Go to next level hacked.
                GoToNextLevel();
            }

            if (KeyboardController.IsPressed(Keys.OemPlus))
            {
                logSize += 5;
                didSomething = true;
            }
            if (KeyboardController.IsPressed(Keys.OemMinus))
            {
                if (logSize > 5)
                {
                    logSize -= 5;
                    for (int i = 0; i < log.Count - logSize; i++)
                    {
                        log.Dequeue();
                    }
                }
                didSomething = true;
            }
            if (KeyboardController.IsPressed(Keys.Escape))
            {
                //Replace with menu eventually...

                if (inventoryOpen)
                {
                    inventoryOpen = false;
                    Log("Inventory closed.");
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }
            IntVec screenpos = MouseController.MouseScreenPosition();
            IntVec worldPos = MouseController.MouseGridPosition();

            didSomething = InventoryInteraction(true, screenpos);

            if (MouseController.LeftClicked())
            {
                if (screenpos.X >= InvButtonPosition.X && screenpos.X <= InvButtonPosition.X + invButton.texture.Width &&
                    screenpos.Y >= InvButtonPosition.Y && screenpos.Y <= InvButtonPosition.Y + invButton.texture.Height
                    )
                {
                    //Show inventory menu.
                    inventoryOpen = !inventoryOpen;
                    Log(inventoryOpen? "Inventory Opened." : "Inventory Closed.");
                    didSomething = true;
                }

            }
            if (MouseController.RightClicked())
            {
                if (!didSomething)
                {
                    didSomething = InventoryInteraction(false, screenpos);
                }

            }
            if (!didSomething)
            {
                if (headSlot.isClicked())
                {
                    if (hero.GetEquipment().helmet != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeArmor(hero.GetEquipment().helmet));
                        didSomething = true;
                    }
                }
                if (chestSlot.isClicked())
                {
                    if (hero.GetEquipment().chestPlate != null)
                    {
                        didSomething = true;
                        hero.GetInventory().addItem(hero.GetEquipment().removeArmor(hero.GetEquipment().chestPlate));
                    }
                }
                if (legSlot.isClicked())
                {
                    if (hero.GetEquipment().grieves != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeArmor(hero.GetEquipment().grieves));
                        didSomething = true;
                    }
                }
                if (ringSlot1.isClicked())
                {
                    if (hero.GetEquipment().rings[0] != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeAccessory(null, 0));
                        didSomething = true;
                    }
                }
                if (ringSlot2.isClicked())
                {
                    if (hero.GetEquipment().rings[1] != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeAccessory(null, 1));
                        didSomething = true;
                    }
                }
                if (neckSlot.isClicked())
                {
                    if (hero.GetEquipment().necklace != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeAccessory(hero.GetEquipment().necklace));
                        didSomething = true;
                    }
                }
                if (weaponSlot1.isClicked())
                {
                    if (hero.GetEquipment().equippedWeapons[0] != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeWeapon(null, hero.canDuelWieldWeapons(), 0));
                        didSomething = true;
                    }
                }
                if (weaponSlot2.isClicked())
                {
                    if (hero.GetEquipment().equippedWeapons[1] != null)
                    {
                        hero.GetInventory().addItem(hero.GetEquipment().removeWeapon(null, hero.canDuelWieldWeapons(), 1));
                        didSomething = true;
                    }
                }
                foreach (AbilityButton ab in abilityBar)
                {
                    if (ab != null)
                    {
                        ab.update();
                    }
                }
            }
            return didSomething;
        } 

        private static void GoToNextLevel()
        {
            //Generate next level
            GenerateLevel();
            //Prompt for save.

            gameStarted = false;
            savePromptOpen = true;
        }

        private static bool InventoryInteraction(bool leftButton, IntVec screenpos)
        {
            bool didsomething = false;
            if (inventoryOpen)
            {
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].currentItem = hero.GetInventory().GetItemAt(i);
                    if (inventoryButtons[i].isClicked())
                    {
                        if (inventoryButtons[i].currentItem != null)
                        {
                            hero.equipWeapon(i);
                            hero.equipArmor(i);
                            hero.equipAccessory(i);
                        }
                    }
                    if (inventoryButtons[i].isRightClicked())
                    {
                        hero.dropItem(i, currentLevel);
                    }
                }
            }
            return didsomething;

        }

        public static void DrawInventory(SpriteBatch sb)
        {
            //InventorySystem.Inventory inv = hero.GetInventory();
            if (inventoryOpen)
            {
                for (int i = 0; i < inventoryButtons.Length; i++)
                {
                    inventoryButtons[i].Draw(sb, hero);
                }
            }
        }

        public static void DrawLog(SpriteBatch spriteBatch)
        {
            if (DOLOG)
            {
                int inc = 0;
                foreach (string s in log)
                {
                    if (s != null)
                        spriteBatch.DrawString(font, s, new Vector2(LogPosition.X, LogPosition.Y + 12 * inc++), Color.Red);
                }
            }
        }
        
        public static void DrawGame(GameTime gameTime)
        {
            if (gameStarted || currentLevel != null)
            {
                worldToView = Matrix.CreateTranslation(-cameraPosition.X * CELLWIDTH + game.Width / 2, -cameraPosition.Y * CELLWIDTH + game.Height / 2, 1.0f)
                        * Matrix.CreateScale(1.0f, 1.0f, 1);

                //Draw lighting.
                DrawLighting(worldToView);

                //Draw level.
                game.GraphicsDevice.SetRenderTarget(mainTarget);
                //game.GraphicsDevice.Clear(Color.Black);
                game.spriteBatch.Begin(SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                            worldToView);
                currentLevel.render();
                foreach (GridSelection gs in gridSelection)
                {
                    game.spriteBatch.Draw(gs.tex.texture, new Rectangle(gs.gridPos.X * CELLWIDTH, gs.gridPos.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH),
                        new Rectangle(0, 0, CELLWIDTH, CELLWIDTH), Color.White, 0, new Vector2(CELLWIDTH / 2, CELLWIDTH / 2), SpriteEffects.None, 0);
                }
                game.spriteBatch.End();

                //Draw both to screen.
                game.GraphicsDevice.SetRenderTarget(null);
                //game.GraphicsDevice.Clear(Color.CornflowerBlue);
                game.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                game.effect.Parameters["lightMask"].SetValue(lightsTarget);
                game.effect.CurrentTechnique.Passes[0].Apply();

                game.spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                game.spriteBatch.End();
            }
        }

        public static void DrawUI(SpriteBatch uisb)
        {
            if (!gameStarted && currentLevel != null)
            {
                uisb.Draw(obscure.texture, new Vector2(0, 0), Color.White);
            }
            if (gameStarted)
            {
                foreach (XPParticle xp in xpList)
                {
                    uisb.Draw(particleTex.texture, xp.screenPosition, 
                        new Rectangle(0, 0, particleTex.texture.Width, particleTex.texture.Height), 
                        Color.White, 0, new Vector2(particleTex.texture.Width/2, particleTex.texture.Height/2), xp.scale, 
                        SpriteEffects.None, 0);
                    //uisb.Draw(particleTex.texture, xp.screenPosition, Color.White);
                }

                foreach (VisualAttack va in vattacks)
                {
                    uisb.Draw(va.tex.texture, va.screenPosition, 
                        new Rectangle(0, 0, va.tex.texture.Width, va.tex.texture.Height), 
                        Color.White, va.angle, 
                        new Vector2(va.tex.texture.Width / 2, va.tex.texture.Height / 2), 
                        va.scale, SpriteEffects.None, 0);
                }
                
                
                //uisb.Draw(healthbar.texture, new Vector2(50, game.Height / 2 - healthcontainer.texture.Height / 2), Color.White);

                uisb.Draw(healthBack.texture, healthBarPosition, Color.White);
                uisb.Draw(healthbar.texture, new Vector2(healthBarPosition.X + 12,
                    healthBarPosition.Y + 2),
                    new Rectangle(0, 0, healthbar.texture.Width, healthbar.texture.Height),
                    Color.White, 0,
                    new Vector2(0, 0),
                    new Vector2((float)hero.health / (float)hero.maxHealth, 1),
                    SpriteEffects.None, 0);
                uisb.Draw(healthcontainer.texture, healthBarPosition, Color.White);

                uisb.Draw(healthBack.texture, xpBarPosition, Color.White);
                uisb.Draw(xpbar.texture, new Vector2(xpBarPosition.X + 12,
                    xpBarPosition.Y + 2),
                    new Rectangle(0, 0, healthbar.texture.Width, healthbar.texture.Height),
                    Color.White, 0,
                    new Vector2(0, 0),
                    new Vector2(hero.GetXpPercent(), 1),
                    SpriteEffects.None, 0);
                uisb.Draw(healthcontainer.texture, xpBarPosition, Color.White);
                //uisb.Draw(xpbar, xpBarPosition, Color.White);
                //uisb.Draw(inventory.texture, new Vector2(game.Width / 2 - inventory.texture.Width / 2, game.Height - 100), Color.White);

                uisb.Draw(jar.texture,
                    new Vector2(game.Width - 50 - jar.texture.Width, game.Height / 2 - jar.texture.Height / 2),
                    Color.White);
                uisb.Draw(bar.texture,
                    new Vector2(game.Width - 50 - jar.texture.Width / 2, game.Height / 2 + jar.texture.Height / 2),
                    new Rectangle(0, 0, jar.texture.Width, jar.texture.Height), Color.White,
                    0,
                    new Vector2(jar.texture.Width / 2, jar.texture.Height),
                    new Vector2(1, (float)hero.jarBarAmount / (float)hero.MaxJarBarAmount),
                    SpriteEffects.None, 0);
                //uisb.Draw(bar.texture, new Vector2(game.Width - 50 - jar.texture.Width, game.Height / 2 - bar.texture.Height / 2), Color.White);
                uisb.Draw(invButton.texture, InvButtonPosition, Color.White);
                    DrawMiniMap(uisb);
                if (inventoryOpen)
                {
                    DrawInventory(uisb);
                }
                if (mainMenuOpen)
                {
                    DrawMainMenu(uisb);
                }

                foreach (AbilityButton ab in abilityBar)
                {
                    if (ab != null)
                    {
                        ab.Draw(uisb);
                    }
                }

                Items.Item it = currentLevel.DroppedItems.FindEntity(MouseController.MouseGridPosition());
                if (it != null && (it.ItemType == Enums.ITypes.Legendary || it.ItemType == Enums.ITypes.Armor || it.ItemType == Enums.ITypes.Weapon || it.ItemType == Enums.ITypes.Accessory || it.ItemType == Enums.ITypes.Offhand))
                {
                    ToolTip.Draw(uisb, (Items.Equipment.Gear)it, new Vector2(MouseController.MouseScreenPosition().X - 200, MouseController.MouseScreenPosition().Y - 250), hero);
                }

                DrawEquip(uisb);
            }

            else
            {
                //game.GraphicsDevice.Clear(Color);
                if (mainMenuOpen)
                {
                    DrawMainMenu(uisb);
                }
                else if (savePromptOpen)
                {
                    DrawSavePrompt(uisb);
                }
                else if (showSaveSlotSelection)
                {
                    DrawSaveSelection(uisb);
                }
                else if (showDeathScreen)
                {
                    DrawDeathScreen(uisb);
                }
            }
            DrawLog(uisb);

            
        }

        private static void DrawDeathScreen(SpriteBatch sb)
        {
            
            sb.Draw(deathNote.texture, new Vector2(game.Width / 2 - (deathNote.texture.Width / 2), game.Height / 4), Color.White);
            restartButton.Draw(sb);
            quitDeathButton.Draw(sb);
        }

        private static void DrawMainMenu(SpriteBatch sb)
        {
            mageButton.Draw(sb);
            warriorButton.Draw(sb);
            rogueButton.Draw(sb);
            quitButton.Draw(sb);
            sb.Draw(newGame.texture, new Vector2(game.Width / 3 - newGame.texture.Width / 2, game.Height / 6), Color.White);
            sb.Draw(loadGame.texture, new Vector2(5 * game.Width / 6 - loadGame.texture.Width / 2 + (CELLWIDTH / 2), game.Height / 10), Color.White);


            for (int i = 0; i < saveSlots.Count(); i++)
            {
                saveSlots[i].Draw(sb);
            }
        }

        private static void DrawSaveSelection(SpriteBatch sb)
        {
            sb.Draw(selectSave.texture, new Vector2(game.Width / 2 - selectSave.texture.Width / 2, game.Height / 2), Color.White);
            for (int i = 0; i < saveSlots.Count(); i++)
            {
                saveSlots[i].Draw(sb);
            }
        }

        private static void DrawSavePrompt(SpriteBatch sb)
        {
            saveButton.Draw(sb);
            continueButton.Draw(sb);
        }

        private static void DrawEquip(SpriteBatch sb)
        {
            if (hero.GetEquipment().helmet != null)
            {
                headSlot.drawOver = hero.GetEquipment().helmet.GetTexture();
            }
            else
            {
                headSlot.drawOver = GetTexture("UI/HelmOverlay");
            } 
            if (hero.GetEquipment().chestPlate != null)
            {
                chestSlot.drawOver = hero.GetEquipment().chestPlate.GetTexture();
            }
            else
            {
                chestSlot.drawOver = GetTexture("UI/ChestOverlay");
            }
            if (hero.GetEquipment().grieves != null)
            {
                legSlot.drawOver = hero.GetEquipment().grieves.GetTexture();
            }
            else
            {
                legSlot.drawOver = GetTexture("UI/LegsOverlay");
            }
            if (hero.GetEquipment().rings[0] != null)
            {
                ringSlot1.drawOver = hero.GetEquipment().rings[0].GetTexture();
            }
            else
            {
                ringSlot1.drawOver = GetTexture("UI/RingOverlay");
            }
            if (hero.GetEquipment().rings[1] != null)
            {
                ringSlot2.drawOver = hero.GetEquipment().rings[1].GetTexture();
            }
            else
            {
                ringSlot2.drawOver = GetTexture("UI/RingOverlay");
            }
            if (hero.GetEquipment().necklace != null)
            {
                neckSlot.drawOver = hero.GetEquipment().necklace.GetTexture();
            }
            else
            {
                neckSlot.drawOver = GetTexture("UI/NeckOverlay");
            }

            if (hero.GetEquipment().equippedWeapons[0] != null)
            {
                weaponSlot1.drawOver = hero.GetEquipment().equippedWeapons[0].GetTexture();
            }
            else
            {
                weaponSlot1.drawOver = GetTexture("UI/InvSlot");
            }

            if (hero.GetEquipment().equippedWeapons[1] != null)
            {
                weaponSlot2.drawOver = hero.GetEquipment().equippedWeapons[1].GetTexture();
            }
            else
            {
                weaponSlot2.drawOver = GetTexture("UI/InvSlot");
            }


            headSlot.Draw(sb);
            if (headSlot.doToolTip && hero.GetEquipment().helmet != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().helmet, armorEquipPosition + new Vector2(100, -250), hero);
            }
            chestSlot.Draw(sb);
            if (chestSlot.doToolTip && hero.GetEquipment().chestPlate != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().chestPlate, armorEquipPosition + new Vector2(100, -250), hero);
            }
            legSlot.Draw(sb);
            if (legSlot.doToolTip && hero.GetEquipment().grieves != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().grieves, armorEquipPosition + new Vector2(100, -250), hero);
            }
            ringSlot1.Draw(sb);
            if (ringSlot1.doToolTip && hero.GetEquipment().rings[0] != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().rings[0], armorEquipPosition + new Vector2(100, -250), hero);
            }
            ringSlot2.Draw(sb);
            if (ringSlot2.doToolTip && hero.GetEquipment().rings[1] != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().rings[1], armorEquipPosition + new Vector2(100, -250), hero);
            }
            neckSlot.Draw(sb);
            if (neckSlot.doToolTip && hero.GetEquipment().necklace != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().necklace, armorEquipPosition + new Vector2(100, -250), hero);
            }

            weaponSlot1.Draw(sb);
            if (weaponSlot1.doToolTip && hero.GetEquipment().equippedWeapons[0] != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().equippedWeapons[0], weaponEquipPosition + new Vector2(50, -250), hero);
            }

            weaponSlot2.Draw(sb);
            if (weaponSlot2.doToolTip && hero.GetEquipment().equippedWeapons[1] != null)
            {
                ToolTip.Draw(sb, hero.GetEquipment().equippedWeapons[1], weaponEquipPosition + new Vector2(50, -250), hero);
            }

        }

        private static bool IsTileInView(IntVec gridloc)
        {
            bool drawthistile = false;
            if (gridloc != null)
            {
                if (gridloc.X >= modifiedCameraPosition.X && gridloc.X <= modifiedCameraPosition.X + windowSizeInTiles.X
                    && gridloc.Y >= modifiedCameraPosition.Y && gridloc.Y <= modifiedCameraPosition.Y + windowSizeInTiles.Y)
                {
                    drawthistile = true;
                }
            }
            return drawthistile;
        }

        private static bool IsLightInView(IntVec gridloc, float intensity)
        {
            bool drawThisLight = false;
            int lightGraceArea = (int)(lightMaskWidthInTilesDividedByTwo * intensity);
            if (gridloc.X >= modifiedCameraPosition.X - lightGraceArea && gridloc.X <= modifiedCameraPosition.X + windowSizeInTiles.X + lightGraceArea
                && gridloc.Y >= modifiedCameraPosition.Y - lightGraceArea && gridloc.Y <= modifiedCameraPosition.Y + windowSizeInTiles.Y + lightGraceArea)
            {
                drawThisLight = true;
            }
            return drawThisLight;
        }

        private static void DrawLighting(Matrix transform)
        {
            if (DOLIGHTING)
            {
                
                game.spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.Additive);
                game.GraphicsDevice.SetRenderTarget(lightsTarget);
                game.GraphicsDevice.Clear(Color.Black);


                IntVec charpos = currentLevel.CharacterEntities.FindPosition(hero);
                if (charpos != null)
                {
                    Vector3 test = Vector3.Transform(new Vector3(charpos.X * CELLWIDTH, charpos.Y * CELLWIDTH, 0), transform);


                    game.spriteBatch.Draw(sightMask.texture, new Vector2((test.X), (test.Y)), new Rectangle(0, 0, sightMask.texture.Width, sightMask.texture.Height), Color.White, 0, new Vector2(sightMask.texture.Width / 2, sightMask.texture.Height / 2), sightDistance, SpriteEffects.None, 0);
                }


                //Vector3 test2 = Vector3.Transform(new Vector3(50 * CELLWIDTH, 50 * CELLWIDTH, 0), transform);
                foreach (ILightSource l in currentLevel.LightSources.Entities())
                {
                    IntVec lightPos = currentLevel.LightSources.FindPosition(l);
                    if (IsLightInView(lightPos, l.GetLightIntensity()))
                    {
                        Vector3 screenPosition = Vector3.Transform(new Vector3(lightPos.X * CELLWIDTH, lightPos.Y * CELLWIDTH, 0), transform);
                        game.spriteBatch.Draw(lightMask.texture, new Vector2(screenPosition.X, screenPosition.Y), new Rectangle(0, 0, lightMask.texture.Width, lightMask.texture.Height), l.GetLightColor(), 0, new Vector2(lightMask.texture.Width / 2, lightMask.texture.Height / 2), l.GetLightIntensity() + l.GetCurrentFlicker(), SpriteEffects.None, 0);
                    }
                }

                game.spriteBatch.End();
            }
            else
            {
                game.spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.Additive);
                game.GraphicsDevice.SetRenderTarget(lightsTarget);
                //
                //game.GraphicsDevice.Clear(Color.White);
                game.spriteBatch.End();
            }
        }
    }
}
