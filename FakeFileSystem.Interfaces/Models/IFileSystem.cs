namespace FakeFileSystem.Interfaces.Models
{
    public interface IFileSystem
    {
        char DirectorySeperator { get; }

        char AltDirectorySeperator { get; }

        IDirectoryComponent Root { get; }
    }
}
