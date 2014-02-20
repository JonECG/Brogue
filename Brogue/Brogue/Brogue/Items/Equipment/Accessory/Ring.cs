using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Accessory
{
    public class Ring : Accessory
    {
        public Ring(int dLevel, int cLevel)
        {
            Name = "Ring";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Rogue, Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Finger_One, Slots.Finger_Two };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
