using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public class DirectoryCommandParameters : IDirectoryCommandParameters
    {
        private IDirectoryService _directoryService;

        private IPathService _pathService;

        private string _directoryName;

        private string _path;

        public DirectoryCommandParameters(IDirectoryService directoryService, IPathService pathService, string path, string directoryName)
        {
            _directoryService = directoryService;
            _pathService = pathService;
            _path = path;
            _directoryName = directoryName;
        }

        public IDirectoryService DirectoryService => _directoryService;

        public IPathService PathService => _pathService;

        public string DirectoryName => _directoryName;

        public string Path => _path;

        public void SetDirectoryName(string directoryName)
        {
            _directoryName = directoryName;
        }

        public void SetDirectoryService(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public void SetPathService(IPathService pathService)
        {
            _pathService = pathService;
        }
    }
}
