using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Models.FileSystems;
using FakeFileSystem.Services;
using Moq;
using System.IO;
using System.Text;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Services
{
    public class InMemoryFileServiceTests
    {
        private readonly Mock<IFileComponentFactory> _fileComponentFactoryMock;

        private readonly Mock<IMemoryStreamFactory> _memoryStreamFactoryMock;

        private readonly Mock<IDirectoryService> _directoryServiceMock;

        private readonly Mock<IPathService> _pathServiceMock;

        private readonly Mock<IFileSystem> _fileSystemMock;

        private readonly Mock<IFilePathFactory> _filePathFactory;

        private IDirectoryComponent _root;

        private IDirectoryComponent _fakeDir;

        private IFileComponent _fakeFile;

        public InMemoryFileServiceTests()
        {
            _fileComponentFactoryMock = new Mock<IFileComponentFactory>();
            _memoryStreamFactoryMock = new Mock<IMemoryStreamFactory>();
            _directoryServiceMock = new Mock<IDirectoryService>();
            _pathServiceMock = new Mock<IPathService>();
            _fileSystemMock = new Mock<IFileSystem>();
            _filePathFactory = new Mock<IFilePathFactory>();

            _fakeFile = new FileComponent("FakeFile.txt", "Test Content");
            _fakeDir = DirectoryComponent.From("FakeDir");
            _root = DirectoryComponent.From("C:");
            _root.Add(_fakeDir);
            _root.Add(_fakeFile);
        }

        private IFileService BuildInMemoryFileService()
        {
            return new InMemoryFileService(_fileComponentFactoryMock.Object, _memoryStreamFactoryMock.Object,
                _directoryServiceMock.Object, _pathServiceMock.Object, _filePathFactory.Object);
        }

        [Fact]
        public void CreateFile_WhenValidPath_ReturnsEmptyMemoryStream()
        {
            // Arrange
            var path = @"C:\FakeDir\Fakefile.txt";
            var pathWithoutFile = @"C:\FakeDir";
            var fileName = "Fakefile.txt";
            var pathPartsWithFile = new string[] { "C:", "FakeDir", fileName };
            var pathParts = new string[] { "C:", "FakeDir" };
            var expected = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            var directoryComponent = DirectoryComponent.From("FakeDir");
            var directoryInfo = new InMemoryDirectoryInfo(_fakeDir);
            var fileComponent = new FileComponent(fileName, string.Empty);                

            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(pathPartsWithFile);
            _pathServiceMock.Setup(x => x.CombinePath(pathParts)).Returns(pathWithoutFile);
            _directoryServiceMock.Setup(x => x.DirectoryExists(pathWithoutFile)).Returns(true);
            _directoryServiceMock.Setup(x => x.GetDirectory(pathWithoutFile)).Returns(directoryInfo);
            _fileComponentFactoryMock.Setup(x => x.Create("Fakefile.txt")).Returns(fileComponent);
            _memoryStreamFactoryMock.Setup(x => x.Create(string.Empty)).Returns(expected);
            _filePathFactory.Setup(x => x.Create(pathWithoutFile, fileName)).Returns(FilePath.From((pathWithoutFile, fileName)));

            var inMemoryFileService = BuildInMemoryFileService();

            // Act
            var actual = inMemoryFileService.CreateFile(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteFile_WhenValidPath()
        {
            // Arrange
            var path = @"C:\FakeFile.txt";
            var pathWithoutFile = @"C:\";
            var fileName = "FakeFile.txt";
            var pathPartsWithFile = new string[] { "C:", fileName };
            var pathParts = new string[] { "C:" };
            var directoryInfo = new InMemoryDirectoryInfo(_root);
            var fileComponent = new FileComponent(fileName, "Test Content");

            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(pathPartsWithFile);
            _pathServiceMock.Setup(x => x.CombinePath(pathParts)).Returns(pathWithoutFile);
            _directoryServiceMock.Setup(x => x.DirectoryExists(pathWithoutFile)).Returns(true);
            _directoryServiceMock.Setup(x => x.GetDirectory(pathWithoutFile)).Returns(directoryInfo);
            _filePathFactory.Setup(x => x.Create(pathWithoutFile, fileName)).Returns(FilePath.From((pathWithoutFile, fileName)));

            var inMemoryFileService = BuildInMemoryFileService();

            // Act
            inMemoryFileService.DeleteFile(path);
            var actual = inMemoryFileService.FileExists(path);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void ReadAllText_WhenValidPath_ReturnsFileContent()
        {
            // Arrange
            var path = @"C:\FakeFile.txt";
            var pathWithoutFile = @"C:\";
            var fileName = "FakeFile.txt";
            var fileContent = "Test Content";
            var pathPartsWithFile = new string[] { "C:", fileName };
            var pathParts = new string[] { "C:" };
            var directoryInfo = new InMemoryDirectoryInfo(_root);
            var fileComponent = new FileComponent(fileName, fileContent);
            var expected = fileContent;

            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(pathPartsWithFile);
            _pathServiceMock.Setup(x => x.CombinePath(pathParts)).Returns(pathWithoutFile);
            _directoryServiceMock.Setup(x => x.DirectoryExists(pathWithoutFile)).Returns(true);
            _directoryServiceMock.Setup(x => x.GetDirectory(pathWithoutFile)).Returns(directoryInfo);
            _filePathFactory.Setup(x => x.Create(pathWithoutFile, fileName)).Returns(FilePath.From((pathWithoutFile, fileName)));

            var inMemoryFileService = BuildInMemoryFileService();

            // Act
            var actual = inMemoryFileService.ReadAllText(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WrtieAllText_WhenValidPath_SetsNewContent()
        {
            // Arrange
            var path = @"C:\FakeFile.txt";
            var pathWithoutFile = @"C:\";
            var fileName = "FakeFile.txt";
            var fileContent = "Test Content";
            var newFileContents = "Test content modified";
            var pathPartsWithFile = new string[] { "C:", fileName };
            var pathParts = new string[] { "C:" };
            var directoryInfo = new InMemoryDirectoryInfo(_root);
            var fileComponent = new FileComponent(fileName, fileContent);
            var expected = fileContent;

            _pathServiceMock.Setup(x => x.SplitPath(path)).Returns(pathPartsWithFile);
            _pathServiceMock.Setup(x => x.CombinePath(pathParts)).Returns(pathWithoutFile);
            _directoryServiceMock.Setup(x => x.DirectoryExists(pathWithoutFile)).Returns(true);
            _directoryServiceMock.Setup(x => x.GetDirectory(pathWithoutFile)).Returns(directoryInfo);
            _filePathFactory.Setup(x => x.Create(pathWithoutFile, fileName)).Returns(FilePath.From((pathWithoutFile, fileName)));
            _fileComponentFactoryMock.Setup(x => x.Create(fileName)).Returns(fileComponent);

            var inMemoryFileService = BuildInMemoryFileService();

            // Act
            inMemoryFileService.WriteAllText(path, newFileContents);

            // Assert
            Assert.Equal(newFileContents, _fakeFile.Content);
        }
    }
}
