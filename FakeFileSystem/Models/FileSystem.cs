using FakeFileSystem.Interfaces.Models;

namespace FakeFileSystem.Models
{
    public sealed class FileSystem : IFileSystem
    {
        private IDirectoryComponent _root;

        public IDirectoryComponent Root => _root;

        public char DirectorySeperator => '\\';

        public char AltDirectorySeperator => '/';

        public FileSystem(IDirectoryComponent root)
        {
            _root = root;
        }
    }
}
