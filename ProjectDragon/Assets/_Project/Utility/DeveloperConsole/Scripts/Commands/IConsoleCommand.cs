namespace _Project.Utility.DeveloperConsole.Scripts.Commands
{
    public interface IConsoleCommand
    {
        string CommandLine { get; }
        bool Process(string[] args);
    }
}
