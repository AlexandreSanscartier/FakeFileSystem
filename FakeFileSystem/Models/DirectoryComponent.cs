using FakeFileSystem.Interfaces.Models;
using ValueOf;

namespace FakeFileSystem.Models
{
    public class DirectoryComponent : ValueOf<string, DirectoryComponent>, IDirectoryComponent
    {
        private IList<IFileSystemComponent> _components;

        public string Name => Value;

        public IDirectoryComponent? Parent { get; private set; }

        public DirectoryComponent() : base()
        {
            _components = new List<IFileSystemComponent>();
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

        protected override void Validate()
        {
            if (!IsValidDirectory())
                throw new ArgumentException($"{Value} is not a valid directory");
        }

        private bool IsValidDirectory()
        {
            /// TODO: Extract the Path.GetInvalidPathChars our of here.
            return !(string.IsNullOrEmpty(Value) || Path.GetInvalidPathChars().Any(c => Value.Contains(c)));
        }

        private bool IsDirectory(IFileSystemComponent fileSystemComponent) => fileSystemComponent.GetType().IsAssignableTo(typeof(IDirectoryComponent));

        private bool IsIFileSystemComponentInList(IFileSystemComponent fileSystemComponent)
        {
            return _components.Any(x => x.Name.Equals(fileSystemComponent.Name));
        }
    }
}
