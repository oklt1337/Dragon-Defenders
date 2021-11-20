using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseProjectileSpeed", fileName = "IncreaseProjectileSpeed")]
    public class IncreaseProjectileSpeed : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is SingleShotAbility ability)) 
                return;
            ability.ProjectileSpeed *= multiplier;
        }
    }
}
