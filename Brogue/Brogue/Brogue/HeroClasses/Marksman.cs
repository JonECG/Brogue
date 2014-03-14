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
    public class Marksman : Hero
    {
        public Marksman()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Marksman");
            Hero.loadSprite();
            heroRole = Classes.Marksman;
            baseHealth = 400;
            healthPerLevel = 55;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Mug();
            abilities[1] = new Invisibility();
            abilities[2] = new Volley();
            abilities[3] = new SteadyShot();
            abilities[4] = new Vault();
            abilities[5] = new EagleEye();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
