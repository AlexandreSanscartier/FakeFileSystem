using FakeFileSystem.Interfaces;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Services.Physical;

namespace FakeFileSystem
{
    internal class FileSystem : IFFSystem
    {
        public IDirectoryService DirectoryService { get; private set; }

        public IFileService FileService { get; private set; }

        public IPathService PathService { get; private set; }

        public FileSystem()
        {
            DirectoryService = new PhysicalDirectoryService();
            FileService = new PhysicalFileService();
            PathService = new PhysicalPathService();
        }
    }
}
