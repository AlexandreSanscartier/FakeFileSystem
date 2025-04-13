using FakeFileSystem.CLI.ConsoleLibrary.Commands.Files;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public sealed class WriteToFileConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public WriteToFileConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var argument = _consoleCommandParameters.Argument;
            var currentDirectory = _consoleCommandParameters.FFSystem.DirectoryService.GetCurrentDirectory();
            if (argument.Equals(string.Empty))
            {
                _consoleCommandParameters.OutputWriter.Write("Which file would you like to write to? ");
                argument = _consoleCommandParameters.InputReader.ReadInput();
            }
            var filePath = _consoleCommandParameters.FFSystem.PathService.CombinePath(currentDirectory, argument);
            _consoleCommandParameters.FileCommandFactory.SetPath(filePath);
            _consoleCommandParameters.FileCommandFactory.SetContent("{ Test Content }, {Secret Content}");
            var command = _consoleCommandParameters.FileCommandFactory.Create(typeof(WriteAllTextCommand));
            _consoleCommandParameters.CommandRunner.Execute(command);
        }
    }
}
