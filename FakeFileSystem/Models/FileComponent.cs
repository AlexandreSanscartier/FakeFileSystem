using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.Models
{
    public class FileComponent : IFileComponent
    {
        private readonly IPathService _pathService;

        public string Content { get; set; }

        public string Name { get; private set; }

        public FileComponent(IPathService pathService, string name)
        {
            _pathService = pathService;
            Name = name;
            Content = string.Empty;
        }

        public FileComponent(IPathService pathService, string name, string content)
        {
            _pathService = pathService;
            Name = name;
            Content = content;
            if (!IsFileComponentValid(name) || !DoesFileHaveValidExtension(name))
                throw new ArgumentException($"{name} is not a valid filename");
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((IFileComponent)obj);
        }

        protected virtual bool Equals(IFileComponent other)
        {
            return Equals(this.Name, other.Name) && Equals(this.Content, other.Content);
        }

        private bool IsFileComponentValid(string name)
        {
            return !(string.IsNullOrEmpty(name) || _pathService.GetInvalidFileNameChars().Any(c => name.Contains(c)));
        }

        private bool DoesFileHaveValidExtension(string name)
        {
            return _pathService.HasExtension(name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Content, Name);
        }
    }
}
