using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Accessory.Rings
{
    class Ring : Accessory
    {
        public Ring(int dLevel)
        {
            Name = "Ring";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Rogue, Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Finger_One, Slots.Finger_Two };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
