using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Models.FileSystems;

namespace FakeFileSystem.Factories.FileSystems
{
    public class InMemoryDirectoryInfoFactory : IDirectoryInfoFactory
    {
        public IDirectoryInfo Create(IDirectoryComponent directoryComponent)
        {
            return new InMemoryDirectoryInfo(directoryComponent);
        }
    }
}
