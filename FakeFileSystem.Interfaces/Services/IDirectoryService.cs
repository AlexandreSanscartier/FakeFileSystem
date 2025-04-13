using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Interfaces.Services
{
    public interface IDirectoryService
    {
        IDirectoryInfo CreateDirectory(string path);

        void DeleteDirectory(string path);

        void DeleteDirectory(string path, bool recursive);

        bool DirectoryExists(string path);

        void SetCurrentDirectory(string path);

        string GetCurrentDirectory();

        IDirectoryInfo GetParent(string path);

        IEnumerable<string> GetFiles(string path);

        IDirectoryInfo GetDirectory(string path);

        IEnumerable<string> GetDirectories(string path);
    }
}
