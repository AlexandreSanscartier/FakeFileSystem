using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.Interfaces.Commands.Factories
{
    public interface IDirectoryCommandFactory
    {
        void SetDirectoryCommandParameter(IDirectoryCommandParameters directoryCommandParameters);

        ICommand Create(Type directoryCommandType);

        void SetPath(string path);
    }
}
