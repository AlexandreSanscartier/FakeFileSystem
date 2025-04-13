using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.Interfaces.Commands.Models
{
    public interface IFileCommandParameters
    {
        IFileService FileService { get; }

        string Path { get; }

        string Contents { get; }

        void SetPath(string path);

        void SetContents(string contents);
    }
}
