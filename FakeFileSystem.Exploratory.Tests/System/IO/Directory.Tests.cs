using System.IO;
using Xunit;

namespace FakeFileSystem.ExternalLibrary.Unit.Tests.System.IO
{
    public class DirectoryTests
    {
        [Fact]
        public void Exists_WhenAbolsolutePath_ReturnsTrue()
        {
            // Arrange
            var path = @"C:\Users\alexs\source\repos\NAJDB\NAJDB";

            // Act
            var actual = Directory.Exists(path);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@"NAJDB\Factories")]
        [InlineData(@"NAJDB\Factories/")]
        [InlineData(@"NAJDB\Factories///")]
        [InlineData(@"NAJDB\Factories\")]
        [InlineData(@"NAJDB\Factories\\\")]
        [InlineData(@"NAJDB\Extensions")]
        [InlineData(@"NAJDB")]
        public void Exists_WhenValidRelativePath_ReturnsTrue(string relativePath)
        {
            // Arrange
            Directory.SetCurrentDirectory(@"C:\Users\alexs\source\repos\NAJDB\");
            var actual = Directory.Exists(relativePath);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@"\Factories")]
        [InlineData(@"/Factories")]
        public void Exists_WhenInvalidRelativePath_ReturnsFalse(string relativePath)
        {
            // Arrange
            Directory.SetCurrentDirectory(@"C:\Users\alexs\source\repos\NAJDB\NAJDB");
            var actual = Directory.Exists(relativePath);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Exists_WhenCurrentDirectorySetAndPathAbsolute_ReturnsFalse()
        {
            // Arrange
            var path = @"C:\Users\alexs\source\repos\NAJDB\NAJDB\Factories";
            Directory.SetCurrentDirectory(@"C:\Users\alexs\source\repos\NAJDB\NAJDB");
            var actual = Directory.Exists(path);

            // Assert
            Assert.True(actual);
        }
    }
}
