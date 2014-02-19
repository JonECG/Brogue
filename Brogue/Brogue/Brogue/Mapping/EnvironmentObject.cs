using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public interface IEnvironmentObject : IRenderable
    {
        bool IsSolid();
    }
}
