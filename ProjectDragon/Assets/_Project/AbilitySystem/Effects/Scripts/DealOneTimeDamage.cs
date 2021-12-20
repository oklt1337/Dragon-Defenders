using System;
using AbilitySystem.Effects.BaseEffects.Scripts;
using AbilitySystem.Intensity.Scripts;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;

namespace AbilitySystem.Effects.Scripts
{
    public class DealOneTimeDamage : Damage
    {
        public override void ApplyEffect(Entity.Scripts.Entity entity)
        {
            switch (intensity.intensityType)
            {
                case IntensityType.FixedValue:
                    switch (entity)
                    {
                        case Commander commander:
                            commander.TakeDamage(intensity.value);
                            break;
                        case Enemy enemy:
                            enemy.TakeDamage(intensity.value);
                            break;
                    }
                    break;
                case IntensityType.Percentage:
                {
                    float damage;
                    switch (entity)
                    {
                        case Commander commander:
                            damage = commander.CommanderStats.MaxHealth * intensity.value;
                            commander.TakeDamage(damage);
                            break;
                        case Enemy enemy:
                            damage = enemy.MaxHealth * intensity.value;
                            enemy.TakeDamage(damage);
                            break;
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            entity.Handler.Effects.Remove(this);
        }
    }
}