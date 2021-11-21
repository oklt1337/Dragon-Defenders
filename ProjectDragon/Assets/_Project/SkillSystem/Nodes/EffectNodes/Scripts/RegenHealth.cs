using Abilities.Effects.ReduceDamageAbilityCooldown.Scripts;
using Abilities.Effects.RegenHealthOverTime.Scripts;
using Abilities.EndAbilities.DecreaseCooldownOfDamageAbility.Scripts;
using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/RegenHealth", fileName = "RegenHealth")]
    public class RegenHealth : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private float tick;
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DecreaseCooldownOfDamageAbility ability)
            {
                ability.Casted += (t) =>
                {
                    var commander = t.GetComponent<Commander>();
                    if (commander == null) 
                        return;
                    var effect = commander.GetComponent<ReduceDamageAbilityCooldownEffect>();
                    if (effect == null) 
                        return;
                    var regenEffect = commander.gameObject.AddComponent<RegenHealthOverTimeEffect>();
                    regenEffect.Init(value, tick);
                    effect.OnEffectDestroyed += regenEffect.Destroy;
                };
            }
        }
    }
}