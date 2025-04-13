using FakeFileSystem.Factories;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Models;
using System;
using System.IO;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Factories
{
    public class FileComponentFactoryTests
    {
        private IFileComponentFactory _fileComponentFactory;

        public FileComponentFactoryTests()
        {
            _fileComponentFactory = new FileComponentFactory();
        }

        [Fact]
        public void Create_WithValidFileName_ReturnsIFileComponent()
        {
            // Arrange
            var fileName = "MyFile.txt";
            var expected = new FileComponent(fileName, string.Empty);

            // Act
            var actual = _fileComponentFactory.Create(fileName);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithValidFileNameAndContent_ReturnsIFileComponent()
        {
            // Arrange
            var fileName = "MyFile.txt";
            var fileContent = "This is fake file contents";
            var expected = new FileComponent(fileName, fileContent);

            // Act
            var actual = _fileComponentFactory.Create(fileName, fileContent);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_WithNoExtensions_ThrowsArgumentException()
        {
            // Arrange
            var fileName = "MyFile";

            // Assert
            Assert.Throws<ArgumentException>(() => _fileComponentFactory.Create(fileName));
        }

        [Fact]
        public void Create_WithInvalidCharacters_ThrowsArgumentException()
        {
            // Arrange
            var fileName = "MyFile.txt";

            foreach (var invalidCharacter in Path.GetInvalidFileNameChars())
            {
                var invalidFileName = $"{invalidCharacter}{fileName}";

                // Assert
                Assert.Throws<ArgumentException>(() => _fileComponentFactory.Create(invalidFileName));
            }
        }
    }
}
