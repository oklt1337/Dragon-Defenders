using UnityEngine;

namespace Utility.DeveloperConsole.Scripts.Commands.Utility
{
    [CreateAssetMenu(fileName = "New Clear Console Command", menuName = "Utilities/DeveloperConsole/Commands/Utility/Clear Console Command")]
    public class ClearCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length > 0)
                return false;

            DeveloperConsoleManager.Instance.ClearConsole();
            return true;
        }
    }
}