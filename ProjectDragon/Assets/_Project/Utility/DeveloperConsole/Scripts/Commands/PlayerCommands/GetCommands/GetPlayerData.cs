using GamePlay.GameManager.Scripts;
using UnityEngine;
using Utility.SceneManager.Scripts;

namespace Utility.DeveloperConsole.Scripts.Commands.PlayerCommands.GetCommands
{
    [CreateAssetMenu(fileName = "New GetPlayer Data",
        menuName = "Utilities/DeveloperConsole/Commands/Player/Get Commands/Get Player Data")]
    public class GetPlayerData : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length > 0)
                return false;
            if (SceneManager.Scripts.SceneManager.CurrentScene != Scene.GameScene) 
                return false;

            var player = GameManager.Instance.PlayerModel;
            DeveloperConsoleManager.Instance.Print("Player Data");
            DeveloperConsoleManager.Instance.Print(string.Concat("Current State: ", player.CurrentState));
            DeveloperConsoleManager.Instance.Print(string.Concat("Money: ", player.Money));
            return true;
        }
    }
}