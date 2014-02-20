﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment
{
    public abstract class Gear : Item
    {
       public int LevelReq { get; protected set; }
       public List<Class> UsedBy { get; protected set; }
       public List<Slots> EquipableIn { get; protected set; }

       public static int findLevelReq(int dLevel, int cLevel)
       {
           int levelRange = 3;
           int max = cLevel + levelRange;
           Random rand = new Random();

           if (max < dLevel)
           {
               return rand.Next(max-levelRange, dLevel);
           }
           else
           {
               return rand.Next(dLevel, max);
           }
       }
    }
}
