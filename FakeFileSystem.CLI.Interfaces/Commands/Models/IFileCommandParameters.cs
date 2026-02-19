using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.Interfaces.Commands.Models
{
    public interface IFileCommandParameters
    {
        IFileService FileService { get; }

        string Path { get; set; }

        string Contents { get; set; }
    }
}
