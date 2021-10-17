using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Utility.DeveloperConsole.Scripts.Commands;

namespace _Project.Utility.DeveloperConsole.Scripts
{
    public class DeveloperConsole
    {
        #region Private Fields

        private readonly string _prefix;
        private readonly IEnumerable<IConsoleCommand> _commands;

        #endregion

        #region Constructor

        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            _prefix = prefix;
            _commands = commands;
        }
        
        #endregion

        #region Private Methdos

        /// <summary>
        /// Process input and try to execute command
        /// </summary>
        /// <param name="commandInput">commandInput</param>
        /// <param name="args">args</param>
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Process input and try to execute command
        /// </summary>
        /// <param name="inputValue">string</param>
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

        #endregion
    }
}
