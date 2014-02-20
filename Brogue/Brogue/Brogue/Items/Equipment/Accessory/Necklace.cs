﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Accessory
{
    public class Necklace : Accessory
    {
        public Necklace(int dLevel, int cLevel)
        {
            Name = "Necklace";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Rogue, Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Neck };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
