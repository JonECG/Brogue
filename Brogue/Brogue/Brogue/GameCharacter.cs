using Brogue.Mapping;
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
        public void Heal(int heal)
        {
            health += heal;
        }
        public virtual Sprite GetSprite()
        {
            return new Sprite(Tile.tileset);
        }
    }
}
