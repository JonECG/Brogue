using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.EnviromentObjects.Interactive
{
    class Wall : IEnvironmentObject
    {
        static DynamicTexture texture = Engine.Engine.GetTexture("levelTileset");

        public bool isSolid;
        public bool isPassable { get; set; }

        public Wall()
        {
            isSolid = true;
            isPassable = false;
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

    class SecretWall : IInteractable, IEnvironmentObject
    {
        static Texture2D sprite;
        static DynamicTexture texture = Engine.Engine.GetTexture("levelTileset");
        public bool isSolid { get; set; }
        public bool isPassable { get; set; }

        public SecretWall()
        {
            isSolid = true;
            isPassable = false;
        }

        public void changeSolid()
        {
            if (isSolid)
            {
                isSolid = false;
            }
            else if (!isSolid)
            {
                isSolid = true;
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
            changeSolid();
        }
    }
}
