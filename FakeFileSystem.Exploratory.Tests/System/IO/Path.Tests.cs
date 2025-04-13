using System.IO;
using System.Linq;
using Xunit;

namespace FakeFileSystem.ExternalLibrary.Unit.Tests.System.IO
{
    public class PathTests
    {
        [Fact]
        public void GetFileNameWithoutExtension_WhenPassingAFileThatDoesNotExist_ReturnsFileName()
        {
            // Arrange
            var fileNameTest = "FakeFileThatDoesNotExistOnThisFileSystem.hfh";
            var expected = "FakeFileThatDoesNotExistOnThisFileSystem";

            // Act
            var actual = Path.GetFileNameWithoutExtension(fileNameTest);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtension_WhenPassingAFileThatDoesNotExist_ReturnsFileExtension()
        {
            // Arrange
            var fileNameTest = "FakeFileThatDoesNotExistOnThisFileSystem.hfh";
            var expected = ".hfh";

            // Act
            var actual = Path.GetExtension(fileNameTest);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProvidingAValidFileName_DoesNotContainInvalidFileNameChars()
        {
            // Arrange
            var fileNameTest = "FakeFileThatDoesNotExistOnThisFileSystem.hfh";
            var expected = true;

            // Act
            var actual = !Path.GetInvalidFileNameChars().Any(c => fileNameTest.Contains(c));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFullPath_WhenPassingAFolderThatDoesNotExist_ReturnsExecutedFilePath()
        {
            // Arrange
            var fileNameTest = "FakeFolder";
            var expected = @"C:\Users\alexs\source\repos\NAJDB\NAJDB\FakeFolder";

            // Act
            var actual = Path.GetFullPath(fileNameTest);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsPathFullyQualified_WhenPassingARelativeFilePath_ReturnsFalse()
        {
            // Arrange
            var fileNameTest = "FakeFolder";

            // Act
            var actual = Path.IsPathFullyQualified(fileNameTest);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Combine_CombinesTwoStringsWithADirectorySeperator_ReturnsCombinedPath()
        {
            // Arrange
            var path1 = "FakeFolder";
            var path2 = "FakeFolder2";
            var expected = $"{path1}\\{path2}";

            // Act
            var actual = Path.Combine(path1, path2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(@"C:\")]
        [InlineData(@"C:/")]
        [InlineData(@"D:\")]
        [InlineData(@"D:\\")]
        [InlineData(@"\\Server-1000\\")]
        [InlineData(@"\\")]
        [InlineData(@"//")]
        public void IsPathFullQualified_WhenValidqualifiedPaths_ReturnsTrue(string path)
        {
            // Act
            var actual = Path.IsPathFullyQualified(path);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(@"Server-1000:\\")]
        [InlineData(@"\Server-1000")]
        [InlineData(@"C:")]
        [InlineData("/")]
        [InlineData(@"\")]
        [InlineData(":")]
        public void IsPathFullQualified_WhenValidqualifiedPaths_ReturnsFalse(string path)
        {
            // Act
            var actual = Path.IsPathFullyQualified(path);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("C:/")]
        [InlineData("/")]
        [InlineData(@"\")]
        public void IsPathRooted_WhenValidRootedPaths_ReturnsTrue(string path)
        {
            // Act
            var actual = Path.IsPathRooted(path);

            // Assert
            Assert.True(actual);
        }


        [Fact]
        public void IsPathRooted_WhenOnlyFrontSlash_ReturnsTrue()
        {
            // Arrange
            var path = "/";

            // Act
            var actual = Path.IsPathRooted(path);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsPathRooted_WhenOnlyBackslash_ReturnsTrue()
        {
            // Arrange
            var path = @"\";

            // Act
            var actual = Path.IsPathRooted(path);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void DirectorySeparatorChar_ReturnsBackslash()
        {
            // Arrange
            var expected = '\\';

            // Act
            var actual = Path.DirectorySeparatorChar;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AltDirectorySeparatorChar_ReturnsFrontSlash()
        {
            // Arrange
            var expected = '/';

            // Act
            var actual = Path.AltDirectorySeparatorChar;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FakeDir/FakeDir2/filename.txt", "filename")]
        [InlineData("FakeDir/FakeDir2/filename.txt.old", "filename.txt")]
        [InlineData("FakeDir", "FakeDir")]
        public void GetFileNameWithoutExtension_ReturnsFileName(string path, string expected)
        {
            // Act
            var actual = Path.GetFileNameWithoutExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FakeDir/FakeDir2/filename.txt", ".txt")]
        [InlineData("FakeDir/FakeDir2/filename.txt.old", ".old")]
        [InlineData("FakeDir/FakeDir2.new/filename.txt.old", ".old")]
        [InlineData("FakeDir", "")]
        public void GetExtension_ReturnsExtension(string path, string expected)
        {
            // Act
            var actual = Path.GetExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FakeDir/FakeDir2/filename.txt", ".newtxt", "FakeDir/FakeDir2/filename.newtxt")]
        [InlineData(@"FakeDir\FakeDir2\filename.txt", ".newtxt", @"FakeDir\FakeDir2\filename.newtxt")]
        [InlineData("FakeDir/FakeDir2/filename.txt.old", ".oldtxt", "FakeDir/FakeDir2/filename.txt.oldtxt")]
        [InlineData("FakeDir/FakeDir2.new/filename.txt.old", ".oldtxt", "FakeDir/FakeDir2.new/filename.txt.oldtxt")]
        [InlineData("FakeDir", ".txt", "FakeDir.txt")]
        public void ChangeExtension_ReturnsExtension(string path, string extension, string expected)
        {
            // Act
            var actual = Path.ChangeExtension(path, extension);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
