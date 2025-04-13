using FakeFileSystem.Interfaces.Models;

namespace FakeFileSystem.Interfaces.Factories
{
    public interface IFileComponentFactory
    {
        IFileComponent Create(string name);

        IFileComponent Create(string name, string content);
    }
}
