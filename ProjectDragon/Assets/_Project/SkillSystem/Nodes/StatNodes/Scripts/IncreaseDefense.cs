using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.CommanderStats.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseDefense", fileName = "IncreaseDefense")]
    public class IncreaseDefense : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is CommanderStats stats)
            {
                stats.Defense *= value;
            }
        }
    }
}