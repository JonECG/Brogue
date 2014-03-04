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
    [Serializable] class HiddenPassage : IEnvironmentObject, IRenderable, IInteractable
    {

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Stairs");
        IntVec exit { get; set; }
        bool isSolid { get; set; }
        bool isVisiable { get; set; }

        public IntVec testPosition { get; set; }
        public IntVec testPlayerPosition { get; set; }

        public Direction directionFacing { get; set; }

        public HiddenPassage other;

        public HiddenPassage()
        {
            isSolid = true;
            isVisiable = false;
        }

        public HiddenPassage(HiddenPassage exitPosition)
        {
            isSolid = true;
            isVisiable = false;
            other = exitPosition;
            exitPosition.other = this;
        }

        private void setToNewPosition(GameCharacter actingCharacter)
        {

            //IntVec exitPosition = other.testPosition;
            //IntVec finalPosition = exitPosition + other.directionFacing;
            //testPlayerPosition = finalPosition;

            IntVec exitPosition = Engine.Engine.currentLevel.InteractableEnvironment.FindPosition(other);
            IntVec finalPosition = exitPosition + other.directionFacing;
            IntVec characterPosition = Engine.Engine.currentLevel.CharacterEntities.FindPosition(actingCharacter);

            Engine.Engine.currentLevel.Move(actingCharacter, finalPosition, true);
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
            //return new Sprite(texture);
            return new Sprite(texture, directionFacing);
        }

        public void actOn(GameCharacter actingCharacter)
        {
            changeState();
            other.setToNewPosition(actingCharacter);
            other.isVisiable = true;
            //target.actOn(actingCharacter);
        }

    }
}
