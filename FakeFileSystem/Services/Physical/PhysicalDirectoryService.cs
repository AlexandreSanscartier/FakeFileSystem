using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Services.Physical.Models;

namespace FakeFileSystem.Services.Physical
{
    internal class PhysicalDirectoryService : IDirectoryService
    {
        public IDirectoryInfo CreateDirectory(string path) => new PhysicalDirectoryInfo(Directory.CreateDirectory(path));

        public void DeleteDirectory(string path) => Directory.Delete(path);

        public void DeleteDirectory(string path, bool recursive) => Directory.Delete(path, recursive);

        public bool DirectoryExists(string path) => Directory.Exists(path);

        public string GetCurrentDirectory() => Directory.GetCurrentDirectory();

        public IEnumerable<string> GetDirectories(string path) => Directory.GetDirectories(path);

        public IEnumerable<string> GetDirectories(string path, string searchPattern) 
            => Directory.GetDirectories(path, searchPattern);

        public IEnumerable<string> GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)
            => Directory.GetDirectories(path, searchPattern, enumerationOptions);

        public IEnumerable<string> GetDirectories(string path, string searchPattern, SearchOption searchOption)
            => Directory.GetDirectories(path, searchPattern, searchOption);

        public IDirectoryInfo GetDirectory(string path) => new PhysicalDirectoryInfo(Directory.GetDirectories(path).First());

        public IEnumerable<string> GetFiles(string path) => Directory.GetFiles(path);
        public IEnumerable<string> GetFiles(string path, string searchPattern)
            => Directory.GetFiles(path, searchPattern);
        public IEnumerable<string> GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) 
            => Directory.GetFiles(path, searchPattern, enumerationOptions);

        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
            => Directory.GetFiles(path, searchPattern, searchOption);

        public IDirectoryInfo GetParent(string path) => new PhysicalDirectoryInfo(Directory.GetParent(path));

        public void SetCurrentDirectory(string path) => Directory.SetCurrentDirectory(path);
    }
}
