using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    class Engine
    {

        public static CELLWIDTH = 48;
        private static Game game;

        public static void Start()
        {
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

        public static void Draw(Texture2D tex, IntVec loc)
        {
            game.spriteBatch.Draw(tex, Vector2(loc.X * CELLWIDTH, loc.Y * CELLWIDTH), Color.White);
        }
    }
}
