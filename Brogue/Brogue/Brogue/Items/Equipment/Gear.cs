using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.HeroClasses;

namespace Brogue.Items.Equipment
{
    [Serializable] public abstract class Gear : Item
    {
       public int LevelReq { get; protected set; }
       public List<Classes> UsedBy { get; protected set; }
       public List<Slots> EquipableIn { get; protected set; }

       public Gear()
       {

       }

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

       public override Item PickUpEffect(Hero player)
       {
           return this;
       }
    }
}
