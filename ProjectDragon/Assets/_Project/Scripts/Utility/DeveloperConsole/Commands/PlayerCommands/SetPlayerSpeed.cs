using UnityEngine;

namespace _Project.Scripts.Utility.DeveloperConsole.Commands.PlayerCommands
{
    [CreateAssetMenu(fileName = "New Set Player Speed Command",
        menuName = "Utilities/DeveloperConsole/Commands/Player/Set Player Speed Command")]
    public class SetPlayerSpeed : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length != 1)
                return false;

            if (!float.TryParse(args[0], out float value))
                return false;

            //PlayerClass.SetSpeed(value);
            return true;
        }
    }
}
