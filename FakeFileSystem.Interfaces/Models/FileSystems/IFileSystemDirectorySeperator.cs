namespace FakeFileSystem.Interfaces.Models.FileSystems
{
    public interface IFileSystemDirectorySeperator
    {
        char DirectorySeperator { get; }

        char AltDirectorySeperator { get; }
    }
}
