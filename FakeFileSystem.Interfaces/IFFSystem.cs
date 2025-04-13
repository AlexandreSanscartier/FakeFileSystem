using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.Interfaces
{
    public interface IFFSystem
    {
        IDirectoryService DirectoryService { get; }
        IFileService FileService { get; }
        IPathService PathService { get; }
    }
}
