﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor
{
    public abstract class Armor : Gear
    {
        public int BaseArmor { get; protected set; }
        public int ArmorValue { get; protected set; }
        public int TypeBonus { get; protected set; }

        public int findArmorValue(int bAmr, int dLevel, int lReq, int tb)
        {
            return bAmr * dLevel + lReq + tb;
        }
    }
}
