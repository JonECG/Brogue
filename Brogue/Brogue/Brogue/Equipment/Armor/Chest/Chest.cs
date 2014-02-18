﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Chest
{
    abstract class Chest : Armor
    {
        Random rand = new Random();

        public Chest()
        {
            BaseArmor = rand.Next(10, 16);
            EquipableIn = new List<Slots> { Slots.Chest };
        }
    }
}
