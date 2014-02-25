﻿using Brogue.Mapping;
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


namespace Brogue.Engine
{
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

    partial class Engine
    {
        public const bool DOLIGHTING = true;
        public const bool DOAUDIO = true;
        public const float sightDistance = 1;
        public static int CELLWIDTH = 48;
        private static int logSize = 10;
        private static Game1 game;
        public static IntVec cameraPosition = new IntVec(12, 8);
        private static Queue<String> log = new Queue<string>(10);
        private static Vector2 LogPosition;
        private static HeroClasses.Hero hero;
        private static bool heroesTurn = true;
        private static RenderTarget2D lightsTarget;
        private static RenderTarget2D mainTarget;
        public static Random enginerand = new Random();
        private static List<XPParticle> xpList = new List<XPParticle>();
        public static Matrix worldToView;
        public static Vector2 xpBarPosition;
        public static IntVec windowSizeInTiles;
        public static int lightMaskWidthInTilesDividedByTwo;
        private static IntVec modifiedCameraPosition = new IntVec(0, 0);

        private static DynamicTexture
            lightMask = GetTexture("lightmask")
        , sightMask = GetTexture("lightmask")
        , particleTex = GetTexture("UI/exp")
        , gridSelectionOverlay = GetTexture("abilityOverlay")
        ;

        static DynamicTexture
            jar = GetTexture("UI/Jar"),
            bar = GetTexture("UI/Bar"), 
            healthcontainer = GetTexture("UI/HealthJar"),
            healthbar = GetTexture("UI/HealthBar"), 
            xpbar = GetTexture("UI/XPBar"), 
            inventory = GetTexture("UI/Inventory");
        
        static SpriteFont font;
        static List<IntVec> gridSelection = new List<IntVec>();

        public static DynamicTexture placeHolder = GetTexture("placeholder");

        public static Level currentLevel;
        
        private static Song backgroundSong;

        public static void AddXP(int xp, IntVec gameGrid)
        {
            IntVec worldPosition = gameGrid * CELLWIDTH;
            Vector2 worldVector = Vector2.Transform(new Vector2(worldPosition.X, worldPosition.Y), worldToView);

            for (int i = 0; i < xp; i++)
            {
                XPParticle newxp = new XPParticle(new Vector2(worldVector.X + enginerand.Next(CELLWIDTH) - CELLWIDTH/2, worldVector.Y + enginerand.Next(CELLWIDTH) - CELLWIDTH/2), enginerand.Next(10) + 5);
                xpList.Add(newxp);
            }
        }

        public static void ClearGridSelections()
        {
            gridSelection.Clear();
        }

        public static void AddGridSelections(IntVec[] gridSpaces)
        {
            foreach (IntVec iv in gridSpaces){
                gridSelection.Add(iv);
            }
        }

        public static void Start(Game1 injectedGame)
        {
            game = injectedGame;
            CharacterCreation();
            GenerateLevel();
            LogPosition = new Vector2(12, 12);
            windowSizeInTiles = new IntVec(game.Width / CELLWIDTH, game.Height / CELLWIDTH);
            game.IsMouseVisible = true;
            StartGame();
        }

        public static void End()
        {

        }

        //public static void LoadContent(ContentManager content)
        //{
        //    jar = content.Load<DynamicTexture>("UI/Jar");
        //    bar = content.Load<DynamicTexture>("UI/Bar");
        //    healthbar = content.Load<DynamicTexture>("UI/HealthBar");
        //    healthcontainer = content.Load<DynamicTexture>("UI/HealthJar");
        //    xpbar = content.Load<DynamicTexture>("UI/XPBar");
        //    inventory = content.Load<DynamicTexture>("UI/Inventory");
        //    font = content.Load<SpriteFont>("UI/Font");
        //    particleTex = content.Load<DynamicTexture>("UI/exp");
        //    gridSelectionOverlay = content.Load<DynamicTexture>("abilityOverlay");
        //    lightMask = content.Load<DynamicTexture>("lightmask");
        //    sightMask = content.Load<DynamicTexture>("lightmask");
        //    lightMaskWidthInTilesDividedByTwo = lightMask.Width / (2 * CELLWIDTH);
        //    placeHolder = content.Load<DynamicTexture>("placeholder");

        //    Door.LoadContent(content);
        //    Tourch.LoadContent(content);
        //    Chair.LoadContent(content);
        //    Plant.LoadContent(content);
        //    Chest.LoadContent(content);

        //    Sprite.LoadContent(content);
        //}

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
             
        }

        public static void GenerateLevel()
        {
            currentLevel = LevelGenerator.generate(802, 35);
            Log("Level generated.");
            hero = new HeroClasses.Mage();
            currentLevel.CharacterEntities.Add(hero, currentLevel.GetStartPoint());
        }

        public static void StartGame()
        {
            //Make new level.
            //Start gameloop.
            Log("Game started");
            KeyboardState keyState = Keyboard.GetState();

            //Check keystate for engine relate key presses (exit, menu things, etc).
            //If it's the players turn, pass keystate to Hero.
            //Otherwise, take AI turn.

        }

        private static void AITurn()
        {
            //Iterate through each AI within maximum AI distance and call its TakeTurn method.
        }

        public static void Draw(Sprite sprite, IntVec destination)
        {
            if (IsTileInView(destination) && sprite.IsVisible && sprite.Texture.texture == null)
            {
                game.spriteBatch.Draw(sprite.Texture.texture, new Rectangle(destination.X * CELLWIDTH, destination.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), new Rectangle(sprite.SourceTile.X * CELLWIDTH, sprite.SourceTile.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), sprite.Blend, sprite.Direction, new Vector2(CELLWIDTH / 2, CELLWIDTH / 2), SpriteEffects.None, 0);
            }
        }

        public static void DrawUI(SpriteBatch uisb)
        {
            foreach (XPParticle xp in xpList)
            {
                uisb.Draw(particleTex.texture, xp.screenPosition, Color.White);
            }
            uisb.Draw(healthcontainer.texture, new Vector2(50, game.Height / 2 - healthcontainer.texture.Height / 2), Color.White);
            uisb.Draw(healthcontainer.texture, xpBarPosition, Color.White);
            uisb.Draw(healthbar.texture, new Vector2(50, game.Height / 2 - healthcontainer.texture.Height / 2), Color.White);
            uisb.Draw(xpbar.texture, new Vector2(xpBarPosition.X + xpbar.texture.Width / 2, xpBarPosition.Y + xpbar.texture.Height / 2), new Rectangle(0, 0, xpbar.texture.Width, xpbar.texture.Height), Color.White, 0, new Vector2(xpbar.texture.Width / 2, xpbar.texture.Height / 2), new Vector2(1, hero.GetXpPercent()), SpriteEffects.None, 0);
            //uisb.Draw(xpbar, xpBarPosition, Color.White);
            uisb.Draw(inventory.texture, new Vector2(game.Width / 2 - inventory.texture.Width / 2, game.Height - 100), Color.White);
            uisb.Draw(jar.texture, new Vector2(game.Width - 50 - jar.texture.Width, game.Height / 2 - jar.texture.Height / 2), Color.White);
            uisb.Draw(bar.texture, new Vector2(game.Width - 50 - jar.texture.Width, game.Height / 2 - bar.texture.Height / 2), Color.White);

            
            DrawLog(uisb);
        }

        

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < xpList.Count; i++)
            {
                if (xpList[i].update())
                {
                    xpList.RemoveAt(i);
                    hero.AddExperience(1);
                }
            }
            GameCommands();
            currentLevel.testUpdate();
            //Game turns
            if (heroesTurn)
            {
                heroesTurn = !hero.TakeTurn(currentLevel);
                cameraPosition = currentLevel.CharacterEntities.FindPosition(hero);
                modifiedCameraPosition.X = cameraPosition.X - (windowSizeInTiles.X / 2);
                modifiedCameraPosition.Y = cameraPosition.Y - (windowSizeInTiles.Y / 2);
                currentLevel.InvalidateCache();
            }
            else
            {
                //Take next NPCs turn.
                //When all NPCs have taken their turn...
                heroesTurn = true;
                currentLevel.InvalidateCache();
            }
        }

        private static void GameCommands()
        {
            if (KeyboardController.IsPressed(Keys.OemPlus))
            {
                logSize += 5;
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

        static int flickerdelay = 0;
        static float flicker = 0;
        static bool temp = false;
        public static void DrawGame(GameTime gameTime)
        {
            worldToView = Matrix.CreateTranslation(-cameraPosition.X * CELLWIDTH + game.Width / 2, -cameraPosition.Y * CELLWIDTH + game.Height / 2, 1.0f)
                    * Matrix.CreateScale(1.0f, 1.0f, 1);
            if (!temp)
            {
                AddXP(100, currentLevel.CharacterEntities.FindPosition(hero));
                temp = true;
            }
            

            //Draw lighting.
            DrawLighting(worldToView); 
            
            //Draw level.
            game.GraphicsDevice.SetRenderTarget(mainTarget);
            game.GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        worldToView);
            currentLevel.render();
            foreach (IntVec iv in gridSelection)
            {
                game.spriteBatch.Draw(gridSelectionOverlay, new Rectangle(iv.X * CELLWIDTH, iv.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), 
                    new Rectangle(0, 0, CELLWIDTH, CELLWIDTH), Color.White, 0, new Vector2(CELLWIDTH / 2, CELLWIDTH / 2), SpriteEffects.None, 0);
            }
            game.spriteBatch.End();

            //Draw both to screen.
            game.GraphicsDevice.SetRenderTarget(null);
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            game.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            game.effect.Parameters["lightMask"].SetValue(lightsTarget);
            game.effect.CurrentTechnique.Passes[0].Apply();
            
            game.spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
            game.spriteBatch.End();  


        }



        private static bool IsTileInView(IntVec gridloc)
        {
            bool drawthistile = false;
            if (gridloc.X >= modifiedCameraPosition.X && gridloc.X <= modifiedCameraPosition.X + windowSizeInTiles.X
                && gridloc.Y >= modifiedCameraPosition.Y && gridloc.Y <= modifiedCameraPosition.Y + windowSizeInTiles.Y)
            {
                drawthistile = true;
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
                if (flickerdelay == 0)
                {
                    flicker = (float)enginerand.NextDouble() / 8;
                    flickerdelay = enginerand.Next(5);
                }
                else
                {
                    flickerdelay--;
                }
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
                game.GraphicsDevice.Clear(Color.White);
                game.spriteBatch.End();
            }
        }
    }
}
