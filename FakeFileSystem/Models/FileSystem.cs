using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Models
{
    public class FileSystem : IFileSystem
    {
        private IDirectoryComponentFactory _directoryComponentFactory;

        private IFileSystemDirectorySeperator _fileSystemDirectorySeperator;

        public IDirectoryComponent Root { get; set; }

        public char DirectorySeperator => _fileSystemDirectorySeperator.DirectorySeperator;

        public char AltDirectorySeperator => _fileSystemDirectorySeperator.AltDirectorySeperator;

        public FileSystem(IDirectoryComponentFactory directoryComponentFactory, IFileSystemDirectorySeperator fileSystemDirectorySeperator) 
        {
            _directoryComponentFactory = directoryComponentFactory;
            _fileSystemDirectorySeperator = fileSystemDirectorySeperator;
            Root = _directoryComponentFactory.Create("C:");
        }
    }
}
