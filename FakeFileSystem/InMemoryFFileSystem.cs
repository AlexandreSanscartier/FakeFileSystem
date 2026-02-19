using FakeFileSystem.Interfaces;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem
{
    public class InMemoryFFileSystem : IFFSystem
    {
        public IDirectoryService DirectoryService { get; private set; }

        public IFileService FileService { get; private set; }

        public IPathService PathService { get; private set; }

        public InMemoryFFileSystem(
                IDirectoryService directoryService,
                IFileService fileService,
                IPathService pathService
            )
        {
            DirectoryService = directoryService;
            FileService = fileService;
            PathService = pathService;
        }
    }
}
