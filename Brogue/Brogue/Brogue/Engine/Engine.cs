﻿using Brogue.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    class Engine
    {

        public static int CELLWIDTH = 48;
        private static Game1 game;
        public static IntVec cameraPosition = new IntVec(12, 8);

        static Texture2D jar, bar, healthcontainer, healthbar, xpbar, inventory;

        public static Texture2D placeHolder;

        static Level currentLevel;


        public static void Start(Game1 injectedGame)
        {
            game = injectedGame;
            CharacterCreation();
            GenerateLevel();
            StartGame();
        }

        public static void End()
        {
            
        }

        public static void LoadContent(ContentManager content)
        {
            jar = content.Load<Texture2D>("UI/Jar");
            bar = content.Load<Texture2D>("UI/Bar");
            healthbar = content.Load<Texture2D>("UI/HealthBar");
            healthcontainer = content.Load<Texture2D>("UI/HealthJar");
            xpbar = content.Load<Texture2D>("UI/XPBar");
            inventory = content.Load<Texture2D>("UI/Inventory");

            placeHolder = content.Load<Texture2D>("levelTileset");
        }

        public static void CharacterCreation()
        {

        }

        public static void GenerateLevel()
        {
            currentLevel = LevelGenerator.generate(1337, 20);
        }

        public static void StartGame()
        {
            //Make new level.
            //Start gameloop.

            KeyboardState keyState = Keyboard.GetState();

            //Check keystate for engine relate key presses (exit, menu things, etc).
            //If it's the players turn, pass keystate to Hero.
            //Otherwise, take AI turn.

        }

        private static void AITurn()
        {
            //Iterate through each AI within maximum AI distance and call its TakeTurn method.
        }

        public static void Draw(Texture2D tex, IntVec destination )
        {
            Draw(tex, destination, Color.White);
        }

        public static void Draw(Texture2D tex, IntVec destination, Color color)
        {

            game.effect.Parameters["blendColor"].SetValue(new Vector4(color.R, color.G, color.B, color.A));
            game.spriteBatch.Draw(tex, new Vector2(destination.X * CELLWIDTH, destination.Y * CELLWIDTH), color);
        }

        public static void Draw(Texture2D tileSheet, IntVec destination, IntVec tilesetSource)
        {
            Draw(tileSheet, destination, tilesetSource, Color.White);
        }

        public static void Draw(Texture2D tileSheet, IntVec destination, IntVec tilesetSource, Color color)
        {
            game.effect.Parameters["blendColor"].SetValue(new Vector4( color.R, color.G, color.B, color.A));
            game.spriteBatch.Draw(tileSheet, new Vector2(destination.X * CELLWIDTH, destination.Y * CELLWIDTH), new Rectangle(tilesetSource.X * CELLWIDTH, tilesetSource.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), color);
            
        }

        public static void DrawUI(SpriteBatch uisb)
        {
            game.effect.Parameters["blendColor"].SetValue(new Vector4(1f, 0f, 0f, 1f));
            uisb.Draw(healthcontainer, new Vector2(50, game.Height / 2 - healthcontainer.Height / 2), Color.White);
            uisb.Draw(healthcontainer, new Vector2(80, game.Height / 2 - healthcontainer.Height / 2), Color.White);
            uisb.Draw(healthbar, new Vector2(50, game.Height / 2 - healthbar.Height / 2), Color.White);
            uisb.Draw(xpbar, new Vector2(80, game.Height / 2 - healthbar.Height / 2), Color.White);
            uisb.Draw(inventory, new Vector2(game.Width / 2 - inventory.Width / 2, game.Height - 100), Color.White);
            uisb.Draw(jar, new Vector2(game.Width - 50 - jar.Width, game.Height / 2 - jar.Height /2 ), Color.White);
            uisb.Draw(bar, new Vector2(game.Width - 50 - jar.Width, game.Height / 2 - bar.Height /2 ), Color.White);
        }

        public static void Update(GameTime gameTime)
        {
            currentLevel.testUpdate();
            cameraPosition += new IntVec((KeyboardController.IsDown(Keys.Right) ? 1 : 0) - (KeyboardController.IsDown(Keys.Left) ? 1 : 0),
                (KeyboardController.IsDown(Keys.Down) ? 1 : 0) - (KeyboardController.IsDown(Keys.Up) ? 1 : 0)) * 4;
        }

        public static void DrawGame(GameTime gameTime)
        {
            currentLevel.render();
        }
    }
}
