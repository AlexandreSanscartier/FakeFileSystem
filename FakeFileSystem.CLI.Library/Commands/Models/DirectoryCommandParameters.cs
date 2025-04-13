using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public sealed class DirectoryCommandParameters : IDirectoryCommandParameters
    {
        public IDirectoryService DirectoryService { get; set; }

        public IPathService PathService { get; set; }

        public string DirectoryName { get; set; }

        public string Path { get; set; }

        public DirectoryCommandParameters(IDirectoryService directoryService, IPathService pathService)
        {
            DirectoryService = directoryService;
            PathService = pathService;
            DirectoryName = string.Empty;
            Path = string.Empty;
        }

        public DirectoryCommandParameters(IDirectoryService directoryService, IPathService pathService, string path, string directoryName)
        {
            DirectoryService = directoryService;
            PathService = pathService;
            Path = path;
            DirectoryName = directoryName;
        }
    }
}
