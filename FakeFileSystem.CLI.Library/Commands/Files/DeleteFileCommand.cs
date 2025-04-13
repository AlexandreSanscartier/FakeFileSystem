using FakeFileSystem.CLI.Interfaces.Commands.Files;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Files
{
    public class DeleteFileCommand : IFileCommand
    {
        IFileService _fileService;

        private readonly string _path;

        public DeleteFileCommand(IFileService fileService, string path)
        {
            _fileService = fileService;
            _path = path;
        }

        public void Execute()
        {
            _fileService.DeleteFile(_path);
        }
    }
}
