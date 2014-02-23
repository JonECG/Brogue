using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public interface ILightSource : IRenderable
    {
        //Return a float representing the intensity of the light. An intensity of 1 will have no scaling effect on the lightmask. 0 is no light. 4 will light the entire screen's size.
        float GetLightIntensity();
        //Returns the color of the light.
        Color GetLightColor();
        //Returns frames between changes in intensity by flickering. Note that this + 3 will be the max delay. The minimum delay will be 3 frames.
        int GetLightFlickerWait();
        //handles the individual flicker values. Has to be here because values need to be specific for each light source.
        float GetCurrentFlicker();

    }
}
