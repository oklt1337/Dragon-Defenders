using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Utility.DeveloperConsole.Commands;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Utility.DeveloperConsole
{
    public class DeveloperConsole
    {
        private readonly string _prefix;
        private readonly IEnumerable<IConsoleCommand> _commands;
        
        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            _prefix = prefix;
            _commands = commands;
        }

        public void ProcessCommand(string inputValue)
        {
            if (!inputValue.StartsWith(_prefix))
                return;

            inputValue = inputValue.Remove(0, _prefix.Length);

            string[] inputSplit = inputValue.Split(' ');
            string commandInput = inputSplit[0];
            string[] args = inputSplit.Skip(1).ToArray();
            
            ProcessCommand(commandInput, args);
        }

        private void ProcessCommand(string commandInput, string[] args)
        {
            foreach (IConsoleCommand command in _commands)
            {
                if (!commandInput.Equals(command.CommandLine, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (command.Process(args))
                {
                    return;
                }
            }
        }
    }
}
