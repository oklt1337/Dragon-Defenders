using UnityEngine;

namespace _Project.Scripts.Utility.DeveloperConsole.Commands
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandLine = string.Empty;

        public string CommandLine => commandLine;
        public abstract bool Process(string[] args);
    }
}
