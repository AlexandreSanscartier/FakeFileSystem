using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.Interfaces.Commands.Models
{
    public interface IDirectoryCommandParameters
    {
        IDirectoryService DirectoryService { get; }

        IPathService PathService { get; }

        string DirectoryName { get; }

        string Path { get; }

        void SetDirectoryService(IDirectoryService directoryService);

        void SetPathService(IPathService pathService);

        void SetDirectoryName(string directoryName);

        void SetPath(string path);
    }
}
