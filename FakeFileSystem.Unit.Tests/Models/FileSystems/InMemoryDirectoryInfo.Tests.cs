using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models.FileSystems;
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

            var directoryInfoComponent = new InMemoryDirectoryInfo(directoryComponent);
            var expected = @"C:\Test";

            // Act
            var actual = directoryInfoComponent.FullPath;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
