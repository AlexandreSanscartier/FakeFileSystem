using FakeFileSystem.Factories;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using Moq;
using System;
using System.IO;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Factories
{
    public class DirectoryComponentFactoryTests
    {
        private IDirectoryComponentFactory _directoryComponentFactory;
        private IPathService _pathService;

        public DirectoryComponentFactoryTests()
        { 
            var pathServiceMock = new Mock<IPathService>();
            pathServiceMock.Setup(x => x.GetInvalidPathChars()).Returns(["\"","<", ">", "|"]);
            _pathService = pathServiceMock.Object;

            _directoryComponentFactory = new DirectoryComponentFactory(_pathService);
        }

        [Fact]
        public void Create_WithValidDirectoryName_ReturnsIDirectoryComponent()
        {
            // Arrange
            var directoryName = "MyDir";
            var expected = new DirectoryComponent(_pathService, directoryName);

            // Act
            var actual = _directoryComponentFactory.Create(directoryName);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithValidDirectoryPath_ReturnsIDirectoryComponent()
        {
            // Arrange
            var directoryName = "MyDir\\MyDir2\\MyDir3";
            var expected = new DirectoryComponent(_pathService, directoryName);

            // Act
            var actual = _directoryComponentFactory.Create(directoryName);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithEmptyDirectoryName_ThrowsArgumentException()
        {
            // Arrange
            var directoryName = string.Empty;

            // Act
            Assert.Throws<ArgumentException>(() => _directoryComponentFactory.Create(directoryName));
        }

        [Fact]
        public void Create_WithInvalidPathCharacters_ThrowsArgumentException()
        {
            // Arrange
            var directoryName = "MyDir";

            foreach (var invalidCharacter in _pathService.GetInvalidPathChars())
            {
                var invalidDirectoryName = $"{directoryName}{invalidCharacter}";

                // Act
                Assert.Throws<ArgumentException>(() => _directoryComponentFactory.Create(invalidDirectoryName));
            }
        }
    }
}
