using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;

namespace FakeFileSystem.Factories
{
    public class FileComponentFactory : IFileComponentFactory
    {
        private readonly IPathService _pathService;

        public FileComponentFactory(IPathService pathService)
        {
            _pathService = pathService;
        }

        public IFileComponent Create(string name)
        {
            return new FileComponent(_pathService, name, string.Empty);
        }

        public IFileComponent Create(string name, string content)
        {
            return new FileComponent(_pathService, name, content);
        }
    }
}
