using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Models
{
    public class FileCommandParameters : IFileCommandParameters
    {
        public string Path { get; private set; }

        public string Contents { get; private set; }

        public IFileService FileService{ get; private set; }

        public FileCommandParameters(IFileService fileService, string path, string contents)
        {
            FileService = fileService;
            Path = path;
            Contents = contents;
        }

        public void SetContents(string contents)
        {
            Contents = contents;
        }

        public void SetPath(string path)
        {
            Path = path;
        }
    }
}
