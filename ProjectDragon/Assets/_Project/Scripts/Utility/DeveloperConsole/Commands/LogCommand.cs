using UnityEngine;

namespace _Project.Scripts.Utility.DeveloperConsole.Commands
{
    [CreateAssetMenu(fileName = "New Log Command", menuName = "Utilities/DeveloperConsole/Commands/Log Command")]
    public class LogCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            string logText = string.Join(" ", args);
            Debug.Log(logText);
            
            return true;
        }
    }
}
