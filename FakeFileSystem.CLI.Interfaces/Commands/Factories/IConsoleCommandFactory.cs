using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.Interfaces.Commands.Factories
{
    public interface IConsoleCommandFactory
    {
        void SetConsoleCommandParameter(IConsoleCommandParameters consoleCommandParameters);

        void SetArguments(string arguments);

        ICommand Create(Type consoleCommandType);
    }
}
