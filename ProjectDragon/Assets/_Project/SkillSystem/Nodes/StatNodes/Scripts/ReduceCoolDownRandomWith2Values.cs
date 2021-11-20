using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/ReduceCoolDownRandomWith2Values", fileName = "ReduceCoolDownRandomWith2Values")]
    public class ReduceCoolDownRandomWith2Values : StatNode
    {
        [SerializeField] private float range;
        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is Ability ability)) 
                return;
            ability.CoolDown *= UnityEngine.Random.Range(multiplier, range);
        }
    }
}