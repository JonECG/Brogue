using Brogue.Abilities.AOE;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    [Serializable]
    public class Assassin : Hero
    {
        public Assassin()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Assassin");
            Hero.loadSprite();
            heroRole = Classes.Assassin;
            baseHealth = 450;
            healthPerLevel = 35;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Mug();
            abilities[1] = new Invisibility();
            abilities[2] = new Parry();
            abilities[3] = new Eviscerate();
            abilities[4] = new Assassinate();
            abilities[5] = new Poison();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
