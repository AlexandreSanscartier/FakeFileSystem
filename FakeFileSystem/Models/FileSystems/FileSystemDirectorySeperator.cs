using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Models.FileSystems
{
    public class FileSystemDirectorySeperator : IFileSystemDirectorySeperator
    {
        public char DirectorySeperator { get; private set; }
        public char AltDirectorySeperator { get; private set; }

        public FileSystemDirectorySeperator()
        {
            this.DirectorySeperator = '\\';
            this.AltDirectorySeperator = '/';
        }

        public FileSystemDirectorySeperator(char directorySeperator, char altDirectorySeperator)
        {
            this.DirectorySeperator = directorySeperator;
            this.AltDirectorySeperator = altDirectorySeperator;
        }
    }
}
