using Brogue.Abilities.AOE;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
using Brogue.Enums;
using Brogue.Items.Equipment.Accessory.Legendary;
using Brogue.Items.Equipment.Armor.Legendary.Chest;
using Brogue.Items.Equipment.Armor.Legendary.Helm;
using Brogue.Items.Equipment.Armor.Legendary.Legs;
using Brogue.Items.Equipment.Weapon.Legendary.Melee;
using Brogue.Items.Equipment.Weapon.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    [Serializable] public class Rogue : Hero
    {
        public Rogue()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/RogueSprite");
            Hero.loadSprite();
            visible = true;
            heroRole = Classes.Rogue;
            inventory.addItem(new TheWolverine(1, 1));
            baseHealth = 225;
            healthPerLevel = 35;
            resetLevel();
            resetHealth();
            canDuelWield = true;
            abilities[0] = new Mug();
            abilities[1] = new Invisibility();
            Engine.Engine.Log(health.ToString());
        }
    }
}
