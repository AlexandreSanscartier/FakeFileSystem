using FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Factories
{
    public sealed class ConsoleCommandFactory : IConsoleCommandFactory
    {
        private IConsoleCommandParameters _consoleCommandParameter;

        public ConsoleCommandFactory(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameter = consoleCommandParameters;
        }

        public void SetConsoleCommandParameter(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameter = consoleCommandParameters;
        }

        public void SetArguments(string arguments)
        {
            _consoleCommandParameter.SetArguments(arguments);
        }

        public ICommand Create(Type consoleCommandType)
        {
            return consoleCommandType switch
            {
                Type _ when consoleCommandType == typeof(TouchConsoleCommand) => BuildTouchConsoleCommand(),
                Type _ when consoleCommandType == typeof(ChangeDirectoryConsoleCommand) => BuildChangeDirectoryConsoleCommand(),
                Type _ when consoleCommandType == typeof(CreateDirectoryConsoleCommand) => BuildCreateDirectoryConsoleCommand(),
                Type _ when consoleCommandType == typeof(RemoveDirectoryConsoleCommand) => BuildRemoveDirectoryConsoleCommand(),
                Type _ when consoleCommandType == typeof(WriteToFileConsoleCommand) => BuildWriteToFileConsoleCommand(),
                Type _ when consoleCommandType == typeof(ReadFromFileConsoleCommand) => BuildReadFromFileConsoleCommand(),
                Type _ when consoleCommandType == typeof(ListFilesFoldersConsoleCommand) => BuildListFilesFoldersConsoleCommand(),
                Type _ when consoleCommandType == typeof(PresentWorkingDirectoryConsoleCommand) => BuildPresentWorkingDirectoryConsoleCommand(),
                _ => new NullCommand()
            };
        }

        private ICommand BuildTouchConsoleCommand()
        {
            return new TouchConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildChangeDirectoryConsoleCommand()
        {
            return new ChangeDirectoryConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildCreateDirectoryConsoleCommand()
        {
            return new CreateDirectoryConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildRemoveDirectoryConsoleCommand()
        {
            return new RemoveDirectoryConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildWriteToFileConsoleCommand()
        {
            return new WriteToFileConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildReadFromFileConsoleCommand()
        {
            return new ReadFromFileConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildListFilesFoldersConsoleCommand()
        {
            return new ListFilesFoldersConsoleCommand(_consoleCommandParameter);
        }

        private ICommand BuildPresentWorkingDirectoryConsoleCommand()
        {
            return new PresentWorkingDirectoryConsoleCommand(_consoleCommandParameter);
        }
    }
}
