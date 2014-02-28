﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.HeroClasses;

namespace Brogue.Items.Consumables
{
    public abstract class Consumable : Item
    {
        public int RestoreAmount { get; protected set; }
        public int BaseAmount { get; protected set; }

        public Consumable()
        {
            ItemType = ITypes.Consumable;
        }

        public static int findRestoreAmount(int dLevel, int bAmt)
        {
            return bAmt * dLevel;
        }

        public override Item PickUpEffect(Hero player)
        {

            player.jarBarAmount += this.RestoreAmount;
            return null;
        }
    }
}
