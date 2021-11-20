using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/KnockBack", fileName = "KnockBack")]
    public class KnockBack : BaseNodes.Scripts.EffectNodes
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility ability)
            {
                ability.Casted += AddKnockBack;
            }
        }

        private void AddKnockBack(Transform target)
        {
            var position = target.position;
            position -= new Vector3(position.x - value, position.y, position.z);
            target.position = position;
        }
    }
}