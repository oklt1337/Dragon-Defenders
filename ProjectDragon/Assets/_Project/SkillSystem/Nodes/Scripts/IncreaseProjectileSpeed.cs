using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.Scripts
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
