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


namespace Brogue.Engine
{
    [Serializable]
    class SaveGameData
    {
        public HeroClasses.Hero character;
        public int seed;
        public int levelComplexity;
        public int dungeonLevel;
    }

    class XPParticle
    {
        public Vector2 screenPosition;
        float speed;
        Vector2 direction;
        float distance;
        public XPParticle(Vector2 screenPosition, float speed)
        {
            this.screenPosition = screenPosition;
            this.speed = speed;
            this.direction = new Vector2(Engine.xpBarPosition.X, Engine.xpBarPosition.Y + Engine.enginerand.Next(175)) - screenPosition;
            distance = direction.Length();
            this.direction.Normalize();
        }
        public bool update()
        {
            screenPosition += direction * speed;
            bool finished = false;
            distance -= speed;
            if (distance < 5)
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
        public DynamicTexture drawOver; 
        public bool doDrawOver;
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
                sb.Draw(drawOver.texture, pos, Color.White);
                sb.DrawString(Engine.font, caption,
                    new Vector2(pos.X - Engine.font.MeasureString(caption).X / 2, pos.Y - 20), Color.Red);
            }
        }
    }

    partial class Engine
    {
        public const bool DOLIGHTING = true;
        public const bool DOAUDIO = false;
        public const bool DOSTARTMENU = false;
        public const float sightDistance = 1;
        public static bool inventoryOpen = false;
        public static bool characterCreationOpen = true;
        public static bool savePromptOpen = false;
        public static int CELLWIDTH = 48;
        private static int logSize = 10;
        private static Game1 game;
        private static bool gameStarted = false;
        public static IntVec cameraPosition = new IntVec(12, 8);
        private static Queue<String> log = new Queue<string>(10);
        private static Vector2 LogPosition, InvButtonPosition, InventoryPosition, InventorySize;
        private static HeroClasses.Hero hero;
        private static bool heroesTurn = true;
        private static RenderTarget2D lightsTarget;
        private static RenderTarget2D mainTarget;
        private const int AIDist = 15;
        public static Random enginerand = new Random();
        private static List<XPParticle> xpList = new List<XPParticle>();
        public static Matrix worldToView;
        public static Vector2 xpBarPosition;
        public static Vector2 weaponEquipPosition;
        public static Vector2 armorEquipPosition;
        public static IntVec windowSizeInTiles;
        public static int lightMaskWidthInTilesDividedByTwo;
        private static IntVec modifiedCameraPosition = new IntVec(0, 0);

        private static int levelSeed;

        private static int levelComplexity;
        private static int currentDungeonLevel = 1;
        

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
            xpbar = GetTexture("UI/XPBar"),
            invSlot = GetTexture("UI/InvSlot"),
            invHighlightSlot = GetTexture("UI/InvSlotHighlighted"),
            invButton = GetTexture("UI/InventoryIcon");


        static UIButton mageButton, warriorButton, rogueButton;
        static UIButton saveButton, continueButton;

        public static SpriteFont font;
        static List<GridSelection> gridSelection = new List<GridSelection>();

        public static DynamicTexture placeHolder = GetTexture("placeholder");

        public static Level currentLevel;
        
        private static Song backgroundSong;

        public static void AddXP(int xp, IntVec gameGrid)
        {
            IntVec worldPosition = gameGrid * CELLWIDTH;
            Vector2 worldVector = Vector2.Transform(new Vector2(worldPosition.X, worldPosition.Y), worldToView);

            for (int i = 0; i < xp; i++)
            {
                XPParticle newxp = new XPParticle(new Vector2(worldVector.X + enginerand.Next(CELLWIDTH) - CELLWIDTH/2, worldVector.Y + enginerand.Next(CELLWIDTH) - CELLWIDTH/2), enginerand.Next(20) + 15);
                xpList.Add(newxp);
            }
        }

        public static void NextLevel()
        {

        }

        public static void SaveGame()
        {
            SaveGameData sg = new SaveGameData();
            sg.character = hero;
            sg.seed = enginerand.Next();
            sg.levelComplexity = enginerand.Next(500) + 50;
            sg.dungeonLevel = currentDungeonLevel + 1;
            
            //IntermediateSerializer  is = new IntermediateSerializer();

            //Write to binary file...
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("myfile.bro", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, sg);
            stream.Close();
        }

        public static void LoadGame()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("myfile.bro", FileMode.Open, FileAccess.Read, FileShare.Read);
            SaveGameData gd = (SaveGameData)formatter.Deserialize(stream);
            stream.Close();
            hero = gd.character;
            currentLevel = LevelGenerator.generate(gd.seed, gd.levelComplexity, gd.dungeonLevel);
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
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

        public static void Start(Game1 injectedGame)
        {
            game = injectedGame;
            CharacterCreation();

            //GenerateLevel();
            LogPosition = new Vector2(12, 12);
            // = new Vector2(game.Width - 48, game.Height - 48);
            InventoryPosition = new Vector2(game.Width - 5 * (CELLWIDTH), game.Height - 5 * (CELLWIDTH));
            InvButtonPosition = new Vector2(game.Width - CELLWIDTH, game.Height - CELLWIDTH);
            InventorySize = new Vector2(4 * CELLWIDTH, 4 * CELLWIDTH);
            weaponEquipPosition = new Vector2(game.Width / 2 - CELLWIDTH, game.Height - CELLWIDTH);
            armorEquipPosition = new Vector2(game.Width - 6 * CELLWIDTH, CELLWIDTH);
            windowSizeInTiles = new IntVec(game.Width / CELLWIDTH, game.Height / CELLWIDTH);
            game.IsMouseVisible = true;


            saveButton = new UIButton(new Vector2(game.Width / 2 - 150, game.Height / 2), true, "UI/Save", "Save and quit");
            continueButton = new UIButton(new Vector2(game.Width / 2 + 150, game.Height / 2), true, "UI/Continue", "Continue");
            //StartGame();
        }

        public static void End()
        {

        }

        public static void Log(string input)
        {
            log.Enqueue(input);
            if (log.Count > logSize)
            {
                log.Dequeue();
            }
        }

        public static void CharacterCreation()
        {
            LoadContent(game.Content);
            if (DOSTARTMENU)
            {
                warriorButton = new UIButton(new Vector2(game.Width / 2, game.Height / 2), true, "Hero/Hero", "Warrior");
                mageButton = new UIButton(new Vector2(game.Width / 2 - 60, game.Height / 2), true, "Hero/Hero", "Mage");
                rogueButton = new UIButton(new Vector2(game.Width / 2 + 60, game.Height / 2), true, "Hero/Hero", "Rogue");
                characterCreationOpen = true;
            }
            else
            {
                hero = new HeroClasses.Warrior();
                StartGame();
            }
        }

        public static void GenerateLevel()
        {
            levelSeed = enginerand.Next();
            levelComplexity = enginerand.Next(500) + 50;
            currentDungeonLevel = 1;
            currentLevel = LevelGenerator.generate(levelSeed, levelComplexity, currentDungeonLevel);
            Log("Level generated.");
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
        }

        public static void StartGame()
        {
            Log("Game started");
            gameStarted = true;
            characterCreationOpen = false;
            GenerateLevel();
            //LoadContent(contentManager);

        }

        public static void ContentLoaded(ContentManager content)
        {
            lightsTarget = new RenderTarget2D(game.GraphicsDevice, game.Width, game.Height);
            mainTarget = new RenderTarget2D(game.GraphicsDevice, game.Width, game.Height);
            lightMaskWidthInTilesDividedByTwo = lightMask.texture.Width / (2 * CELLWIDTH);
            xpBarPosition = new Vector2(80, game.Height / 2 - healthbar.texture.Height / 2);
            font = content.Load<SpriteFont>("UI/Font");

            //////////////////////////////////
            Audio.LoadContent(content);
            Audio.playMusic("Doom", 0.15f);
            //////////////////////////////////
            
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
                        if (charIndex >= currentLevel.CharacterEntities.Entities().Count<GameCharacter>())
                        {
                            charIndex = 0;
                        }
                    }
                    charIndex += hero.TakeTurn(currentLevel) ? 1 : 0;

                    cameraPosition = currentLevel.CharacterEntities.FindPosition(hero);
                    modifiedCameraPosition.X = cameraPosition.X - (windowSizeInTiles.X / 2);
                    modifiedCameraPosition.Y = cameraPosition.Y - (windowSizeInTiles.Y / 2);
                    currentLevel.InvalidateCache();

                }
            }
            else
            {
                if (characterCreationOpen)
                {
                    if (mageButton.isClicked())
                    {
                        hero = new HeroClasses.Mage();
                        StartGame();
                    }
                    if (warriorButton.isClicked())
                    {
                        hero = new HeroClasses.Warrior();
                        StartGame();
                    }
                    if (rogueButton.isClicked())
                    {
                        hero = new HeroClasses.Rogue();
                        StartGame();
                    }
                }

                else if (savePromptOpen)
                {
                    if (saveButton.isClicked())
                    {
                        SaveGame();
                        System.Environment.Exit(0);
                    }
                    if (continueButton.isClicked())
                    {
                        savePromptOpen = false;
                        gameStarted = true;
                    }
                }

            }
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
            if (KeyboardController.IsPressed(Keys.U))
            {
                SaveGame();
                didSomething = true;
                
            }

            if (KeyboardController.IsPressed(Keys.I))
            {
                LoadGame();
                didSomething = true;
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

                if (!didSomething)
                {
                    didSomething = InventoryInteraction(true, screenpos);
                }
            }
            if (MouseController.RightClicked())
            {
                if (!didSomething)
                {
                    didSomething = InventoryInteraction(false, screenpos);
                }

            }

            return didSomething;
        }

        private static void GoToNextLevel()
        {
            //Generate next level
            currentLevel = LevelGenerator.generate(enginerand.Next(), enginerand.Next(500) + 50);
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
            //Prompt for save.

            gameStarted = false;
            savePromptOpen = true;
        }


        private static bool InventoryInteraction(bool leftButton, IntVec screenpos)
        {
            bool didsomething = false;
            if (inventoryOpen &&
                    screenpos.X > InventoryPosition.X && screenpos.Y > InventoryPosition.Y &&
                    screenpos.X < InventoryPosition.X + InventorySize.X &&
                    screenpos.Y < InventoryPosition.Y + InventorySize.Y)
            {
                //clicked in the inventory somewhere. Figure out which cell it was and adjust appropriately.
                Vector2 relativeInventoryLocation = new Vector2(screenpos.X, screenpos.Y) - InventoryPosition;
                IntVec inventoryCell = new IntVec((int)(relativeInventoryLocation.X / CELLWIDTH), (int)(relativeInventoryLocation.Y / CELLWIDTH));

                int inventorySlotIndex = inventoryCell.Y * 4 + inventoryCell.X;

                if (leftButton)
                {

                    //hero.equipWeapon(inventorySlotIndex);
                    hero.equipArmor(inventorySlotIndex);

                    hero.equipWeapon(inventorySlotIndex, 0);
                    hero.equipArmor(inventorySlotIndex);

                }
                else
                {
                    hero.dropItem(inventorySlotIndex, currentLevel);
                }
                didsomething = true;
            }
            return didsomething;
        }

        public static void DrawInventory(SpriteBatch sb)
        {
            InventorySystem.Inventory inv = hero.GetInventory();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector2 curpos = new Vector2(InventoryPosition.X + (invSlot.texture.Width * i), InventoryPosition.Y + (invSlot.texture.Height * j));
                    sb.Draw(invSlot.texture, curpos, Color.White);
                    Items.Item item = inv.GetItemAt(j * 4 + i);
                    if (item != null){
                        sb.Draw(inv.GetItemAt(j * 4 + i).GetTexture().texture, curpos, Color.White);
                    }
                }
            }
        }

        public static void DrawLog(SpriteBatch spriteBatch)
        {
            int inc = 0;
            foreach (string s in log)
            {
                spriteBatch.DrawString(font, s, new Vector2(LogPosition.X, LogPosition.Y + 12 * inc++), Color.Red);
            }
        }
        
        public static void DrawGame(GameTime gameTime)
        {
            if (gameStarted)
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
            if (gameStarted)
            {
                foreach (XPParticle xp in xpList)
                {
                    uisb.Draw(particleTex.texture, xp.screenPosition, Color.White);
                }
                uisb.Draw(healthcontainer.texture, new Vector2(50, game.Height / 2 - healthcontainer.texture.Height / 2), Color.White);
                uisb.Draw(healthcontainer.texture, xpBarPosition, Color.White);
                uisb.Draw(healthbar.texture, new Vector2(50, game.Height / 2 - healthcontainer.texture.Height / 2), Color.White);
                uisb.Draw(xpbar.texture, new Vector2(xpBarPosition.X + xpbar.texture.Width / 2,
                    xpBarPosition.Y + xpbar.texture.Height / 2),
                    new Rectangle(0, 0, xpbar.texture.Width, xpbar.texture.Height),
                    Color.White, 0, new Vector2(xpbar.texture.Width / 2, xpbar.texture.Height / 2),
                    new Vector2(1, hero.GetXpPercent()), SpriteEffects.None, 0);
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
                    new Vector2(1, hero.jarBarAmount / HeroClasses.Hero.MaxJarBarAmount),
                    SpriteEffects.None, 0);
                //uisb.Draw(bar.texture, new Vector2(game.Width - 50 - jar.texture.Width, game.Height / 2 - bar.texture.Height / 2), Color.White);
                uisb.Draw(invButton.texture, InvButtonPosition, Color.White);
                DrawMiniMap(uisb);
                if (inventoryOpen)
                {
                    DrawInventory(uisb);
                }
                if (characterCreationOpen)
                {
                    DrawCharacterCreation(uisb);
                }

                DrawEquip(uisb);
            }

            else
            {

                //game.GraphicsDevice.Clear(Color);
                if (characterCreationOpen)
                {
                    DrawCharacterCreation(uisb);
                }
                else if (savePromptOpen)
                {
                    DrawSavePrompt(uisb);
                }
            }
            DrawLog(uisb);
        }

        private static void DrawCharacterCreation(SpriteBatch sb)
        {
            mageButton.Draw(sb);
            warriorButton.Draw(sb);
            rogueButton.Draw(sb);
        }

        private static void DrawSavePrompt(SpriteBatch sb)
        {
            saveButton.Draw(sb);
            continueButton.Draw(sb);
        }

        private static void DrawEquip(SpriteBatch sb)
        {
            for (int i = 0; i < HeroClasses.Equipment.MAX_ARMOR_SLOTS; i++)
            {
                Vector2 curpos = new Vector2(armorEquipPosition.X + (invSlot.texture.Width * i), armorEquipPosition.Y);
                sb.Draw(invSlot.texture, curpos, Color.White);
                Items.Item item = null;
                switch (i)
                {
                    case 0:
                        item = hero.currentlyEquippedItems.helmet;
                        break;
                    case 1:
                        item = hero.currentlyEquippedItems.chestPlate;
                        break;
                    case 2:
                        item = hero.currentlyEquippedItems.grieves;
                        break;
                    case 3:
                        item = hero.currentlyEquippedItems.rings[0];
                        break;
                    case 4:
                        item = hero.currentlyEquippedItems.rings[1];
                        break;
                    case 5:
                        item = hero.currentlyEquippedItems.necklace;
                        break;
                }
                if (item != null)
                {
                    sb.Draw(item.GetTexture().texture, curpos, Color.White);
                }
            }
            for (int i = 0; i < HeroClasses.Equipment.MAX_WEAPON_SLOTS; i++)
            {
                Vector2 curpos = new Vector2(weaponEquipPosition.X + (invSlot.texture.Width * i), weaponEquipPosition.Y);
                sb.Draw(invSlot.texture, curpos, Color.White);
                Items.Item item = hero.currentlyEquippedItems.equippedWeapons[i];
                if (item != null)
                {
                    sb.Draw(item.GetTexture().texture, curpos, Color.White);
                }
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
                Vector3 test = Vector3.Transform(new Vector3(charpos.X * CELLWIDTH, charpos.Y * CELLWIDTH, 0), transform);
                
                game.spriteBatch.Draw(sightMask.texture, new Vector2((test.X), (test.Y)), new Rectangle(0, 0, sightMask.texture.Width, sightMask.texture.Height), Color.White, 0, new Vector2(sightMask.texture.Width / 2, sightMask.texture.Height / 2), sightDistance, SpriteEffects.None, 0);

                Vector3 test2 = Vector3.Transform(new Vector3(50 * CELLWIDTH, 50 * CELLWIDTH, 0), transform);
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
