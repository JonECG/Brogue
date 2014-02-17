using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public static IntVec cameraPosition = new IntVec(0, 0);

        static Texture2D jar, bar, healthcontainer, healthbar, xpbar, inventory;

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
        }

        public static void CharacterCreation()
        {

        }

        public static void GenerateLevel()
        {

        }

        public static void StartGame()
        {

        }

        public static void Draw(Texture2D tex, IntVec destination)
        {
            game.spriteBatch.Draw(tex, new Vector2(destination.X * CELLWIDTH, destination.Y * CELLWIDTH), Color.White);
        }

        public static void Draw(Texture2D tileSheet, IntVec destination, IntVec tilesetSource)
        {
            game.spriteBatch.Draw(tileSheet, new Vector2(destination.X * CELLWIDTH, destination.Y * CELLWIDTH), new Rectangle(tilesetSource.X * CELLWIDTH, tilesetSource.Y * CELLWIDTH, CELLWIDTH, CELLWIDTH), Color.White);
        }

        public static void DrawUI(SpriteBatch uisb)
        {
            uisb.Draw(healthcontainer, new Vector2(50, game.Height / 2 - healthcontainer.Height / 2), Color.White);
            uisb.Draw(healthcontainer, new Vector2(80, game.Height / 2 - healthcontainer.Height / 2), Color.White);
            uisb.Draw(healthbar, new Vector2(50, game.Height / 2 - healthbar.Height / 2), Color.White);
            uisb.Draw(xpbar, new Vector2(80, game.Height / 2 - healthbar.Height / 2), Color.White);
            uisb.Draw(inventory, new Vector2(game.Width / 2 - inventory.Width / 2, game.Height - 100), Color.White);
            uisb.Draw(jar, new Vector2(game.Width - 50 - jar.Width, game.Height / 2 - jar.Height /2 ), Color.White);
            uisb.Draw(bar, new Vector2(game.Width - 50 - jar.Width, game.Height / 2 - bar.Height /2 ), Color.White);
        }
    }
}
