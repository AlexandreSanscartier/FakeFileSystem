using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public sealed class FileCommandParameters : IFileCommandParameters
    {
        public string Path { get; set; }

        public string Contents { get; set; }

        public IFileService FileService{ get; private set; }

        public FileCommandParameters(IFileService fileService)
        {
            FileService = fileService;
            Path = string.Empty;
            Contents = string.Empty;
        }

        public FileCommandParameters(IFileService fileService, string path, string contents)
        {
            FileService = fileService;
            Path = path;
            Contents = contents;
        }
    }
}
