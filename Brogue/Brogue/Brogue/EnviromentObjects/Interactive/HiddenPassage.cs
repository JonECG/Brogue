using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Brogue.Engine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Interactive
{
    class HiddenPassage : IEnvironmentObject, IRenderable, IInteractable
    {

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Stairs");
        IntVec exit { get; set; }
        bool isSolid { get; set; }
        bool isVisiable { get; set; }

        Direction directionFacing { get; set; }

        HiddenPassage other = new HiddenPassage();

        HiddenPassage()
        {
            isSolid = true;
            isVisiable = false;
        }

        HiddenPassage(HiddenPassage exitPosition)
        {
            isSolid = true;
            isVisiable = false;
            other = exitPosition;
        }

        private void setToNewPosition(GameCharacter actingCharacter)
        {
            IntVec exitPosition = Engine.Engine.currentLevel.InteractableEnvironment.FindPosition(other);
            actingCharacter.position = exitPosition;
        }

        private void changeState()
        {
            if (!isVisiable)
            {
                isVisiable = true;
            }
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
            changeState();
            other.actOn(actingCharacter);
            setToNewPosition(actingCharacter);

            //target.actOn(actingCharacter);
        }

    }
}
