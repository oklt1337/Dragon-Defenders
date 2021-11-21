using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseBuffDuration", fileName = "IncreaseBuffDuration")]
    public class IncreaseBuffDuration : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is UtilityAbility ability) 
                ability.Duration *= value;
        }
    }
}