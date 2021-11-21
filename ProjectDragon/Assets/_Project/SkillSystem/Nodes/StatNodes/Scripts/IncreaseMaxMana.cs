using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.CommanderStats.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/IncreaseMaxMana", fileName = "IncreaseMaxMana")]
    public class IncreaseMaxMana : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is CommanderStats commanderStats)
            {
                commanderStats.MAXMana *= value;
            }
        }
    }
}