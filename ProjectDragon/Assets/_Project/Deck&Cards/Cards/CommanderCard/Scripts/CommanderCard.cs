using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using _Project.GamePlay.Player.Commander.CommanderModel.Scripts;
using UnityEngine;

namespace _Project.Deck_Cards.Cards.CommanderCard.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/CommanderCard", fileName = "CommanderCard")]
    public class CommanderCard : BaseCards.Scripts.BaseCards
    {
        [SerializeField] private CommanderModel commander;
        public CommanderModel Commander => commander;
    }
}
