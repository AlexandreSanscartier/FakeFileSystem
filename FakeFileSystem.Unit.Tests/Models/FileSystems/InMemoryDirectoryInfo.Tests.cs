using FakeFileSystem.Factories;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Models.FileSystems;
using FakeFileSystem.Services;
using Moq;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Models.FileSystems
{
    public class InMemoryDirectoryInfoTests
    {
        [Fact]
        public void GetFullPath_ReturnsFullPath()
        {
            // Arrange
            var directoryComponentMockName = "Test";
            var directoryComponentParentMockName = "C:";
            var directoryComponentMock = new Mock<IDirectoryComponent>();
            var directoryComponentParentMock = new Mock<IDirectoryComponent>();

            directoryComponentParentMock.Setup(x => x.Name).Returns(directoryComponentParentMockName);
            var directoryComponentParent = directoryComponentParentMock.Object;

            directoryComponentMock.Setup(x => x.Parent).Returns(directoryComponentParent);
            directoryComponentMock.Setup(x => x.Name).Returns(directoryComponentMockName);
            var directoryComponent = directoryComponentMock.Object;
            var fileSystemDirectorySeperator = new FileSystemDirectorySeperator();
            var pathService = new InMemoryPathService(fileSystemDirectorySeperator);
            var directoryComponentFactory = new DirectoryComponentFactory(pathService);

            var fileSystem = new FileSystem(directoryComponentFactory, fileSystemDirectorySeperator);;

            var directoryInfoComponent = new InMemoryDirectoryInfo(directoryComponent, pathService);
            var expected = @"C:\Test";

            // Act
            var actual = directoryInfoComponent.FullPath;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
