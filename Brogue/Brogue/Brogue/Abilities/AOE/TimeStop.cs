using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] class TimeStop : AreaOfEffect
    {
        public TimeStop()
        {
            name = "Time Stop";
            description = "The spellweaver stops time for the \nenemies around him.";
            radius = 4;
            isActuallyFilled = false;
            castSquares = new IntVec[40];
            baseDamage = 0;
            abilityCooldown = 20;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            abilitySprite = new Sprite(abilityLine, new IntVec(14, 0));
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return 0;
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            int damage = calculateDamage(heroLevel, heroDamage);
            cooldown = abilityCooldown;
            wasJustCast = true;
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter enemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (enemy != null)
                {
                    drawVisualEffect(hero, enemy);
                    enemy.DealElementalDamage(Enums.ElementAttributes.Ice, 7);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            isActuallyFilled = false;
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            //Engine.Engine.AddVisualAttack(enemy, "Hero/Bolt", .5f, 1.5f, .05f);
            //Audio.playSound("lightning", .5f);
        }
    }
}
