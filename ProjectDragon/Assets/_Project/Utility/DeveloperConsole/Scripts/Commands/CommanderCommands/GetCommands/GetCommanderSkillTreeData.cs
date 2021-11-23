using GamePlay.GameManager.Scripts;
using UnityEngine;
using Utility.SceneManager.Scripts;

namespace Utility.DeveloperConsole.Scripts.Commands.CommanderCommands.GetCommands
{
    [CreateAssetMenu(fileName = "New Get Commander SkillTree Data",
        menuName = "Utilities/DeveloperConsole/Commands/Comander/Get Commands/Get Commander SkillTree Data")]
    public class GetCommanderSkillTreeData : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length > 0)
                return false;
            if (SceneManager.Scripts.SceneManager.CurrentScene != Scene.GameScene)
                return false;
            var skillTree = GameManager.Instance.PlayerModel.Commander.SkillTree;
            DeveloperConsoleManager.Instance.Print("SkillTree");
            foreach (var skill in skillTree.Nodes)
            {
                DeveloperConsoleManager.Instance.Print(string.Concat("Name:", skill.NodeObj.NodeName));
                DeveloperConsoleManager.Instance.Print(string.Concat("State: ", skill.NodeState));
                DeveloperConsoleManager.Instance.Print(string.Concat("Cost: ", skill.NodeObj.Cost));
                DeveloperConsoleManager.Instance.Print(string.Empty);
            }
            return true;
        }
    }
}