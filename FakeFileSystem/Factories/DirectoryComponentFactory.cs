using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;

namespace FakeFileSystem.Factories
{
    public class DirectoryComponentFactory : IDirectoryComponentFactory
    {
        private readonly IPathService _pathService;
        public DirectoryComponentFactory(IPathService pathService)
        {
            _pathService = pathService;
        }

        public IDirectoryComponent Create(string name)
        {
            return new DirectoryComponent(_pathService, name);
        }
    }
}
