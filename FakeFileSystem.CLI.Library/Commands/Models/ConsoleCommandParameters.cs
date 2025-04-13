using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.CLI.Interfaces.Commands.Services;
using FakeFileSystem.CLI.Interfaces.Services;
using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public class ConsoleCommandParameters : IConsoleCommandParameters
    {
        private IInputReader _inputReader;

        private IOutputWriter _outputWriter;

        private IFFSystem _fFSystem;

        private IDirectoryCommandFactory _directoryCommandFactory;

        private IFileCommandFactory _fileCommandFactory;

        private ICommandRunner _commandRunner;

        private string _arguments;

        public IInputReader InputReader => _inputReader;

        public IOutputWriter OutputWriter => _outputWriter;

        public IFFSystem FFSystem => _fFSystem;

        public IDirectoryCommandFactory DirectoryCommandFactory => _directoryCommandFactory;

        public IFileCommandFactory FileCommandFactory => _fileCommandFactory;

        public string Argument => _arguments;

        public ICommandRunner CommandRunner => _commandRunner;

        public ConsoleCommandParameters(IInputReader inputReader, IOutputWriter outputWriter, IFFSystem fFSystem, 
            IDirectoryCommandFactory directoryCommandFactory, IFileCommandFactory fileCommandFactory, ICommandRunner commandRunner)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _fFSystem = fFSystem;
            _directoryCommandFactory = directoryCommandFactory;
            _fileCommandFactory = fileCommandFactory;
            _commandRunner = commandRunner;
            _arguments = string.Empty;
        }

        public void SetArguments(string arguments)
        {
            _arguments = arguments;
        }
    }
}
