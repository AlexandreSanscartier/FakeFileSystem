using FakeFileSystem.Factories;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using Moq;
using System;
using System.IO;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Factories
{
    public class FileComponentFactoryTests
    {
        private Mock<IPathService> _pathServiceMock;

        public FileComponentFactoryTests()
        {
            _pathServiceMock = new Mock<IPathService>();
        }

        [Fact]
        public void Create_WithValidFileName_ReturnsIFileComponent()
        {
            // Arrange
            var fileName = "MyFile.txt";

            _pathServiceMock.Setup(x => x.HasExtension(fileName)).Returns(true);
            var pathService = _pathServiceMock.Object;

            var fileComponentFactory = new FileComponentFactory(pathService);

            var expected = new FileComponent(pathService, fileName, string.Empty);

            // Act
            var actual = fileComponentFactory.Create(fileName);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithValidFileNameAndContent_ReturnsIFileComponent()
        {
            // Arrange
            var fileName = "MyFile.txt";
            var fileContent = "This is fake file contents";

            _pathServiceMock.Setup(x => x.HasExtension(fileName)).Returns(true);
            var pathService = _pathServiceMock.Object;

            var fileComponentFactory = new FileComponentFactory(pathService);

            var expected = new FileComponent(pathService, fileName, fileContent);

            // Act
            var actual = fileComponentFactory.Create(fileName, fileContent);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithNoExtensions_ThrowsArgumentException()
        {
            // Arrange
            var fileName = "MyFile";
            var pathService = _pathServiceMock.Object;
            var fileComponentFactory = new FileComponentFactory(pathService);

            // Assert
            Assert.Throws<ArgumentException>(() => fileComponentFactory.Create(fileName));
        }

        [Fact]
        public void Create_WithInvalidCharacters_ThrowsArgumentException()
        {
            // Arrange
            var fileName = "MyFile.txt";

            foreach (var invalidCharacter in Path.GetInvalidFileNameChars())
            {
                var invalidFileName = $"{invalidCharacter}{fileName}";
                var pathService = _pathServiceMock.Object;
                var fileComponentFactory = new FileComponentFactory(pathService);

                // Assert
                Assert.Throws<ArgumentException>(() => fileComponentFactory.Create(invalidFileName));
            }
        }
    }
}
