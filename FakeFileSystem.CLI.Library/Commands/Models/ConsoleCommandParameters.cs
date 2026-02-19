using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.CLI.Interfaces.Commands.Services;
using FakeFileSystem.CLI.Interfaces.Services;
using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public sealed class ConsoleCommandParameters : IConsoleCommandParameters
    {
        public IInputReader InputReader { get; private set; }

        public IOutputWriter OutputWriter { get; private set; }

        public IFFSystem FFSystem { get; private set; }

        public IDirectoryCommandFactory DirectoryCommandFactory { get; private set; }

        public IFileCommandFactory FileCommandFactory { get; private set; }

        public string Argument { get; private set; }

        public ICommandRunner CommandRunner { get; private set; }

        public ConsoleCommandParameters(IInputReader inputReader, IOutputWriter outputWriter, IFFSystem fFSystem, 
            IDirectoryCommandFactory directoryCommandFactory, IFileCommandFactory fileCommandFactory, ICommandRunner commandRunner)
        {
            InputReader = inputReader;
            OutputWriter = outputWriter;
            FFSystem = fFSystem;
            DirectoryCommandFactory = directoryCommandFactory;
            FileCommandFactory = fileCommandFactory;
            CommandRunner = commandRunner;
            Argument = string.Empty;
        }

        public void SetArguments(string arguments)
        {
            Argument = arguments;
        }
    }
}
