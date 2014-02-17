using Microsoft.Xna.Framework;
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
        public static IntVec cameraPosition;

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
    }
}
