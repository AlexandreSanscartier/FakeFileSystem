using FakeFileSystem.Factories;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Models;
using System;
using System.IO;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Factories
{
    public class DirectoryComponentFactoryTests
    {
        private IDirectoryComponentFactory _directoryComponentFactory;

        public DirectoryComponentFactoryTests()
        { 
            _directoryComponentFactory = new DirectoryComponentFactory();
        }

        [Fact]
        public void Create_WithValidDirectoryName_ReturnsIDirectoryComponent()
        {
            // Arrange
            var directoryName = "MyDir";
            var expected = DirectoryComponent.From(directoryName);

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
            var expected = DirectoryComponent.From(directoryName);

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

            foreach (var invalidCharacter in Path.GetInvalidPathChars())
            {
                var invalidDirectoryName = $"{directoryName}{invalidCharacter}";

                // Act
                Assert.Throws<ArgumentException>(() => _directoryComponentFactory.Create(invalidDirectoryName));
            }
        }
    }
}
