using FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public class ChangeDirectoryConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public ChangeDirectoryConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var argument = _consoleCommandParameters.Argument;
            var currentDirectory = _consoleCommandParameters.FFSystem.DirectoryService.GetCurrentDirectory();
            if (argument.Equals(string.Empty))
            {
                _consoleCommandParameters.OutputWriter.Write("Which directory do you  want to goto? ");
                argument = _consoleCommandParameters.InputReader.ReadInput();
            }
            var filePath = _consoleCommandParameters.FFSystem.PathService.CombinePath(currentDirectory, argument);
            _consoleCommandParameters.DirectoryCommandFactory.SetPath(filePath);
            var command = _consoleCommandParameters.DirectoryCommandFactory.Create(typeof(SetCurrentDirectoryCommand));
            _consoleCommandParameters.CommandRunner.Execute(command);
        }
    }
}
