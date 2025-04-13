using FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public class ListFilesFoldersConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public ListFilesFoldersConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var currentDirectory = _consoleCommandParameters.FFSystem.DirectoryService.GetCurrentDirectory();
            _consoleCommandParameters.DirectoryCommandFactory.SetPath(currentDirectory);
            var command = _consoleCommandParameters.DirectoryCommandFactory.Create(typeof(ListAllCommand));
            _consoleCommandParameters.CommandRunner.Execute(command);
        }
    }
}
