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

       private static Random rand = new Random();

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

       public override Item PickUpEffect()
       {
           return this;
       }

       public static string findName(string itemName, int itemLevel)
       {
           int finditem;

           if (itemLevel < 11)
           {
               finditem = rand.Next(Enum.GetNames(typeof(PoorDescriptions)).Length);
               return (PoorDescriptions)finditem + " " + itemName;
           }
           else if (itemLevel > 20 && itemLevel < 31)
           {
               finditem = rand.Next(Enum.GetNames(typeof(GoodDescriptions)).Length);
               return (GoodDescriptions)finditem + " " + itemName;
           }
           else if (itemLevel > 30 && itemLevel < 41)
           {
               finditem = rand.Next(Enum.GetNames(typeof(RareDescriptions)).Length);
               return (RareDescriptions)finditem + " " + itemName;
           }
           else if (itemLevel > 40 && itemLevel < 50)
           {
               finditem = rand.Next(Enum.GetNames(typeof(SuperDescriptions)).Length);
               return (SuperDescriptions)finditem + " " + itemName;
           }
           else if (itemLevel > 49)
           {
               finditem = rand.Next(Enum.GetNames(typeof(MasterDescription)).Length);
               return (MasterDescription)finditem + " " + itemName;
           }

           return itemName;
       }

    }
}
