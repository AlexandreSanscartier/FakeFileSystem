using FakeFileSystem.Interfaces.Models;

namespace FakeFileSystem.Interfaces.Factories
{
    public interface IFilePathFactory
    {
        IFilePath Create(string path, string fileName);
    }
}
