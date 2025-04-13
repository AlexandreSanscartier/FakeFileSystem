using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using ValueOf;

namespace FakeFileSystem.Models
{
    public class DirectoryComponent : IDirectoryComponent
    {
        private IList<IFileSystemComponent> _components;
        private readonly IPathService _pathService;

        public string Name { get; private set; }

        public IDirectoryComponent? Parent { get; private set; }

        public DirectoryComponent(IPathService pathService, string name)
        {
            _components = new List<IFileSystemComponent>();
            _pathService = pathService;
            Name = name;
            if (!IsValidDirectory())
                throw new ArgumentException($"{Name} is not a valid directory");
        }

        public void Add(IFileSystemComponent fileSystemComponent)
        {
            if (!IsIFileSystemComponentInList(fileSystemComponent))
            {
                AddFileSystemComponent(fileSystemComponent);
            }
        }

        private void AddFileSystemComponent(IFileSystemComponent fileSystemComponent)
        {
            if (IsDirectory(fileSystemComponent))
                ((IDirectoryComponent)fileSystemComponent).SetParent(this);

            _components.Add(fileSystemComponent);
        }

        public void SetParent(IDirectoryComponent parent)
        {
            Parent = parent;
        }

        public IEnumerable<IFileSystemComponent> GetFileSystemComponents()
        {
            return _components;
        }

        public void Remove(IFileSystemComponent fileSystemComponent)
        {
            _components.Remove(fileSystemComponent);
        }

        private bool IsValidDirectory()
        {
            return !(string.IsNullOrEmpty(Name) || _pathService.GetInvalidPathChars().Any(c => Name.Contains(c)));
        }

        private bool IsDirectory(IFileSystemComponent fileSystemComponent) => fileSystemComponent.GetType().IsAssignableTo(typeof(IDirectoryComponent));

        private bool IsIFileSystemComponentInList(IFileSystemComponent fileSystemComponent)
        {
            return _components.Any(x => x.Name.Equals(fileSystemComponent.Name));
        }

        public override bool Equals(object? obj)
        {
            return obj is DirectoryComponent component &&
                   Name == component.Name &&
                   EqualityComparer<IDirectoryComponent?>.Default.Equals(Parent, component.Parent);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Parent);
        }
    }
}
