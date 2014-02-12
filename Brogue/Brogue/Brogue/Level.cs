using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    class Level
    {
        Tile[,] tiles;
        //EnvironmentObject[] environment;
        List<Item> droppedItems;
        List<GameCharacter> characterEntities;
        bool[,] cachedSolid;
    }
}
