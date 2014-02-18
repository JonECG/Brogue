using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment
{
    abstract class Gear
    {
       public string Name { get; protected set; }
       public int LevelReq { get; protected set; }
       public List<Class> UsedBy { get; protected set; }
       public List<Slots> EquipableIn { get; protected set; }

       public static int findLevelReq(int dLevel)
       {
           int levelRange = 3;
           Random rand = new Random();
           return rand.Next(dLevel, levelRange + dLevel) + dLevel;
       }
    }
}
