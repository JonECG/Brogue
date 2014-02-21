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

        private static int[] keyHeld = new int[NUMBER_OF_KEYS];

        private static bool[] previousDown = new bool[NUMBER_OF_KEYS];
        //private static bool[] down = new bool[NUMBER_OF_KEYS];

        public static void Update()
        {
            KeyboardState state = Keyboard.GetState();

            for (int i = 0; i < NUMBER_OF_KEYS; i++)
            {
                previousDown[i] = IsDown(i);
            }
            for (int i = 0; i < NUMBER_OF_KEYS; i++)
            {
                keyHeld[i] = (state.IsKeyDown((Keys)i)) ? keyHeld[i] + 1: 0;
            }
        }

        public static bool IsDown(int key)
        {
            return keyHeld[key] != 0;
        }

        public static bool IsUp(int key)
        {
            return !IsDown(key);
        }

        public static bool IsPressed(int key)
        {
            return keyHeld[key] == 1;
        }

        public static bool IsReleased(int key)
        {
            return !IsDown(key) && previousDown[key];
        }

        public static bool IsTyped(int key, int delay = 30, int interval = 10)
        {
            return IsPressed(key) || ( keyHeld[key] > delay && (keyHeld[key]+delay) % interval == 0 );
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

        public static bool IsTyped(Keys key, int delay = 30, int interval = 10)
        {
            return IsTyped((int)key, delay, interval);
        }
    }
}
