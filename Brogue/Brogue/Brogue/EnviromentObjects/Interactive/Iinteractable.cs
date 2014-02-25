using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue;

namespace Brogue.EnviromentObjects.Interactive
{
    public interface IInteractable : IEnvironmentObject
    {
       void actOn(GameCharacter actingCharacter);
    }
}
