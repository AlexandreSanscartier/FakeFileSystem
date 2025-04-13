using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Services;
using FakeFileSystem.CLI.Interfaces.Services;
using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.Interfaces.Commands.Models
{
    public interface IConsoleCommandParameters
    {
        IInputReader InputReader { get; }

        IOutputWriter OutputWriter { get; }

        IFFSystem FFSystem { get; }

        IDirectoryCommandFactory DirectoryCommandFactory { get; }

        IFileCommandFactory FileCommandFactory { get; }

        ICommandRunner CommandRunner { get; }

        string Argument { get; }

        void SetArguments(string arguments);
    }
}
