using Abilities.EndAbilities.SingleShotSetRange.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseDamageSingleShotSetRange", fileName = "IncreaseDamageSingleShotSetRange")]
    public class IncreaseDamageSingleShotSetRange : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is SingleShotSetRangeAbility ability)
            {
                ability.Damage *= value;
            }
        }
    }
}