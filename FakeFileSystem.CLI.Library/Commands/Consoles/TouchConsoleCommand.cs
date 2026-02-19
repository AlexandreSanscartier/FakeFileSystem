using FakeFileSystem.CLI.ConsoleLibrary.Commands.Files;
using FakeFileSystem.CLI.Interfaces.Commands.Consoles;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles
{
    public sealed class TouchConsoleCommand : IConsoleCommandWithResult<Stream?>
    {
        private IConsoleCommandParameters _consoleCommandParameters;

        public Stream? Result { get; private set; }

        public TouchConsoleCommand(IConsoleCommandParameters consoleCommandParameters)
        {
            _consoleCommandParameters = consoleCommandParameters;
        }

        public void Execute()
        {
            var arguments = _consoleCommandParameters.Argument;
            var currentDirectory = _consoleCommandParameters.FFSystem.DirectoryService.GetCurrentDirectory();
            if (arguments.Equals(string.Empty))
            {
                _consoleCommandParameters.OutputWriter.Write("Enter a name for your file: ");
                arguments = _consoleCommandParameters.InputReader.ReadInput();
            }
            var filePath = _consoleCommandParameters.FFSystem.PathService.CombinePath(currentDirectory, arguments);
            _consoleCommandParameters.FileCommandFactory.SetPath(filePath);
            var command = _consoleCommandParameters.FileCommandFactory.Create(typeof(CreateFileCommand));
            Result = _consoleCommandParameters.CommandRunner.Execute<Stream>(command);
        }
    }
}
