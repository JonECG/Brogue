using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Mapping;

namespace Brogue.EnviromentObjects.Interactive
{
    class Stairs : IEnvironmentObject, IInteractable, IRenderable
    {
        Direction directionFacing { get; set; }

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/StairsDesent");

        public bool isSolid { get; set; }

        public Stairs() 
        {
            isSolid = true;
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public Sprite GetSprite()
        {
            return new Sprite(texture);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            Audio.playSound("stairs");
            Engine.Engine.NextLevel();
            Audio.playRandomSong();
        }
    }
}
