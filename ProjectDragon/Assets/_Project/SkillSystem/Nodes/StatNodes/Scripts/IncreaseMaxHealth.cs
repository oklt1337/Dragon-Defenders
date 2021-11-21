using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.CommanderStats.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseMaxHealth", fileName = "IncreaseMaxHealth")]
    public class IncreaseMaxHealth : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is CommanderStats commanderStats)
            {
                commanderStats.MAXHealth *= value;
            }
        }
    }
}