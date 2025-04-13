using FakeFileSystem.Interfaces.Models;

namespace FakeFileSystem.Models
{
    public class FileComponent : IFileComponent
    {
        public string Content { get; private set; }

        public string Name { get; private set; }

        public FileComponent(string name)
        {
            Name = name;
            Content = string.Empty;
        }

        public FileComponent(string name, string content)
        {
            if(!IsFileComponentValid(name) || !DoesFileHaveValidExtension(name))
                throw new ArgumentException($"{name} is not a valid filename");
            Name = name;
            Content = content;
        }

        public void SetContent(string content)
        {
            Content = content;
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
            /// TODO: Remove the dependance an Path.GetInvalidFileNameChars
            return !(string.IsNullOrEmpty(name) || Path.GetInvalidFileNameChars().Any(c => name.Contains(c)));
        }

        private bool DoesFileHaveValidExtension(string name)
        {
            /// TODO: Remove the dependance an Path.HasExtension
            return Path.HasExtension(name);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Content, Name);
        }
    }
}
