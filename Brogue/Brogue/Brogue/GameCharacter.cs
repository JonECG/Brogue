using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public abstract class GameCharacter : IRenderable
    {
        protected int health;
		public IntVec position;
        public bool isFriendly;
        abstract public void TakeTurn(Level level);
        public abstract void TakeDamage(int damage, GameCharacter attacker);
        public void Heal(int heal)
        {
            health += heal;
        }
        public Sprite GetSprite()
        {
            return new Sprite(Tile.tileset);
        }
    }
}
