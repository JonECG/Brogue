using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Brogue.EnviromentObjects.Interactive
{
    class HiddenPassage : IEnvironmentObject, IRenderable
    {

        static DynamicTexture texture = Engine.Engine.GetTexture("Enviroment/Stairs");
        IntVec exit { get; set; }
        bool isSolid { get; set; }
        bool isVisiable { get; set; }

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

        private void ChangeState()
        {
            if (isSolid)
            {
                //isSolid = false;
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

    }
}
