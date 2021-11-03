using UnityEngine;

namespace Utility.DeveloperConsole.Scripts.Commands
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandLine = string.Empty;

        public string CommandLine => commandLine;
        /// <summary>
        /// Logic of command.
        /// </summary>
        /// <param name="args">string[]</param>
        /// <returns></returns>
        public abstract bool Process(string[] args);
    }
}
