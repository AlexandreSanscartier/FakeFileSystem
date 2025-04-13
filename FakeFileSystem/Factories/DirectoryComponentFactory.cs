using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models;

namespace FakeFileSystem.Factories
{
    public class DirectoryComponentFactory : IDirectoryComponentFactory
    {
        public IDirectoryComponent Create(string name)
        {
            return DirectoryComponent.From(name);
        }
    }
}
