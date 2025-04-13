using FakeFileSystem.Interfaces.Models;

namespace FakeFileSystem.Interfaces.Factories
{
    public interface IDirectoryComponentFactory
    {
        IDirectoryComponent Create(string name);
    }
}
