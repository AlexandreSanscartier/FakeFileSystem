using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Services;
using Moq;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Services
{
    public class InMemoryPathServiceTests
    {
        private Mock<IFileSystemDirectorySeperator> _fileSystemDirectorySeperatorMock;

        public InMemoryPathServiceTests()
        {
            _fileSystemDirectorySeperatorMock = new Mock<IFileSystemDirectorySeperator>();
        }

        [Fact]
        public void DirectorySeperator_ReturnsValueSetByFileSystem()
        {
            // Arrange
            var expected = '=';
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns(expected);
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.DirectorySeperator;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AltDirectorySeperator_ReturnsValueSetByFileSystem()
        {
            // Arrange
            var expected = '=';
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns(expected);
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.AltDirectorySeperator;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombinePath_With2Arguments_ReturnsCombinedPath()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path1 = "test";
            var path2 = "test2";
            var expected = @"test\test2";

            // Act
            var actual = inMemoryPathService.CombinePath(path1, path2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CombinePath_With3Arguments_ReturnsCombinedPath()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path1 = "test";
            var path2 = "test2";
            var path3 = "test3";
            var expected = @"test\test2\test3";

            // Act
            var actual = inMemoryPathService.CombinePath(path1, path2, path3);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new string[] { "test", "test2" }, @"test\test2")]
        [InlineData(new string[] { "test", @"test2\" }, @"test\test2\")]
        [InlineData(new string[] { "", "test2" }, @"\test2")]
        [InlineData(new string[] { "test", "" }, @"test\")]
        [InlineData(new string[] { "", "" }, @"\")]
        public void CombinePath_WhenCombiningArrays_ReturnsCombinedPath(string[] pathParts, string expected)
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.CombinePath(pathParts);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(@"\test\test")]
        [InlineData(@"\\test\test")]
        [InlineData(@"C:test\test")]
        [InlineData(@"C:\test\test")]
        [InlineData(@"Server100:\test")]
        [InlineData(@"Server-100:\test")]
        public void IsPathRooted_ReturnsTrue(string path)
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.IsPathRooted(path);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsPathRooted_ReturnsFalse()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = @"test\test";

            // Act
            var actual = inMemoryPathService.IsPathRooted(path);

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(@"C:\")]
        [InlineData(@"D:\")]
        [InlineData(@"D:\\")]
        [InlineData(@"\\Server-1000\\")]
        [InlineData(@"\\")]
        [InlineData(@"//")]
        public void IsPathFullQualified_WhenValidqualifiedPaths_ReturnsTrue(string path)
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.IsPathFullyQualified(path);

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
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.IsPathFullyQualified(path);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void SplitPath_ReturnsPathParts()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = @"test\test\test\test\test\test2\test3";
            var expected = new string[] { "test", "test", "test", "test", "test", "test2", "test3" };

            // Act
            var actual = inMemoryPathService.SplitPath(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SplitPath_WhenNoDirectorySeperators_ReturnsPathParts()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = @"test";
            var expected = new string[] { "test" };

            // Act
            var actual = inMemoryPathService.SplitPath(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SplitPath_WhenDirectorySeperator_ReturnsEmptyPathParts()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "\\";
            var expected = new string[] { "", "" };

            // Act
            var actual = inMemoryPathService.SplitPath(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SplitPath_WhenEmpty_ReturnsEmptyPathParts()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = string.Empty;
            var expected = new string[] { string.Empty };

            // Act
            var actual = inMemoryPathService.SplitPath(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileNameWithoutExtension_WhenPassingFileNameWithExtension_ReturnsFileNameOnly()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile.txt";
            var expected = "FakeFile";

            // Act
            var actual = inMemoryPathService.GetFileNameWithoutExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileNameWithoutExtension_WhenPassingFileNameWithoutExtension_ReturnsFileNameOnly()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile";
            var expected = "FakeFile";

            // Act
            var actual = inMemoryPathService.GetFileNameWithoutExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileNameWithoutExtension_WhenPassingFileNameWithMultipleExtensions_ReturnsFileNameOnly()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile.txt.old";
            var expected = "FakeFile.txt";

            // Act
            var actual = inMemoryPathService.GetFileNameWithoutExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileNameWithoutExtension_WhenPassingFileNameWithPath_ReturnsFileNameOnly()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "C:/FakeDir/FakeDir2/FakeFile.txt";
            var expected = "FakeFile";

            // Act
            var actual = inMemoryPathService.GetFileNameWithoutExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileName_WhenPassingFileNameWithoutExtension_ReturnsFileName()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile";
            var expected = "FakeFile";

            // Act
            var actual = inMemoryPathService.GetFileName(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileName_WhenPassingFileNameWithExtension_ReturnsFileName()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile.txt";
            var expected = "FakeFile.txt";

            // Act
            var actual = inMemoryPathService.GetFileName(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFileName_WhenPassingFileNameWithMultipleExtensions_ReturnsFileName()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeFile.txt.old";
            var expected = "FakeFile.txt.old";

            // Act
            var actual = inMemoryPathService.GetFileName(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtension_WhenFileNamewithPath_ReturnsExtension()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeDir/FakeDir2/filename.txt";
            var expected = ".txt";

            // Act
            var actual = inMemoryPathService.GetExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtension_WhenOneExtension_ReturnsExtension()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename.txt";
            var expected = ".txt";

            // Act
            var actual = inMemoryPathService.GetExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtension_WhenMultipleExtensions_ReturnsExtension()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename.txt.old";
            var expected = ".old";

            // Act
            var actual = inMemoryPathService.GetExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExtension_WhenNoExtension_ReturnsEmptyString()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename";
            var expected = string.Empty;

            // Act
            var actual = inMemoryPathService.GetExtension(path);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChangeExtension_WhenFileNamewithPath_ChangesSuccessfully()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "FakeDir/FakeDir/filename.txt";
            var newExtension = ".newtxt";
            var expected = "FakeDir/FakeDir/filename.newtxt";

            // Act
            var actual = inMemoryPathService.ChangeExtension(path, newExtension);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChangeExtension_WhenFileNameHasNoExtension_ChangesSuccessfully()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename";
            var newExtension = ".txt";
            var expected = "filename.txt";

            // Act
            var actual = inMemoryPathService.ChangeExtension(path, newExtension);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChangeExtension_WhenFileNameHasMultipleExtensions_ChangesSuccessfully()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename.txt.test.new.txt";
            var newExtension = ".aabbccdd";
            var expected = "filename.txt.test.new.aabbccdd";

            // Act
            var actual = inMemoryPathService.ChangeExtension(path, newExtension);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChangeExtension_WhenFileNameWhenEmptyExtension_ChangesSuccessfully()
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);
            var path = "filename.txt";
            var newExtension = string.Empty;
            var expected = "filename";

            // Act
            var actual = inMemoryPathService.ChangeExtension(path, newExtension);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FakeDir/FakeDir/FakeDir3/")]
        [InlineData(@"FakeDir/FakeDir/FakeDir3\")]
        public void EndsInDirectorySeperator_WhenValidDirectorySeperator_ReturnsTrue(string path)
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.EndsInDirectorySeperator(path);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("FakeDir/FakeDir/FakeDir3/:")]
        [InlineData(@"FakeDir/FakeDir/FakeDir3\;")]
        public void EndsInDirectorySeperator_WhenNonValidDirectorySeperator_ReturnsFalse(string path)
        {
            // Arrange
            _fileSystemDirectorySeperatorMock.Setup(x => x.DirectorySeperator).Returns('\\');
            _fileSystemDirectorySeperatorMock.Setup(x => x.AltDirectorySeperator).Returns('/');
            var inMemoryPathService = new InMemoryPathService(_fileSystemDirectorySeperatorMock.Object);

            // Act
            var actual = inMemoryPathService.EndsInDirectorySeperator(path);

            // Assert
            Assert.False(actual);
        }
    }
}
