using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models;

namespace FakeFileSystem.Factories
{
    public class FileComponentFactory : IFileComponentFactory
    {
        public IFileComponent Create(string name)
        {
            return new FileComponent(name, string.Empty);
        }

        public IFileComponent Create(string name, string content)
        {
            return new FileComponent(name, content);
        }
    }
}
