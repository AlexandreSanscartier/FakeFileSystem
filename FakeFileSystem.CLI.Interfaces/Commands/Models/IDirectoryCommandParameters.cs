using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.Interfaces.Commands.Models
{
    public interface IDirectoryCommandParameters
    {
        IDirectoryService DirectoryService { get; set; }

        IPathService PathService { get; set; }

        string DirectoryName { get; set; }

        string Path { get; set; }
    }
}
