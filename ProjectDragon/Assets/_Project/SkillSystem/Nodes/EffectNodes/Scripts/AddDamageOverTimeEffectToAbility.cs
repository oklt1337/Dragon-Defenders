using Abilities.Ability.Scripts;
using Abilities.Effects.DamageOverTimeEffect.Scripts;
using Abilities.VisitorPattern.Scripts;
using AI.Enemies.Base_Enemy.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/AddDamageOverTimeEffectToAbility", fileName = "AddDamageOverTimeEffectToAbility")]
    public class AddDamageOverTimeEffectToAbility : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private float duration;
        [SerializeField] private float tick;
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility ability)
            {
                ability.Casted += (enemyTrans) =>
                {
                    var enemy = enemyTrans.GetComponent<Enemy>();
                    if (enemy == null) 
                        return;
                    var effect = enemy.GetComponent<DamageOverTimeEffect>();
                    if (effect != null)
                        Destroy(effect);

                    effect = enemy.gameObject.AddComponent<DamageOverTimeEffect>();
                    effect.Init(duration, value, tick);
                };
            }
        }
    }
}