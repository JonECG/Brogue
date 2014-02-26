using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Offhand
{
    public abstract class Offhand : Gear
    {
        public List<ElementAttributes> Element { get; protected set; }

        public Offhand()
        {
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            ItemType = ITypes.Offhand;
        }

        public ElementAttributes findElement()
        {
            Random rand = new Random();
            int size = Enum.GetNames(typeof(ElementAttributes)).Length;

            return (ElementAttributes)rand.Next(size);
        }
    }
}
