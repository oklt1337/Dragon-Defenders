using UnityEngine;

namespace Utility.DeveloperConsole.Scripts.Commands.Utility
{
    [CreateAssetMenu(fileName = "New Log Command", menuName = "Utilities/DeveloperConsole/Commands/Utility/Log Command")]
    public class LogCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            var logText = string.Join(" ", args);
            Debug.Log(logText);
            
            return true;
        }
    }
}
