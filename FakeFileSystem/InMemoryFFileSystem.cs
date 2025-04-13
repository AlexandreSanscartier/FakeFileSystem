using FakeFileSystem.Factories;
using FakeFileSystem.Factories.FileSystems;
using FakeFileSystem.Interfaces;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Services;

namespace FakeFileSystem
{
    public class InMemoryFFileSystem : IFFSystem
    {
        public IDirectoryService DirectoryService => _directoryService;

        public IFileService FileService => _fileService;

        public IPathService PathService => _pathService;

        private readonly IDirectoryService _directoryService;

        private IFileService _fileService;

        private IPathService _pathService;

        private IDirectoryComponentFactory _directoryComponentFactory;

        private IDirectoryInfoFactory _directoryInfoFactory;

        private IFileSystem _fileSystem;

        private IFileComponentFactory _fileComponentFactory;

        private IMemoryStreamFactory _memoryStreamFactory;

        private IFilePathFactory _filePathFactory;

        private IDirectoryComponent _root;

        public InMemoryFFileSystem()
        {
            _directoryComponentFactory = new DirectoryComponentFactory();
            _root = _directoryComponentFactory.Create("C:");
            _fileSystem = new FileSystem(_root);
            _fileComponentFactory = new FileComponentFactory();
            _memoryStreamFactory = new MemoryStreamFactory();
            _filePathFactory = new FilePathFactory();

            _pathService = new InMemoryPathService(_fileSystem);
            _directoryInfoFactory = new InMemoryDirectoryInfoFactory(_pathService);
            _directoryService = new InMemoryDirectoryService(_directoryComponentFactory, _pathService, _fileSystem, _directoryInfoFactory);
            _fileService = new InMemoryFileService(_fileComponentFactory, _memoryStreamFactory, _directoryService, _pathService, _filePathFactory);
        }

        public InMemoryFFileSystem(string rootFolder)
        {
            _directoryComponentFactory = new DirectoryComponentFactory();
            _root = _directoryComponentFactory.Create(rootFolder);
            _fileSystem = new FileSystem(_root);
            _fileComponentFactory = new FileComponentFactory();
            _memoryStreamFactory = new MemoryStreamFactory();
            _filePathFactory = new FilePathFactory();

            _pathService = new InMemoryPathService(_fileSystem);
            _directoryInfoFactory = new InMemoryDirectoryInfoFactory(_pathService);
            _directoryService = new InMemoryDirectoryService(_directoryComponentFactory, _pathService, _fileSystem, _directoryInfoFactory);
            _fileService = new InMemoryFileService(_fileComponentFactory, _memoryStreamFactory, _directoryService, _pathService, _filePathFactory);
        }
    }
}
