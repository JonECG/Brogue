using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;

namespace Brogue.EnviromentObjects.Interactive
{
    public interface Iinteractable : IEnvironmentObject
    {
       void actOn();
    }
}
