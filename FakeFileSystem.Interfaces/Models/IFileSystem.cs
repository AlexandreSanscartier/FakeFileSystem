using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Interfaces.Models
{
    public interface IFileSystem
    {
        IDirectoryComponent Root { get; set; }

        char DirectorySeperator { get; }

        char AltDirectorySeperator { get; }
    }
}
