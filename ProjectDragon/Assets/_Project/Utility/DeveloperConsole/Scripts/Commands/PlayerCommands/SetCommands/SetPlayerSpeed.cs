using GamePlay.GameManager.Scripts;
using UnityEngine;
using Utility.SceneManager.Scripts;

namespace Utility.DeveloperConsole.Scripts.Commands.PlayerCommands.SetCommands
{
    [CreateAssetMenu(fileName = "New Set Player Speed Command",
        menuName = "Utilities/DeveloperConsole/Commands/Player/Set Commands/Set Player Speed Command")]
    public class SetPlayerSpeed : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length != 1)
                return false;
            if (!float.TryParse(args[0], out var value))
                return false;

            if (SceneManager.Scripts.SceneManager.CurrentScene != Scene.GameScene) 
                return false;
            GameManager.Instance.PlayerModel.Commander.CommanderStats.Speed = value;
            return true;
        }
    }
}