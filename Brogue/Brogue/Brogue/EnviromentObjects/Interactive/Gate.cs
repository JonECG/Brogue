using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Mapping;

namespace Brogue.EnviromentObjects.Interactive
{
    [Serializable]
    class Gate : IInteractable, IEnvironmentObject, IRenderable
    {
        bool isSolid { get; set; }
        bool isOpen { get; set; }
        bool rotated { get; set; }

        public Direction directionFacing { get; set; }

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/IronGate");

        public Gate()
        {
            isSolid = true;
            isOpen = false;
        }

        public Gate(Direction setDirection)
        {
            isSolid = true;
            isOpen = false;
            directionFacing = setDirection;
        }

        public void changeSolid()
        {
            if (isSolid)
            {
                isSolid = false;
                isOpen = true;
                directionFacing = directionFacing.Rotate(true);
            }
            else if (!isSolid)
            {
                isSolid = true;
                isOpen = false;
                directionFacing = directionFacing.Rotate(false);
            }
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture, directionFacing);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            if (actingCharacter == null)
            {
                changeSolid();
                Audio.playSound("IronGate");
            }
        }
    }
}
