using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Services.Physical.Models
{
    // Alias avoids name collisions / confusion
    using SystemDirectoryInfo = DirectoryInfo;

    internal sealed class PhysicalDirectoryInfo : IDirectoryInfo
    {
        private readonly SystemDirectoryInfo _inner; // The original <see cref="DirectoryInfo"/>.

        public PhysicalDirectoryInfo(string path) : this(new SystemDirectoryInfo(path)) { }

        public PhysicalDirectoryInfo(SystemDirectoryInfo inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public IDirectoryComponent DirectoryComponent => throw new NotImplementedException();
        public string FullPath => _inner.FullName;
    }
}
