using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    class Minimap
    {
        public Level level;
        private bool[,] detected;
        private Rectangle active;

        public int ViewWidth { get { return active.Width; } set { active.Width = value; } }
        public int ViewHeight { get { return active.Height; } set { active.Height = value; } }

        public Minimap(Level level)
        {
            this.level = level;
            detected = new bool[level.GetWidth(), level.GetHeight()];
            active = new Rectangle();
            ViewWidth = 30;
            ViewHeight = 17;
        }

        public void UpdateView(IntVec position)
        {
            active.X = position.X - ViewWidth / 2;
            active.Y = position.Y - ViewHeight / 2;

            for (int x = 0; x < ViewWidth; x++)
            {
                for (int y = 0; y < ViewHeight; y++)
                {
                    if (x + active.X >= 0 && y + active.Y >= 0 && x + active.X < detected.GetLength(0) && y + active.Y < detected.GetLength(1))
                    detected[x + active.X, y + active.Y] = true;
                }
            }
        }

        public bool IsDetected(IntVec position)
        {
            return detected[position.X, position.Y];
        }

        public bool IsActive(IntVec position)
        {
            return active.Contains( position.X, position.Y );
        }

    }
}
