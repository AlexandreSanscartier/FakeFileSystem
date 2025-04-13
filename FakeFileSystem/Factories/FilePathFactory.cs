using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models;

namespace FakeFileSystem.Factories
{
    public class FilePathFactory : IFilePathFactory
    {
        public IFilePath Create(string path, string fileName)
        {
            return FilePath.From((path, fileName));
        }
    }
}
