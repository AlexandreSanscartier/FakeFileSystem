using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models.FileSystems;

namespace FakeFileSystem.Factories.FileSystems
{
    public class InMemoryDirectoryInfoFactory : IDirectoryInfoFactory
    {
        private readonly IPathService _pathService;
        public InMemoryDirectoryInfoFactory(IPathService pathService)
        {
            _pathService = pathService;
        }
        public IDirectoryInfo Create(IDirectoryComponent directoryComponent)
        {
            return new InMemoryDirectoryInfo(directoryComponent, _pathService);
        }
    }
}
