using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    static class KeyboardController
    {
        private const int NUMBER_OF_KEYS = 255;
        private static bool[] previousDown = new bool[NUMBER_OF_KEYS];
        private static bool[] down = new bool[NUMBER_OF_KEYS];

        public static void Update()
        {
            KeyboardState state = Keyboard.GetState();

            for (int i = 0; i < down.Length; i++)
            {
                previousDown[i] = down[i];
            }
            for (int i = 0; i < down.Length; i++)
            {
                down[i] = state.IsKeyDown((Keys)i);
            }
        }

        public static bool IsDown(int key)
        {
            return down[key];
        }

        public static bool IsUp(int key)
        {
            return !down[key];
        }

        public static bool IsPressed(int key)
        {
            return down[key] && !previousDown[key];
        }

        public static bool IsReleased(int key)
        {
            return !down[key] && previousDown[key];
        }

        public static bool IsDown(Keys key)
        {
            return IsDown((int)key);
        }

        public static bool IsUp(Keys key)
        {
            return IsUp((int)key);
        }

        public static bool IsPressed(Keys key)
        {
            return IsPressed((int)key);
        }

        public static bool IsReleased(Keys key)
        {
            return IsReleased((int)key);
        }
    }
}
