using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public interface ILightSource : IRenderable
    {
        float Intensity { get; set; }
        int MyProperty { get; set; }
        Color Color { get; set; }
        int FlickerWait { get; set; }
    }
}
