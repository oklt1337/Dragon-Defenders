using UnityEngine;

namespace _Project.Utility.DeveloperConsole.Scripts.Commands
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandLine = string.Empty;

        public string CommandLine => commandLine;
        public abstract bool Process(string[] args);
    }
}
