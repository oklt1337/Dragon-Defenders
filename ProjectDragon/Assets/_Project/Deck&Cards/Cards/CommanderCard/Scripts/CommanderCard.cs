using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace _Project.Deck_Cards.Cards.CommanderCard.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/CommanderCard", fileName = "CommanderCard")]
    public class CommanderCard : BaseCards.Scripts.BaseCards
    {
        [SerializeField] private Commander commander;
        public Commander Commander => commander;
    }
}
