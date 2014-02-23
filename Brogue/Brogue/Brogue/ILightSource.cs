﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public interface ILightSource : IRenderable
    {
        float GetLightIntensity();
        Color GetLightColor();
        int GetLightFlickerWait();
    }
}
