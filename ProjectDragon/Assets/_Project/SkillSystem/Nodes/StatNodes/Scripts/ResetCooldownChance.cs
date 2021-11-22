using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/ResetCooldownChance", fileName = "ResetCooldownChance")]
    public class ResetCooldownChance : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is Ability ability)
            {
                ability.Casted += transform =>
                {
                    ResetCooldown(ability);
                };
            }
        }

        private void ResetCooldown(Ability ability)
        {
            var percentage = Random.Range(0, 1);
            if (percentage < value)
                ability.TimeLeft = 0;
        }
    }
}