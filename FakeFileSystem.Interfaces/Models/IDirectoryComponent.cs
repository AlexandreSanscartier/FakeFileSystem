namespace FakeFileSystem.Interfaces.Models
{
    public interface IDirectoryComponent : IFileSystemComponent
    {
        IDirectoryComponent? Parent { get; }

        void SetParent(IDirectoryComponent parent);

        IEnumerable<IFileSystemComponent> GetFileSystemComponents();

        void Add(IFileSystemComponent fileSystemComponent);

        void Remove(IFileSystemComponent fileSystemComponent);
    }
}
