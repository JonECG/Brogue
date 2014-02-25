using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public abstract class GameCharacter : IRenderable
    {
        public int health;
        public int maxHealth;
		public IntVec position;
        public bool isFriendly;
        abstract public bool TakeTurn(Level level);
        public abstract void TakeDamage(int damage, GameCharacter attacker);
        public static DynamicTexture texture;
        public void Heal(int heal)
        {
            health += heal;
        }
        public Sprite GetSprite()
        {
            return new Sprite(GetTexture());
        }

        public virtual DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("GameCharacter");
        }
    }
}
