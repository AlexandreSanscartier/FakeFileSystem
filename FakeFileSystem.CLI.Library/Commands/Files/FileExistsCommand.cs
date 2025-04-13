using FakeFileSystem.CLI.Interfaces.Commands.Files;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Files
{
    public class FileExistsCommand : IFileCommand
    {
        IFileService _fileService;

        private readonly string _path;

        public FileExistsCommand(IFileService fileService, string path)
        {
            _fileService = fileService;
            _path = path;
        }

        public void Execute()
        {
            _fileService.FileExists(_path);
        }
    }
}
