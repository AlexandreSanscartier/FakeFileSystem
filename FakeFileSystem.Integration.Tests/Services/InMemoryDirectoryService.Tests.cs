using FakeFileSystem.Factories;
using FakeFileSystem.Factories.FileSystems;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Services;
using Xunit;

namespace FakeFileSystem.Integration.Tests.Services
{
    public class InMemoryDirectoryServiceTest
    {
        private IFileSystem _fileSystem;
        private IDirectoryComponentFactory _directoryComponentFactory;
        private IPathService _pathService;
        private IDirectoryService _directoryService;
        private IDirectoryComponent _root;
        private IDirectoryInfoFactory _directoryInfoFactory;

        public InMemoryDirectoryServiceTest()
        {
            _directoryComponentFactory = new DirectoryComponentFactory();
            _root = _directoryComponentFactory.Create("C:");
            _fileSystem = new FileSystem(_root);
            _pathService = new InMemoryPathService(_fileSystem);
            _directoryInfoFactory = new InMemoryDirectoryInfoFactory(_pathService);
            _directoryService = new InMemoryDirectoryService(_directoryComponentFactory, _pathService, _fileSystem, _directoryInfoFactory);
        }

        private void InitializeFileSystem()
        {
            for(var i = 0; i < 10; i++)
            {
                var directory = _directoryComponentFactory.Create($"FakeDir{i}");
                _root.Add(directory);
            }
            var level1 = _directoryComponentFactory.Create("Level1");
            var level2 = _directoryComponentFactory.Create("Level2");
            var level3 = _directoryComponentFactory.Create("Level3");
            var level4 = _directoryComponentFactory.Create("Level4");
            level1.Add(level2);
            level2.Add(level3);
            level3.Add(level4);
            _root.Add(level1);
        }

        [Fact]
        public void DirectoryExists_WhenSetCurrentDirectoryAndRelativePath_ReturnsTrue()
        {
            // Arrange
            InitializeFileSystem();

            // Act
            _directoryService.SetCurrentDirectory(@"C:\level1\level2");
            var actual = _directoryService.DirectoryExists("level3");

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void DirectoryExists_WhenSetCurrentDirectoryAndAbsolutePath_ReturnsTrue()
        {
            // Arrange
            InitializeFileSystem();

            // Act
            _directoryService.SetCurrentDirectory(@"C:\level1\level2");
            var actual = _directoryService.DirectoryExists(@"C:\level1\level2\level3");

            // Assert
            Assert.True(actual);
        }
    }
}
