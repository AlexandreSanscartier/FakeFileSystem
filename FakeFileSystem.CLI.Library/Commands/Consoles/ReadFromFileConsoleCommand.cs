using FakeFileSystem.CLI.ConsoleLibrary.Commands.Files;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public sealed class ReadFromFileConsoleCommand : ICommand
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public ReadFromFileConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var argument = _consoleCommandParameters.Argument;
            var currentDirectory = _consoleCommandParameters.FFSystem.DirectoryService.GetCurrentDirectory();
            if (argument.Equals(string.Empty))
            {
                _consoleCommandParameters.OutputWriter.Write("Which file would you like to load? ");
                argument = _consoleCommandParameters.InputReader.ReadInput();
            }
            var filePath = _consoleCommandParameters.FFSystem.PathService.CombinePath(currentDirectory, argument);
            _consoleCommandParameters.FileCommandFactory.SetPath(filePath);
            var command = _consoleCommandParameters.FileCommandFactory.Create(typeof(ReadAllTextCommand));
            var result = _consoleCommandParameters.CommandRunner.Execute<string>(command);
            _consoleCommandParameters.OutputWriter.WriteLine($"{filePath} has content {result}");
        }
    }
}
