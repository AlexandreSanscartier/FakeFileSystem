using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Interfaces.Factories.FileSystems
{
    public interface IDirectoryInfoFactory
    {
        IDirectoryInfo Create(IDirectoryComponent directoryComponent);
    }
}
