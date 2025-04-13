using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Models.FileSystems;
using FakeFileSystem.Services;
using FakeFileSystem.Unit.Tests.Mocks;
using Moq;
using System.IO;
using System.Linq;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Services
{
    public class InMemoryDirectoryServiceTests
    {
        private Mock<IFileSystem> _fileSystemMock;
        private Mock<IDirectoryComponentFactory> _directoryComponentFactoryMock;
        private Mock<IDirectoryInfoFactory> _directoryInfoFactoryMock;
        private Mock<IPathService> _pathServiceMock;

        private IDirectoryComponent _root;
        private IDirectoryComponent _level1;
        private IDirectoryComponent _level2;
        private IDirectoryComponent _level3;

        public InMemoryDirectoryServiceTests()
        {
            _fileSystemMock = new Mock<IFileSystem>();
            _directoryComponentFactoryMock = new Mock<IDirectoryComponentFactory>();
            _pathServiceMock = new Mock<IPathService>();
            _directoryInfoFactoryMock = new Mock<IDirectoryInfoFactory>();
            /*
             * TODO: Put this in a mock file that mocks different file system configurations.
             * The result of of the code is below
             * C:\FakeDir1
             *      - FakeDir2
             *          - FakeDir3
             *      - FakeDir2A
             *      - FakeDir2B
             *      - FakeDir2C
             *      - FakeFile.txt
             **/
            _root = DirectoryComponent.From(@"C:");
            _level1 = DirectoryComponent.From("FakeDir");
            _level2 = DirectoryComponent.From("FakeDir2");
            var level2a = DirectoryComponent.From("FakeDir2A");
            var level2b = DirectoryComponent.From("FakeDir2B");
            var level2c = DirectoryComponent.From("FakeDir2C");
            var level2File = new FileComponent("FakeFile.txt", "Fake content");
            _level3 = DirectoryComponent.From("FakeDir3");
            _level1.Add(_level2);
            _level1.Add(level2a);
            _level1.Add(level2b);
            _level1.Add(level2c);
            _level1.Add(level2File);
            _level2.Add(_level3);
            _root.Add(_level1);
        }

        private IDirectoryService BuildInMemoryDirectoryService()
        {
            return new InMemoryDirectoryService(_directoryComponentFactoryMock.Object, _pathServiceMock.Object,
                _fileSystemMock.Object, _directoryInfoFactoryMock.Object);
        }

        [Fact]
        public void CreateDirectory_WhenDirectoryExists_ReturnsDirectoryInfo()
        {
            // Arrange
            var directoryName = "FakeDir4";
            var directoryToAdd = DirectoryComponent.From(directoryName);
            var directoryInfo = new InMemoryDirectoryInfo(directoryToAdd, _pathServiceMock.Object);
            var path = @"C:\FakeDir\FakeDir2\FakeDir3\FakeDir4";
            var pathWithoutNewDirectory = @"C:\FakeDir\FakeDir2\FakeDir3";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3", "FakeDir4" };
            var splitPathWithoutNewDirectory = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3" };
            var parentDirectory = DirectoryComponent.From(splitPath.SkipLast(1).Last());
            var parentDirectoryInfo = new InMemoryDirectoryInfo(parentDirectory, _pathServiceMock.Object);
            var rootDirectory = DirectoryComponent.From(splitPath.First());
            var rootDirectoryInfo = new InMemoryDirectoryInfo(rootDirectory, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _directoryComponentFactoryMock.Setup(x => x.Create(directoryName)).Returns(directoryToAdd);
            _directoryInfoFactoryMock.Setup(x => x.Create(directoryToAdd)).Returns(directoryInfo);
            _directoryInfoFactoryMock.Setup(x => x.Create(parentDirectory)).Returns(parentDirectoryInfo);
            _directoryInfoFactoryMock.Setup(x => x.Create(rootDirectory)).Returns(rootDirectoryInfo);            
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.SplitPath(pathWithoutNewDirectory)).Returns(splitPathWithoutNewDirectory);
            _pathServiceMock.Setup(x => x.CombinePath(splitPathWithoutNewDirectory)).Returns(pathWithoutNewDirectory);
            var expected = directoryInfo;
            var expectedParentName = "FakeDir3";

            // directory = FindDirectory(_pathService.CombinePath(currentPathParts.SkipLast(1).ToArray())).DirectoryComponent;

            // Act
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var actual = inMemoryDirectoryService.CreateDirectory(path);

            // Assert
            Assert.Equal(expectedParentName, actual.DirectoryComponent?.Parent?.Name);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DirectoryExists_WhenAbsolutePathExists_ReturnsTrue()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3" };
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            var actual = inMemoryDirectoryService.DirectoryExists(path);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void DirectoryExists_WhenRelativePathExists_ReturnsTrue()
        {
            // Arrange
            var path = @"FakeDir";
            var splitPath = new string[] { "FakeDir" };
            var directory = DirectoryComponent.From(path);
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            var actual = inMemoryDirectoryService.DirectoryExists(path);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void DirectoryExists_WhenRelativePathDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var path = @"FakeDir2";
            var splitPath = new string[] { "FakeDir2" };
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            var actual = inMemoryDirectoryService.DirectoryExists(path);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void DirectoryExists_WhenDifferentRoot_ReturnsFalse()
        {
            // Arrange
            var path = @"D:\";
            var splitPath = new string[] { "D:" };
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(path)).Returns(true);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            var actual = inMemoryDirectoryService.DirectoryExists(path);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void SetCurrentDirectory_WhenPathExists_Successful()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2" };
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _pathServiceMock.Setup(x => x.CombinePath(splitPath)).Returns(path);

            var directoryInfo = new InMemoryDirectoryInfo(_level2, _pathServiceMock.Object);
            _directoryInfoFactoryMock.Setup(x => x.Create(_level2)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = @"C:\FakeDir\FakeDir2";

            // Act
            inMemoryDirectoryService.SetCurrentDirectory(path);
            var actual = inMemoryDirectoryService.GetCurrentDirectory();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetCurrentDirectory_WhenPathDoesNotExist_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            var path = @"C:\FakeDir1\FakeDir2";
            var splitPath = new string[] { "C:", "FakeDir1", "FakeDir2" };
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Assert
            Assert.Throws<DirectoryNotFoundException>(() => inMemoryDirectoryService.SetCurrentDirectory(path));
        }

        [Fact]
        public void GetDirectories_WhenHasDirectories_ReturnsDirectoryPaths()
        {
            // Arrange
            var path = @"C:\FakeDir";
            var splitPath = new string[] { "C:", "FakeDir" };
            var directoryInfo = new InMemoryDirectoryInfo(_level1, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(_level1)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[] { "FakeDir2", "FakeDir2A", "FakeDir2B", "FakeDir2C" };

            // Act
            var actual = inMemoryDirectoryService.GetDirectories(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDirectories_WhenHasNoDirectories_ReturnsEmptyList()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3" };
            var directoryInfo = new InMemoryDirectoryInfo(_level3, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(_level3)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[0];

            // Act
            var actual = inMemoryDirectoryService.GetDirectories(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDirectories_WhenPathDoesNotExist_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3\FakeDir4";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3", "FakeDir4" };
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[0];

            // Assert
            Assert.Throws<DirectoryNotFoundException>(() => inMemoryDirectoryService.GetDirectories(path));
        }

        [Fact]
        public void GetFiles_WhenHasDirectories_ReturnsFilePaths()
        {
            // Arrange
            var path = @"C:\FakeDir";
            var splitPath = new string[] { "C:", "FakeDir" };
            var directoryInfo = new InMemoryDirectoryInfo(_level1, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(_level1)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[] { "FakeFile.txt" };

            // Act
            var actual = inMemoryDirectoryService.GetFiles(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFiles_WhenHasNoDirectories_ReturnsEmptyList()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3" };
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[0];

            // Act
            var actual = inMemoryDirectoryService.GetFiles(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFiles_WhenPathDoesNotExist_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3\FakeDir4";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3", "FakeDir4" };
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object    );
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();
            var expected = new string[0];

            // Assert
            Assert.Throws<DirectoryNotFoundException>(() => inMemoryDirectoryService.GetFiles(path));
        }

        [Fact]
        public void DeleteDirectory_WhenPathExistsAndEmpty_DeletesSuccessfully()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2\FakeDir3";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2", "FakeDir3" };
            var parentDirectory = DirectoryComponent.From(splitPath.SkipLast(1).Last());
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            parentDirectory.Add(directory);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            inMemoryDirectoryService.DeleteDirectory(path);

            // Assert
            Assert.Empty(parentDirectory.GetFileSystemComponents());
        }

        [Fact]
        public void DeleteDirectory_WhenPathExistsAndRecursive_DeletesSuccessfully()
        {
            // Arrange
            var path = @"C:\FakeDir\FakeDir2";
            var splitPath = new string[] { "C:", "FakeDir", "FakeDir2" };
            var parentDirectory = DirectoryComponent.From(splitPath.SkipLast(1).Last());
            var directory = DirectoryComponent.From(splitPath.Last());
            var directoryInfo = new InMemoryDirectoryInfo(directory, _pathServiceMock.Object);
            var parentDirectoryInfo = new InMemoryDirectoryInfo(parentDirectory, _pathServiceMock.Object);
            parentDirectory.Add(directory);
            _fileSystemMock.Setup(x => x.Root).Returns(_root);
            _directoryInfoFactoryMock.Setup(x => x.Create(directory)).Returns(directoryInfo);
            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(splitPath);
            _pathServiceMock.Setup(x => x.IsPathRooted(splitPath.First())).Returns(true);
            var inMemoryDirectoryService = BuildInMemoryDirectoryService();

            // Act
            inMemoryDirectoryService.DeleteDirectory(path, true);

            // Assert
            Assert.Empty(parentDirectory.GetFileSystemComponents());
        }
    }
}
