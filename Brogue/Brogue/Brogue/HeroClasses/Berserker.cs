using Brogue.Abilities.AOE;
using Brogue.Abilities.Damaging.SingleTargets;
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
    public class Berserker : Hero
    {
        public Berserker()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Berserker");
            Hero.loadSprite();
            heroRole = Classes.Berserker;
            baseHealth = 500;
            healthPerLevel = 40;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
            abilities[2] = new Slam();
            abilities[3] = new DoubleSlash();
            abilities[4] = new Rage();
            abilities[5] = new Execute();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
