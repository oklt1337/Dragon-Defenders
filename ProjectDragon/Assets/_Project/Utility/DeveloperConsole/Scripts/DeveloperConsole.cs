using System;
using System.Collections.Generic;
using System.Linq;
using Utility.DeveloperConsole.Scripts.Commands;

namespace Utility.DeveloperConsole.Scripts
{
    public class DeveloperConsole
    {
        #region Private Fields

        private readonly string prefix;
        private readonly IEnumerable<IConsoleCommand> commands;

        #endregion

        #region Constructor

        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            this.prefix = prefix;
            this.commands = commands;
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
            foreach (var command in commands)
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
            if (!inputValue.StartsWith(prefix))
                return;

            inputValue = inputValue.Remove(0, prefix.Length);

            var inputSplit = inputValue.Split(' ');
            var commandInput = inputSplit[0];
            var args = inputSplit.Skip(1).ToArray();
            
            ProcessCommand(commandInput, args);
        }

        #endregion
    }
}
