using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.Models.FileSystems
{
    public class InMemoryDirectoryInfo : IDirectoryInfo
    {
        private readonly IDirectoryComponent _directoryComponent;

        private readonly IPathService _pathService;

        public string FullPath { get; private set; }

        public IDirectoryComponent DirectoryComponent => _directoryComponent;

        public InMemoryDirectoryInfo(IDirectoryComponent directoryComponent, IPathService pathService)
        {
            _pathService = pathService;
            _directoryComponent = directoryComponent;
            FullPath = GenerateFullPath();
        }

        private string GenerateFullPath()
        {
            List<string> pathParts = new List<string>();
            IDirectoryComponent? currentPath = DirectoryComponent;
            do
            {
                pathParts.Add(currentPath.Name);
                currentPath = currentPath.Parent;
            } while (currentPath is not null);
            pathParts.Reverse();

            return _pathService.CombinePath(pathParts.ToArray());
        }
    }
}
