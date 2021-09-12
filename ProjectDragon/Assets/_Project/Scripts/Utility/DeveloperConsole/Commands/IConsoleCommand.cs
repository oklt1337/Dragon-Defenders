namespace _Project.Scripts.Utility.DeveloperConsole.Commands
{
    public interface IConsoleCommand
    {
        string CommandLine { get; }
        bool Process(string[] args);
    }
}
