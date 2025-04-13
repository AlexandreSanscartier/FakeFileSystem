namespace FakeFileSystem.Interfaces.Services
{
    public interface IFileService
    {
        Stream CreateFile(string path);

        void DeleteFile(string path);

        bool FileExists(string path);

        string ReadAllText(string path);

        void WriteAllText(string path, string contents);
    }
}
