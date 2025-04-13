using FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public class PresentWorkingDirectoryConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public PresentWorkingDirectoryConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var command = _consoleCommandParameters.DirectoryCommandFactory.Create(typeof(PresentWorkingDirectoryCommand));
            _consoleCommandParameters.CommandRunner.Execute(command);
        }
    }
}
