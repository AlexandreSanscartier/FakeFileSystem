using FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public class RemoveDirectoryConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public RemoveDirectoryConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var argument = _consoleCommandParameters.Argument;
            if (argument.Equals(string.Empty))
            {
                _consoleCommandParameters.OutputWriter.Write("Enter a name of a directory to delete: ");
                argument = _consoleCommandParameters.InputReader.ReadInput();
            }
            _consoleCommandParameters.DirectoryCommandFactory.SetPath(argument);
            var command = _consoleCommandParameters.DirectoryCommandFactory.Create(typeof(DeleteDirectoryCommand));
            _consoleCommandParameters.CommandRunner.Execute(command);
            _consoleCommandParameters.OutputWriter.WriteLine($"Successfully deleted folder {argument}");
        }
    }
}
