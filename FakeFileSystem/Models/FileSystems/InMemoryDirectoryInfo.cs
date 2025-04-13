using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Models.FileSystems
{
    public class InMemoryDirectoryInfo : IDirectoryInfo
    {
        private readonly IDirectoryComponent _directoryComponent;

        private string _fullPath = string.Empty;

        public IDirectoryComponent DirectoryComponent => _directoryComponent;

        public InMemoryDirectoryInfo(IDirectoryComponent directoryComponent)
        {
            _directoryComponent = directoryComponent;
            _fullPath = GenerateFullPath();
        }
        public string FullPath => _fullPath;

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

            return Path.Combine(pathParts.ToArray());
        }
    }
}
