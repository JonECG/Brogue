using Brogue.EnviromentObjects.Interactive;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    class ColorEnvironment : IEnvironmentObject, Iinteractable, ILightSource
    {
        Color color;
        bool solid;


        public ColorEnvironment( Color color, bool solid = false )
        {
            this.color = color;
            this.solid = solid;
        }

        public bool IsSolid()
        {
            return solid;
        }

        public void actOn()
        {
            color = new Color(color.G, color.B, color.R);
        }

        public Sprite GetSprite()
        {
            return new Sprite(Engine.Engine.placeHolder, color);
        }

        public float GetLightIntensity()
        {
            return 1;
        }

        public Color GetLightColor()
        {
            return color;
        }

        public int GetLightFlickerWait()
        {
            return 8;
        }



        float currentFlicker = 0;
        int flickerCountdown = 5;

        public float GetCurrentFlicker()
        {
            if (flickerCountdown == 0)
            {
                currentFlicker = (float)Engine.Engine.enginerand.NextDouble() / 6;
                flickerCountdown = Engine.Engine.enginerand.Next(8) + 3;
            }
            else
            {
                flickerCountdown--;
            }
            return currentFlicker;
        }
    }
}
