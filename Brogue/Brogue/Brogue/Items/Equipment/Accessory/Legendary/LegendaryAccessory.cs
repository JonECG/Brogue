﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Accessory.Legendary
{
    public abstract class LegendaryAccessory : Accessory
    {
        public string FlavorText { get; protected set; }
    }
}