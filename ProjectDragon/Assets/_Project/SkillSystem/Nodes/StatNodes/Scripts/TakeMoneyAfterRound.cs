using Abilities.VisitorPattern.Scripts;
using GamePlay.GameManager.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/TakeMoneyAfterRound", fileName = "TakeMoneyAfterRound")]
    public class TakeMoneyAfterRound : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            GameManager.Instance.OnGameStateChanged += TakeMoney;
        }

        private void TakeMoney(GameState state)
        {
            if (state == GameState.Build)
            {
                GameManager.Instance.PlayerModel.ModifyMoney(-(int) multiplier);
            }
        }
    }
}